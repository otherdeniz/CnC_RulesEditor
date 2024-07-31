namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class LookupItemModel
    {
        public LookupItemModel(string entityType, string key, string name)
        {
            EntityType = entityType;
            Key = key;
            Name = name;
        }

        public string EntityType { get; }

        public string Key { get; }

        public string Name { get; }
    }
}
