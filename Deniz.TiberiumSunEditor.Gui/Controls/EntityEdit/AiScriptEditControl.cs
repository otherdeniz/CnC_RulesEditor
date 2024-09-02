using Deniz.TiberiumSunEditor.Gui.Model;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    public partial class AiScriptEditControl : EntityEditBaseControl
    {
        private bool _doEvents;

        public AiScriptEditControl()
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
            LoadValuesGrid();
        }

        private void LoadValuesGrid()
        {
            var keyValueModelList = EntityModel!.FileSection.KeyValues
                .Select(k => (num: int.TryParse(k.Key, out var keyNumber) ? (int?)keyNumber : null, k))
                .Where(k => k.num.HasValue)
                .OrderBy(k => k.num)
                .Select(k => new AiScriptKeyValueModel(EntityModel!, k.k.Key, null))
                .ToList();
            valuesGrid.DataSource = keyValueModelList;
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].Width = 30;
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            EntityModel?.FileSection.SetValue("Name", textName.Text);
            RaiseNameChanged();
        }
    }
}
