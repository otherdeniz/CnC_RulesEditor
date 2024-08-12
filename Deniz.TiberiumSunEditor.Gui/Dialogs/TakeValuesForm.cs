using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class TakeValuesForm : Form
    {
        private GameEntityModel _targetModel = null!;
        private UnitPickerControl? _selectedUnitPickerControl;

        public TakeValuesForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public static void ExecuteShow(Form parentForm, GameEntityModel targetModel)
        {
            using (var form = new TakeValuesForm())
            {
                form.LoadTargetModel(targetModel);
                form.LoadUnits();
                form.ShowDialog(parentForm);
            }
        }

        private void LoadTargetModel(GameEntityModel targetModel)
        {
            _targetModel = targetModel;
            labelKeyTarget.Text = targetModel.EntityKey;
            labelName.Text = targetModel.EntityName;
            pictureThumbnailTarget.Image = targetModel.Thumbnail?.Image
                                           ?? BitmapRepository.Instance.BlankImage;
        }

        private void LoadUnits()
        {
            unitsLayoutPanel.Visible = false;
            // clear controls
            var controlsToDispose = unitsLayoutPanel.Controls
                .OfType<Control>().ToList();
            unitsLayoutPanel.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            // add controls
            foreach (var entityModel in _targetModel.RulesRootModel.LookupEntities[_targetModel.EntityType])
            {
                if (entityModel.EntityKey == _targetModel.EntityKey)
                    continue;
                var unitPicker = new UnitPickerControl
                {
                    ReadonlyMode = true
                };
                unitPicker.UnitClick += (sender, args) => OnUnitClick(unitPicker, entityModel);
                unitPicker.LoadModel(entityModel);
                unitsLayoutPanel.Controls.Add(unitPicker);
            }
            unitsLayoutPanel.Visible = true;
        }

        private void LoadValueGrid(GameEntityModel sourceModel)
        {
            var keysWithValue = sourceModel.FileSection.KeyValues.Where(v => v.Value != string.Empty)
                .Union(_targetModel.FileSection.KeyValues.Where(v => v.Value != string.Empty))
                .Select(k => k.Key)
                .Distinct()
                .OrderBy(k => k)
                .ToList();
            valuesGrid.DataSource = keysWithValue.Select(k => new EntityValueCompareModel(k,
                    _targetModel.FileSection,
                    sourceModel.FileSection,
                    _targetModel.EntityValueList.FirstOrDefault(v => v.Key == k)?.Description ?? string.Empty))
                .ToList();
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridReadonlyCellBackColor;
            valuesGrid.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridReadonlyCellBackColor;
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["TargetValue"].Width = 120;
            valuesGrid.DisplayLayout.Bands[0].Columns["SourceValue"].Width = 120;
        }

        private void OnUnitClick(UnitPickerControl pickerControl, GameEntityModel entityModel)
        {
            if (_selectedUnitPickerControl != null)
            {
                _selectedUnitPickerControl.IsSelected = false;
            }

            _selectedUnitPickerControl = pickerControl;
            pickerControl.IsSelected = true;
            labelKeySource.Text = entityModel.EntityKey;
            labelNameSource.Text = entityModel.EntityName;
            pictureThumbnailSouce.Image = entityModel.Thumbnail?.Image
                                     ?? BitmapRepository.Instance.BlankImage;
            LoadValueGrid(entityModel);
            groupBoxSource.Visible = true;
            valuesGrid.Visible = true;
        }

        private void valuesGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Row.ListObject is EntityValueCompareModel valueModel)
            {
                e.Cell.CancelUpdate();
                if (e.Cell.Column.Key == "UseValueImage"
                    && valueModel.TargetValue != valueModel.SourceValue)
                {
                    valueModel.TargetValue = valueModel.SourceValue;
                    e.Cell.Refresh();
                    var valueCell = e.Cell.Row.Cells["TargetValue"];
                    valueCell.Refresh();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TakeValuesForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
