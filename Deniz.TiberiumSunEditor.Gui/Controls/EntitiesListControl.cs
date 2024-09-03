using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Exceptions;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class EntitiesListControl : UserControl
    {
        private IRootModel _rootModel = null!;
        private FilterByParentModel? _filterKeyValue;
        private Type _bindedListItemType = null!;
        private object _bindedList = null!;
        private bool _listOnTop;
        private int _listSize = 260;
        private bool _readonlyMode;
        private bool _doEvents;

        public EntitiesListControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? AddEntityManual;
        public event EventHandler<GameEntityEventArgs>? AddedEntity;

        [DefaultValue(false)]
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                toolStripAdd.Visible = !value;
            }
        }

        [DefaultValue("")]
        [Browsable(false)]
        public string SearchText { get; set; } = "";

        [DefaultValue("")]
        public string EntityType { get; set; } = "";

        [DefaultValue(260)]
        public int ListSize
        {
            get => _listSize;
            set
            {
                if (_listSize == value) return;
                _listSize = value;
                if (_listOnTop)
                {
                    panelLeft.Height = _listSize;
                }
                else
                {
                    panelLeft.Width = _listSize;
                }
            }
        }

        [DefaultValue(false)]
        public bool ListOnTop
        {
            get => _listOnTop;
            set
            {
                if (_listOnTop == value) return;
                _listOnTop = value;
                panelLeft.Dock = _listOnTop
                    ? DockStyle.Top
                    : DockStyle.Left;
                splitterUnitPicker.Dock = _listOnTop
                    ? DockStyle.Top
                    : DockStyle.Left;
                if (_listOnTop)
                {
                    panelLeft.Height = _listSize;
                }
                else
                {
                    panelLeft.Width = _listSize;
                }
            }
        }

        [Browsable(false)]
        public FilterByParentModel? ParentFilterKeyValue => _filterKeyValue;

        public bool LoadListModel<TListItemModel>(
            IRootModel rootModel,
            List<TListItemModel> listItems,
            FilterByParentModel? filterKeyValue = null,
            string? selectKey = null)
            where TListItemModel : EntityListItemModel
        {
            _rootModel = rootModel;
            _bindedListItemType = typeof(TListItemModel);
            _bindedList = listItems;
            _filterKeyValue = filterKeyValue;
            _doEvents = false;
            SelectListItem(null, null);
            var filteredItems = FilterListItems(listItems);
            entitiesGrid.DataSource = null;
            entitiesGrid.DataSource = filteredItems;
            entitiesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            entitiesGrid.DisplayLayout.Bands[0].ScrollTipField = "Name";
            entitiesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            _doEvents = true;
            if (selectKey != null)
            {
                var selectedIndex = filteredItems!.FindIndex(v => v.EntityModel.EntityKey == selectKey);
                if (selectedIndex > -1)
                {
                    var selectedRow = entitiesGrid.Rows[selectedIndex];
                    entitiesGrid.Selected.Rows.Add(selectedRow);
                    entitiesGrid.ActiveRowScrollRegion.ScrollRowIntoView(selectedRow);
                }
            }
            return filteredItems.Any();
        }

        private List<TListItemModel> FilterListItems<TListItemModel>(List<TListItemModel> listItems)
            where TListItemModel : EntityListItemModel
        {
            if (_filterKeyValue == null && SearchText == string.Empty)
            {
                return listItems;
            }
            var filteredItems = _filterKeyValue == null
                ? listItems
                : listItems.Where(e => e.EntityModel.FileSection.KeyValues.Any(k =>
                {
                    if (_filterKeyValue.FilterFunction != null)
                    {
                        return _filterKeyValue.FilterFunction(k);
                    }
                    return k.Key == _filterKeyValue.Key && k.Value.Equals(_filterKeyValue.Value,
                        StringComparison.InvariantCultureIgnoreCase);
                })).ToList();
            if (SearchText != string.Empty)
            {
                filteredItems = filteredItems
                    .Where(f => f.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            }
            return filteredItems;
        }

        private void ReloadList(string? selectKey = null)
        {
            typeof(EntitiesListControl)
                .GetMethod("ReloadListModel")?
                .MakeGenericMethod(_bindedListItemType)
                .Invoke(this, new object?[] { selectKey });
        }

        public void ReloadListModel<TListItemModel>(string? selectKey) where TListItemModel : EntityListItemModel
        {
            LoadListModel(_rootModel, (List<TListItemModel>)_bindedList, _filterKeyValue, selectKey);
        }

        private void SelectListItem(EntityListItemModel? entity, UltraGridRow? row)
        {
            var controlsToDispose = panelContent.Controls.OfType<Control>().ToList();
            panelContent.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            if (entity != null && row != null)
            {
                var entityEditControlType =
                    _rootModel.EntityTypeEditControl.FirstOrDefault(e => e.EntityType == entity.EntityModel.EntityType)?.EditControlType
                    ?? throw new RuntimeException(
                        $"_rootModel.EntityTypeEditControl has no entry for EntityType '{entity.EntityModel.EntityType}'");
                var contentControl = (EntityEditBaseControl)Activator.CreateInstance(entityEditControlType)!;
                ThemeManager.Instance.UseTheme(contentControl);
                contentControl.ReadonlyMode = _readonlyMode;
                contentControl.Dock = DockStyle.Fill;
                contentControl.LoadEntity(entity.EntityModel, _filterKeyValue);
                contentControl.NameChanged += (sender, args) => row.Refresh();
                contentControl.EntityDeleted += (sender, args) => ReloadList();
                panelContent.Controls.Add(contentControl);
            }
        }

        private void entitiesGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (!_doEvents || entitiesGrid.Selected.Rows.Count == 0) return;
            if (entitiesGrid.Selected.Rows[0].ListObject is EntityListItemModel listItem)
            {
                SelectListItem(listItem, entitiesGrid.Selected.Rows[0]);
            }
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            if (AddEntityManual != null)
            {
                AddEntityManual?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (_rootModel is AiRootModel aiRootModel 
                && EntityType != string.Empty)
            {
                var entityFactory = new AiGameEntityFactory(aiRootModel);
                var newEntity = entityFactory.AddNewGameEntity(EntityType, _filterKeyValue?.ParentEntity);
                AddedEntity?.Invoke(this, new GameEntityEventArgs(newEntity));
                ReloadList(newEntity.EntityKey);
            }
        }
    }
}
