using Deniz.TiberiumSunEditor.Gui.Model;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class BalancingToolForm : Form
    {
        private string? _selectedEntityTypes;

        public BalancingToolForm()
        {
            InitializeComponent();
        }

        public void LoadModel(RulesRootModel rulesRootModel)
        {
            sideBalanceLeft.LoadModel(rulesRootModel, 0);
            sideBalanceRight.LoadModel(rulesRootModel, 1);
            ultraComboTypes.SelectedIndex = 0;
        }

        private void CompareEntities()
        {
            if (_selectedEntityTypes == null) return;
            sideBalanceLeft.CompareEntities(_selectedEntityTypes,
                checkBoxStrength.Checked,
                checkBoxPoints.Checked,
                checkBoxSpeed.Checked,
                checkBoxPowerOutput.Checked,
                checkBoxPrimDamage.Checked,
                checkBoxPrimRof.Checked);
            sideBalanceRight.CompareEntities(_selectedEntityTypes,
                checkBoxStrength.Checked,
                checkBoxPoints.Checked,
                checkBoxSpeed.Checked,
                checkBoxPowerOutput.Checked,
                checkBoxPrimDamage.Checked,
                checkBoxPrimRof.Checked);
        }

        private void ultraComboTypes_ValueChanged(object sender, EventArgs e)
        {
            _selectedEntityTypes = (string)ultraComboTypes.SelectedItem.DataValue;
            if (_selectedEntityTypes == "BuildingTypes")
            {
                checkBoxSpeed.Checked = false;
                checkBoxSpeed.Enabled = false;
                checkBoxPowerOutput.Enabled = true;
            }
            else
            {
                checkBoxSpeed.Enabled = true;
                checkBoxPowerOutput.Checked = false;
                checkBoxPowerOutput.Enabled = false;
            }
            CompareEntities();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CompareEntities();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sideBalanceLeft_AfterEntityValueChanged(object sender, EventArgs e)
        {
            sideBalanceRight.RefreshGridCells();
        }

        private void sideBalanceRight_AfterEntityValueChanged(object sender, EventArgs e)
        {
            sideBalanceLeft.RefreshGridCells();
        }
    }
}
