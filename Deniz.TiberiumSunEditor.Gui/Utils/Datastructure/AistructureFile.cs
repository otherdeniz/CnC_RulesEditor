using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

public class AistructureFile : JsonFileBase
{
    private static AistructureFile? _instance;

    public static AistructureFile Instance => _instance ??= LoadFile("Aistructure.json");

    protected static AistructureFile LoadFile(string fileName)
    {
        using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream(fileName))
        {
            return Load<AistructureFile>(fileStream);
        }
    }

    public List<UnitValueDefinition> Teams { get; set; } = new();

    public List<UnitValueDefinition> TriggerVirtualSections { get; set; } = new();

}