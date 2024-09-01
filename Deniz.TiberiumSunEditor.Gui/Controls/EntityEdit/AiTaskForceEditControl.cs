using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
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

        public event EventHandler<EventArgs>? CopyEntity;
        public event EventHandler<EventArgs>? DeleteEntity;

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
            valuesGrid.DisplayLayout.Bands[0].Columns["Cost"].Width = 60;
        }

        private void LoadTeamsList(string? selectedTeamKey = null)
        {
            if (EntityModel?.RootModel is AiRootModel aiRootModel)
            {
                var childFilter = new FilterByParentModel("TaskForce", EntityModel.EntityKey);
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
            CopyEntity?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            DeleteEntity?.Invoke(this, EventArgs.Empty);
        }

        private void entitiesListTeams_AddEntity(object sender, EventArgs e)
        {
            //TODO: add new Team
            //LoadTeamsList("new-team-key");
        }

    }
}
