using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Newtonsoft.Json;

namespace Deniz.TiberiumSunEditor.Gui.Utils.UserSettings
{
    public class UserSettingsFile : JsonFileBase
    {
        private static UserSettingsFile? _instance;
        private static FileChangeWatcher? _watcher;
        public static UserSettingsFile Instance => _instance ??= LoadFile();
        private static UserSettingsFile LoadFile()
        {
            var settingsFilePath = Path.Combine(UserSettingsFolder.Instance.AppDataFolder, "UserSettings.json");
            _watcher = new FileChangeWatcher(UserSettingsFolder.Instance.AppDataFolder, "UserSettings.json");
            _watcher.Changed += (sender, args) =>
            {
                var changedFile = Load<UserSettingsFile>(settingsFilePath);
                changedFile.ChangeWatcher = _watcher;
                _instance = changedFile;
                ExternalChanged?.Invoke(changedFile, EventArgs.Empty);
            };
            var file = Load<UserSettingsFile>(settingsFilePath);
            file.ChangeWatcher = _watcher;
            return file;
        }

        private UserFavoriteSettings? _sectionsSettings;
        private UserFavoriteSettings? _commonValuesSettings;
        private UserFavoriteSettings? _unitValuesSettings;
        private int _settingUnitPickerColumns = 2;

        public static event EventHandler? ExternalChanged;
        public event EventHandler? SettingUnitPickerColumnsChanged;

        public List<string> FavoriteSections { get; set; } = new();

        public List<string> FavoriteCommonValues { get; set; } = new();

        public List<string> FavoriteUnitValues { get; set; } = new();

        public List<EntityGroupSetting> EntityGroups { get; set; } = new();

        public bool SettingPlayOpeningSound { get; set; } = true;

        public bool SettingAutoUpdate { get; set; } = true;

        public int SettingUnitPickerColumns
        {
            get => _settingUnitPickerColumns;
            set
            {
                if (_settingUnitPickerColumns == value) return;
                _settingUnitPickerColumns = value;
                SettingUnitPickerColumnsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

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
                    GameKey = fileType.GameDefinition.GetModKeyOrGameKey(),
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

        public void AddEntityToGroup(string gameKey, string entityType, string entityKey, string groupName)
        {
            RemoveEntityFromGroups(gameKey, entityType, entityKey);
            var addToGroup = EntityGroups.FirstOrDefault(g => g.EntityType == entityType
                                                              && g.GameKey == gameKey
                                                              && g.GroupName == groupName);
            if (addToGroup == null)
            {
                addToGroup = new EntityGroupSetting
                {
                    GameKey = gameKey,
                    EntityType = entityType,
                    GroupName = groupName
                };
                EntityGroups.Add(addToGroup);
            }
            addToGroup.Keys.Add(entityKey);
            Save();
        }

        public void RemoveEntityFromGroups(string gameKey, string entityType, string entityKey)
        {
            var removeFromGroup = EntityGroups.FirstOrDefault(g => g.EntityType == entityType
                                                                   && g.GameKey == gameKey
                                                                   && g.Keys.Any(k => k == entityKey));
            if (removeFromGroup != null)
            {
                removeFromGroup.Keys.Remove(entityKey);
                if (!removeFromGroup.Keys.Any())
                {
                    EntityGroups.Remove(removeFromGroup);
                }
                Save();
            }
        }
    }
}
