namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class LookupItemModel
    {
        private readonly GameEntityModel? _entityModel;
        private readonly string _name;

        public LookupItemModel(string entityType, string key, GameEntityModel entityModel)
        {
            _entityModel = entityModel;
            _name = string.Empty;
            EntityType = entityType;
            Key = key;
        }
        public LookupItemModel(string entityType, string key, string name)
        {
            _name = name;
            EntityType = entityType;
            Key = key;
        }

        public string EntityType { get; }

        public string Key { get; }

        public string Name => _entityModel?.EntityName ?? _name;
    }
}
