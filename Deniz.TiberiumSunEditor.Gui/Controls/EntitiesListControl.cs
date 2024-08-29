using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Model;
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
            entitiesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.Default;
            entitiesGrid.DataSource = _listItems;
            entitiesGrid.DisplayLayout.Bands[0].ScrollTipField = "Name";
            entitiesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            entitiesGrid.DisplayLayout.Bands[0].Columns["Modifications"].Width = 30;
            _doEvents = true;
            return _listItems.Any();
        }
    }
}
