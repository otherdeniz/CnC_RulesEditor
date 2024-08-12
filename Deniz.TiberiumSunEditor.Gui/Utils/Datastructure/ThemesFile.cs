using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class ThemesFile : JsonFileBase
    {
        private static ThemesFile? _instance;
        public static ThemesFile Instance => _instance ??= LoadFile();
        private static ThemesFile LoadFile()
        {
            using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream("Themes.json"))
            {
                return Load<ThemesFile>(fileStream);
            }
        }

        public List<ThemeDefinition> Themes { get; set; } = null!;

    }
}
