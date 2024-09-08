using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

public class AistructureFile : JsonFileBase
{
    private static AistructureFile? _instance;
    private static AistructureFile? _instanceAres;
    private static AistructureFile? _instancePhobos;

    public static AistructureFile Instance => _instance ??= LoadFile("Aistructure.json");
    public static AistructureFile InstanceAres => _instanceAres ??= LoadFile("AistructureAres.json");
    public static AistructureFile InstancePhobos => _instancePhobos ??= LoadFile("AistructurePhobos.json");

    protected static AistructureFile LoadFile(string fileName)
    {
        using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream(fileName))
        {
            return Load<AistructureFile>(fileStream);
        }
    }

    public List<UnitValueDefinition> Teams { get; set; } = new();

    public List<UnitValueDefinition> TriggerVirtualSections { get; set; } = new();

    public List<ScriptActionDefinition> ScriptActions { get; set; } = new();

    public List<ScriptBuildingParameter2Definition> ScriptBuildingParameter2 { get; set; } = new();

    public IEnumerable<ScriptActionDefinition> GetScriptActionsFiltered(string gameKey)
    {
        return ScriptActions.Where(v =>
            v.GameKeyFilter == null || v.GameKeyFilter.Split(",").Any(g => g == gameKey));
    }

    public AistructureFile MergeWith(AistructureFile priorityFile)
    {
        return new AistructureFile
        {
            Teams = priorityFile.Teams,
            TriggerVirtualSections = priorityFile.TriggerVirtualSections,
            ScriptActions = priorityFile.ScriptActions
                .UnionBy(ScriptActions, k => k.Number, StringEqualityComparer.Instance)
                .ToList(),
            ScriptBuildingParameter2 = priorityFile.ScriptBuildingParameter2
        };
    }
}