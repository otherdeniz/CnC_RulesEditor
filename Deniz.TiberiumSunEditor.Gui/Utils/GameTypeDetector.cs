﻿using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using ImageMagick;
using Infragistics.Win;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class GameTypeDetector
    {
        public GameTypeDetector(string gameDirectory)
        {
            GameDirectory = gameDirectory;
            BaseType = DetectedBaseType();
            FileManager = new CCFileManager(gameDirectory);
            InitFileManager();
        }

        public string GameDirectory { get; }

        public GameDefinition? BaseType { get; }

        public CCFileManager FileManager { get; }

        public List<string> CheckFilesToMoveToIniFolder(IEnumerable<string> searchPatterns)
        {
            var moveFileNames = new List<string>();
            var iniPath = Path.Combine(GameDirectory, "INI");
            foreach (var searchPattern in searchPatterns)
            {
                var iniFiles = Directory.GetFiles(GameDirectory, searchPattern)
                    .Select(p => Path.GetFileName(p));
                foreach (var iniFile in iniFiles)
                {
                    if (!File.Exists(Path.Combine(iniPath, iniFile)))
                    {
                        moveFileNames.Add(iniFile);
                    }
                }
            }
            return moveFileNames;
        }

        public IEnumerable<string> IniFolderSearch(string searchPattern)
        {
            var iniPath = Path.Combine(GameDirectory, "INI");
            if (Directory.Exists(iniPath))
            {
                return Directory.GetFiles(iniPath, searchPattern)
                    .Select(p => $"INI\\{Path.GetFileName(p)}");
            }

            return Enumerable.Empty<string>();
        }

        public string? GetClientLogoPath()
        {
            var clientIconPath = Path.Combine(GameDirectory, @"Resources\clienticon.ico");
            return File.Exists(clientIconPath)
                ? clientIconPath
                : null;
        }

        public bool HasModuleAres()
        {
            return File.Exists(Path.Combine(GameDirectory, "Ares.dll"));
        }

        public bool HasModulePhobos()
        {
            return File.Exists(Path.Combine(GameDirectory, "Phobos.dll"));
        }

        private GameDefinition? DetectedBaseType()
        {
            if (File.Exists(Path.Combine(GameDirectory, "TIBSUN.MIX")))
            {
                return GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == "TiberianSun");
            }
            if (File.Exists(Path.Combine(GameDirectory, "ra2md.mix")))
            {
                return GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == "RA2YR");
            }
            if (File.Exists(Path.Combine(GameDirectory, "ra2.mix")))
            {
                return GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == "RA2");
            }
            return null;
        }

        private void InitFileManager()
        {
            FileManager.LoadAllMixFilesInDirectory(null, true);
            FileManager.LoadAllMixFilesInDirectory("MIX", true);
        }
    }
}
