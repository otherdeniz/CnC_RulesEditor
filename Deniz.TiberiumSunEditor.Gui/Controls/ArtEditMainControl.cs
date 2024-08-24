using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Infragistics.Win.UltraWinTabControl;
using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class ArtEditMainControl : UserControl
    {
        private bool _readonlyMode;
        private bool _filterVisible;
        private bool _showOnlyFavoriteValues;
        private bool _showOnlyFavoriteUnits;
        private bool _titleVisible = true;
        private string _searchText = "";

        public ArtEditMainControl()
        {
            InitializeComponent();
        }

        public ArtRootModel Model { get; private set; } = null!;

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
        public bool FilterVisible
        {
            get => _filterVisible;
            set
            {
                _filterVisible = value;
                filterControl.Visible = value;
            }
        }

        [DefaultValue(false)]
        public bool ReadonlyMode
        {
            get => _readonlyMode;
            set
            {
                _readonlyMode = value;
                unitsBuildings.ReadonlyMode = _readonlyMode;
                unitsInfantry.ReadonlyMode = _readonlyMode;
                unitsVehicles.ReadonlyMode = _readonlyMode;
                unitsAircrafts.ReadonlyMode = _readonlyMode;
                unitsProjectiles.ReadonlyMode = _readonlyMode;
                unitsAnimations.ReadonlyMode = _readonlyMode;
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
                unitsBuildings.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsInfantry.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsVehicles.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsAircrafts.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsProjectiles.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
                unitsAnimations.ShowOnlyFavoriteValues = _showOnlyFavoriteValues;
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
                unitsProjectiles.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
                unitsAnimations.ShowOnlyFavoriteUnits = _showOnlyFavoriteUnits;
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
                unitsBuildings.SearchText = _searchText;
                unitsInfantry.SearchText = _searchText;
                unitsVehicles.SearchText = _searchText;
                unitsAircrafts.SearchText = _searchText;
                unitsProjectiles.SearchText = _searchText;
                unitsAnimations.SearchText = _searchText;
            }
        }

        public void LoadModel(ArtRootModel model)
        {
            Model = model;
            labelType.Text = model.FileType.TypeLabel;
            labelName.Text = model.FileType.Title;
            filterControl.LoadModel(model);
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
            mainTab.Tabs["Buildings"].Visible = unitsBuildings.LoadModel(Model.BuildingEntities, filterControl.CurrentFilter);
            mainTab.Tabs["Infantry"].Visible = unitsInfantry.LoadModel(Model.InfantryEntities, filterControl.CurrentFilter);
            mainTab.Tabs["Vehicles"].Visible = unitsVehicles.LoadModel(Model.VehicleEntities, filterControl.CurrentFilter);
            mainTab.Tabs["Aircrafts"].Visible = unitsAircrafts.LoadModel(Model.AircraftEntities, filterControl.CurrentFilter);
            mainTab.Tabs["Projectiles"].Visible = unitsProjectiles.LoadModel(Model.ProjectileEntities, filterControl.CurrentFilter);
            mainTab.Tabs["Animations"].Visible = unitsAnimations.LoadModel(Model.AnimationEntities, filterControl.CurrentFilter);
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
            if (Model.RulesModel.UsePhobos)
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
                    ThemeManager.Instance.UseTheme(unitListConrol);
                    var hasEntries = unitListConrol.LoadModel(additionalGameEntities.Entities);
                    var isFiltered = !string.IsNullOrEmpty(_searchText);
                    if (hasEntries || !(_readonlyMode || isFiltered))
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
                        if (tabPhobos.SelectedTab == null)
                        {
                            tabPhobos.SelectedTab = additionalTab;
                        }
                        hasPhobos = true;
                    }
                }
            }
            mainTab.Tabs["Phobos"].Visible = hasPhobos;
            AnimationsAsyncLoader.Instance.Start();
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
                            ? entitiesTypesSection.KeyValues.Max(k => int.TryParse(k.Key, out var number) ? number : 0) + 1
                            : Model.FileType.BaseType == FileBaseType.Rules ? 1 : 900;
                        entitiesTypesSection.SetValue(typeKey.ToString(), newKey);
                    }
                    Model.ReloadGameEntites();
                }
            }
        }

        private void checkBoxPhobosShowEmpty_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var phobosTab in tabPhobos.Tabs)
            {
                phobosTab.Visible = checkBoxPhobosShowEmpty.Checked || (bool)phobosTab.Tag;
            }
        }

        private void filterControl_FilterChanged(object sender, EventArgs e)
        {
            LoadModels();
        }
    }
}
