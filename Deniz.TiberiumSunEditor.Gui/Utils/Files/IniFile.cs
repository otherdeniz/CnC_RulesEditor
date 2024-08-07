using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Files
{
    public class IniFile
    {
        private static readonly Regex CategoryLineRegEx = new Regex(@"^\[([\w\d\s]+)\]", RegexOptions.Compiled);
        private static readonly Regex CommentLineRegEx = new Regex(@"^\s*;+(.*)", RegexOptions.Compiled);
        private static readonly Regex KeyValueLineRegEx = new Regex(@"^([\w\d\.]+)\s*=?\s*([^;]+)?(?:;(.*))?", RegexOptions.Compiled);

        private Dictionary<string, IniFileSection>? _sectionsDictionary;

        public List<IniFileSection> Sections { get; } = new();

        public string? OriginalFullPath { get; set; }

        public string OriginalFileName { get; set; } = "";

        public static IniFile Load(byte[] fileBytes, string originalFilename = "")
        {
            using (var fileStream = new MemoryStream(fileBytes))
            {
                var iniFile = LoadStream(fileStream);
                iniFile.OriginalFileName = originalFilename;
                return iniFile;
            }
        }

        public static IniFile Load(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var iniFile = LoadStream(fileStream);
                iniFile.OriginalFullPath = filePath;
                iniFile.OriginalFileName = Path.GetFileName(filePath);
                return iniFile;
            }
        }

        private static IniFile LoadStream(Stream fileStream)
        {
            var iniFile = new IniFile();

            var currentSection = new IniFileSection();
            var lastCommentLines = new List<IniFileLineComment>();
            using (var reader = new StreamReader(fileStream, Encoding.UTF8, false))
            {
                string? lineText = null;
                do
                {
                    lineText = reader.ReadLine();
                    if (lineText != null)
                    {
                        var parsedLine = ParseLine(lineText, iniFile);
                        if (parsedLine is IniFileSection parsedSection)
                        {
                            if (!currentSection.IsEmpty || currentSection.KeepWhenEmpty)
                            {
                                iniFile.Sections.Add(currentSection);
                            }
                            currentSection = parsedSection;
                            parsedSection.HeaderComments = lastCommentLines;
                        }
                        else
                        {
                            if (parsedLine is IniFileLineComment parsedComment)
                            {
                                lastCommentLines.Add(parsedComment);
                            }
                            else
                            {
                                lastCommentLines = new List<IniFileLineComment>();
                            }

                            if (parsedLine is IniFileLineKeyValue parsedKeyValue
                                && currentSection.Lines.OfType<IniFileLineKeyValue>()
                                    .Any(k => k.Key == parsedKeyValue.Key))
                            {
                                // duplicate entry, we add a Raw Line
                                currentSection.Lines.Add(new IniFileLineRaw(parsedLine.ToString()));
                            }
                            else
                            {
                                currentSection.Lines.Add(parsedLine);
                            }
                        }
                    }
                } while (lineText != null);
            }
            if (!currentSection.IsEmpty || currentSection.KeepWhenEmpty)
            {
                iniFile.Sections.Add(currentSection);
            }

            return iniFile;
        }

        public void SaveAs(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            var utf8WithoutBom = new UTF8Encoding(false);
            using (var writer = new StreamWriter(filePath, utf8WithoutBom,
                       new FileStreamOptions
                       {
                           Mode = FileMode.CreateNew, 
                           Access = FileAccess.Write
                       }))
            {
                foreach (var category in Sections.Where(s => !s.IsEmpty || s.KeepWhenEmpty))
                {
                    writer.Write(category.ToString());
                }
                writer.Flush();
            }
        }

        public IniFile GetChangesFile(IniFile defaultFile)
        {
            var changesFile = new IniFile();
            foreach (var localSection in Sections.Where(s => s.SectionName != null))
            {
                var defaultSection = defaultFile.GetSection(localSection.SectionName);
                var changedValues = localSection.KeyValues
                    .Where(k => !string.Equals(k.Value, defaultSection?.GetValue(k.Key)?.Value, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
                if (changedValues.Any())
                {
                    var changesSection = changesFile.AddSection(localSection.SectionName!);
                    foreach (var changedValue in changedValues)
                    {
                        changesSection.SetValue(changedValue.Key, changedValue.Value);
                    }
                    changesSection.Lines.Add(new IniFileLineEmpty());
                }
            }
            return changesFile;
        }

        public void MergeWithFile(IniFile fileToMerge, IniFile? defaultFile = null)
        {
            foreach (var mergeSection in fileToMerge.Sections.Where(s => s.SectionName != null))
            {
                var localSection = GetSection(mergeSection.SectionName)
                                   ?? AddSection(mergeSection.SectionName!);
                var defaultSection = defaultFile?.GetSection(mergeSection.SectionName);
                foreach (var mergeValue in mergeSection.KeyValues)
                {
                    if (!string.Equals(defaultSection?.GetValue(mergeValue.Key)?.Value, mergeValue.Value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (localSection.SectionName!.EndsWith("Types")
                            && localSection.KeyValues.Any(k => k.Value == mergeValue.Value))
                        {
                            // this unit is already in the Types-collection
                            continue;
                        }
                        localSection.SetValue(mergeValue.Key, mergeValue.Value);
                    }
                }
            }
        }

        public IniFileSection AddSection(string name, bool keepWhenEmpty = false)
        {
            var newSection = new IniFileSection
            {
                SectionName = name,
                KeepWhenEmpty = keepWhenEmpty
            };
            if (Sections.LastOrDefault()?.SectionName == "Digest")
            {
                Sections.Insert(Sections.Count-1, newSection);
            }
            else
            {
                Sections.Add(newSection);
            }
            _sectionsDictionary = null;
            return newSection;
        }

        public IniFileSection? GetSection(string? name)
        {
            if (_sectionsDictionary == null)
            {
                _sectionsDictionary = new Dictionary<string, IniFileSection>();
                foreach (var section in Sections)
                {
                    if (!_sectionsDictionary.ContainsKey(section.SectionName ?? ""))
                    {
                        _sectionsDictionary.Add(section.SectionName ?? "", section);
                    }
                }
            }

            return _sectionsDictionary.TryGetValue(name ?? "", out var resultSection) 
                ? resultSection 
                : null;
        }

        private static IniFileLineBase ParseLine(string lineText, IniFile currentFile)
        {
            if (string.IsNullOrWhiteSpace(lineText))
            {
                return new IniFileLineEmpty();
            }
            var categoryMatch = CategoryLineRegEx.Match(lineText);
            if (categoryMatch.Success)
            {
                var sectionName = categoryMatch.Groups[1].Value;
                return currentFile.Sections.FirstOrDefault(s => s.SectionName == sectionName) // merge duplicate sections
                       ?? new IniFileSection()
                       {
                           SectionName = sectionName,
                           KeepWhenEmpty = true
                       };
            }

            var commentMatch = CommentLineRegEx.Match(lineText);
            if (commentMatch.Success)
            {
                return new IniFileLineComment()
                {
                    Comment = commentMatch.Groups[1].Value.Trim()
                };
            }

            var valueMatch = KeyValueLineRegEx.Match(lineText);
            if (valueMatch.Success)
            {
                return new IniFileLineKeyValue(valueMatch.Groups[1].Value.Trim(),
                    valueMatch.Groups[2].Success
                        ? valueMatch.Groups[2].Value.Trim()
                        : "",
                    valueMatch.Groups[3].Success
                        ? valueMatch.Groups[3].Value.Trim()
                        : null);
            }

            //throw new RuntimeException($"The line '{lineText}' could not be parsed");
            return new IniFileLineRaw(lineText);
        }
    }

    public class IniFileSection : IniFileLineBase
    {
        private List<IniFileLineKeyValue>? _keyValuesList;
        private Dictionary<string, IniFileLineKeyValue>? _keyValuesDictionary;

        public event EventHandler<IniFileSectionChangedEventArgs>? ValueChanged;

        public string? SectionName { get; set; }

        public bool KeepWhenEmpty { get; set; }

        public bool IsEmpty => !Lines.Any(l => l is IniFileLineComment or IniFileLineKeyValue or IniFileLineRaw);

        public List<IniFileLineComment> HeaderComments { get; set; } = new List<IniFileLineComment>();

        public List<IniFileLineBase> Lines { get; } = new List<IniFileLineBase>();

        public List<IniFileLineKeyValue> KeyValues => _keyValuesList
            ??= Lines.OfType<IniFileLineKeyValue>().ToList();

        public IniFileLineKeyValue? GetValue(string key)
        {
            return (_keyValuesDictionary ??= Lines
                    .OfType<IniFileLineKeyValue>()
                    .DistinctBy(k => k.Key)
                    .ToDictionary(k => k.Key, v => v))
                .TryGetValue(key, out var keyValue)
                    ? keyValue
                    : null;
        }

        public void SetValue(string key, string value)
        {
            var keyValue = GetValue(key);
            if (keyValue != null)
            {
                if (value == "" && keyValue.RuntimeAdded)
                {
                    Lines.Remove(keyValue);
                    _keyValuesList = null;
                    _keyValuesDictionary = null;
                }
                else
                {
                    keyValue.Value = value;
                }
            }
            else
            {
                Lines.Add(new IniFileLineKeyValue(key, value, runtimeAdded:true));
                _keyValuesList = null;
                _keyValuesDictionary = null;
            }
            ValueChanged?.Invoke(this, new IniFileSectionChangedEventArgs(key, value));
        }

        public override string ToString()
        {
            var linesText = new StringBuilder();
            if (SectionName != null)
            {
                linesText.AppendLine($"[{SectionName}]");
            }
            foreach (var line in Lines)
            {
                linesText.AppendLine(line.ToString());
            }
            if (Lines.Any() 
                && Lines.Last() is not IniFileLineEmpty)
            {
                linesText.AppendLine("");
            }
            return linesText.ToString();
        }
    }

    public abstract class IniFileLineBase
    {
        public override string ToString()
        {
            return "";
        }
    }

    public class IniFileLineEmpty : IniFileLineBase
    {
    }

    public class IniFileLineRaw : IniFileLineBase
    {
        private readonly string _rawText;

        public IniFileLineRaw(string rawText)
        {
            _rawText = rawText;
        }
        public override string ToString()
        {
            return _rawText;
        }
    }

    public class IniFileLineComment : IniFileLineBase
    {
        public string? Comment { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Comment) 
                ? "" 
                : $";{Comment}";
        }
    }

    public class IniFileLineKeyValue : IniFileLineBase
    {
        private string _value;

        public IniFileLineKeyValue(string key, string value, string? comment = null, bool runtimeAdded = false)
        {
            Key = key;
            _value = value;
            Comment = comment;
            RuntimeAdded = runtimeAdded;
        }

        public event EventHandler? ValueChanged;

        public string Key { get; }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Browsable(false)]
        public string? Comment { get; }

        [Browsable(false)]
        public bool RuntimeAdded { get; }

        public override string ToString()
        {
            var commentText = string.IsNullOrEmpty(Comment)
                ? ""
                : $" ;{Comment}";

            return $"{Key}={Value}{commentText}";
        }
    }

    public class IniFileSectionChangedEventArgs : EventArgs
    {
        public IniFileSectionChangedEventArgs(string key, string newValue)
        {
            Key = key;
            NewValue = newValue;
        }

        public string Key { get; }

        public string NewValue { get; }
    }
}
