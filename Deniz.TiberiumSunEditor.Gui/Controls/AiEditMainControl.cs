using Deniz.TiberiumSunEditor.Gui.Model;
using Infragistics.Win.UltraWinTabControl;
using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;

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
                entitiesListTaskForces.ReadonlyMode = _readonlyMode;
                entitiesListScripts.ReadonlyMode = _readonlyMode;
                entitiesListTeams.ReadonlyMode = _readonlyMode;
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
                entitiesListTaskForces.SearchText = _searchText;
                entitiesListScripts.SearchText = _searchText;
                entitiesListTeams.SearchText = _searchText;
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
            model.EntitiesReloaded += (sender, args) => LoadModels();
            model.GlobalEntityNotification += ModelOnGlobalEntityNotification;
        }

        private void ModelOnGlobalEntityNotification(object? sender, GlobalEntityNotificationEventArgs e)
        {
            if (e.NotificationName == "RefreshInfoNumber")
            {
                unitsListInfantry.RefreshInfoNumber(e.EntitiyKey);
                unitsListVehicles.RefreshInfoNumber(e.EntitiyKey);
                unitsListAircrafts.RefreshInfoNumber(e.EntitiyKey);
            }
        }

        public void LoadModels()
        {
            mainTab.Tabs["TaskForces"].Visible =
                entitiesListTaskForces.LoadListModel(Model, Model.TaskForceEntities);
            mainTab.Tabs["Scripts"].Visible =
                entitiesListScripts.LoadListModel(Model, Model.ScriptEntities);
            mainTab.Tabs["Teams"].Visible =
                entitiesListTeams.LoadListModel(Model, Model.TeamEntities);
            mainTab.Tabs["Infantry"].Visible =
                unitsListInfantry.LoadModel(Model.RulesModel.InfantryEntities);
            mainTab.Tabs["Vehicles"].Visible =
                unitsListVehicles.LoadModel(Model.RulesModel.VehicleEntities);
            mainTab.Tabs["Aircrafts"].Visible =
                unitsListAircrafts.LoadModel(Model.RulesModel.AircraftEntities);
        }

        private void entitiesListTaskForces_AddEntityManual(object sender, EventArgs e)
        {
            var newUnit = SelectUnitForm.ExecuteSelect(ParentForm!, Model.RulesModel,
                SelectTechnoTypes.Infantry | SelectTechnoTypes.Vehicles | SelectTechnoTypes.Aircrafts);
            if (newUnit != null)
            {
                var newEntityListItem = Model.AddGameEntity("TaskForces");
                newEntityListItem.EntityModel.FileSection.SetValue("Name", $"1 {newUnit.GetNameOrKey()}");
                newEntityListItem.EntityModel.FileSection.SetValue("Group", "-1");
                newEntityListItem.EntityModel.FileSection.SetValue("0", $"1,{newUnit.EntityKey}");
                Model.RaiseGlobalEntityNotification(newUnit.EntityKey, "RefreshInfoNumber");
                entitiesListTaskForces.LoadListModel(Model, Model.TaskForceEntities,
                    selectKey: newEntityListItem.EntityModel.EntityKey);
            }
        }
    }
}
