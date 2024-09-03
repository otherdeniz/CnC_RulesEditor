﻿using Deniz.TiberiumSunEditor.Gui.Model;
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
        private readonly UltraTab _ultraTabScripts;
        private readonly UltraTab _ultraTabTaskForces;
        private readonly UltraTab _ultraTabTeams;

        public AiEditMainControl()
        {
            InitializeComponent();
            _ultraTabScripts = mainTab.Tabs["Scripts"];
            _ultraTabTaskForces = mainTab.Tabs["TaskForces"];
            _ultraTabTeams = mainTab.Tabs["Teams"];
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

        [DefaultValue(true)]
        public bool ShowUnitTaskForceTabs { get; set; } = true;

        public void LoadModel(AiRootModel model)
        {
            Model = model;
            labelType.Text = model.FileType.TypeLabel;
            labelName.Text = model.FileType.Title;
            //filterControl.LoadModel(model);
            ReloadModels();
            var firstVisibleTab = mainTab.Tabs.OfType<UltraTab>().FirstOrDefault(t => t.Visible);
            if (firstVisibleTab != null)
            {
                mainTab.SelectedTab = firstVisibleTab;
            }
            model.EntitiesReloaded += (sender, args) => ReloadModels();
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

        public void ReloadModels()
        {
            _ultraTabScripts.Visible = entitiesListScripts.LoadListModel(Model, Model.ScriptEntities)
                                       || _searchText == string.Empty && !_readonlyMode;
            _ultraTabTaskForces.Visible = entitiesListTaskForces.LoadListModel(Model, Model.TaskForceEntities)
                                          || _searchText == string.Empty && !_readonlyMode;
            _ultraTabTeams.Visible = entitiesListTeams.LoadListModel(Model, Model.TeamEntities)
                                     || _searchText == string.Empty && !_readonlyMode;
            mainTab.Tabs["Infantry"].Visible = ShowUnitTaskForceTabs
                                               && (unitsListInfantry.LoadModel(Model.RulesModel.InfantryEntities)
                                                   || _searchText == string.Empty && !_readonlyMode);
            mainTab.Tabs["Vehicles"].Visible = ShowUnitTaskForceTabs
                                               && (unitsListVehicles.LoadModel(Model.RulesModel.VehicleEntities)
                                                   || _searchText == string.Empty && !_readonlyMode);
            mainTab.Tabs["Aircrafts"].Visible = ShowUnitTaskForceTabs
                                                && (unitsListAircrafts.LoadModel(Model.RulesModel.AircraftEntities)
                                                    || _searchText == string.Empty && !_readonlyMode);
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
