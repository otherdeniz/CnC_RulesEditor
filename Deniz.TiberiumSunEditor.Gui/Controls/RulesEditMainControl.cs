using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Infragistics.Win.UltraWinTabControl;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class RulesEditMainControl : UserControl
    {
        private bool _readonlyMode;
        private bool _showOnlyFavoriteValues;
        private bool _titleVisible = true;
        private string _searchText = "";
        private bool _showOnlyFavoriteUnits;

        public RootModel Model { get; private set; } = null!;

        public RulesEditMainControl()
        {
            InitializeComponent();
        }

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
                valuesEditCommon.ReadonlyMode = _readonlyMode;
                valuesEditTiberium.ReadonlyMode = _readonlyMode;
                valuesEditAi.ReadonlyMode = _readonlyMode;
                valuesEditSuperWeapons.ReadonlyMode = _readonlyMode;
                unitsBuildings.ReadonlyMode = _readonlyMode;
                unitsInfantry.ReadonlyMode = _readonlyMode;
                unitsVehicles.ReadonlyMode = _readonlyMode;
                unitsAircrafts.ReadonlyMode = _readonlyMode;
                unitsWeapons.ReadonlyMode = _readonlyMode;
                unitsWarheads.ReadonlyMode = _readonlyMode;
                unitsSuperWeapons.ReadonlyMode = _readonlyMode;
                panelPhobosShowEmpty.Visible = !_readonlyMode;
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
                valuesEditCommon.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                valuesEditTiberium.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                valuesEditAi.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                valuesEditSuperWeapons.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsBuildings.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsInfantry.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsVehicles.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsAircrafts.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsWeapons.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsWarheads.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsSuperWeapons.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
            }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        public bool ShowOnlyFavoriteUnits
        {
            get => _showOnlyFavoriteUnits;
            set
            {
                _showOnlyFavoriteUnits = value;
                unitsBuildings.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                unitsInfantry.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                unitsVehicles.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                unitsAircrafts.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                unitsWeapons.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                unitsWarheads.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                unitsSuperWeapons.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
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
                valuesEditCommon.SearchText = _searchText;
                valuesEditTiberium.SearchText = _searchText;
                valuesEditAi.SearchText = _searchText;
                valuesEditSuperWeapons.SearchText = _searchText;
                unitsBuildings.SearchText = _searchText;
                unitsInfantry.SearchText = _searchText;
                unitsVehicles.SearchText = _searchText;
                unitsAircrafts.SearchText = _searchText;
                unitsWeapons.SearchText = _searchText;
                unitsWarheads.SearchText = _searchText;
                unitsSuperWeapons.SearchText = _searchText;
            }
        }

        public void LoadModel(RootModel model, string? fileTypeOverride = null, string? nameOverride = null)
        {
            Model = model;
            labelType.Text = fileTypeOverride ?? model.FileType.TypeLabel;
            labelName.Text = nameOverride ?? model.FileType.Title;
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
            AnimationsAsyncLoader.Instance.Stop(true, false);
            mainTab.Tabs["Buildings"].Visible = unitsBuildings.LoadModel(Model.BuildingEntities);
            mainTab.Tabs["Infantry"].Visible = unitsInfantry.LoadModel(Model.InfantryEntities);
            mainTab.Tabs["Vehicles"].Visible = unitsVehicles.LoadModel(Model.VehicleEntities);
            mainTab.Tabs["Aircrafts"].Visible = unitsAircrafts.LoadModel(Model.AircraftEntities);
            mainTab.Tabs["Weapons"].Visible = unitsWeapons.LoadModel(Model.WeaponEntities);
            mainTab.Tabs["Warheads"].Visible = unitsWarheads.LoadModel(Model.WarheadEntities);
            var hasSuperWeapons = unitsSuperWeapons.LoadModel(Model.SuperWeaponEntities);
            if (valuesEditSuperWeapons.LoadValuesGrid(Model, Model.SuperWeaponValues))
            {
                hasSuperWeapons = true;
            }
            mainTab.Tabs["SuperWeapons"].Visible = hasSuperWeapons;
            mainTab.Tabs["Common"].Visible = valuesEditCommon.LoadValuesGrid(Model, Model.CommonValues);
            mainTab.Tabs["Tiberium"].Visible = valuesEditTiberium.LoadValuesGrid(Model, Model.TiberiumValues);
            mainTab.Tabs["AI"].Visible = valuesEditAi.LoadValuesGrid(Model, Model.AiValues);
            mainTab.Tabs["AudioVisual"].Visible = valuesEditAudioVisual.LoadValuesGrid(Model, Model.AudioVisualValues);
            var hasPhobos = false;
            tabPhobos.Tabs.Clear();
            tabPhobos.Controls.OfType<UltraTabPageControl>().ToList().ForEach(c =>
            {
                if (c.Tag as string == "Custom")
                {
                    tabPhobos.Controls.Remove(c);
                    c.Dispose();
                }
            });
            if (Model.UsePhobos)
            {
                foreach (var additionalGameEntities in Model.AdditionalEntities.Where(a => a.Module == "PHOBOS"))
                {
                    var unitListConrol = new UnitsListControl
                    {
                        Dock = DockStyle.Fill,
                        CanAddEmpty = true
                    };
                    unitListConrol.ReadonlyMode = _readonlyMode;
                    unitListConrol.SearchText = _searchText;
                    unitListConrol.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                    unitListConrol.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                    unitListConrol.UnitAddEmpty += (sender, args) =>
                    {
                        Cursor = Cursors.WaitCursor;
                        AddEmptyUnit(additionalGameEntities.EntityType);
                        Cursor = Cursors.Default;
                    };
                    var hasEntries = unitListConrol.LoadModel(additionalGameEntities.Entities);
                    if (hasEntries || !_readonlyMode)
                    {
                        var additionalTabControl = new UltraTabPageControl
                        {
                            Location = new Point(-10000, -10000),
                            Margin = new Padding(4, 3, 4, 3),
                            Tag = "Custom"
                        };
                        var additionalTab = new UltraTab
                        {
                            Text = additionalGameEntities.EntityType,
                            Visible = hasEntries || checkBoxPhobosShowEmpty.Checked,
                            Tag = hasEntries
                        };
                        additionalTab.TabPage = additionalTabControl;
                        additionalTabControl.Controls.Add(unitListConrol);
                        tabPhobos.Controls.Add(additionalTabControl);
                        tabPhobos.Tabs.Add(additionalTab);
                        hasPhobos = true;
                    }
                }
            }
            mainTab.Tabs["Phobos"].Visible = hasPhobos;
            AnimationsAsyncLoader.Instance.Start();
        }

        private void CreateCopy(EntityCopyEventArgs e,
            string? entityTypes,
            bool addImage,
            UnitsListControl unitsListControl)
        {
            var newSection = Model.File.AddSection(e.NewKey);
            e.SourceEntityModel.FileSection.KeyValues.ForEach(k => newSection.SetValue(k.Key, k.Value));
            newSection.SetValue("Name", e.NewName);
            if (addImage && newSection.GetValue("Image") == null)
            {
                newSection.SetValue("Image", e.SourceEntityModel.EntityKey);
            }
            if (entityTypes != null)
            {
                var entitiesTypesSection = Model.File.GetSection(entityTypes)
                                           ?? Model.File.AddSection(entityTypes);
                var typeKey = entitiesTypesSection.KeyValues.Any()
                    ? entitiesTypesSection.KeyValues.Max(k => int.Parse(k.Key)) + 1
                    : 900;
                entitiesTypesSection.SetValue(typeKey.ToString(), e.NewKey);
            }
            Model.ReloadGameEntites();
            unitsListControl.SelectKey(e.NewKey);
        }

        private void AddEmptyUnit(string? entityTypes)
        {
            using (var addEmptyForm = new AddEmptyForm())
            {
                addEmptyForm.LoadModel(Model);
                if (addEmptyForm.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    var newKey = addEmptyForm.TextNewKey.Text;
                    Model.File.AddSection(newKey);
                    if (entityTypes != null)
                    {
                        var entitiesTypesSection = Model.File.GetSection(entityTypes)
                                                   ?? Model.File.AddSection(entityTypes);
                        var typeKey = entitiesTypesSection.KeyValues.Any()
                            ? entitiesTypesSection.KeyValues.Max(k => int.Parse(k.Key)) + 1
                            : Model.FileType.BaseType == FileBaseType.Rules ? 1 : 900;
                        entitiesTypesSection.SetValue(typeKey.ToString(), newKey);
                    }
                    Model.ReloadGameEntites();
                }
            }
        }

        private void AddNewUnit(string entityType,
            List<GameEntityModel> existingEntityModels,
            List<KeyValueDefinition> newUnitTemplate,
            UnitsListControl unitsListControl)
        {
            var addUnitResult = AddUnitForm.ExecuteAddUnit(this.ParentForm!, Model, entityType, existingEntityModels);
            if (addUnitResult != null)
            {
                var newSection = Model.File.AddSection(addUnitResult.Key);
                foreach (var keyValue in newUnitTemplate)
                {
                    newSection.SetValue(keyValue.Key, keyValue.Value);
                }
                newSection.SetValue("Name", addUnitResult.Name);
                newSection.SetValue("Owner", Model.Sides[0].Value.FirstOrDefault() ?? "");
                var entitiesTypesSection = Model.File.GetSection(entityType)
                                           ?? Model.File.AddSection(entityType);
                var typeKey = entitiesTypesSection.KeyValues.Any()
                    ? entitiesTypesSection.KeyValues.Max(k => int.Parse(k.Key)) + 1
                    : 900;
                entitiesTypesSection.SetValue(typeKey.ToString(), addUnitResult.Key);
                Model.ReloadGameEntites();
                unitsListControl.SelectKey(addUnitResult.Key);
            }
        }
        private void unitsBuildings_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            CreateCopy(e, "BuildingTypes", true, unitsBuildings);
        }

        private void unitsInfantry_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            CreateCopy(e, "InfantryTypes", true, unitsInfantry);
        }

        private void unitsVehicles_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            CreateCopy(e, "VehicleTypes", true, unitsVehicles);
        }

        private void unitsAircrafts_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            CreateCopy(e, "AircraftTypes", true, unitsAircrafts);
        }

        private void unitsWeapons_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            CreateCopy(e, null, false, unitsWeapons);
        }

        private void unitsWarheads_UnitCreateCopy(object sender, EntityCopyEventArgs e)
        {
            CreateCopy(e, "Warheads", false, unitsWarheads);
        }

        private void unitsBuildings_UnitAdd(object sender, EventArgs e)
        {
            AddNewUnit("BuildingTypes", Model.BuildingEntities,
                Model.Datastructure.NewBuilding, unitsBuildings);
        }

        private void unitsInfantry_UnitAdd(object sender, EventArgs e)
        {
            AddNewUnit("InfantryTypes", Model.InfantryEntities,
                Model.Datastructure.NewInfantry, unitsInfantry);
        }

        private void unitsVehicles_UnitAdd(object sender, EventArgs e)
        {
            AddNewUnit("VehicleTypes", Model.VehicleEntities.Union(Model.AircraftEntities).ToList(),
                Model.Datastructure.NewVehicle, unitsVehicles);
        }

        private void unitsAircrafts_UnitAdd(object sender, EventArgs e)
        {
            AddNewUnit("AircraftTypes", Model.VehicleEntities.Union(Model.AircraftEntities).ToList(),
                Model.Datastructure.NewAircraft, unitsAircrafts);
        }

        private void checkBoxPhobosShowEmpty_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var phobosTab in tabPhobos.Tabs)
            {
                phobosTab.Visible = checkBoxPhobosShowEmpty.Checked || (bool)phobosTab.Tag;
            }
        }
    }
}
