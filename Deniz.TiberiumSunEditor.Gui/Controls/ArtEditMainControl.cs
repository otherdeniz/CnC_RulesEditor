using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Infragistics.Win.UltraWinTabControl;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class ArtEditMainControl : UserControl
    {
        public ArtEditMainControl()
        {
            InitializeComponent();
        }

        public ArtRootModel Model { get; private set; } = null!;

        public void LoadModel(ArtRootModel model)
        {
            Model = model;
            labelType.Text = "Art.ini";
            labelName.Text = "Art.ini";
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
            //mainTab.Tabs["Buildings"].Visible = unitsBuildings.LoadModel(Model.BuildingEntities);
            //mainTab.Tabs["Infantry"].Visible = unitsInfantry.LoadModel(Model.InfantryEntities);
            //mainTab.Tabs["Vehicles"].Visible = unitsVehicles.LoadModel(Model.VehicleEntities);
            //mainTab.Tabs["Aircrafts"].Visible = unitsAircrafts.LoadModel(Model.AircraftEntities);
            //mainTab.Tabs["Weapons"].Visible = unitsWeapons.LoadModel(Model.WeaponEntities);
            //mainTab.Tabs["Projectiles"].Visible = unitsProjectiles.LoadModel(Model.ProjectileEntities);
            //mainTab.Tabs["Warheads"].Visible = unitsWarheads.LoadModel(Model.WarheadEntities);
            //var hasSuperWeapons = unitsSuperWeapons.LoadModel(Model.SuperWeaponEntities);
            //if (valuesEditSuperWeapons.LoadValuesGrid(Model, Model.SuperWeaponValues))
            //{
            //    hasSuperWeapons = true;
            //}
            //mainTab.Tabs["SuperWeapons"].Visible = hasSuperWeapons;
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
            //if (Model.UsePhobos)
            //{
            //    foreach (var additionalGameEntities in Model.AdditionalEntities.Where(a => a.Module == "PHOBOS"))
            //    {
            //        var unitListConrol = new UnitsListControl
            //        {
            //            Dock = DockStyle.Fill,
            //            CanAddEmpty = true
            //        };
            //        unitListConrol.ReadonlyMode = _readonlyMode;
            //        unitListConrol.SearchText = _searchText;
            //        unitListConrol.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
            //        unitListConrol.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
            //        unitListConrol.UnitAddEmpty += (sender, args) =>
            //        {
            //            Cursor = Cursors.WaitCursor;
            //            AddEmptyUnit(additionalGameEntities.EntityType);
            //            Cursor = Cursors.Default;
            //        };
            //        var hasEntries = unitListConrol.LoadModel(additionalGameEntities.Entities);
            //        if (hasEntries || !_readonlyMode)
            //        {
            //            var additionalTabControl = new UltraTabPageControl
            //            {
            //                Location = new Point(-10000, -10000),
            //                Margin = new Padding(4, 3, 4, 3),
            //                Tag = "Custom"
            //            };
            //            var additionalTab = new UltraTab
            //            {
            //                Text = additionalGameEntities.EntityType,
            //                Visible = hasEntries || checkBoxPhobosShowEmpty.Checked,
            //                Tag = hasEntries
            //            };
            //            additionalTab.TabPage = additionalTabControl;
            //            additionalTabControl.Controls.Add(unitListConrol);
            //            tabPhobos.Controls.Add(additionalTabControl);
            //            tabPhobos.Tabs.Add(additionalTab);
            //            hasPhobos = true;
            //        }
            //    }
            //}
            mainTab.Tabs["Phobos"].Visible = hasPhobos;
            AnimationsAsyncLoader.Instance.Start();
        }

    }
}
