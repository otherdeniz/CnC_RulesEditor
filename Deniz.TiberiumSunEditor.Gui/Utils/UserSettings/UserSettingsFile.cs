using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
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

        public List<RecentFileSetting> RecentFiles { get; set; } = new();

        [JsonIgnore]
        public UserFavoriteSettings SectionsSettings => _sectionsSettings
            ??= new UserFavoriteSettings(FavoriteSections);

        [JsonIgnore]
        public UserFavoriteSettings CommonValuesSettings => _commonValuesSettings
            ??= new UserFavoriteSettings(FavoriteCommonValues);

        [JsonIgnore]
        public UserFavoriteSettings UnitValuesSettings => _unitValuesSettings
            ??= new UserFavoriteSettings(FavoriteUnitValues);

        public List<(RecentFileSetting Setting, GameDefinition Definition)> GetRecentFiles()
        {
            var recentFiles = new List<(RecentFileSetting Setting, GameDefinition Definition)>();
            foreach (var fileSetting in RecentFiles)
            {
                var gameDefinition = GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == fileSetting.GameKey)
                                     ?? CustomMods.FirstOrDefault(m => m.Key == fileSetting.GameKey)?.ToGameDefinition();
                if (gameDefinition != null)
                {
                    recentFiles.Add(new ValueTuple<RecentFileSetting, GameDefinition>(fileSetting, gameDefinition));
                }
            }
            return recentFiles;
        }

        public void AddRecentFile(string filePath, FileTypeModel fileType)
        {
            if (RecentFiles.All(f => f.FilePath != filePath))
            {
                RecentFiles.Insert(0, new RecentFileSetting
                {
                    GameKey = fileType.GameDefinition.GameKey,
                    FileType = fileType.BaseType.ToString(),
                    FilePath = filePath
                });
                while (RecentFiles.Count > 10)
                {
                    RecentFiles.RemoveAt(10);
                }
                Save();
            }
        }
    }
}
