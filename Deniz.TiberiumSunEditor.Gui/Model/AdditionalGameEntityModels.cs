namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class AdditionalGameEntityModels
    {
        public AdditionalGameEntityModels(string module, string entityType, List<GameEntityModel> entities)
        {
            Module = module;
            EntityType = entityType;
            Entities = entities;
        }

        public string Module { get; }

        public string EntityType { get; }

        public List<GameEntityModel> Entities { get; }

    }
}
