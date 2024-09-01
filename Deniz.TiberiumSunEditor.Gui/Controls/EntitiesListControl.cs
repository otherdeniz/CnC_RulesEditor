using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class EntitiesListControl : UserControl
    {
        private Type _entityEditControlType = null!;
        private FilterByParentModel? _filterKeyValue;
        private bool _listOnTop;
        private int _listSize = 260;
        private bool _readonlyMode;
        private bool _doEvents;

        public EntitiesListControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? AddEntity;

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

        public bool LoadModel<TListItemModel>(List<TListItemModel> listItems,
            Type entityEditControlType,
            FilterByParentModel? filterKeyValue = null,
            string? selectKey = null)
            where TListItemModel : EntityListItemModel
        {
            _entityEditControlType = entityEditControlType;
            _filterKeyValue = filterKeyValue;
            _doEvents = false;
            SelectListItem(null, null);
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

        private void SelectListItem(EntityListItemModel? entity, UltraGridRow? row)
        {
            var controlsToDispose = panelContent.Controls.OfType<Control>().ToList();
            panelContent.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            if (entity != null && row != null)
            {
                var contentControl = (EntityEditBaseControl)Activator.CreateInstance(_entityEditControlType)!;
                ThemeManager.Instance.UseTheme(contentControl);
                contentControl.ReadonlyMode = _readonlyMode;
                contentControl.Dock = DockStyle.Fill;
                contentControl.LoadEntity(entity.EntityModel, _filterKeyValue);
                contentControl.NameChanged += (sender, args) => row.Refresh();
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
            AddEntity?.Invoke(this, EventArgs.Empty);
        }
    }
}
