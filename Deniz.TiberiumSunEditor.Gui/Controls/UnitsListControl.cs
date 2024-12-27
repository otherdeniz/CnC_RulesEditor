using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UnitsListControl : UserControl
    {
        private const int PageSize = 250;
        private List<GameEntityModel> _entities = null!;
        private List<GameEntityModel> _orderedEntities = null!;
        private FilterModel? _filter;
        private readonly List<UnitPickerControl> _unitPickerControls = new();
        private UnitPickerControl? _selectedUnitPickerControl;
        private bool _readonlyMode;
        private bool _showOnlyFavoriteValues;
        private bool _showOnlyModifiedCheckbox = true;
        private int _currentPage = 1;
        private bool _showOnlyFavoriteUnits;
        private bool _doEvents;
        private List<EntityGroupSetting> _entityGroups = new();
        private Dictionary<string, EntityGroupSetting> _keyEntityGroups = new();
        private readonly Dictionary<string, UnitPickerGroupControl> _groupControls = new();

        public UnitsListControl()
        {
            InitializeComponent();
            toolStripAdd.Visible = false;
            unitEdit.Dock = DockStyle.Fill;
            panelContent.Dock = DockStyle.Fill;
        }

        public event EventHandler<EntityCopyEventArgs>? UnitCreateCopy;
        public event EventHandler<EntityDeleteEventArgs>? UnitDelete;
        public event EventHandler<EventArgs>? UnitAddUnlisted;
        public event EventHandler<EventArgs>? UnitAddEmpty;

        [DefaultValue(true)]
        public bool OrderByThumbnail { get; set; } = true;

        [DefaultValue(false)]
        public bool CanAddUnlisted { get; set; }

        [DefaultValue(false)]
        public bool CanAddEmpty { get; set; }

        [DefaultValue(true)]
        public bool CanTakeValues
        {
            get => unitEdit.CanTakeValues;
            set => unitEdit.CanTakeValues = value;
        }

        [DefaultValue(false)]
        public bool CanCopy
        {
            get => unitEdit.CanCopy;
            set => unitEdit.CanCopy = value;
        }

        [DefaultValue(true)]
        public bool ShowUsedBy
        {
            get => unitEdit.ShowUsedBy;
            set => unitEdit.ShowUsedBy = value;
        }

        [DefaultValue(false)]
        public bool UseValueNameColumn
        {
            get => unitEdit.UseValueNameColumn;
            set => unitEdit.UseValueNameColumn = value;
        }

        [DefaultValue(false)]
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                unitEdit.ReadonlyMode = _readonlyMode;
                checkBoxOnlyModified.Visible = !_readonlyMode
                                               && !_showOnlyFavoriteUnits
                                               && _showOnlyModifiedCheckbox;
            }
        }

        [DefaultValue(true)]
        public bool ShowOnlyModifiedCheckbox
        {
            get => _showOnlyModifiedCheckbox;
            set
            {
                _showOnlyModifiedCheckbox = value;
                checkBoxOnlyModified.Visible = !_readonlyMode 
                                               && !_showOnlyFavoriteUnits 
                                               && _showOnlyModifiedCheckbox;
            }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        public bool ShowOnlyFavoriteValues
        {
            get => _showOnlyFavoriteValues;
            set
            {
                _showOnlyFavoriteValues = value;
                unitEdit.ShowOnlyFavoriteValues = value;
            }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        public bool ShowOnlyFavoriteUnits
        {
            get => _showOnlyFavoriteUnits;
            set
            {
                _showOnlyFavoriteUnits = value;
                checkBoxOnlyModified.Visible = !_readonlyMode
                                               && !_showOnlyFavoriteUnits
                                               && _showOnlyModifiedCheckbox;
            }
        }

        [DefaultValue("")]
        [Browsable(false)]
        public string SearchText { get; set; } = "";

        public bool LoadModel(List<GameEntityModel> entities, 
            FilterModel? filter = null)
        {
            _entities = entities;
            _filter = filter;
            _doEvents = false;
            checkBoxOnlyModified.Checked = false;
            _doEvents = true;
            return LoadUnits();
        }

        public void SelectKey(string key)
        {
            var unitPicker = _unitPickerControls.FirstOrDefault(c => c.EntityModel?.EntityKey == key);
            if (unitPicker != null)
            {
                OnUnitClick(unitPicker, unitPicker.EntityModel!);
            }
        }

        private bool LoadUnits()
        {
            if (_entities.Any())
            {
                var entityType = _entities[0].EntityType;
                _entityGroups = UserSettingsFile.Instance.EntityGroups
                    .Where(g => g.EntityType == entityType)
                    .OrderBy(g => g.GroupName)
                    .ToList();
                _keyEntityGroups = _entityGroups.SelectMany(g => g.Keys.Select(k => new { Key = k, Group = g }))
                    .ToDictionary(k => k.Key, v => v.Group);
            }
            else
            {
                _entityGroups = new List<EntityGroupSetting>();
                _keyEntityGroups = new Dictionary<string, EntityGroupSetting>();
            }
            var filteredList = _entities.Where(FilterValue).ToList();
            var orderedList = OrderByThumbnail
                ? filteredList
                    .OrderBy(OrderByGroup)
                    .ThenBy(e => e.Favorite ? 0 : 1)
                    .ThenBy(OrderByOwner)
                    .ThenBy(e => e.Thumbnail == null ? 1 : 0)
                    .ThenBy(e => e.EntityKey)
                : filteredList
                    .OrderBy(OrderByGroup)
                    .ThenBy(e => e.Favorite ? 0 : 1)
                    .ThenBy(e => e.EntityKey);
            _orderedEntities = orderedList.ToList();
            LoadPage(1);
            return filteredList.Any();
        }

        private void LoadPage(int pageNumber)
        {
            _currentPage = pageNumber;
            _selectedUnitPickerControl = null;
            _unitPickerControls.Clear();
            unitEdit.ClearModel();
            unitEdit.Visible = false;
            panelContent.Visible = false;
            ultraPanelScroll.Visible = false;
            _groupControls.Clear();
            // clear controls
            var controlsToDispose = unitsLayoutPanel.Controls
                .OfType<Control>().ToList();
            unitsLayoutPanel.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            var groupsToDispose = ultraPanelScroll.ClientArea.Controls
                .OfType<UnitPickerGroupControl>().ToList();
            groupsToDispose.ForEach(c =>
            {
                ultraPanelScroll.ClientArea.Controls.Remove(c);
                c.Dispose();
            });
            // add controls
            foreach (var entityModel in _orderedEntities.Skip(PageSize * (pageNumber - 1)).Take(PageSize))
            {
                var unitPicker = new UnitPickerControl
                {
                    ReadonlyMode = _readonlyMode
                };
                unitPicker.FavoriteClick += (sender, args) => OnFavoriteClick(unitPicker, entityModel);
                unitPicker.UnitClick += (sender, args) => OnUnitClick(unitPicker, entityModel);
                unitPicker.GroupChanged += (sender, args) => LoadUnits();
                unitPicker.LoadModel(entityModel);
                if (_keyEntityGroups.TryGetValue(entityModel.EntityKey, out var entityGroupSetting))
                {
                    if (!_groupControls.TryGetValue(entityGroupSetting.GroupName, out var groupControl))
                    {
                        groupControl = new UnitPickerGroupControl
                        {
                            Dock = DockStyle.Top
                        };
                        groupControl.InitGroup(entityGroupSetting);
                        _groupControls.Add(entityGroupSetting.GroupName, groupControl);
                        ultraPanelScroll.ClientArea.Controls.Add(groupControl);
                    }
                    groupControl.AddPickerControl(unitPicker);
                }
                else
                {
                    unitsLayoutPanel.Controls.Add(unitPicker);
                }
                unitPicker.InitGroups(_entityGroups, entityGroupSetting);
                _unitPickerControls.Add(unitPicker);
            }
            //order custom groups
            foreach (var groupControl in ultraPanelScroll.ClientArea.Controls
                         .OfType<UnitPickerGroupControl>()
                         .OrderBy(g => g.EntityGroupSetting.GroupName))
            {
                ultraPanelScroll.ClientArea.Controls.SetChildIndex(groupControl, 1);
            }
            toolStripLabelTotal.Text = $"{_orderedEntities.Count:#,##0} Items";
            if (_orderedEntities.Count > PageSize)
            {
                var fromItem = PageSize * (pageNumber - 1) + 1;
                var toItem = PageSize * pageNumber > _orderedEntities.Count
                    ? _orderedEntities.Count
                    : PageSize * pageNumber;
                toolStripLabelItem.Text = $"{fromItem:#,000} - {toItem:#,000}";
                toolStripButtonPrev.Enabled = pageNumber > 1;
                toolStripButtonNext.Enabled = PageSize * pageNumber < _orderedEntities.Count;
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
            ultraPanelScroll.Visible = true;
        }

        public void RefreshInfoNumber(string entityKey)
        {
            _unitPickerControls.FirstOrDefault(c => c.UnitKey == entityKey)
                ?.RefreshInfoNumber();
        }

        private bool FilterValue(GameEntityModel entityModel)
        {
            if (_filter != null)
            {
                if (!string.IsNullOrEmpty(_filter.FilterByHouse) &&
                    !entityModel.IsBuildableByHouse(_filter.FilterByHouse))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(_filter.FieldKey))
                {
                    var entityValue = entityModel.FileSection.GetValue(_filter.FieldKey);
                    switch (_filter.Comparison)
                    {
                        case FilterComparison.Contains:
                            if (_filter.Value != string.Empty
                                && entityValue?.Value.Contains(_filter.Value,
                                    StringComparison.InvariantCultureIgnoreCase) != true)
                            {
                                return false;
                            }
                            break;
                        case FilterComparison.GreaterThan:
                            if (decimal.TryParse(_filter.Value, out var filterGreater)
                                && (entityValue != null
                                    && decimal.TryParse(entityValue.Value, out var valueGreater)
                                    && valueGreater > filterGreater) != true)
                            {
                                return false;
                            }
                            break;
                        case FilterComparison.LesserThan:
                            if (decimal.TryParse(_filter.Value, out var filterLesser)
                                && (entityValue != null
                                    && decimal.TryParse(entityValue.Value, out var valueLesser)
                                    && valueLesser < filterLesser) != true)
                            {
                                return false;
                            }
                            break;
                        case FilterComparison.IsYes:
                            return entityValue != null
                                && (entityValue.Value.Equals("yes", StringComparison.InvariantCultureIgnoreCase)
                                    || entityValue.Value.Equals("true", StringComparison.InvariantCultureIgnoreCase));
                        case FilterComparison.IsNo:
                            return entityValue != null
                                   && (entityValue.Value.Equals("no", StringComparison.InvariantCultureIgnoreCase)
                                       || entityValue.Value.Equals("false", StringComparison.InvariantCultureIgnoreCase));
                        case FilterComparison.IsEmpty:
                            return string.IsNullOrEmpty(entityValue?.Value);
                        case FilterComparison.IsNotEmpty:
                            return !string.IsNullOrEmpty(entityValue?.Value);
                    }
                }
            }

            if (SearchText != "")
            {
                return entityModel.EntityKey.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)
                       || entityModel.EntityName.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);
            }

            return checkBoxOnlyModified.Checked
                ? entityModel.ModificationCount > 0
                : !ShowOnlyFavoriteUnits || entityModel.Favorite || ReadonlyMode;
        }

        private int OrderByOwner(GameEntityModel entityModel)
        {
            var entitySides = entityModel.Sides;
            if (entitySides.Any())
            {
                for (int i = 0; i < entityModel.RulesRootModel.FileType.GameDefinition.Sides.Count; i++)
                {
                    var gameSide = entityModel.RulesRootModel.FileType.GameDefinition.Sides[i];
                    if (entitySides.Contains(gameSide.Name))
                    {
                        return i + 1;
                    }
                }
            }
            return 9;
        }

        private string OrderByGroup(GameEntityModel entityModel)
        {
            return _keyEntityGroups.TryGetValue(entityModel.EntityKey, out var entityGroup)
                ? entityGroup.GroupName
                : "ZZ";
        }

        private void OnFavoriteClick(UnitPickerControl pickerControl, GameEntityModel entityModel)
        {
            if (unitEdit.EntityModel?.EntityKey == entityModel.EntityKey)
            {
                unitEdit.RefreshIsFavorite();
            }
        }

        private void OnUnitClick(UnitPickerControl pickerControl, GameEntityModel entityModel)
        {
            if (_selectedUnitPickerControl != null)
            {
                _selectedUnitPickerControl.IsSelected = false;
            }

            _selectedUnitPickerControl = pickerControl;
            pickerControl.IsSelected = true;
            var entityEditControlType =
                entityModel.RootModel.EntityTypeEditControl.FirstOrDefault(e => e.EntityType == entityModel.EntityType)
                    ?.EditControlType;
            if (entityEditControlType != null)
            {
                var controlsToDispose = panelContent.Controls.OfType<Control>().ToList();
                panelContent.Controls.Clear();
                controlsToDispose.ForEach(c => c.Dispose());
                var contentControl = (EntityEditBaseControl)Activator.CreateInstance(entityEditControlType)!;
                ThemeManager.Instance.UseTheme(contentControl);
                contentControl.Dock = DockStyle.Fill;
                contentControl.LoadEntity(entityModel);
                panelContent.Controls.Add(contentControl);
                panelContent.Visible = true;
            }
            else
            {
                unitEdit.LoadModel(entityModel);
                unitEdit.Visible = true;
            }
        }

        private void buttonAddUnit_Click(object sender, EventArgs e)
        {
            UnitAddUnlisted?.Invoke(this, e);
        }

        private void buttonAddEmpty_Click(object sender, EventArgs e)
        {
            UnitAddEmpty?.Invoke(this, e);
        }

        private void unitEdit_FavoriteClick(object sender, EventArgs e)
        {
            _unitPickerControls.FirstOrDefault(c => c.UnitKey == unitEdit.EntityModel!.EntityKey)
                ?.RefreshIsFavorite();
        }

        private void unitEdit_UnitModificationsChanged(object sender, EventArgs e)
        {
            RefreshInfoNumber(unitEdit.EntityModel!.EntityKey);
        }

        private void unitEdit_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            UnitCreateCopy?.Invoke(this, e);
        }

        private void unitEdit_UnitDelete(object sender, EntityDeleteEventArgs e)
        {
            UnitDelete?.Invoke(this, e);
        }

        private void toolStripButtonPrev_Click(object sender, EventArgs e)
        {
            AnimationsAsyncLoader.Instance.Stop(true, false);
            LoadPage(_currentPage - 1);
            AnimationsAsyncLoader.Instance.Start();
        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            AnimationsAsyncLoader.Instance.Stop(true, false);
            LoadPage(_currentPage + 1);
            AnimationsAsyncLoader.Instance.Start();
        }

        private void UnitsListControl_Load(object sender, EventArgs e)
        {
            buttonAddUnit.Visible = CanAddUnlisted;
            buttonAddEmpty.Visible = CanAddEmpty;
            toolStripAdd.Visible = (CanAddUnlisted || CanAddEmpty)
                                   && !ReadonlyMode
                                   && CCGameRepository.Instance.IsLoaded;
        }

        private void checkBoxOnlyModified_CheckedChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            LoadUnits();
        }

    }
}
