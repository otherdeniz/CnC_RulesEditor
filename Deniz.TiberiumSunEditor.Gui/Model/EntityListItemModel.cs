using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class EntityListItemModel
    {
        public EntityListItemModel(string id, GameEntityModel entityModel)
        {
            Id = id;
            EntityModel = entityModel;
        }

        public string Id { get; }

        public string Name => EntityModel.FileSection.GetValue("Name")?.Value ?? string.Empty;

        [Browsable(false)]
        public bool IsModified => EntityModel.ModificationCount > 0;

        [Browsable(false)]
        public GameEntityModel EntityModel { get; }
    }
}
