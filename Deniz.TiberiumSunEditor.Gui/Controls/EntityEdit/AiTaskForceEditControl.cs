using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiTaskForceEditControl : EntityEditBaseControl
    {
        private List<AiTaskForceKeyValueModel> _keyValueModelList = null!;
        private bool _doEvents;

        public AiTaskForceEditControl()
        {
            InitializeComponent();
        }

        public override void LoadEntity(GameEntityModel entity, FilterByParentModel? filterKeyValue = null)
        {
            base.LoadEntity(entity, filterKeyValue);
            _doEvents = false;
            labelKey.Text = entity.EntityKey;
            textName.Text = entity.EntityName;
            comboGroup.Text = entity.FileSection.GetValue("Group")?.Value ?? string.Empty;
            _doEvents = true;
            _keyValueModelList = new string[]
            {
                "0", "1", "2", "3", "4"
            }.Select(k => new AiTaskForceKeyValueModel(entity, k, RefreshName)).ToList();
            LoadValuesGrid();
            LoadTeamsList();
        }

        private void LoadValuesGrid()
        {
            valuesGrid.DataSource = _keyValueModelList;
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].Width = 30;
        }

        private void LoadTeamsList(string? selectedTeamKey = null)
        {
            if (EntityModel?.RootModel is AiRootModel aiRootModel)
            {
                var childFilter = new FilterByParentModel(EntityModel, "TaskForce", EntityModel.EntityKey);
                entitiesListTeams.LoadListModel(aiRootModel, aiRootModel.TeamEntities, childFilter, selectedTeamKey);
            }
        }

        private void RefreshName()
        {
            textName.Text = string.Join(", ",
                _keyValueModelList.Where(v => v.Count != null && v.UnitModel != null)
                    .Select(v => $"{v.Count} {v.UnitModel!.GetNameOrKey()}"));
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            EntityModel?.FileSection.SetValue("Name", textName.Text);
            RaiseNameChanged();
        }

        private void comboGroup_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            EntityModel!.FileSection.SetValue("Group", comboGroup.Text);
        }

        private void LookupEntityValue(AiTaskForceKeyValueModel valueModel, UltraGridRow row)
        {
            if (EntityModel == null) return;
            var newUnit = SelectUnitForm.ExecuteSelect(ParentForm!, EntityModel.RulesRootModel,
                SelectTechnoTypes.Vehicles | SelectTechnoTypes.Infantry | SelectTechnoTypes.Aircrafts);
            if (newUnit != null)
            {
                valueModel.UnitKey = newUnit.EntityKey;
                EntityModel!.RootModel.RaiseGlobalEntityNotification(newUnit.EntityKey, "RefreshInfoNumber");
                row.Refresh();
            }
        }

        private void valuesGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Row.ListObject is AiTaskForceKeyValueModel valueModel)
            {
                if (e.Cell.Column.Key == "DeleteImage"
                    && valueModel.DeleteImage != null)
                {
                    var oldUnitKey = valueModel.UnitKey;
                    valueModel.UnitKey = string.Empty;
                    EntityModel!.RootModel.RaiseGlobalEntityNotification(oldUnitKey, "RefreshInfoNumber");
                    e.Cell.Row.Refresh();
                }
                else if (e.Cell.Column.Key == "PlusImage"
                         && valueModel.PlusImage != null)
                {
                    var count = valueModel.Count;
                    if (count != null)
                    {
                        valueModel.Count = count + 1;
                        e.Cell.Row.Refresh();
                    }
                }
                else if (e.Cell.Column.Key == "MinusImage"
                         && valueModel.MinusImage != null)
                {
                    var count = valueModel.Count;
                    if (count != null)
                    {
                        valueModel.Count = count - 1;
                        e.Cell.Row.Refresh();
                    }
                }
                else if (e.Cell.Column.Key == "UnitPicture")
                {
                    e.Cell.CancelUpdate();
                    LookupEntityValue(valueModel, e.Cell.Row);
                }
            }
        }

        private void valuesGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.ListObject is AiTaskForceKeyValueModel)
            {
                e.Row.Cells["Key"].Activation = Activation.NoEdit;
                e.Row.Cells["Cost"].Activation = Activation.NoEdit;
                e.Row.Cells["Speed"].Activation = Activation.NoEdit;
            }
        }

        private void buttonRefreshName_Click(object sender, EventArgs e)
        {
            RefreshName();
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            //CopyEntity?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (EntityModel!.RootModel is AiRootModel aiRootModel)
            {
                if (MessageBox.Show("Do you want to delete this TaskForce\n" +
                                    "and all Teams of this TaskForce?\n" +
                                    "and all AITriggers where any TaskForce-Team is Team1?", "Delete with all relatives?", MessageBoxButtons.YesNo) ==
                    DialogResult.Yes)
                {
                    var unitKeys = _keyValueModelList.Where(v => v.Count != null && v.UnitModel != null)
                        .Select(v => v.UnitModel!.EntityKey)
                        .ToList();
                    // delete all teams
                    var teamsToDelete = aiRootModel.TeamEntities.Where(t =>
                        t.EntityModel.FileSection.KeyValues.Any(k =>
                            k.Key == "TaskForce" && k.Value == EntityModel.EntityKey))
                        .ToList();
                    foreach (var teamToDelete in teamsToDelete)
                    {
                        // delete Triggers (by Team1)
                        var triggersToDelete = aiRootModel.TriggerEntities.Where(t =>
                            t.EntityModel.FileSection.KeyValues.Any(k =>
                                k.Key == "Team1" && k.Value == teamToDelete.EntityModel.EntityKey));
                        aiRootModel.TriggerEntities.RemoveWhere(t => triggersToDelete.Any(d => d.EntityModel.EntityKey == t.EntityModel.EntityKey));
                        aiRootModel.File.GetSection("AITriggerTypes")?.RemoveValues(v => triggersToDelete.Any(d => d.EntityModel.EntityKey == v.Key));
                        // update Triggers (by Team2)
                        foreach (var updateTrigger in aiRootModel.TriggerEntities.Where(t =>
                                     t.EntityModel.FileSection.KeyValues.Any(k =>
                                         k.Key == "Team2" && k.Value == teamToDelete.EntityModel.EntityKey)))
                        {
                            updateTrigger.EntityModel.FileSection.SetValue("Team2", "<none>");
                        }
                        // delete Team
                        aiRootModel.TeamEntities.RemoveWhere(t => t.EntityModel.EntityKey == teamToDelete.EntityModel.EntityKey);
                        aiRootModel.File.GetSection("TeamTypes")?.RemoveValues(v => v.Key == teamToDelete.EntityModel.EntityKey);
                        aiRootModel.LookupItems.RemoveWhere(l => l.Key == teamToDelete.EntityModel.EntityKey);
                        if (aiRootModel.LookupEntities.TryGetValue("TeamTypes", out var teamLookupEntities))
                        {
                            teamLookupEntities.RemoveWhere(l => l.EntityKey == teamToDelete.EntityModel.EntityKey);
                        }
                    }
                    // delete TaskForce
                    aiRootModel.TaskForceEntities.RemoveWhere(t => t.EntityModel.EntityKey == EntityModel.EntityKey);
                    aiRootModel.File.GetSection("TaskForces")?.RemoveValues(v => v.Key == EntityModel.EntityKey);
                    aiRootModel.LookupItems.RemoveWhere(l => l.Key == EntityModel.EntityKey);
                    if (aiRootModel.LookupEntities.TryGetValue("TaskForces", out var lookupEntities))
                    {
                        lookupEntities.RemoveWhere(l => l.EntityKey == EntityModel.EntityKey);
                    }
                    // for each unit: RefreshInfoNumber
                    unitKeys.ForEach(k => EntityModel!.RootModel.RaiseGlobalEntityNotification(k, "RefreshInfoNumber"));
                    RaiseEntityDeleted();
                }
            }
        }

        private void entitiesListTeams_AddedEntity(object sender, GameEntityEventArgs e)
        {
            e.GameEntity.FileSection.SetValue("Name", AiTeamEditControl.GenerateName(e.GameEntity));
        }
    }
}
