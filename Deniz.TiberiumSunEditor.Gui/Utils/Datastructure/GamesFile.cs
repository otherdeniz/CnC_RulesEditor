using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class GamesFile : JsonFileBase
    {
        private static GamesFile? _instance;
        public static GamesFile Instance => _instance ??= LoadFile();
        private static GamesFile LoadFile()
        {
            using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream("Games.json"))
            {
                return Load<GamesFile>(fileStream);
            }
        }

        public List<GameDefinition> Games { get; set; } = null!;
    }
}
