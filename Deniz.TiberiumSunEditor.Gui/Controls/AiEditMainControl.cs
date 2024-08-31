using Deniz.TiberiumSunEditor.Gui.Model;
using Infragistics.Win.UltraWinTabControl;
using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Dialogs;

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
                unitsListInfantry.ReadonlyMode = _readonlyMode;
                unitsListVehicles.ReadonlyMode = _readonlyMode;
                unitsListAircrafts.ReadonlyMode = _readonlyMode;
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
                unitsListInfantry.SearchText = _searchText;
                unitsListVehicles.SearchText = _searchText;
                unitsListAircrafts.SearchText = _searchText;
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
            mainTab.Tabs["Infantry"].Visible =
                unitsListInfantry.LoadModel(Model.RulesModel.InfantryEntities, null, typeof(AiUnitEditControl));
            mainTab.Tabs["Vehicles"].Visible =
                unitsListVehicles.LoadModel(Model.RulesModel.VehicleEntities, null, typeof(AiUnitEditControl));
            mainTab.Tabs["Aircrafts"].Visible =
                unitsListAircrafts.LoadModel(Model.RulesModel.AircraftEntities, null, typeof(AiUnitEditControl));
        }

        private void entitiesListTaskForces_AddEntity(object sender, EventArgs e)
        {
            var newUnit = SelectUnitForm.ExecuteSelect(ParentForm!, Model.RulesModel,
                SelectTechnoTypes.Infantry | SelectTechnoTypes.Vehicles | SelectTechnoTypes.Aircrafts);
            if (newUnit != null)
            {
                var newEntityListItem = Model.AddGameEntity("TaskForces");
                newEntityListItem.EntityModel.FileSection.SetValue("Name", $"1 {newUnit.GetNameOrKey()}");
                newEntityListItem.EntityModel.FileSection.SetValue("Group", "-1");
                newEntityListItem.EntityModel.FileSection.SetValue("0", $"1,{newUnit.EntityKey}");
                entitiesListTaskForces.LoadModel(Model.TaskForceEntities, typeof(AiTaskForceEditControl),
                    selectKey: newEntityListItem.EntityModel.EntityKey);
            }
        }
    }
}
