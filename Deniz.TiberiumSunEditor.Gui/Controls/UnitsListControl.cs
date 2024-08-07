using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UnitsListControl : UserControl
    {
        private const int PageSize = 250;
        private List<GameEntityModel> _entities = null!;
        private List<GameEntityModel> _orderedEntities = null!;
        private UnitPickerControl? _selectedUnitPickerControl;
        private bool _readonlyMode;
        private bool _showOnlyFavoriteValues;
        private int _currentPage = 1;

        public UnitsListControl()
        {
            InitializeComponent();
            toolStripAdd.Visible = false;
        }

        public event EventHandler<EntityCopyEventArgs>? UnitCreateCopy;
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
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                unitEdit.ReadonlyMode = _readonlyMode;
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
        public bool ShowOnlyFavoriteUnits { get; set; }

        [DefaultValue("")]
        [Browsable(false)]
        public string SearchText { get; set; } = "";

        public bool LoadModel(List<GameEntityModel> entities)
        {
            _entities = entities;
            return LoadUnits();
        }

        public void SelectKey(string key)
        {
            var unitPicker = unitsLayoutPanel.Controls.OfType<UnitPickerControl>()
                .FirstOrDefault(c => c.EntityModel?.EntityKey == key);
            if (unitPicker != null)
            {
                OnUnitClick(unitPicker, unitPicker.EntityModel!);
            }
        }

        private bool LoadUnits()
        {
            var filteredList = _entities.Where(FilterValue).ToList();
            var orderedList = OrderByThumbnail
                ? filteredList
                    .OrderBy(OrderByOwner)
                    .ThenBy(e => e.Thumbnail == null ? 1 : 0)
                    .ThenBy(e => e.EntityKey)
                : filteredList
                    .OrderBy(e => e.EntityKey);
            _orderedEntities = orderedList.ToList();
            LoadPage(1);
            return filteredList.Any();
        }

        private void LoadPage(int pageNumber)
        {
            _currentPage = pageNumber;
            _selectedUnitPickerControl = null;
            unitEdit.ClearModel();
            unitEdit.Visible = false;
            unitsLayoutPanel.Visible = false;
            // clear controls
            var controlsToDispose = unitsLayoutPanel.Controls
                .OfType<Control>().ToList();
            unitsLayoutPanel.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            // add controls
            foreach (var entityModel in _orderedEntities.Skip(PageSize * (pageNumber - 1)).Take(PageSize))
            {
                var unitPicker = new UnitPickerControl
                {
                    ReadonlyMode = _readonlyMode
                };
                unitPicker.FavoriteClick += (sender, args) => OnFavoriteClick(unitPicker, entityModel);
                unitPicker.UnitClick += (sender, args) => OnUnitClick(unitPicker, entityModel);
                unitPicker.LoadModel(entityModel);
                unitsLayoutPanel.Controls.Add(unitPicker);
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
            unitsLayoutPanel.Visible = true;
        }

        private bool FilterValue(GameEntityModel entityModel)
        {
            if (SearchText == "")
            {
                return !ShowOnlyFavoriteUnits || entityModel.Favorite || ReadonlyMode;
            }

            return entityModel.EntityKey.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)
                   || entityModel.EntityName.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);
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
            unitEdit.LoadModel(entityModel);
            unitEdit.Visible = true;
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
            unitsLayoutPanel.Controls
                .OfType<UnitPickerControl>()
                .FirstOrDefault(c => c.UnitKey == unitEdit.EntityModel!.EntityKey)
                ?.RefreshIsFavorite();
        }

        private void unitEdit_UnitModificationsChanged(object sender, EventArgs e)
        {
            unitsLayoutPanel.Controls
                .OfType<UnitPickerControl>()
                .FirstOrDefault(c => c.UnitKey == unitEdit.EntityModel!.EntityKey)
                ?.RefreshModifications();
        }

        private void unitEdit_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            UnitCreateCopy?.Invoke(this, e);
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

    }
}
