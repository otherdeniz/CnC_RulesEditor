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

        public virtual string Id { get; }

        public virtual string Name => EntityModel.EntityName;

        [Browsable(false)]
        public bool IsModified => EntityModel.ModificationCount > 0;

        [Browsable(false)]
        public GameEntityModel EntityModel { get; }
    }
}
