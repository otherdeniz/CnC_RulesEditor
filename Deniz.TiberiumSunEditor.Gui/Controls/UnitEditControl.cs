using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Dialogs.Popup;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UnitEditControl : UserControl
    {
        private EntityValueModel? _lookupEntityValue;
        private UltraGridRow? _lookupEntityRow;
        private bool _canCopy;
        private bool _canTakeValues = true;
        private bool _readonlyMode;
        private bool _showModifications = true;
        private List<GameEntityModel>? _usedByEntityModels;
        private UsedByPopupForm? _usedByPopupForm;

        public UnitEditControl()
        {
            InitializeComponent();
            lookupValue.Dock = DockStyle.Fill;
            lookupColor.Dock = DockStyle.Fill;
        }

        public event EventHandler<EventArgs>? FavoriteClick;
        public event EventHandler<EventArgs>? UnitModificationsChanged;
        public event EventHandler<EntityCopyEventArgs>? UnitCreateCopy;

        public GameEntityModel? EntityModel { get; private set; }

        [DefaultValue(false)]
        public bool CanCopy
        {
            get => _canCopy;
            set
            {
                _canCopy = value;
                ButtonCopy.Visible = _canCopy;
            }
        }

        [DefaultValue(true)]
        public bool CanTakeValues
        {
            get => _canTakeValues;
            set
            {
                _canTakeValues = value;
                ButtonTakeValues.Visible = _canTakeValues;
            }
        }

        [DefaultValue(false)]
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                panelTopRight.Visible = !_readonlyMode;
                panelAddNew.Visible = !_readonlyMode;
                pictureBoxUnitPreview.Visible = !_readonlyMode;
            }
        }

        [DefaultValue(true)]
        public bool ShowModifications
        {
            get => _showModifications;
            set
            {
                _showModifications = value;
                labelModifications.Visible = value;
            }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        public bool ShowOnlyFavoriteValues { get; set; }

        [DefaultValue("")]
        [Browsable(false)]
        public string SearchText { get; set; } = "";

        public void ClearModel()
        {
            EntityModel = null;
        }

        public void LoadModel(GameEntityModel entityModel)
        {
            EntityModel = entityModel;
            labelName.Text = entityModel.EntityName;
            labelKey.Text = entityModel.EntityKey;
            if (EntityModel!.Thumbnail?.Kind == ThumbnailKind.Animation)
            {
                pictureThumbnail.Image = BitmapRepository.Instance.BlankImage;
                EntityModel!.Thumbnail.LoadAnimationAsync(img =>
                {
                    pictureThumbnail.Image = img;
                });
            }
            else
            {
                pictureThumbnail.Image = EntityModel!.Thumbnail?.Image
                                         ?? BitmapRepository.Instance.BlankImage;
            }
            ButtonCloseValue_Click(this, EventArgs.Empty);
            RefreshModifications();
            RefreshUsedByLabel();
            RefreshIsFavorite();
            LoadValueGrid();
            LoadAddNewKeys();
            LoadUnitPreview();
        }

        public void RefreshModifications()
        {
            if (EntityModel == null) return;
            labelModifications.Text = $"{EntityModel.ModificationCount} Modifications";
            labelModifications.ForeColor = EntityModel.ModificationCount == 0
                ? Color.Black
                : Color.Red;
        }

        public void RefreshIsFavorite()
        {
            if (EntityModel == null) return;
            pictureBoxFavorite.Image = EntityModel.Favorite
                ? ImageListComponent.Instance.Favorite48.Images[1]
                : ImageListComponent.Instance.Favorite48.Images[0];
        }

        private void RefreshUsedByLabel()
        {
            if (EntityModel == null) return;
            labelUsedBy.Visible = false;
            _usedByEntityModels = EntityModel.RootModel.LookupEntities.Values.SelectMany(l => l)
                .Where(e => e.FileSection.KeyValues.Any(k =>
                    k.Value.Split(",").Any(v => v == EntityModel.EntityKey)))
                .ToList();
            if (_usedByEntityModels.Count > 0)
            {
                labelUsedBy.Text = $"Used by: {_usedByEntityModels.Count}";
                labelUsedBy.Visible = true;
            }
        }

        private void LoadUnitPreview()
        {
            pictureBoxUnitPreview.Image = null;
            if (EntityModel!.EntityType == "InfantryTypes" && !_readonlyMode)
            {
                // easter egg :)
                var synchronizationContext = SynchronizationContext.Current;
                Task.Run(() =>
                {
                    var entityKey = EntityModel!.FileSection.GetValue("Image")?.Value;
                    if (string.IsNullOrEmpty(entityKey) || entityKey == "null")
                    {
                        entityKey = EntityModel!.EntityKey;
                    }
                    var animatedImage = CCGameRepository.Instance.GetUnitPreviewAnimation(entityKey);
                    if (animatedImage != null)
                    {
                        synchronizationContext?.Post(state =>
                        {
                            pictureBoxUnitPreview.Image = state as Image;
                        }, animatedImage);
                    }
                });
            }
            else
            {
                pictureBoxUnitPreview.Visible = false;
            }
        }

        private void LoadValueGrid()
        {
            valuesGrid.DataSource = null;
            valuesGrid.DataSource = EntityModel!.EntityValueList
                .Where(FilterValue)
                .OrderByDescending(v => v.Favorite)
                .ToList();
            if (valuesGrid.DisplayLayout.Bands[0].SortedColumns.Count == 0)
            {
                valuesGrid.DisplayLayout.Bands[0].SortedColumns.Add("Category", false, true);
            }
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].Columns["NormalValue"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].Columns["DefaultValue"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = Color.FromArgb(230, 230, 230);
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["Value"].Width = 120;
            valuesGrid.DisplayLayout.Bands[0].Columns["NormalValue"].Width = 120;
            if (ReadonlyMode)
            {
                valuesGrid.DisplayLayout.Bands[0].Columns["FavoriteImage"].Hidden = true;
                valuesGrid.DisplayLayout.Bands[0].Columns["UseNormalImage"].Hidden = true;
            }
        }

        private bool FilterValue(EntityValueModel value)
        {
            if (SearchText == "")
            {
                return (!ShowOnlyFavoriteValues || value.Favorite)
                       && (!ReadonlyMode || value.FileKeyValue != null);
            }
            return value.Key.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)
                   || value.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);
        }

        private void LoadAddNewKeys()
        {
            if (EntityModel == null || ReadonlyMode) return;
            var allSectionKeys = EntityModel.RootModel.LookupItems
                .Where(l => l.EntityType == EntityModel.EntityType)
                .Select(l => l.Key)
                .ToList();
            var allValueKeys = allSectionKeys.SelectMany(s =>
                    EntityModel.RootModel.DefaultFile.GetSection(s)?.KeyValues ??
                    new List<IniFileLineKeyValue>())
                .GroupBy(k => k.Key)
                .OrderBy(k => k.Key)
                .Where(k => EntityModel.EntityValueList.All(v => v.Key != k.Key))
                .ToList();
            ultraComboAddValue.Items.Clear();
            ultraComboAddValue.Items.AddRange(allValueKeys.Select(k => new ValueListItem(k, k.Key)).ToArray());
        }

        private void LookupEntityValue(EntityValueModel valueModel, UltraGridRow row, bool isColor)
        {
            _lookupEntityValue = valueModel;
            _lookupEntityRow = row;
            groupBoxValueChooser.Text = isColor ? "Color" : valueModel.ValueDefinition.LookupType ?? "Value";
            if (valueModel.DefaultValue != valueModel.NormalValue)
            {
                panelUseDefault.Visible = true;
                var defaultValueLabel = string.IsNullOrEmpty(valueModel.DefaultValue)
                    ? "[empty]"
                    : valueModel.DefaultValue;
                ButtonUseDefault.Text = $"Use Default: {defaultValueLabel}";
            }
            else
            {
                panelUseDefault.Visible = false;
            }
            if (isColor)
            {
                lookupColor.LoadValue(valueModel);
            }
            else
            {
                lookupValue.LoadValues(EntityModel!.RootModel, valueModel, EntityModel.EntityType);
            }
            lookupValue.Visible = !isColor;
            lookupColor.Visible = isColor;
            panelValueChooser.Visible = true;
        }

        private void pictureBoxFavorite_Click(object sender, EventArgs e)
        {
            if (EntityModel == null) return;
            EntityModel.Favorite = !EntityModel.Favorite;
            RefreshIsFavorite();
            FavoriteClick?.Invoke(this, EventArgs.Empty);
        }

        private void valuesGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (ReadonlyMode) return;
            ButtonCloseValue_Click(this, EventArgs.Empty);
            if (e.Cell.Row.ListObject is EntityValueModel valueModel)
            {
                if (e.Cell.Column.Key == "FavoriteImage")
                {
                    valueModel.Favorite = !valueModel.Favorite;
                    e.Cell.Refresh();
                }
                else if (e.Cell.Column.Key == "UseNormalImage"
                         && valueModel.Value != valueModel.NormalValue)
                {
                    valueModel.Value = valueModel.NormalValue;
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
                            && EntityModel!.RootModel.LookupEntities.ContainsKey(valueModel.ValueDefinition.LookupType))
                        {
                            QuickEditForm.ExecueShow(this.ParentForm!, EntityModel!.RootModel, valueModel.Value);
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
            if (e.Row.ListObject is EntityValueModel valueModel)
            {
                e.Row.Cells["Value"].Appearance.BackColor = !valueModel.IsModified
                    ? Color.White
                    : Color.NavajoWhite;
                if (ReadonlyMode)
                {
                    e.Row.Activation = Activation.NoEdit;
                }
                else
                {
                    if (valueModel.ValueDefinition.DetectTypeAtRuntime)
                    {
                        var detectByKey = !string.IsNullOrEmpty(valueModel.Value)
                            ? valueModel.Value.Split(",").First()
                            : !string.IsNullOrEmpty(valueModel.DefaultValue)
                                ? valueModel.DefaultValue.Split(",").First()
                                : null;
                        var detectedLookupType = detectByKey != null
                            ? EntityModel!.RootModel.DetectLookupType(detectByKey)
                            : null;
                        if (detectedLookupType != null)
                        {
                            valueModel.ValueDefinition = new UnitValueDefinition
                            {
                                Key = valueModel.ValueDefinition.Key,
                                LookupType = detectedLookupType,
                                MultipleValues = valueModel.Value.Contains(",")
                                                 || valueModel.DefaultValue.Contains(",")
                            };
                        }
                    }
                    if (valueModel.ValueDefinition.LookupType != null
                        && EntityModel!.RootModel.LookupEntities.ContainsKey(valueModel.ValueDefinition.LookupType))
                    {
                        e.Row.Cells["Value"].ToolTipText = "Right-click to open Quick-Edit";
                    }
                    e.Row.Cells["Key"].Activation = Activation.NoEdit;
                    e.Row.Cells["NormalValue"].Activation = Activation.NoEdit;
                    e.Row.Cells["DefaultValue"].Activation = Activation.NoEdit;
                    e.Row.Cells["Description"].Activation = Activation.NoEdit;
                }
            }
        }

        private void valuesGrid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell.Row.ListObject is EntityValueModel valueModel
                && e.Cell.Column.Key == "Value")
            {
                e.Cell.Appearance.BackColor = !valueModel.IsModified
                    ? Color.White
                    : Color.NavajoWhite;
                e.Cell.Row.Cells["UseNormalImage"].Refresh();
                RefreshModifications();
                UnitModificationsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void lookupValue_RefreshEntityValue(object sender, EventArgs e)
        {
            var valueCell = _lookupEntityRow?.Cells["Value"];
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

        private void lookupValue_SelectedValueChanged(object sender, EventArgs e)
        {
            var valueCell = _lookupEntityRow?.Cells["Value"];
            if (valueCell != null)
            {
                valueCell.Refresh();
                valuesGrid_AfterCellUpdate(this, new CellEventArgs(valueCell));
                ButtonCloseValue_Click(sender, e);
            }
        }

        private void ButtonCloseValue_Click(object sender, EventArgs e)
        {
            var valueCell = _lookupEntityRow?.Cells["Value"];
            if (valueCell != null)
            {
                valueCell.Appearance.BackColor = (valueCell.Row.ListObject is EntityValueModel valueModel)
                                                 && valueModel.IsModified
                    ? Color.NavajoWhite
                    : Color.White;
            }
            panelValueChooser.Visible = false;
            _lookupEntityValue = null;
            _lookupEntityRow = null;
        }

        private void ButtonUseDefault_Click(object sender, EventArgs e)
        {
            if (_lookupEntityValue == null) return;
            _lookupEntityValue.Value = _lookupEntityValue.DefaultValue;
            lookupValue_SelectedValueChanged(sender, e);
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            if (EntityModel == null) return;
            using (var copyForm = new CreateCopyForm())
            {
                copyForm.LoadModel(EntityModel.RootModel);
                copyForm.LabelKey.Text = $"{EntityModel.EntityKey} ({EntityModel.EntityName})";
                if (copyForm.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    UnitCreateCopy?.Invoke(this,
                        new EntityCopyEventArgs(EntityModel!, copyForm.TextNewKey.Text, copyForm.TextNewName.Text));
                }
            }
        }

        private void ButtonTakeValues_Click(object sender, EventArgs e)
        {
            if (EntityModel == null) return;
            TakeValuesForm.ExecuteShow(this.ParentForm!, EntityModel);
            LoadValueGrid();
            RefreshModifications();
            UnitModificationsChanged?.Invoke(this, EventArgs.Empty);
            valuesGrid.Focus();
        }

        private void ultraComboAddValue_ValueChanged(object sender, EventArgs e)
        {
            if (EntityModel == null) return;
            if (ultraComboAddValue.SelectedItem?.DataValue is IGrouping<string, IniFileLineKeyValue> selectedKey)
            {
                EntityModel.EntityValueList.Add(new EntityValueModel(
                    EntityModel,
                    "Other values",
                    EntityModel.FileSection,
                    EntityModel.DefaultSection,
                    selectedKey.Key,
                    new UnitValueDefinition
                    {
                        Key = selectedKey.Key,
                        Description = $"Existing values: {string.Join(",", selectedKey.Select(v => v.Value).Distinct())}"
                    }));
                LoadValueGrid();
                LoadAddNewKeys();
                valuesGrid.Focus();
            }
        }

        private void labelUsedBy_MouseEnter(object sender, EventArgs e)
        {
            if (_usedByEntityModels == null || EntityModel == null) return;
            _usedByPopupForm = new UsedByPopupForm
            {
                Location = labelUsedBy.PointToScreen(new Point(0, labelUsedBy.Height + 5))
            };
            _usedByPopupForm.LoadUsedByEntities(_usedByEntityModels, EntityModel.EntityKey);
            _usedByPopupForm.Show(ParentForm);
        }

        private void labelUsedBy_MouseLeave(object sender, EventArgs e)
        {
            _usedByPopupForm?.Close();
        }

    }

    public class EntityCopyEventArgs : EventArgs
    {
        public EntityCopyEventArgs(GameEntityModel sourceEntityModel, string newKey, string newName)
        {
            SourceEntityModel = sourceEntityModel;
            NewKey = newKey;
            NewName = newName;
        }

        public GameEntityModel SourceEntityModel { get; }

        public string NewKey { get; }

        public string NewName { get; }
    }
}
