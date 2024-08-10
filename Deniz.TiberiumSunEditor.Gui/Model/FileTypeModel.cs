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
                 || iniFile.OriginalFullPath != null && overrideGameDefinition?.ResourcesDefaultArtIniFile == iniFile.OriginalFullPath
                 || iniFile.Sections.Any() && iniFile.Sections[0].Lines.OfType<IniFileLineComment>().Any(c => c.Comment.Contains("ART.INI")))
                && fileGameDefintion != null)
            {
                var artGame = overrideGameDefinition 
                              ?? fileGameDefintion(iniFile.OriginalFileName);
                if (artGame == null)
                {
                    return null;
                }

                var fileTitle = iniFile.OriginalFileName;
                if (iniFile.OriginalFullPath != null 
                    && overrideGameDefinition?.ResourcesDefaultArtIniFile == iniFile.OriginalFullPath
                    && overrideGameDefinition.CustomMod != null)
                {
                    fileTitle = overrideGameDefinition.CustomMod.ArtIniMixSource ?? "";
                }
                return new FileTypeModel(FileBaseType.Art, fileTitle, artGame);
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
                var keyLabel = Key;
                if (GameDefinition.CustomMod != null)
                {
                    keyLabel = GameDefinition.CustomMod.Name.Replace("&", "&&");
                }
                switch (BaseType)
                {
                    case FileBaseType.Rules:
                        return $"Rules: {keyLabel}";
                    case FileBaseType.Map:
                        return $"Map: {keyLabel}";
                    case FileBaseType.Art:
                        return $"Art: {keyLabel}";
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
