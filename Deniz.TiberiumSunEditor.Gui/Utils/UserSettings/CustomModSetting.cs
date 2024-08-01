using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.UserSettings
{
    public class CustomModSetting
    {
        public string Key { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string GamePath { get; set; } = null!;

        public string BaseGameKey { get; set; } = null!;

        public string LogoFile { get; set; } = "";

        public string? RulesIniMixSource { get; set; }

        public string RulesIniPath { get; set; } = "";

        public string IniNameMatchDetection { get; set; } = "";

        public bool HasAres { get; set; }

        public bool HasPhobos { get; set; }

        public string GetRulesIniFilePath()
        {
            return Path.Combine(GamePath, RulesIniPath);
        }

        public IniFile LoadRulesIniFile()
        {
            return IniFile.Load(GetRulesIniFilePath());
        }

        public GameDefinition? ToGameDefinition()
        {
            var baseGameDefinition =
                GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == BaseGameKey);
            if (baseGameDefinition != null)
            {
                return new GameDefinition(GamePath)
                {
                    BitmapsFolders = baseGameDefinition.BitmapsFolders,
                    SnippetsFolder = baseGameDefinition.SnippetsFolder,
                    GameKey = baseGameDefinition.GameKey,
                    MixFiles = baseGameDefinition.MixFiles,
                    ResourcesDefaultIniFile = GetRulesIniFilePath(),
                    ResourcesDescriptionIniFile = baseGameDefinition.ResourcesDescriptionIniFile ??
                                                  baseGameDefinition.ResourcesDefaultIniFile,
                    Sides = baseGameDefinition.Sides,
                    SaveAsFilename = Path.GetFileName(RulesIniPath),
                    UseAres = HasAres,
                    UsePhobos = HasPhobos
                };
            }
            return null;
        }
    }
}
