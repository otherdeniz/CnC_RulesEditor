namespace Deniz.TiberiumSunEditor.Gui.Model;

public class EntityTypeEditControlTypeModel
{
    public EntityTypeEditControlTypeModel(string entityType, Type editControlType)
    {
        EntityType = entityType;
        EditControlType = editControlType;
    }

    public string EntityType { get; }

    public Type EditControlType { get; }

}