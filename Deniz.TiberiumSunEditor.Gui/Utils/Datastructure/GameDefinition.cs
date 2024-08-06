using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class GameDefinition
    {
        private readonly string? _customGamePath;

        public GameDefinition()
        {
        }

        public GameDefinition(string customGamePath)
        {
            _customGamePath = customGamePath;
        }

        public bool IsCustomMod => _customGamePath != null;

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

        public string MixFiles { get; set; } = "";

        public bool UseAres { get; set; }

        public bool UsePhobos { get; set; }

        public List<SideDefinition> Sides { get; set; } = new();

        public string? GetUserGamePath()
        {
            return _customGamePath 
                   ?? UserSettingsFile.Instance.GamePaths.FirstOrDefault(g => g.GameKey == GameKey)?.GamePath;
        }

    }
}
