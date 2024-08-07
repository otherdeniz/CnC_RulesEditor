using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class AddUnitForm : Form
    {
        private RulesRootModel _rulesRootModel = null!;
        private string _entityType = null!;
        private List<GameEntityModel> _existingEntityModels = null!;
        private UnitPickerControl? _selectedUnitPickerControl;
        private GameEntityModel? _selectedModel;

        public static AddUnitResult? ExecuteAddUnit(Form parentForm,
            RulesRootModel rulesRootModel,
            string entityType,
            List<GameEntityModel> existingEntityModels)
        {
            using (var form = new AddUnitForm())
            {
                form._rulesRootModel = rulesRootModel;
                form._entityType = entityType;
                form._existingEntityModels = existingEntityModels;
                form.LoadUnits();
                if (form.ShowDialog(parentForm) == DialogResult.OK
                    && form._selectedModel != null)
                {
                    return new AddUnitResult
                    {
                        Key = form._selectedModel.EntityKey,
                        Name = form.textBoxName.Text
                    };
                }
            }
            return null;
        }

        public AddUnitForm()
        {
            InitializeComponent();
        }

        private List<IniFileSection> GetArtSections()
        {
            if (_entityType == "BuildingTypes")
            {
                return CCGameRepository.Instance.ArtFile!.Sections
                    .Where(s => s.KeyValues.Any(k => k.Key == "Foundation")
                                && (s.KeyValues.Any(k => k.Key == "Cameo") || s.HeaderComments.Any()))
                    .ToList();
            }
            if (_entityType == "InfantryTypes")
            {
                return CCGameRepository.Instance.ArtFile!.Sections
                    .Where(s => s.KeyValues.Any(k => k.Key == "Crawls"))
                    .ToList();
            }
            if (_entityType == "VehicleTypes"
                || _entityType == "AircraftTypes")
            {
                return CCGameRepository.Instance.ArtFile!.Sections
                    .Where(s => s.KeyValues.Any(k => k.Key == "Voxel"))
                    .ToList();
            }
            return new List<IniFileSection>();
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
            foreach (var artSection in GetArtSections()
                         .Where(s => _existingEntityModels.All(m => m.EntityKey != s.SectionName))
                         .OrderBy(s => s.SectionName))
            {
                var entityModel =
                    new GameEntityModel(_rulesRootModel, _rulesRootModel, _entityType, artSection, artSection, new List<CategorizedValueDefinition>());
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

        private void OnUnitClick(UnitPickerControl pickerControl, GameEntityModel entityModel)
        {
            if (_selectedUnitPickerControl != null)
            {
                _selectedUnitPickerControl.IsSelected = false;
            }

            _selectedUnitPickerControl = pickerControl;
            pickerControl.IsSelected = true;
            _selectedModel = entityModel;
            labelKey.Text = entityModel.EntityKey;
            textBoxName.Text = entityModel.EntityName;
            pictureThumbnail.Image = entityModel.Thumbnail?.Image
                                     ?? BitmapRepository.Instance.BlankImage;
            valuesGrid.DataSource = entityModel.FileSection.KeyValues;
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            panelUnit.Visible = true;
            buttonOk.Enabled = true;
            textBoxName.Focus();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("Enter a name for the unit", "No name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxName.Focus();
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void valuesGrid_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {

        }
    }
}
