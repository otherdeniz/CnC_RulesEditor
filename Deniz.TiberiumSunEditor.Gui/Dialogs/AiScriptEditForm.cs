using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class AiScriptEditForm : Form
    {
        private AiRootModel _aiRootModel = null!;
        private AiScriptKeyValueModel _editItem = null!;
        private string? _actionValue;
        private string? _parameterValue;
        private string? _commentValue;
        private bool _doEvents;

        public AiScriptEditForm()
        {
            InitializeComponent();
            panelParamNumber.Dock = DockStyle.Fill;
            paramValuesGrid.Dock = DockStyle.Fill;
            panelParamBuilding.Dock = DockStyle.Fill;
            ThemeManager.Instance.UseTheme(this);
        }

        public static bool ExecuteEdit(Form parentForm, AiRootModel aiRootModel, AiScriptKeyValueModel editItem)
        {
            using (var form = new AiScriptEditForm())
            {
                form.LoadEditItem(aiRootModel, editItem);
                if (form.ShowDialog(parentForm) == DialogResult.OK)
                {
                    form.SaveEditItem();
                    return true;
                }
            }
            return false;
        }

        private void LoadEditItem(AiRootModel aiRootModel, AiScriptKeyValueModel editItem)
        {
            _aiRootModel = aiRootModel;
            _editItem = editItem;
            _actionValue = _editItem.ActionValue;
            _parameterValue = _editItem.ParameterValue;
            _commentValue = _editItem.CommentValue;
            AistructureFile.Instance.ScriptBuildingParameter2.ForEach(p =>
                ultraComboBuildingTarget.Items.Add(new ValueListItem(p)));
            LoadActionsGrid(_actionValue);
        }

        private void SaveEditItem()
        {
            _editItem.ActionValue = _actionValue;
            _editItem.ParameterValue = _parameterValue;
            _editItem.WriteValue();
        }

        private void LoadActionsGrid(string? selectedAction)
        {
            _doEvents = false;
            var gameKey = _aiRootModel.FileType.GameDefinition.GameKey;
            var actionDefinitionList = AistructureFile.Instance.GetScriptActionsFiltered(gameKey).ToList();
            actionsGrid.DataSource = actionDefinitionList;
            actionsGrid.DisplayLayout.Bands[0].Columns["Number"].PerformAutoResize();
            actionsGrid.DisplayLayout.Bands[0].Columns["ActionName"].PerformAutoResize();
            actionsGrid.DisplayLayout.Bands[0].Columns["ParameterName"].PerformAutoResize();
            actionsGrid.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
            actionsGrid.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.FontData.SizeInPoints = this.Font.Size - 1;
            if (selectedAction != null)
            {
                var selectedIndex = actionDefinitionList!.FindIndex(v => v.Number == selectedAction);
                if (selectedIndex > -1)
                {
                    var selectedRow = actionsGrid.Rows[selectedIndex];
                    actionsGrid.Selected.Rows.Add(selectedRow);
                    actionsGrid.ActiveRowScrollRegion.ScrollRowIntoView(selectedRow);
                    OnActionSelect(actionDefinitionList[selectedIndex]);
                }
            }
            _doEvents = true;
        }

        private void OnActionSelect(ScriptActionDefinition scriptActionDefinition)
        {
            _doEvents = false;
            panelParamNumber.Visible = false;
            paramValuesGrid.Visible = false;
            panelParamBuilding.Visible = false;
            buttonOk.Enabled = false;
            switch (scriptActionDefinition.ParameterValue)
            {
                case "n":
                    if (!int.TryParse(_parameterValue, out var parameterNumber))
                    {
                        parameterNumber = 0;
                        _parameterValue = parameterNumber.ToString("0");
                    }
                    numericParamValue.Value = parameterNumber;
                    panelParamNumber.Visible = true;
                    buttonOk.Enabled = true;
                    break;
                case "v":
                    LoadParamValuesGrid(scriptActionDefinition, _parameterValue);
                    paramValuesGrid.Visible = true;
                    break;
                case "BuildingTypes":
                    smallEntityBuilding.EntityModel = null;
                    ultraComboBuildingTarget.SelectedIndex = -1;
                    if (int.TryParse(_parameterValue, out var buildingParameterNumber))
                    {
                        var resolvedBuildingParameter = AiScriptKeyValueModel.ResolveBuildingParameter(_aiRootModel.RulesModel,
                            buildingParameterNumber, _commentValue);
                        if (resolvedBuildingParameter.Building != null
                            && resolvedBuildingParameter.Parameter2 != null)
                        {
                            smallEntityBuilding.EntityModel = resolvedBuildingParameter.Building;
                            ultraComboBuildingTarget.SelectedIndex =
                                AistructureFile.Instance.ScriptBuildingParameter2.IndexOf(resolvedBuildingParameter.Parameter2);
                        }
                    }
                    panelParamBuilding.Visible = true;
                    break;
            }
            _doEvents = true;
        }

        private void LoadParamValuesGrid(ScriptActionDefinition scriptActionDefinition, string? selectedValue)
        {
            _doEvents = false;
            var gameKey = _aiRootModel.FileType.GameDefinition.GameKey;
            var valueDefinitionList = scriptActionDefinition.GetParameterValuesFiltered(gameKey).ToList();
            paramValuesGrid.DataSource = valueDefinitionList;
            paramValuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            if (selectedValue != null)
            {
                var selectedIndex = valueDefinitionList!.FindIndex(v => v.Value == selectedValue);
                if (selectedIndex > -1)
                {
                    var selectedRow = paramValuesGrid.Rows[selectedIndex];
                    paramValuesGrid.Selected.Rows.Add(selectedRow);
                    paramValuesGrid.ActiveRowScrollRegion.ScrollRowIntoView(selectedRow);
                }
            }
            _doEvents = true;
        }

        private void actionsGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (!_doEvents || actionsGrid.Selected.Rows.Count == 0) return;
            if (actionsGrid.Selected.Rows[0].ListObject is ScriptActionDefinition actionDefinition)
            {
                _actionValue = actionDefinition.Number;
                if (actionDefinition.ParameterValue == "0")
                {
                    _parameterValue = "0";
                    buttonOk_Click(this, EventArgs.Empty);
                    return;
                }
                _parameterValue = null;
                _commentValue = null;
                OnActionSelect(actionDefinition);
            }
        }

        private void paramValuesGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (!_doEvents || paramValuesGrid.Selected.Rows.Count == 0) return;
            if (paramValuesGrid.Selected.Rows[0].ListObject is ScriptParameterValueDefinition parameterValueDefinition)
            {
                _parameterValue = parameterValueDefinition.Value;
                buttonOk_Click(this, EventArgs.Empty);
            }
        }

        private void numericParamValue_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _parameterValue = numericParamValue.Value.ToString("0");
        }

        private void SetBuildingParameter()
        {
            if (smallEntityBuilding.EntityModel != null
                && ultraComboBuildingTarget.SelectedItem?.DataValue is ScriptBuildingParameter2Definition
                    buildingParameter2)
            {
                _parameterValue =
                    (smallEntityBuilding.EntityModel.TypesIndex + buildingParameter2.AddValue)?.ToString();
            }
            else
            {
                _parameterValue = null;
            }
            buttonOk.Enabled = _parameterValue != null;
        }

        private void smallEntityBuilding_Click(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            var newBuilding = SelectUnitForm.ExecuteSelect(this, _aiRootModel.RulesModel, SelectTechnoTypes.Buildings);
            if (newBuilding != null)
            {
                smallEntityBuilding.EntityModel = newBuilding;
            }
            SetBuildingParameter();
        }

        private void ultraComboBuildingTarget_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            SetBuildingParameter();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AiScriptEditForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }

    }
}
