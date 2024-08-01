namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public class CCFileManager
    {
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                .ToUpperInvariant();
        }

        private readonly List<string> _searchDirectories;

        public CCFileManager(string gameDirectory)
        {
            GameDirectory = gameDirectory;
            _searchDirectories = new List<string>
            {
                Path.Combine(GameDirectory, "INI"),
                GameDirectory,
                Path.Combine(GameDirectory, "MIX")
            };
        }

        public string GameDirectory { get; }

        /// <summary>
        /// Contains information on which MIX file each found game file can be loaded from.
        /// </summary>
        public Dictionary<uint, FileLocationInfo> FileLocationInfos { get; } = new();

        public List<MixFileContent> MixFilesContents { get; } = new();

        /// <summary>
        /// List of all MIX files that have been registered with the file manager.
        /// </summary>
        public List<MixFile> MixFiles { get; } = new();

        /// <summary>
        /// List of all CSF files that have been registered with the file manager.
        /// </summary>
        public List<CsfFile> CsfFiles { get; } = new();

        /// <summary>
        /// Adds a directory to the list of directories where files will be
        /// searched from.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        public void AddSearchDirectory(string path)
        {
            _searchDirectories.Add(Path.Combine(GameDirectory, path));
        }

        /// <summary>
        /// Loads a MIX file.
        /// Searches for it from both the search directories
        /// as well as already loaded MIX files.
        /// </summary>
        /// <param name="name">The name of the MIX file.</param>
        /// <param name="loadRecursive">load sub mix files inside of this mix file</param>
        /// <returns>True if the MIX file was successfully loaded, otherwise false.</returns>
        public bool LoadMixFile(string name, bool loadRecursive)
        {
            uint identifier = MixFile.GetFileID(name);

            // First check from game directory, if not found then check from already loaded MIX files
            var rootMixFile = LoadMixFromDirectories(name);
            if (rootMixFile != null)
            {
                if (loadRecursive)
                {
                    LoadMixFileRecursive(rootMixFile);
                }
                return true;
            }

            if (FileLocationInfos.TryGetValue(identifier, out FileLocationInfo value))
            {
                var mixFile = new MixFile(name, value.MixFile, value.Offset);
                mixFile.Parse();
                AddMix(mixFile);
                if (loadRecursive)
                {
                    LoadMixFileRecursive(mixFile);
                }
                return true;
            }

            return false;
        }

        public void LoadAllMixFilesInDirectory(string? gameSubFolder, bool loadRecursive)
        {
            var directoryPath = gameSubFolder == null
                ? GameDirectory
                : Path.Combine(GameDirectory, gameSubFolder);
            if (!Directory.Exists(directoryPath)) return;
            foreach (var filePath in Directory.GetFiles(directoryPath, "*.mix"))
            {
                var fileName = Path.GetFileName(filePath);
                if (fileName.Contains("movie")) continue;
                var mixFile = new MixFile(fileName);
                mixFile.Parse(filePath);
                AddMix(mixFile);
                if (loadRecursive)
                {
                    LoadMixFileRecursive(mixFile);
                }
            }
        }

        private void LoadMixFileRecursive(MixFile parentFile)
        {
            foreach (var subMix in parentFile.GetFileContents().Where(f => 
                         f.FileName?.EndsWith(".mix") == true
                         && f.FileName.Contains("movie") == false))
            {
                if (FileLocationInfos.TryGetValue(subMix.Id, out FileLocationInfo value))
                {
                    var mixFile = new MixFile(subMix.FileName!, value.MixFile, value.Offset);
                    mixFile.Parse();
                    AddMix(mixFile);
                }
            }
        }

        /// <summary>
        /// Attempts to search for and load a MIX file from the search directories.
        /// Returns true if loading the MIX file succeeds, otherwise false.
        /// </summary>
        /// <param name="name">The name of the MIX file.</param>
        /// <returns>True if the MIX file was successfully loaded, otherwise false.</returns>
        private MixFile? LoadMixFromDirectories(string name)
        {
            string? searchDir = null;

            foreach (string dir in _searchDirectories)
            {
                if (File.Exists(Path.Combine(dir, name)))
                {
                    searchDir = dir;
                    break;
                }
            }

            if (searchDir == null)
                return null;

            var mixFile = new MixFile(name);
            mixFile.Parse(Path.Combine(searchDir, name));
            AddMix(mixFile);

            return mixFile;
        }

        /// <summary>
        /// Registers a MIX file to the file system.
        /// Adds all file entries from the MIX file to the file location tracking system.
        /// </summary>
        /// <param name="mixFile">The MIX file to register.</param>
        private void AddMix(MixFile mixFile)
        {
            MixFiles.Add(mixFile);

            foreach (MixFileEntry fileEntry in mixFile.Entries)
            {
                if (FileLocationInfos.ContainsKey(fileEntry.Identifier))
                    continue;

                FileLocationInfos[fileEntry.Identifier] = new FileLocationInfo(mixFile, fileEntry.Offset, fileEntry.Size);
            }

            mixFile.GetFileContents().ForEach(MixFilesContents.Add);

        }

        /// <summary>
        /// Loads MIX files of the format NAME##.
        /// </summary>
        /// <param name="name">The common name of the MIX files.</param>
        public void LoadIndexedMixFiles(string name)
        {
            for (int i = 99; i >= 0; i--)
            {
                LoadMixFile($"{name}{i:00}.mix", true);
            }
        }

        /// <summary>
        /// Loads MIX files with a wildcard.
        /// </summary>
        /// <param name="name">The common name of the MIX files.</param>
        public void LoadWildcardMixFiles(string name)
        {
            foreach (string searchDirectory in _searchDirectories)
            {
                if (!Directory.Exists(searchDirectory))
                    continue;

                var files = Directory.GetFiles(searchDirectory, name);
                foreach (string file in files)
                {
                    LoadMixFile(Path.GetFileName(file), true);
                }
            }
        }

        /// <summary>
        /// Loads a required CSF file.
        /// Throws a FileNotFoundException if the CSF file isn't found.
        /// </summary>
        /// <param name="name">The name of the CSf file.</param>
        public void LoadStringTable(string name)
        {
            var data = LoadFile(name);
            if (data == null) throw new FileNotFoundException("CSF file not found: " + name);
            var file = new CsfFile(name);
            file.ParseFromBuffer(data);
            CsfFiles.Add(file);
        }

        public byte[]? LoadFile(string name)
        {
            foreach (string searchDirectory in _searchDirectories)
            {
                string looseFilePath = Path.Combine(searchDirectory, name);
                if (File.Exists(looseFilePath))
                {
                    return File.ReadAllBytes(looseFilePath);
                }
            }

            uint id = MixFile.GetFileID(name);

            if (FileLocationInfos.TryGetValue(id, out FileLocationInfo value))
            {
                return value.MixFile.GetSingleFileData(value.Offset, value.Size);
            }

            return null;
        }

        private bool IsSpecialMixName(string name)
        {
            name = name.ToUpper();
            switch (name)
            {
                case "$TSECACHE":
                case "$RA2ECACHE":
                case "$TSELOCAL":
                case "$RA2ELOCAL":
                case "$EXPAND":
                case "$EXPANDMD":
                    return true;
                default:
                    return false;
            }
        }

        private void HandleSpecialMixName(string name)
        {
            name = name.ToUpper();
            switch (name)
            {
                case "$TSECACHE":
                    LoadIndexedMixFiles("ecache");
                    break;
                case "$RA2ECACHE":
                    LoadWildcardMixFiles("ecache*.mix");
                    break;
                case "$TSELOCAL":
                    LoadIndexedMixFiles("elocal");
                    break;
                case "$RA2ELOCAL":
                    LoadWildcardMixFiles("elocal*.mix");
                    break;
                case "$EXPAND":
                    LoadIndexedMixFiles("expand");
                    break;
                case "$EXPANDMD":
                    LoadIndexedMixFiles("expandmd");
                    break;
            }
        }
    }

    /// <summary>
    /// Struct for holding data on which MIX file a file exists in,
    /// and where the file exists within the MIX file.
    /// </summary>
    public struct FileLocationInfo
    {
        public FileLocationInfo(MixFile mixFile, int offset, int size)
        {
            MixFile = mixFile;
            Offset = offset;
            Size = size;
        }

        public MixFile MixFile { get; private set; }
        public int Offset { get; private set; }
        public int Size { get; private set; }
    }
}
