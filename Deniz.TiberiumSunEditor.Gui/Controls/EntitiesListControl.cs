using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class EntitiesListControl : UserControl
    {
        private List<EntityListItemModel> _listItems = null!;
        private Type _entityEditControlType = null!;
        private FilterByParentModel? _filterKeyValue;
        private bool _doEvents;

        public EntitiesListControl()
        {
            InitializeComponent();
        }

        public bool LoadModel(List<EntityListItemModel> listItems,
            Type entityEditControlType,
            FilterByParentModel? filterKeyValue = null)
        {
            _listItems = listItems;
            _entityEditControlType = entityEditControlType;
            _filterKeyValue = filterKeyValue;
            return LoadListItems();
        }

        private bool LoadListItems()
        {
            _doEvents = false;
            SelectListItem(null, null);
            var filteredItems = _filterKeyValue == null
                ? _listItems
                : _listItems.Where(e => e.EntityModel.FileSection.KeyValues.Any(k =>
                {
                    if (_filterKeyValue.FilterFunction != null)
                    {
                        return _filterKeyValue.FilterFunction(k);
                    }
                    return k.Key == _filterKeyValue.Key && k.Value.Equals(_filterKeyValue.Value,
                            StringComparison.InvariantCultureIgnoreCase);
                })).ToList();
            entitiesGrid.DataSource = filteredItems;
            entitiesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            entitiesGrid.DisplayLayout.Bands[0].ScrollTipField = "Name";
            entitiesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            _doEvents = true;
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

        }
    }
}
