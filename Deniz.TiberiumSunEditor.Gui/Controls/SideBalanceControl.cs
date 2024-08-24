using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class SideBalanceControl : UserControl
    {
        private RulesRootModel _rulesRootModel = null!;
        private GameEntityModel? _selectedSideEntityModel;
        private string? _compareEntityTypes;
        private bool _showStreangth;
        private bool _showPoints;
        private bool _showSpeed;
        private bool _showPower;
        private bool _showPrimaryDamage;
        private bool _showPrimaryRof;

        public SideBalanceControl()
        {
            InitializeComponent();
        }

        public event EventHandler? AfterEntityValueChanged;

        public void LoadModel(RulesRootModel rulesRootModel, int sideIndex)
        {
            _rulesRootModel = rulesRootModel;
            InitSidesCombo(sideIndex);
        }

        public void CompareEntities(string entityTypes,
            bool showStreangth,
            bool showPoints,
            bool showSpeed,
            bool showPower,
            bool showPrimaryDamage,
            bool showPrimaryRof)
        {
            _compareEntityTypes = entityTypes;
            _showStreangth = showStreangth;
            _showPoints = showPoints;
            _showSpeed = showSpeed;
            _showPower = showPower;
            _showPrimaryDamage = showPrimaryDamage;
            _showPrimaryRof = showPrimaryRof;
            LoadEntityValues();
        }

        public void RefreshGridCells()
        {
            valuesGrid.Rows.Refresh(RefreshRow.RefreshDisplay);
        }

        private void InitSidesCombo(int sideIndex)
        {
            foreach (var side in _rulesRootModel.SideEntities
                         .Where(SideHasAnyEntites)
                         .OrderBy(s => s.Thumbnail != null ? 0 : 1))
            {
                var valueListItem = ultraComboSide.Items.Add(side.EntityKey);
                var sideDefinition = _rulesRootModel.FileType.GameDefinition.Sides
                    .FirstOrDefault(d => d.Name.Equals(side.FileSection.GetValue("Side")?.Value
                                                       ?? side.DefaultSection?.GetValue("Side")?.Value
                                                       ?? side.EntityKey,
                        StringComparison.InvariantCultureIgnoreCase));
                if (sideDefinition != null)
                {
                    valueListItem.Appearance.Image = sideDefinition.GetLogoImage();
                }
            }
            if (ultraComboSide.Items.Count > sideIndex)
            {
                ultraComboSide.SelectedIndex = sideIndex;
            }
        }

        private bool SideHasAnyEntites(GameEntityModel sideEntityModel)
        {
            var entitiesTypeList = new[]
            {
                "BuildingTypes",
                "InfantryTypes",
                "VehicleTypes",
                "AircraftTypes"
            };
            return _rulesRootModel.LookupEntities.Any(l =>
                entitiesTypeList.Any(t => l.Key == t)
                && l.Value.Any(e => e.IsBuildableByHouse(sideEntityModel.EntityKey)));
        }

        private void LoadSide(GameEntityModel sideEntityModel)
        {
            _selectedSideEntityModel = sideEntityModel;
            LoadEntityValues();
        }

        private void LoadEntityValues()
        {
            if (_compareEntityTypes == null || _selectedSideEntityModel == null) return;
            var balanceRows = new List<BalanceRowValueModel>();
            var category = "";
            switch (_compareEntityTypes)
            {
                case "InfantryTypes":
                    category = "Infantry";
                    break;
                default:
                    category = $"{_compareEntityTypes.TrimStart("Types")}s";
                    break;
            }
            AddBalanceRows(balanceRows, _selectedSideEntityModel, _compareEntityTypes, category);
            valuesGrid.DataSource = null;
            valuesGrid.DataSource = balanceRows;
            if (valuesGrid.DisplayLayout.Bands[0].SortedColumns.Count == 0)
            {
                valuesGrid.DisplayLayout.Bands[0].SortedColumns.Add("Category", false, true);
            }
            valuesGrid.DisplayLayout.Bands[0].Columns["Key"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridHeaderBackColor;
            valuesGrid.DisplayLayout.Bands[0].Columns["Cost"].Format = "0";
            valuesGrid.DisplayLayout.Bands[0].Columns["Strength"].Format = "0";
            valuesGrid.DisplayLayout.Bands[0].Columns["Strength"].Hidden = !_showStreangth;
            valuesGrid.DisplayLayout.Bands[0].Columns["StrengthOverCost"].Format = "0.00";
            valuesGrid.DisplayLayout.Bands[0].Columns["StrengthOverCost"].Hidden = !_showStreangth;
            valuesGrid.DisplayLayout.Bands[0].Columns["StrengthOverCost"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridHeaderBackColor;
            valuesGrid.DisplayLayout.Bands[0].Columns["Points"].Format = "0";
            valuesGrid.DisplayLayout.Bands[0].Columns["Points"].Hidden = !_showPoints;
            valuesGrid.DisplayLayout.Bands[0].Columns["Speed"].Format = "0";
            valuesGrid.DisplayLayout.Bands[0].Columns["Speed"].Hidden = !_showSpeed;
            valuesGrid.DisplayLayout.Bands[0].Columns["SpeedOverCost"].Format = "0.00";
            valuesGrid.DisplayLayout.Bands[0].Columns["SpeedOverCost"].Hidden = !_showSpeed;
            valuesGrid.DisplayLayout.Bands[0].Columns["SpeedOverCost"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridHeaderBackColor;
            valuesGrid.DisplayLayout.Bands[0].Columns["Power"].Format = "0";
            valuesGrid.DisplayLayout.Bands[0].Columns["Power"].Hidden = !_showPower;
            valuesGrid.DisplayLayout.Bands[0].Columns["PrimaryWeapon"].Hidden = !_showPrimaryDamage && !_showPrimaryRof;
            valuesGrid.DisplayLayout.Bands[0].Columns["PrimaryWeapon"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridHeaderBackColor;
            valuesGrid.DisplayLayout.Bands[0].Columns["PrimaryDamage"].Format = "0";
            valuesGrid.DisplayLayout.Bands[0].Columns["PrimaryDamage"].Hidden = !_showPrimaryDamage;
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverCost"].Format = "0.00";
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverCost"].Hidden = !_showPrimaryDamage;
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverCost"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridHeaderBackColor;
            valuesGrid.DisplayLayout.Bands[0].Columns["PrimaryRof"].Format = "0";
            valuesGrid.DisplayLayout.Bands[0].Columns["PrimaryRof"].Hidden = !_showPrimaryRof;
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverRof"].Format = "0.00";
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverRof"].Hidden = !_showPrimaryRof;
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverRof"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridHeaderBackColor;
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverRofOverCost"].Format = "0.00";
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverRofOverCost"].Hidden = !_showPrimaryRof;
            valuesGrid.DisplayLayout.Bands[0].Columns["DamageOverRofOverCost"].CellAppearance.BackColor = ThemeManager.Instance.CurrentTheme.GridHeaderBackColor;
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["PrimaryWeapon"].Width = 90;
        }

        private void AddBalanceRows(List<BalanceRowValueModel> rows,
            GameEntityModel sideEntityModel,
            string entityType,
            string category)
        {
            if (_rulesRootModel.LookupEntities.TryGetValue(entityType, out var lookupEntities))
            {
                var valuesList = new List<BalanceTechnoValueModel>();
                foreach (var entityModel in lookupEntities.Where(e => e.IsBuildableByHouse(sideEntityModel.EntityKey)))
                {
                    var valueModel = new BalanceTechnoValueModel(entityModel, category);
                    valuesList.Add(valueModel);
                    rows.Add(valueModel);
                }
                if (valuesList.Count > 1)
                {
                    rows.Add(new BalanceSummaryValueModel(valuesList, category));
                }
            }
        }

        private void OpenPopup(GameEntityModel entityModel, RowUIElement hoverRow)
        {
            ClosePopup();
            var popupPosition = new Point(
                hoverRow.RectInsideBorders.X + 16,
                hoverRow.RectInsideBorders.Y + hoverRow.Row.Height + valuesGrid.Top + 1);
            var entityThumbnail = entityModel.Thumbnail;
            if (entityThumbnail != null)
            {
                pictureThumbnail.Location = popupPosition;
                if (entityThumbnail.Kind == ThumbnailKind.Animation)
                {
                    var animationImage = entityThumbnail.LoadAnimation();
                    if (animationImage != null)
                    {
                        pictureThumbnail.Image = animationImage;
                        pictureThumbnail.Visible = true;
                    }
                }
                else
                {
                    pictureThumbnail.Image = entityThumbnail.Image;
                    pictureThumbnail.Visible = true;
                }
            }
        }

        private void ClosePopup()
        {
            pictureThumbnail.Visible = false;
        }

        private void ultraComboSide_ValueChanged(object sender, EventArgs e)
        {
            var selectedSide =
                _rulesRootModel.SideEntities.FirstOrDefault(e =>
                    e.EntityKey == (string)ultraComboSide.SelectedItem.DataValue);
            if (selectedSide != null)
            {
                LoadSide(selectedSide);
            }
        }

        private void valuesGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.ListObject is BalanceSummaryValueModel)
            {
                e.Row.Activation = Activation.NoEdit;
            }
        }

        private void valuesGrid_MouseEnterElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
            if (e.Element is RowUIElement rowElement)
            {
                if (rowElement.Row.ListObject is BalanceTechnoValueModel technoValueModel)
                {
                    OpenPopup(technoValueModel.TechnoEntityModel, rowElement);
                }
            }
        }

        private void valuesGrid_MouseLeaveElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
            if (e.Element is RowUIElement)
            {
                ClosePopup();
            }
        }

        private void valuesGrid_AfterRowUpdate(object sender, RowEventArgs e)
        {
            RefreshGridCells();
            AfterEntityValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
