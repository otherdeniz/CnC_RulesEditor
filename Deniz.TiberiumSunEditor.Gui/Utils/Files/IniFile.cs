﻿using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Newtonsoft.Json.Linq;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Files
{
    public class IniFile
    {
        private static readonly Regex CategoryLineRegEx = new Regex(@"^\[([\w\d\s\-]+)\]", RegexOptions.Compiled);
        private static readonly Regex CommentLineRegEx = new Regex(@"^\s*;+(.*)", RegexOptions.Compiled);
        private static readonly Regex KeyValueLineRegEx = new Regex(@"^([\w\d\.\$\-]+)\s*=?\s*([^;]+)?(?:;(.*))?", RegexOptions.Compiled);

        private Dictionary<string, IniFileSection>? _sectionsDictionary;

        public event EventHandler<IniFileSectionChangedEventArgs>? ValueChanged;
        public event EventHandler<EventArgs>? BeforeOriginalFileSaved;
        public event EventHandler<EventArgs>? AfterOriginalFileSaved;

        public List<IniFileSection> Sections { get; } = new();

        public string? OriginalFullPath { get; set; }

        public string FileName { get; set; } = "";

        public static IniFile Load(byte[] fileBytes, string filename = "", string? originalFullpath = null)
        {
            using (var fileStream = new MemoryStream(fileBytes))
            {
                var iniFile = LoadStream(fileStream);
                iniFile.FileName = filename;
                iniFile.OriginalFullPath = originalFullpath;
                return iniFile;
            }
        }

        public static IniFile Load(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var iniFile = LoadStream(fileStream);
                iniFile.OriginalFullPath = filePath;
                iniFile.FileName = Path.GetFileName(filePath);
                return iniFile;
            }
        }

        private static IniFile LoadStream(Stream fileStream)
        {
            var iniFile = new IniFile();

            var currentSection = new IniFileSection(); // first create the 'null' Section that contains all the comments before the first real Section
            var lastCommentLines = new List<IniFileLineComment>(); // we use this to write trailing comments as 'header' comments to the next Section
            using (var reader = new StreamReader(fileStream, Encoding.UTF8, false))
            {
                string? lineText = null;
                do
                {
                    lineText = reader.ReadLine();
                    if (lineText != null)
                    {
                        var parsedLine = ParseLine(lineText, iniFile, currentSection);
                        if (parsedLine is IniFileSection parsedSection)
                        {
                            if (!currentSection.IsEmpty || currentSection.KeepWhenEmpty)
                            {
                                if (iniFile.Sections.Any(s =>
                                        s.SectionName?.Equals(currentSection.SectionName,
                                            StringComparison.InvariantCultureIgnoreCase) == true))
                                {
                                    // duplicate section, keep as RAW
                                    iniFile.Sections.LastOrDefault()?.Lines
                                        .Add(new IniFileLineRaw(currentSection.ToString()));
                                }
                                else
                                {
                                    iniFile.Sections.Add(currentSection);
                                }
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

        public void Save()
        {
            if (OriginalFullPath != null)
            {
                SaveAs(OriginalFullPath, true);
            }
        }

        public void SaveAs(string filePath, bool saveAsOriginalFile = false)
        {
            if (filePath == OriginalFullPath)
            {
                saveAsOriginalFile = true;
            }

            if (saveAsOriginalFile)
            {
                BeforeOriginalFileSaved?.Invoke(this, EventArgs.Empty);
            }

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

            if (saveAsOriginalFile)
            {
                OriginalFullPath = filePath;
                AfterOriginalFileSaved?.Invoke(this, EventArgs.Empty);
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
            newSection.ValueChanged += (sender, args) => ValueChanged?.Invoke(sender, args);
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

        public void RemoveSection(string? name)
        {
            var removeIndex = Sections.FindIndex(s => s.SectionName == name);
            if (removeIndex > -1)
            {
                Sections.RemoveAt(removeIndex);
                _sectionsDictionary = null;
            }
        }

        public IniFileSection? GetSection(string? name)
        {
            if (_sectionsDictionary == null)
            {
                _sectionsDictionary = new Dictionary<string, IniFileSection>();
                foreach (var section in Sections)
                {
                    var sectgionName = section.SectionName?.ToUpper(CultureInfo.InvariantCulture);
                    if (!_sectionsDictionary.ContainsKey(sectgionName ?? ""))
                    {
                        _sectionsDictionary.Add(sectgionName ?? "", section);
                    }
                }
            }

            return _sectionsDictionary.TryGetValue(name?.ToUpper(CultureInfo.InvariantCulture) ?? "", out var resultSection) 
                ? resultSection 
                : null;
        }

        private static IniFileLineBase ParseLine(string lineText, IniFile currentFile, IniFileSection currentSection)
        {
            if (string.IsNullOrWhiteSpace(lineText))
            {
                return new IniFileLineEmpty();
            }
            var categoryMatch = CategoryLineRegEx.Match(lineText);
            if (categoryMatch.Success)
            {
                var sectionName = categoryMatch.Groups[1].Value;
                var newSection = new IniFileSection()
                       {
                           SectionName = sectionName,
                           KeepWhenEmpty = true
                       };
                newSection.ValueChanged += (sender, args) => currentFile.ValueChanged?.Invoke(sender, args);
                return newSection;
            }

            var commentMatch = CommentLineRegEx.Match(lineText);
            if (commentMatch.Success)
            {
                return new IniFileLineComment
                {
                    Comment = commentMatch.Groups[1].Value.Trim()
                };
            }

            var valueMatch = KeyValueLineRegEx.Match(lineText);
            if (valueMatch.Success)
            {
                return new IniFileLineKeyValue(currentSection,
                    valueMatch.Groups[1].Value.Trim(),
                    valueMatch.Groups[2].Success
                        ? valueMatch.Groups[2].Value.Trim()
                        : "",
                    valueMatch.Groups[3].Success
                        ? valueMatch.Groups[3].Value.Trim()
                        : null);
            }

            return new IniFileLineRaw(lineText);
        }
    }

    public class IniFileSection : IniFileLineBase
    {
        private List<IniFileLineKeyValue>? _keyValuesList;
        private Dictionary<string, IniFileLineKeyValue>? _keyValuesDictionary;
        private List<IniFileLineBase> _lines = new List<IniFileLineBase>();

        public event EventHandler<IniFileSectionChangedEventArgs>? ValueChanged;

        public string? SectionName { get; set; }

        public bool KeepWhenEmpty { get; set; }

        public bool IsEmpty => !Lines.Any(l => l is IniFileLineComment or IniFileLineKeyValue or IniFileLineRaw);

        public List<IniFileLineComment> HeaderComments { get; set; } = new List<IniFileLineComment>();

        public List<IniFileLineBase> Lines
        {
            get => _lines;
            set
            {
                _lines = value;
                _keyValuesList = null;
                _keyValuesDictionary = null;
            }
        }

        public List<IniFileLineKeyValue> KeyValues => _keyValuesList
            ??= Lines.OfType<IniFileLineKeyValue>().ToList();

        public void RaiseValueChanged(string key, string value)
        {
            ValueChanged?.Invoke(this, new IniFileSectionChangedEventArgs(SectionName ?? "", key, value));
        }

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

        public void SetValue(string key, string value, string? comment = null, bool removeEmptyRuntimeAdded = true)
        {
            var keyValue = GetValue(key);
            if (keyValue != null)
            {
                if (value == "" && keyValue.RuntimeAdded && removeEmptyRuntimeAdded)
                {
                    Lines.Remove(keyValue);
                    _keyValuesList = null;
                    _keyValuesDictionary = null;
                    RaiseValueChanged(key, value);
                }
                else
                {
                    keyValue.Value = value;
                    if (comment != null)
                    {
                        keyValue.Comment = comment;
                    }
                }
            }
            else
            {
                var lastKeyValueLineIndex = Lines.FindLastIndex(l => l is IniFileLineKeyValue);
                if (lastKeyValueLineIndex > -1 && lastKeyValueLineIndex < Lines.Count - 1)
                {
                    Lines.Insert(lastKeyValueLineIndex + 1, new IniFileLineKeyValue(this, key, value, comment, runtimeAdded: true));
                }
                else
                {
                    Lines.Add(new IniFileLineKeyValue(this, key, value, comment, runtimeAdded: true));
                }
                _keyValuesList = null;
                _keyValuesDictionary = null;
                RaiseValueChanged(key, value);
            }
        }

        public void RemoveValues(Func<IniFileLineKeyValue, bool> removeMatch)
        {
            var removeLines = Lines.Where(l =>
                l is IniFileLineKeyValue keyValue
                && removeMatch(keyValue)).ToList();
            removeLines.ForEach(l => Lines.Remove(l));
            _keyValuesList = null;
            _keyValuesDictionary = null;
        }

        public int? GetMaxKeyValue()
        {
            var numberValues = KeyValues
                .Select(k => int.TryParse(k.Key, out var number) ? (int?)number : null)
                .Where(v => v.HasValue)
                .ToArray();
            return numberValues.Any() 
                ? numberValues.Max()
                : null;
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
                && Lines.LastEx() is not IniFileLineEmpty)
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
        public string Comment { get; set; } = "";

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

        public IniFileLineKeyValue(IniFileSection section, string key, string value, string? comment = null, bool runtimeAdded = false)
        {
            Section = section;
            Key = key;
            _value = value;
            Comment = comment;
            RuntimeAdded = runtimeAdded;
        }

        public event EventHandler? ValueChanged;

        [Browsable(false)]
        public IniFileSection Section { get; }

        public string Key { get; }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
                Section.RaiseValueChanged(Key, Value);
            }
        }

        [Browsable(false)]
        public string? Comment { get; set; }

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
        public IniFileSectionChangedEventArgs(string section, string key, string newValue)
        {
            Section = section;
            Key = key;
            NewValue = newValue;
        }

        public string Section { get; }

        public string Key { get; }

        public string NewValue { get; }
    }
}
