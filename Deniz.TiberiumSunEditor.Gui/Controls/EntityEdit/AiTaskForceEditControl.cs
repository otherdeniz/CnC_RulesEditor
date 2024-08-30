using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Model.KeyValue;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiTaskForceEditControl : EntityEditBaseControl
    {
        private List<AiTaskForceKeyValueModel> _keyValueModelList = null!;

        public AiTaskForceEditControl()
        {
            InitializeComponent();
        }

        public override void LoadEntity(GameEntityModel entity)
        {
            base.LoadEntity(entity);
            labelKey.Text = entity.EntityKey;
            textName.Text = entity.EntityName;
            _keyValueModelList = new string[]
            {
                "0", "1", "2", "3", "4"
            }.Select(k => new AiTaskForceKeyValueModel(entity, k, RefreshName)).ToList();
            LoadValuesGrid();
        }

        private void LoadValuesGrid()
        {
            valuesGrid.DataSource = _keyValueModelList;
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].Width = 30;
        }

        private void RefreshName()
        {
            textName.Text = string.Join(", ",
                _keyValueModelList.Where(v => v.Count != null && v.UnitModel != null)
                    .Select(v => $"{v.Count} {v.UnitModel!.GetNameOrKey()}"));
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            EditEntity?.FileSection.SetValue("Name", textName.Text);
        }

        private void valuesGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.ListObject is AiTaskForceKeyValueModel)
            {
                e.Row.Cells["Key"].Activation = Activation.NoEdit;
            }
        }

        private void LookupEntityValue(AiTaskForceKeyValueModel valueModel, UltraGridRow row)
        {
            if (EditEntity == null) return;
            var newUnit = SelectUnitForm.ExecuteSelect(ParentForm!, EditEntity.RulesRootModel,
                SelectTechnoTypes.Vehicles | SelectTechnoTypes.Infantry | SelectTechnoTypes.Aircrafts);
            if (newUnit != null)
            {
                valueModel.UnitKey = newUnit.EntityKey;
                row.Refresh();
            }
        }

        private void valuesGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Row.ListObject is AiTaskForceKeyValueModel valueModel)
            {
                if (e.Cell.Column.Key == "UnitPicture")
                {
                    e.Cell.CancelUpdate();
                    LookupEntityValue(valueModel, e.Cell.Row);
                }
            }
        }

        private void buttonCreateName_Click(object sender, EventArgs e)
        {
            RefreshName();
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {

        }

    }
}
