using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class FileTypeModel
    {
        public static FileTypeModel Empty { get; } = new FileTypeModel(FileBaseType.Unknown, "Empty");

        public static FileTypeModel? ParseFile(IniFile iniFile, Func<string, GameDefinition?>? mapGameDefintion = null)
        {
            var nameValue = iniFile.GetSection("General")?.GetValue("Name");
            if (nameValue != null)
            {
                foreach (var customMod in UserSettingsFile.Instance.CustomMods)
                {
                    if (nameValue.Value == customMod.IniNameMatchDetection)
                    {
                        var gameDefinition = customMod.ToGameDefinition();
                        if (gameDefinition != null)
                        {
                            return new FileTypeModel(FileBaseType.Rules, nameValue.Value, gameDefinition, customMod.Name);
                        }
                    }
                }
                foreach (var gameDefinition in GamesFile.Instance.Games)
                {
                    if (nameValue.Value.Contains(gameDefinition.IniNameMatchDetection))
                    {
                        return new FileTypeModel(FileBaseType.Rules, nameValue.Value, gameDefinition);
                    }
                }
            }
            if (iniFile.GetSection("Map") != null && mapGameDefintion != null)
            {
                var mapName = iniFile.GetSection("Basic")?.GetValue("Name")?.Value ?? "?";
                var mapGame = mapGameDefintion(mapName);
                if (mapGame == null)
                {
                    return null;
                }
                return new FileTypeModel(FileBaseType.Map, mapName, mapGame);
            }
            return new FileTypeModel(FileBaseType.Unknown, iniFile.OriginalFileName);
        }

        private readonly string? _keyOverride;

        public FileTypeModel(FileBaseType baseType, string title, GameDefinition? gameDefinition = null, string? keyOverride = null)
        {
            _keyOverride = keyOverride;
            BaseType = baseType;
            Title = title;
            GameDefinition = gameDefinition ?? new GameDefinition();
        }

        public FileBaseType BaseType { get; }

        public string Key => _keyOverride ?? GameDefinition.GameKey;

        public string TypeLabel
        {
            get
            {
                switch (BaseType)
                {
                    case FileBaseType.Rules:
                        return Key;
                    case FileBaseType.Map:
                        return $"Map:{Key}";
                    default:
                        return BaseType.ToString();
                }
            }
        }

        public string Title { get; }

        public GameDefinition GameDefinition { get; }

        public string[] GetBitmapSubFolders()
        {
            return GameDefinition.BitmapsFolders.Split(",");
        }
    }

    public enum FileBaseType
    {
        Rules,
        Map,
        Unknown
    }
}
