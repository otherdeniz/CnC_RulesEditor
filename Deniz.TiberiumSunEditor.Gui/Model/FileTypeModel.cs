using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class FileTypeModel
    {
        public static FileTypeModel Empty { get; } = new FileTypeModel(FileBaseType.Unknown, "Empty");

        public static FileTypeModel? ParseFile(IniFile iniFile, 
            GameDefinition? overrideGameDefinition = null, 
            Func<string, GameDefinition?>? fileGameDefintion = null)
        {
            var nameValue = iniFile.GetSection("General")?.GetValue("Name");
            if (nameValue != null)
            {
                if (overrideGameDefinition != null)
                {
                    return new FileTypeModel(FileBaseType.Rules, nameValue.Value, overrideGameDefinition);
                }
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
            if (iniFile.GetSection("Map") != null && fileGameDefintion != null)
            {
                var mapName = iniFile.GetSection("Basic")?.GetValue("Name")?.Value ?? "?";
                var mapGame = overrideGameDefinition
                              ?? fileGameDefintion(mapName);
                if (mapGame == null)
                {
                    return null;
                }
                return new FileTypeModel(FileBaseType.Map, mapName, mapGame);
            }

            if ((iniFile.OriginalFileName.Equals("art.ini", StringComparison.InvariantCultureIgnoreCase)
                 || iniFile.OriginalFileName.Equals("artmd.ini", StringComparison.InvariantCultureIgnoreCase)
                 || iniFile.Sections.Any()
                 && iniFile.Sections[0].Lines.OfType<IniFileLineComment>().FirstOrDefault()?.Comment == "ART.INI")
                && fileGameDefintion != null)
            {
                var artGame = overrideGameDefinition 
                              ?? fileGameDefintion(iniFile.OriginalFileName);
                if (artGame == null)
                {
                    return null;
                }
                return new FileTypeModel(FileBaseType.Art, iniFile.OriginalFileName, artGame);
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
                        return $"Map: {Key}";
                    case FileBaseType.Art:
                        return $"Art: {Key}";
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
        Art,
        Unknown
    }
}
