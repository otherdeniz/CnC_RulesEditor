using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiTeamEditControl : EntityEditBaseControl
    {
        private bool _doEvents;

        public AiTeamEditControl()
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
            LoadTriggersList();
        }

        private void LoadTriggersList(string? selectedTeamKey = null)
        {
            if (EntityModel?.RootModel is AiRootModel aiRootModel)
            {
                var filterByParent = new FilterByParentModel(k =>
                    (k.Key == "Team1" && k.Value == EntityModel.EntityKey)
                    || (k.Key == "Team2" && k.Value == EntityModel.EntityKey));
                entitiesListTriggers.LoadListModel(aiRootModel, aiRootModel.TriggerEntities,
                    filterByParent, selectKey: selectedTeamKey);
            }
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
                if (MessageBox.Show("Do you want to delete this Team\n"+
                                    "and all AITriggers where this Team is Team1?", "Delete?", MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    // delete Triggers (by Team1)
                    var triggersToDelete = aiRootModel.TriggerEntities.Where(t =>
                        t.EntityModel.FileSection.KeyValues.Any(k =>
                            k.Key == "Team1" && k.Value == EntityModel.EntityKey));
                    aiRootModel.TriggerEntities.RemoveWhere(t => triggersToDelete.Any(d => d.EntityModel.EntityKey == t.EntityModel.EntityKey));
                    aiRootModel.File.GetSection("AITriggerTypes")?.RemoveValues(v => triggersToDelete.Any(d => d.EntityModel.EntityKey == v.Key));
                    // update Triggers (by Team2)
                    foreach (var updateTrigger in aiRootModel.TriggerEntities.Where(t =>
                                 t.EntityModel.FileSection.KeyValues.Any(k =>
                                     k.Key == "Team2" && k.Value == EntityModel.EntityKey)))
                    {
                        updateTrigger.EntityModel.FileSection.SetValue("Team2", "<none>");
                    }
                    // delete Team
                    aiRootModel.TeamEntities.RemoveWhere(t => t.EntityModel.EntityKey == EntityModel.EntityKey);
                    aiRootModel.File.GetSection("TeamTypes")?.RemoveValues(v => v.Key == EntityModel.EntityKey);
                    if (aiRootModel.LookupEntities.TryGetValue("TeamTypes", out var lookupEntities))
                    {
                        lookupEntities.RemoveWhere(l => l.EntityKey == EntityModel.EntityKey);
                    }
                    RaiseEntityDeleted();
                }
            }
        }
    }
}
