using Deniz.TiberiumSunEditor.Gui.Model;
using Infragistics.Win.UltraWinTabControl;
using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class AiEditMainControl : UserControl
    {
        private bool _readonlyMode;
        private bool _filterVisible;
        private bool _titleVisible = true;
        private string _searchText = "";

        public AiEditMainControl()
        {
            InitializeComponent();
        }

        public AiRootModel Model { get; private set; } = null!;

        [DefaultValue(true)]
        public bool TitleVisible
        {
            get => _titleVisible;
            set
            {
                _titleVisible = value;
                panelTitle.Visible = value;
            }
        }

        [DefaultValue(false)]
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                //unitsBuildings.ReadonlyMode = _readonlyMode;
                //unitsInfantry.ReadonlyMode = _readonlyMode;
                //unitsVehicles.ReadonlyMode = _readonlyMode;
                //unitsAircrafts.ReadonlyMode = _readonlyMode;
                //unitsProjectiles.ReadonlyMode = _readonlyMode;
                //unitsAnimations.ReadonlyMode = _readonlyMode;
                //panelPhobosShowEmpty.Visible = !_readonlyMode;
            }
        }

        [DefaultValue("")]
        [Browsable(false)]
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                //unitsBuildings.SearchText = _searchText;
                //unitsInfantry.SearchText = _searchText;
                //unitsVehicles.SearchText = _searchText;
                //unitsAircrafts.SearchText = _searchText;
                //unitsProjectiles.SearchText = _searchText;
                //unitsAnimations.SearchText = _searchText;
            }
        }

        public void LoadModel(AiRootModel model)
        {
            Model = model;
            labelType.Text = model.FileType.TypeLabel;
            labelName.Text = model.FileType.Title;
            //filterControl.LoadModel(model);
            LoadModels();
            var firstVisibleTab = mainTab.Tabs.OfType<UltraTab>().FirstOrDefault(t => t.Visible);
            if (firstVisibleTab != null)
            {
                mainTab.SelectedTab = firstVisibleTab;
            }
            model.EntitiesChanged += (sender, args) => LoadModels();
        }

        public void LoadModels()
        {
            mainTab.Tabs["TaskForces"].Visible =
                entitiesListTaskForces.LoadModel(Model.TaskForceEntities, typeof(AiTaskForceEditControl));
        }
    }
}
