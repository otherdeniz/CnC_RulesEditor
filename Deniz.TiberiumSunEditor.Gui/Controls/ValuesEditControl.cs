using Infragistics.Win.UltraWinGrid;
using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class ValuesEditControl : UserControl
    {
        private IRootModel _rootModel = null!;
        private List<CommonValueModel>? _values;
        private CommonValueModel? _lookupValue;
        private UltraGridRow? _lookupValueRow;

        public ValuesEditControl()
        {
            InitializeComponent();
            lookupValue.Dock = DockStyle.Fill;
            lookupColor.Dock = DockStyle.Fill;
        }

        [DefaultValue(false)]
        public bool ReadonlyMode { get; set; }

        [DefaultValue(false)]
        [Browsable(false)]
        public bool ShowOnlyFavoriteValues { get; set; }

        [DefaultValue("")]
        [Browsable(false)]
        public string SearchText { get; set; } = "";

        public bool LoadValuesGrid(IRootModel rootModel, List<CommonValueModel> values)
        {
            _rootModel = rootModel;
            ButtonCloseValue_Click(this, EventArgs.Empty);
            var commonValuesList = values
                .Where(FilterValue)
                .OrderByDescending(v => v.Favorite)
                .ToList();
            valuesGrid.DataSource = commonValuesList;
            if (valuesGrid.DisplayLayout.Bands[0].SortedColumns.Count == 0)
            {
                valuesGrid.DisplayLayout.Bands[0].SortedColumns.Add("Category", false, true);
            }
            valuesGrid.DisplayLayout.Bands[0].Columns["Value"].MinWidth = 120;
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].Columns["DefaultValue"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["Value"].Width = 120;
            valuesGrid.DisplayLayout.Bands[0].Columns["DefaultValue"].Width = 120;
            if (ReadonlyMode)
            {
                valuesGrid.DisplayLayout.Bands[0].Columns["FavoriteImage"].Hidden = true;
                valuesGrid.DisplayLayout.Bands[0].Columns["UseDefaultImage"].Hidden = true;
            }
            if (_values != null)
            {
                ReInitialiseRows();
            }
            _values = values;
            return commonValuesList.Count > 0;
        }

        private bool FilterValue(CommonValueModel value)
        {
            if (SearchText == "")
            {
                return !ShowOnlyFavoriteValues || value.Favorite || ReadonlyMode;
            }
            return value.Key.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)
                   || value.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);
        }

        private void ReInitialiseRows()
        {
            foreach (var row in valuesGrid.Rows
                         .OfType<UltraGridGroupByRow>()
                         .SelectMany(r => r.Rows))
            {
                valuesGrid_InitializeRow(this, new InitializeRowEventArgs(row, true));
                row.Refresh();
            }
        }


        private void valuesGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (ReadonlyMode) return;
            ButtonCloseValue_Click(this, EventArgs.Empty);
            if (e.Cell.Row.ListObject is CommonValueModel valueModel)
            {
                if (e.Cell.Column.Key == "FavoriteImage")
                {
                    valueModel.Favorite = !valueModel.Favorite;
                    e.Cell.Refresh();
                }
                else if (e.Cell.Column.Key == "UseDefaultImage"
                         && valueModel.HasChanges
                         && !string.IsNullOrEmpty(valueModel.DefaultValue))
                {
                    valueModel.Value = valueModel.DefaultValue;
                    e.Cell.Refresh();
                    var valueCell = e.Cell.Row.Cells["Value"];
                    valueCell.Refresh();
                    valuesGrid_AfterCellUpdate(this, new CellEventArgs(valueCell));
                }
                else if (e.Cell.Column.Key == "Value")
                {
                    if (valueModel.ValueDefinition.ValueList != null
                        || valueModel.ValueDefinition.ValueType != null
                        || valueModel.ValueDefinition.LookupType != null)
                    {
                        e.Cell.CancelUpdate();
                        e.Cell.Appearance.BackColor = Color.LightSkyBlue;
                        LookupEntityValue(valueModel, e.Cell.Row, false);
                        if (_isRightClick
                            && valueModel.ValueDefinition.LookupType != null
                            && _rootModel.LookupEntities.ContainsKey(valueModel.ValueDefinition.LookupType))
                        {
                            QuickEditForm.ExecueShow(this.ParentForm!, _rootModel, valueModel.Value);
                        }
                        _isRightClick = false;
                    }
                    else if (valueModel.IsColorValue)
                    {
                        e.Cell.CancelUpdate();
                        e.Cell.Appearance.BackColor = Color.LightSkyBlue;
                        LookupEntityValue(valueModel, e.Cell.Row, true);
                    }
                    else if (valueModel.Value.IsYesNo() || valueModel.DefaultValue.IsYesNo())
                    {
                        if (valueModel.Value != "yes")
                        {
                            e.Cell.CancelUpdate();
                            valueModel.Value = "yes";
                            e.Cell.Refresh();
                            valuesGrid_AfterCellUpdate(this, new CellEventArgs(e.Cell));
                        }
                        else
                        {
                            e.Cell.CancelUpdate();
                            valueModel.Value = "no";
                            e.Cell.Refresh();
                            valuesGrid_AfterCellUpdate(this, new CellEventArgs(e.Cell));
                        }
                    }
                    else if (valueModel.Value.IsTrueFalse() || valueModel.DefaultValue.IsTrueFalse())
                    {
                        if (valueModel.Value != "true")
                        {
                            e.Cell.CancelUpdate();
                            valueModel.Value = "true";
                            e.Cell.Refresh();
                            valuesGrid_AfterCellUpdate(this, new CellEventArgs(e.Cell));
                        }
                        else
                        {
                            e.Cell.CancelUpdate();
                            valueModel.Value = "false";
                            e.Cell.Refresh();
                            valuesGrid_AfterCellUpdate(this, new CellEventArgs(e.Cell));
                        }
                    }
                }
            }
        }

        private bool _isRightClick;
        private void valuesGrid_MouseDown(object sender, MouseEventArgs e)
        {
            _isRightClick = e.Button == MouseButtons.Right;
        }

        private void valuesGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.ListObject is CommonValueModel valueModel)
            {
                e.Row.Cells["Value"].Appearance.BackColor = valueModel.HasChanges
                    ? Color.NavajoWhite
                    : Color.White;
                if (ReadonlyMode)
                {
                    e.Row.Activation = Activation.NoEdit;
                }
                else
                {
                    if (valueModel.ValueDefinition.LookupType != null
                        && _rootModel.LookupEntities.ContainsKey(valueModel.ValueDefinition.LookupType))
                    {
                        e.Row.Cells["Value"].ToolTipText = "Right-click to open Quick-Edit";
                    }
                    e.Row.Cells["Key"].Activation = Activation.NoEdit;
                    e.Row.Cells["DefaultValue"].Activation = Activation.NoEdit;
                    e.Row.Cells["Description"].Activation = Activation.NoEdit;
                }
            }
        }

        private void valuesGrid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell.Row.ListObject is CommonValueModel valueModel
                && e.Cell.Column.Key == "Value")
            {
                e.Cell.Appearance.BackColor = valueModel.HasChanges
                    ? Color.NavajoWhite
                    : Color.White;
                e.Cell.Row.Cells["UseDefaultImage"].Refresh();
            }
        }

        private void LookupEntityValue(CommonValueModel valueModel, UltraGridRow row, bool isColor)
        {
            _lookupValue = valueModel;
            _lookupValueRow = row;
            groupBoxValueChooser.Text = isColor ? "Color" : valueModel.ValueDefinition.LookupType ?? "Value";
            panelUseDefault.Visible = true;
            var defaultValueLabel = string.IsNullOrEmpty(valueModel.DefaultValue)
                ? "[empty]"
                : valueModel.DefaultValue;
            ButtonUseDefault.Text = $"Use Default: {defaultValueLabel}";
            if (isColor)
            {
                lookupColor.LoadValue(valueModel);
            }
            else
            {
                lookupValue.LoadValues(_rootModel, valueModel);
            }
            lookupValue.Visible = !isColor;
            lookupColor.Visible = isColor;
            panelValueChooser.Visible = true;
        }

        private void lookupValue_SelectedValueChanged(object sender, EventArgs e)
        {
            var valueCell = _lookupValueRow?.Cells["Value"];
            if (valueCell != null)
            {
                valueCell.Refresh();
                valuesGrid_AfterCellUpdate(this, new CellEventArgs(valueCell));
                ButtonCloseValue_Click(sender, e);
            }
        }

        private void lookupValue_RefreshEntityValue(object sender, EventArgs e)
        {
            var valueCell = _lookupValueRow?.Cells["Value"];
            if (valueCell != null)
            {
                valueCell.Refresh();
                valuesGrid_AfterCellUpdate(this, new CellEventArgs(valueCell));
            }
        }

        private void lookupColor_RefreshEntityValue(object sender, EventArgs e)
        {
            lookupValue_RefreshEntityValue(sender, e);
        }

        private void ButtonCloseValue_Click(object sender, EventArgs e)
        {
            var valueCell = _lookupValueRow?.Cells["Value"];
            if (valueCell != null)
            {
                valueCell.Appearance.BackColor = (valueCell.Row.ListObject is CommonValueModel valueModel)
                                                 && valueModel.HasChanges
                    ? Color.NavajoWhite
                    : Color.White;
            }
            panelValueChooser.Visible = false;
            _lookupValue = null;
            _lookupValueRow = null;
        }

        private void ButtonUseDefault_Click(object sender, EventArgs e)
        {
            if (_lookupValue == null) return;
            _lookupValue.Value = _lookupValue.DefaultValue;
            lookupValue_SelectedValueChanged(sender, e);
        }

    }
}
