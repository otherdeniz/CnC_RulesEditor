using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiTriggerEditControl : EntityEditBaseControl
    {
        private bool _doEvents;

        public AiTriggerEditControl()
        {
            InitializeComponent();
        }

        public override void LoadEntity(GameEntityModel entity, FilterByParentModel? filterKeyValue = null)
        {
            base.LoadEntity(entity, filterKeyValue);
            _doEvents = false;
            labelKey.Text = entity.EntityKey;
            textName.Text = entity.EntityName;
            _doEvents = true;
            var hiddenValueKeys = new List<string> { "Name" };
            if (!string.IsNullOrEmpty(filterKeyValue?.Key))
            {
                hiddenValueKeys.Add(filterKeyValue.Key);
            }
            unitEdit.LoadModel(entity, hiddenValueKeys);
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            EntityModel?.FileSection.SetValue("Name", textName.Text);
            RaiseNameChanged();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (EntityModel!.RootModel is AiRootModel aiRootModel)
            {
                if (MessageBox.Show("Do you want to delete this AITrigger?", "Delete?", MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    aiRootModel.TriggerEntities.RemoveWhere(t => t.EntityModel.EntityKey == EntityModel.EntityKey);
                    aiRootModel.File.GetSection("AITriggerTypes")?.RemoveValues(v => v.Key == EntityModel.EntityKey);
                    RaiseEntityDeleted();
                }
            }
        }
    }
}
