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

        public string? ResourcesDescriptionIniFile { get; set; }

        public string IniNameMatchDetection { get; set; } = "";

        public string SaveAsFilename { get; set; } = "";

        public string SaveAsArtFilename { get; set; } = "";

        public string SaveAsRelativeToGameFolder { get; set; } = "";

        public string GameExecutable { get; set; } = "";

        public string SnippetsFolder { get; set; } = "";

        public string BitmapsFolders { get; set; } = "";

        public string? MixFiles { get; set; } = null;

        public string? SoundIni { get; set; } = null;

        public bool UseAres { get; set; }

        public bool UsePhobos { get; set; }

        public bool UseVinifera { get; set; }

        public bool UseSectionInheritance { get; set; }

        public List<SideDefinition> Sides { get; set; } = new();

        public string? GetUserGamePath()
        {
            return _customMod?.GamePath
                   ?? UserSettingsFile.Instance.GamePaths.FirstOrDefault(g => g.GameKey == GameKey)?.GamePath;
        }

        public IniFile LoadDefaultRulesFile()
        {
            return !string.IsNullOrEmpty(ResourcesDefaultIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(ResourcesDefaultIniFile), SaveAsFilename)
                : new IniFile();
        }

        public IniFile LoadDefaultArtFile()
        {
            return !string.IsNullOrEmpty(ResourcesDefaultArtIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(ResourcesDefaultArtIniFile), SaveAsArtFilename)
                : new IniFile();
        }

        public IniFile? LoadDescriptionRulesFile()
        {
            return !string.IsNullOrEmpty(ResourcesDescriptionIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(ResourcesDescriptionIniFile))
                : null;
        }

    }
}
