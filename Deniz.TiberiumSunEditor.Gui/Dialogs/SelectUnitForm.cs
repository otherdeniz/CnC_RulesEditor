using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class SelectUnitForm : Form
    {
        private GameEntityModel? _selectedEntityModel;

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
            if (technoTypes.HasFlag(SelectTechnoTypes.Aircrafts))
            {
                AddTechnosGroup("Aircrafts", rulesRootModel.AircraftEntities);
            }
            if (technoTypes.HasFlag(SelectTechnoTypes.Vehicles))
            {
                AddTechnosGroup("Vehicles", rulesRootModel.VehicleEntities);
            }
            if (technoTypes.HasFlag(SelectTechnoTypes.Infantry))
            {
                AddTechnosGroup("Infantry", rulesRootModel.InfantryEntities);
            }
            if (technoTypes.HasFlag(SelectTechnoTypes.Buildings))
            {
                AddTechnosGroup("Buildings", rulesRootModel.BuildingEntities);
            }
        }

        private void AddTechnosGroup(string title, List<GameEntityModel> entities)
        {
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
            foreach (var entityModel in entities)
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
        }

        private void OnUnitClick(GameEntityModel entityModel)
        {
            _selectedEntityModel = entityModel;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectUnitForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
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
