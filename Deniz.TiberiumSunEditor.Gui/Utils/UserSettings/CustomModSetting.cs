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

        public string RulesIniPath { get; set; } = "";

        public string IniNameMatchDetection { get; set; } = "";

        public string GetRulesIniFilePath()
        {
            return Path.Combine(GamePath, RulesIniPath);
        }

        public IniFile LoadRulesIniFile()
        {
            return IniFile.Load(GetRulesIniFilePath());
        }

    }
}
