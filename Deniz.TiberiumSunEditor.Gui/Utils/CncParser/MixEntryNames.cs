namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public class MixEntryNames
    {
        private static MixEntryNames? _instance;
        public static MixEntryNames Instance
        {
            get
            {
                if (_instance == null)
                {
                    var mixFileEntriesPath = Path.Combine(Application.StartupPath, "Resources\\MixFileEntries");
                    _instance = LoadFromDirectory(mixFileEntriesPath);
                }
                return _instance;
            }
        }

        public static MixEntryNames LoadFromDirectory(string path)
        {
            var mapping = new Dictionary<uint, MixEntryItem>();
            foreach (var txtFile in Directory.GetFiles(path))
            {
                using (var fileReader = File.OpenText(txtFile))
                {
                    while (fileReader.ReadLine() is { } currentLine)
                    {
                        var name = currentLine.Trim();
                        var description = "";
                        var tabPos = currentLine.IndexOf("\t", StringComparison.InvariantCulture);
                        if (tabPos > -1)
                        {
                            name = currentLine.Substring(0, tabPos);
                            if (currentLine.Length > tabPos + 1)
                            {
                                description = currentLine.Substring(tabPos + 1);
                            }
                        }
                        var id = MixFile.GetFileID(name);
                        if (!mapping.ContainsKey(id))
                        {
                            mapping.Add(id, new MixEntryItem(name, description));
                        }
                    }
                }
            }
            return new MixEntryNames(mapping);
        }

        public MixEntryNames(Dictionary<uint, MixEntryItem> idNameMapping)
        {
            IdNameMapping = idNameMapping;
        }

        public Dictionary<uint, MixEntryItem> IdNameMapping { get; }

        public string GetName(uint id)
        {
            return IdNameMapping.TryGetValue(id, out var entryItem) 
                ? entryItem.Name
                : "";
        }
    }

    public class MixEntryItem
    {
        public string Name { get; }

        public string Description { get; }

        public MixEntryItem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
