using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class EntityListIemModel
    {
        public EntityListIemModel(string id, GameEntityModel entityModel)
        {
            Id = id;
            EntityModel = entityModel;
        }

        public string Id { get; }

        public string Name => EntityModel.FileSection.GetValue("Name")?.Value ?? string.Empty;

        [DisplayName("Mod.")]
        public int? Modifications
        {
            get
            {
                var modifications = EntityModel.ModificationCount;
                return modifications == 0 
                    ? null 
                    : modifications;
            }
        }

        [Browsable(false)]
        public GameEntityModel EntityModel { get; }
    }
}
