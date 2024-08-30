using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class EntitiesListControl : UserControl
    {
        private List<EntityListIemModel> _listItems = null!;
        private Type _entityEditControlType = null!;
        private bool _doEvents;

        public EntitiesListControl()
        {
            InitializeComponent();
        }

        public bool LoadModel(List<EntityListIemModel> listItems, Type entityEditControlType)
        {
            _listItems = listItems;
            _entityEditControlType = entityEditControlType;
            return LoadListItems();
        }

        private bool LoadListItems()
        {
            _doEvents = false;
            SelectListItem(null);
            entitiesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            entitiesGrid.DataSource = _listItems;
            entitiesGrid.DisplayLayout.Bands[0].ScrollTipField = "Name";
            entitiesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            entitiesGrid.DisplayLayout.Bands[0].Columns["Modifications"].Width = 30;
            _doEvents = true;
            return _listItems.Any();
        }

        private void SelectListItem(EntityListIemModel? entity)
        {
            var controlsToDispose = panelContent.Controls.OfType<Control>().ToList();
            panelContent.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            if (entity != null)
            {
                var contentControl = (EntityEditBaseControl)Activator.CreateInstance(_entityEditControlType)!;
                ThemeManager.Instance.UseTheme(contentControl);
                contentControl.Dock = DockStyle.Fill;
                contentControl.LoadEntity(entity.EntityModel);
                panelContent.Controls.Add(contentControl);
            }
        }

        private void entitiesGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (!_doEvents || entitiesGrid.Selected.Rows.Count == 0) return;
            if (entitiesGrid.Selected.Rows[0].ListObject is EntityListIemModel listItem)
            {
                SelectListItem(listItem);
            }
        }
    }
}
