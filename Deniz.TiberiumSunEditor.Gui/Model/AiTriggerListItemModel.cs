using static Deniz.TiberiumSunEditor.Gui.Model.AiRootModel;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class AiTriggerListItemModel : EntityListItemModel
    {
        public AiTriggerListItemModel(string id, VirtualTriggerGameEntitiyModel virtualTriggerEntityModel) 
            : base(id, virtualTriggerEntityModel)
        {
        }

        public override string Id => base.Id;

        public override string Name => base.Name;

        public string Easy => EntityModel.FileSection.GetValue("Easy")?.Value ?? "no";

        public string Medium => EntityModel.FileSection.GetValue("Medium")?.Value ?? "no";

        public string Hard => EntityModel.FileSection.GetValue("Hard")?.Value ?? "no";
    }
}
