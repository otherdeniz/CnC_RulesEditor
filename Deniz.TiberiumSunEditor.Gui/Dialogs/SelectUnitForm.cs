using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class SelectUnitForm : Form
    {
        private const int PageSize = 250;
        private GameEntityModel? _selectedEntityModel;
        private RulesRootModel _rulesRootModel = null!;
        private SelectTechnoTypes _technoTypes;
        private int _currentPage = 1;
        private string _searchText = string.Empty;
        private int _filterTechLevel = -1;

        public SelectUnitForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public static GameEntityModel? ExecuteSelect(Form parentForm, RulesRootModel rulesRootModel, SelectTechnoTypes technoTypes)
        {
            using (var form = new SelectUnitForm())
            {
                parentForm.Cursor = Cursors.WaitCursor;
                form.LoadTechnos(rulesRootModel, technoTypes);
                parentForm.Cursor = Cursors.Default;
                if (form.ShowDialog(parentForm) == DialogResult.OK)
                {
                    return form._selectedEntityModel;
                }
            }
            return null;
        }

        private void LoadTechnos(RulesRootModel rulesRootModel, SelectTechnoTypes technoTypes)
        {
            _rulesRootModel = rulesRootModel;
            _technoTypes = technoTypes;
            LoadPage(1);
        }

        private void LoadPage(int pageNumber)
        {
            ultraPanelUnits.Visible = false;
            // clear controls
            var controlsToDispose = ultraPanelUnits.ClientArea.Controls
                .OfType<Control>().ToList();
            ultraPanelUnits.ClientArea.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            // load page
            _currentPage = pageNumber;
            var skip = PageSize * (pageNumber - 1);
            var take = PageSize;
            var totalCount = 0;
            if (_technoTypes.HasFlag(SelectTechnoTypes.Buildings))
            {
                AddTechnosGroup("Buildings", _rulesRootModel.BuildingEntities, ref skip, ref take, ref totalCount);
            }
            if (_technoTypes.HasFlag(SelectTechnoTypes.Infantry))
            {
                AddTechnosGroup("Infantry", _rulesRootModel.InfantryEntities, ref skip, ref take, ref totalCount);
            }
            if (_technoTypes.HasFlag(SelectTechnoTypes.Vehicles))
            {
                AddTechnosGroup("Vehicles", _rulesRootModel.VehicleEntities, ref skip, ref take, ref totalCount);
            }
            if (_technoTypes.HasFlag(SelectTechnoTypes.Aircrafts))
            {
                AddTechnosGroup("Aircrafts", _rulesRootModel.AircraftEntities, ref skip, ref take, ref totalCount);
            }
            ultraPanelUnits.Visible = true;
            toolStripLabelTotal.Text = $"{totalCount:#,##0} Items";
            if (totalCount > PageSize)
            {
                var fromItem = PageSize * (pageNumber - 1) + 1;
                var toItem = PageSize * pageNumber > totalCount
                    ? totalCount
                    : PageSize * pageNumber;
                toolStripLabelItem.Text = $"{fromItem:#,000} - {toItem:#,000}";
                toolStripButtonPrev.Enabled = pageNumber > 1;
                toolStripButtonNext.Enabled = PageSize * pageNumber < totalCount;
                toolStripLabelItem.Visible = true;
                toolStripButtonPrev.Visible = true;
                toolStripButtonNext.Visible = true;
            }
            else
            {
                toolStripLabelItem.Visible = false;
                toolStripButtonPrev.Visible = false;
                toolStripButtonNext.Visible = false;
            }
        }

        private void AddTechnosGroup(string title, List<GameEntityModel> entities, ref int skip, ref int take, ref int totalCount)
        {
            if (_searchText != string.Empty)
            {
                entities = entities.Where(e =>
                        e.EntityKey.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase)
                        || e.EntityName.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            }
            if (_filterTechLevel > -1)
            {
                entities = entities.Where(e =>
                        int.TryParse(e.FileSection.GetValue("TechLevel")?.Value, out var techLevel)
                        && techLevel >= _filterTechLevel)
                    .ToList();
            }
            totalCount += entities.Count;
            if (take == 0) return;
            if (entities.Count <= skip)
            {
                skip -= entities.Count;
                return;
            }
            var groupControl = new GroupBox
            {
                Text = title,
                Dock = DockStyle.Top,
            };
            ThemeManager.Instance.UseTheme(groupControl);
            var flowLayoutControl = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true,
                Margin = new Padding(3)
            };
            var takenEntites = entities.Skip(skip).Take(take).ToList();
            skip = 0;
            take -= takenEntites.Count;
            foreach (var entityModel in takenEntites)
            {
                var unitPicker = new UnitPickerControl
                {
                    ReadonlyMode = true
                };
                unitPicker.UnitClick += (sender, args) => OnUnitClick(entityModel);
                unitPicker.LoadModel(entityModel);
                flowLayoutControl.Controls.Add(unitPicker);
            }

            flowLayoutControl.SizeChanged += (sender, args) =>
                groupControl.Height = flowLayoutControl.Top + flowLayoutControl.Height + 3;

            groupControl.Controls.Add(flowLayoutControl);
            ultraPanelUnits.ClientArea.Controls.Add(groupControl);
            ultraPanelUnits.ClientArea.Controls.SetChildIndex(groupControl, 0);
        }

        private void OnUnitClick(GameEntityModel entityModel)
        {
            _selectedEntityModel = entityModel;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void toolStripButtonPrev_Click(object sender, EventArgs e)
        {
            LoadPage(_currentPage - 1);
        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            LoadPage(_currentPage + 1);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectUnitForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }

        private void buttonResetSearch_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = string.Empty;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

            if (textBoxSearch.Text.Length > 1)
            {
                _searchText = textBoxSearch.Text;
                LoadPage(_currentPage);
                buttonResetSearch.Enabled = true;
                return;
            }
            if (_searchText != string.Empty)
            {
                _searchText = string.Empty;
                LoadPage(_currentPage);
            }
            buttonResetSearch.Enabled = false;
        }

        private void numericTechLevel_ValueChanged(object sender, EventArgs e)
        {
            _filterTechLevel = Convert.ToInt32(numericTechLevel.Value);
            LoadPage(_currentPage);
        }

    }

    [Flags]
    public enum SelectTechnoTypes
    {
        Buildings = 1 << 0,
        Infantry = 1 << 1,
        Vehicles = 1 << 2,
        Aircrafts = 1 << 3
    }
}
