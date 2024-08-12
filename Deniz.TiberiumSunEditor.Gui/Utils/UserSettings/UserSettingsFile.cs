using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Newtonsoft.Json;

namespace Deniz.TiberiumSunEditor.Gui.Utils.UserSettings
{
    public class UserSettingsFile : JsonFileBase
    {
        private static UserSettingsFile? _instance;
        public static UserSettingsFile Instance => _instance ??= LoadFile();
        private static UserSettingsFile LoadFile()
        {
            var settingsFilePath = Path.Combine(UserSettingsFolder.Instance.AppDataFolder, "UserSettings.json");
            return Load<UserSettingsFile>(settingsFilePath);
        }

        private UserFavoriteSettings? _sectionsSettings;
        private UserFavoriteSettings? _commonValuesSettings;
        private UserFavoriteSettings? _unitValuesSettings;

        public List<string> FavoriteSections { get; set; } = new();

        public List<string> FavoriteCommonValues { get; set; } = new();

        public List<string> FavoriteUnitValues { get; set; } = new();

        public bool SettingPlayOpeningSound { get; set; } = true;

        public bool SettingAutoUpdate { get; set; } = true;

        public List<GamePathSetting> GamePaths { get; set; } = new();

        public List<CustomModSetting> CustomMods { get; set; } = new();

        public string SelectedTheme { get; set; } = string.Empty;

        [JsonIgnore]
        public UserFavoriteSettings SectionsSettings => _sectionsSettings
            ??= new UserFavoriteSettings(FavoriteSections);

        [JsonIgnore]
        public UserFavoriteSettings CommonValuesSettings => _commonValuesSettings
            ??= new UserFavoriteSettings(FavoriteCommonValues);

        [JsonIgnore]
        public UserFavoriteSettings UnitValuesSettings => _unitValuesSettings
            ??= new UserFavoriteSettings(FavoriteUnitValues);

    }
}
