using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class GameDefinition
    {
        private readonly CustomModSetting? _customMod;

        public GameDefinition()
        {
        }

        public GameDefinition(CustomModSetting customMod)
        {
            _customMod = customMod;
        }

        public bool IsCustomMod => _customMod != null;

        public CustomModSetting? CustomMod => _customMod;

        public string GameKey { get; set; } = "";

        public string Logo { get; set; } = "";

        public string NewMenuLabel { get; set; } = "";

        public bool NewMenuSeparator { get; set; }

        public string ResourcesDefaultIniFile { get; set; } = "";

        public string ResourcesDefaultArtIniFile { get; set; } = "";

        public string ResourcesDefaultAiIniFile { get; set; } = "";

        public string? ResourcesDescriptionIniFile { get; set; }

        public string IniNameMatchDetection { get; set; } = "";

        public string SaveAsFilename { get; set; } = "";

        public string SaveAsArtFilename { get; set; } = "";

        public string SaveAsAiFilename { get; set; } = "";

        public string SaveAsRelativeToGameFolder { get; set; } = "";

        public string GameExecutable { get; set; } = "";

        public string SnippetsFolder { get; set; } = "";

        public string BitmapsFolders { get; set; } = "";

        public string? MixFiles { get; set; } = null;

        public string? SoundIni { get; set; } = null;

        public bool UseAres { get; set; }

        public bool UsePhobos { get; set; }

        public bool UsePhobosSectionInheritance { get; set; }

        public bool UseVinifera { get; set; }

        public bool UseSectionInheritance { get; set; }

        public List<SideDefinition> Sides { get; set; } = new();

        public string? GetUserGamePath()
        {
            return _customMod?.GamePath
                   ?? UserSettingsFile.Instance.GamePaths.FirstOrDefault(g => g.GameKey == GameKey)?.GamePath;
        }

        public IniFile LoadCurrentRulesFile()
        {
            var currentIniPath = GetUserGamePath();
            if (!string.IsNullOrEmpty(currentIniPath)
                && Directory.Exists(currentIniPath))
            {
                if (!string.IsNullOrEmpty(SaveAsRelativeToGameFolder))
                {
                    currentIniPath = Path.Combine(currentIniPath, SaveAsRelativeToGameFolder);
                    var currentIniFilePath = Path.Combine(currentIniPath, SaveAsFilename);
                    if (File.Exists(currentIniFilePath))
                    {
                        return IniFile.Load(currentIniFilePath);
                    }
                }
            }
            return LoadDefaultRulesFile();
        }

        public IniFile LoadDefaultRulesFile(bool isNewFile = false)
        {
            return !string.IsNullOrEmpty(ResourcesDefaultIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(ResourcesDefaultIniFile), 
                    SaveAsFilename,
                    IsCustomMod && !isNewFile ? ResourcesDefaultIniFile : null)
                : new IniFile();
        }

        public IniFile LoadDefaultArtFile(bool isNewFile = false)
        {
            return !string.IsNullOrEmpty(ResourcesDefaultArtIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(ResourcesDefaultArtIniFile), 
                    SaveAsArtFilename,
                    IsCustomMod && !isNewFile ? ResourcesDefaultArtIniFile : null)
                : new IniFile();
        }

        public IniFile LoadCurrentArtFile()
        {
            var currentIniPath = GetUserGamePath();
            if (!string.IsNullOrEmpty(currentIniPath)
                && Directory.Exists(currentIniPath))
            {
                if (!string.IsNullOrEmpty(SaveAsRelativeToGameFolder))
                {
                    currentIniPath = Path.Combine(currentIniPath, SaveAsRelativeToGameFolder);
                    var currentIniFilePath = Path.Combine(currentIniPath, SaveAsArtFilename);
                    if (File.Exists(currentIniFilePath))
                    {
                        return IniFile.Load(currentIniFilePath);
                    }
                }
            }
            return LoadDefaultArtFile();
        }

        public IniFile LoadDefaultAiFile(bool isNewFile = false)
        {
            return !string.IsNullOrEmpty(ResourcesDefaultAiIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(ResourcesDefaultAiIniFile), 
                    SaveAsAiFilename,
                    IsCustomMod && !isNewFile ? ResourcesDefaultAiIniFile : null)
                : new IniFile();
        }

        public IniFile LoadCurrentAiFile()
        {
            var currentIniPath = GetUserGamePath();
            if (!string.IsNullOrEmpty(currentIniPath)
                && Directory.Exists(currentIniPath))
            {
                if (!string.IsNullOrEmpty(SaveAsRelativeToGameFolder))
                {
                    currentIniPath = Path.Combine(currentIniPath, SaveAsRelativeToGameFolder);
                    var currentIniFilePath = Path.Combine(currentIniPath, SaveAsAiFilename);
                    if (File.Exists(currentIniFilePath))
                    {
                        return IniFile.Load(currentIniFilePath);
                    }
                }
            }
            return LoadDefaultAiFile();
        }

        public IniFile? LoadDescriptionRulesFile()
        {
            return !string.IsNullOrEmpty(ResourcesDescriptionIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(ResourcesDescriptionIniFile))
                : null;
        }

    }
}
