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
            unitEdit.LoadModel(entity, hiddenValueKeys);
        }

        protected override void OnReadonlyChanged()
        {
            panelButtons.Visible = !ReadonlyMode;
            buttonRefreshName.Visible = !ReadonlyMode;
            unitEdit.ReadonlyMode = ReadonlyMode;
            textName.ReadOnly = ReadonlyMode;
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            EntityModel?.FileSection.SetValue("Name", textName.Text);
            RaiseNameChanged();
        }

        public static string GenerateName(GameEntityModel teamEntityModel)
        {
            var prefix = "";
            if (teamEntityModel.FileSection.GetValue("Easy")?.Value == "yes")
            {
                prefix = "E";
            }
            if (teamEntityModel.FileSection.GetValue("Medium")?.Value == "yes")
            {
                prefix += "M";
            }
            if (teamEntityModel.FileSection.GetValue("Hard")?.Value == "yes")
            {
                prefix += "H";
            }

            if (prefix != string.Empty)
            {
                prefix += ": ";
            }
            var team1Name = teamEntityModel.EntityValueList.FirstOrDefault(v => v.Key == "Team1")?.ValueName
                            ?? string.Empty;
            if (team1Name == string.Empty)
            {
                team1Name = "[please assign Team1]";
            }
            return $"{prefix}{team1Name}";
        }

        private void buttonRefreshName_Click(object sender, EventArgs e)
        {
            if (EntityModel == null) return;
            textName.Text = GenerateName(EntityModel);
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

        private void unitEdit_KeyValueChanged(object sender, KeyValueChangedEventArgs e)
        {
            if (e.Key == "Team1" || e.Key == "Easy" || e.Key == "Medium" || e.Key == "Hard")
            {
                buttonRefreshName_Click(this, e);
            }
        }
    }
}
