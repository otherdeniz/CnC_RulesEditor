using Deniz.CCAudioPlayerCore;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class LookupTextControl : UserControl
    {
        private readonly string[] _allTechoTypes = new string[]
            { "BuildingTypes", "InantryTypes", "VehicleTypes", "AircraftTypes" };
        private IValueModel _valueModel = null!;
        private RulesRootModel _rulesRootModel = null!;
        private List<LookupTextValueModel>? _textValuesModel;
        private List<LookupMultiTextValueModel>? _multiTextValuesModel;
        private List<LookupSingleValueModel>? _singleValuesModel;
        private List<LookupMultiValueModel>? _multiValuesModel;
        private bool _doEvents;
        private UltraGridRow? _hoverRow;
        private List<GameEntityModel>? _lookupEntities;
        private AnimationRequirementToken? _popupAnimationAsyncLoadToken;
        private string? _lookupType;

        public LookupTextControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? RefreshEntityValue;
        public event EventHandler<EventArgs>? SelectedValueChanged;

        public void LoadValues(RulesRootModel rulesRootModel, IValueModel valueModel, string? selfLookupType = null)
        {
            ClosePopup();
            _rulesRootModel = rulesRootModel;
            _valueModel = valueModel;
            valuesGrid.DataSource = null;
            _textValuesModel = null;
            _multiTextValuesModel = null;
            _singleValuesModel = null;
            _multiValuesModel = null;
            _doEvents = false;
            textBoxSearch.Text = string.Empty;
            IEnumerable<string>? valueList = null;
            if (valueModel.ValueDefinition.ValueList != null)
            {
                valueList = valueModel.ValueDefinition.ValueList.Split(",");
            }
            else if (valueModel.ValueDefinition.ValueType != null)
            {
                valueList = _rulesRootModel.Datastructure.ValueTypes
                    .FirstOrDefault(v => v.ValueType == valueModel.ValueDefinition.ValueType)
                    ?.Values.Split(",");
            }
            switch (valueModel.ValueDefinition.LookupType)
            {
                case "Animations":
                    valueList = rulesRootModel.Animations;
                    break;
                case "MovementZones":
                    valueList = rulesRootModel.MovementZones;
                    break;
                case "WeaponSounds":
                    valueList = rulesRootModel.WeaponSounds;
                    break;
                case "WeaponProjectiles":
                    valueList = rulesRootModel.WeaponProjectiles;
                    break;
                case "Houses":
                    valueList = rulesRootModel.Houses;
                    break;
            }
            _lookupType = valueModel.ValueDefinition.LookupType == "self"
                ? selfLookupType
                : valueModel.ValueDefinition.LookupType;
            if (_lookupType == "WeaponTypes")
            {
                // only when not Ares, otherwise use 'WeaponTypes' and map all 'Weapons' to 'WeaponTypes'
                _lookupType = "Weapons";
            }
            if (_lookupType == "WarheadTypes")
            {
                _lookupType = "Warheads";
            }
            if (_lookupType != null)
            {
                if (_rulesRootModel.LookupEntities.TryGetValue(_lookupType, out var lookupEntityModels))
                {
                    _lookupEntities = lookupEntityModels;
                }
                else if (_lookupType == "TechnoTypes")
                {
                    _lookupEntities = _rulesRootModel.LookupEntities
                        .Where(e => _allTechoTypes.Any(t => e.Key == t))
                        .SelectMany(e => e.Value)
                        .ToList();
                }
            }
            if (valueList != null)
            {
                if (valueModel.ValueDefinition.MultipleValues)
                {
                    var selectedTexts = valueModel.Value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                    _multiTextValuesModel = valueList
                        .Select(v => new LookupMultiTextValueModel { Selected = selectedTexts.Contains(v), Value = v })
                        .ToList();
                    LoadMultiTextValuesGrid();
                }
                else
                {
                    _textValuesModel = valueList
                        .Select(v => new LookupTextValueModel { Value = v })
                        .ToList();
                    LoadTextValuesGrid();
                }
            }
            else if (_lookupType != null)
            {
                var lookupItems = rulesRootModel.LookupItems
                    .Where(e => _lookupType == "TechnoTypes"
                        ? _allTechoTypes.Any(t => e.EntityType == t)
                        : e.EntityType == _lookupType)
                    .ToList();
                if (valueModel.ValueDefinition.MultipleValues)
                {
                    var selectedKeys = valueModel.Value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                    _multiValuesModel = lookupItems
                        .Select(v => new LookupMultiValueModel
                        {
                            Selected = selectedKeys.Contains(v.Key),
                            Key = v.Key,
                            Name = v.Name
                        })
                        .ToList();
                    if (valueModel is EntityValueModel entityValueModel)
                    {
                        _multiValuesModel.InsertRange(0,
                            _rulesRootModel.GetAllPossibleValues(entityValueModel.EntityModel.EntityType, valueModel.Key)
                                .Where(v => _multiValuesModel.All(l => !string.Equals(l.Key, v, StringComparison.CurrentCultureIgnoreCase)))
                                .Select(v => new LookupMultiValueModel
                                {
                                    Selected = selectedKeys.Contains(v),
                                    Key = v,
                                    Name = ""
                                }));
                    }
                    LoadMultiValuesGrid();
                }
                else
                {
                    _singleValuesModel = new List<LookupSingleValueModel>
                        {
                            new LookupSingleValueModel
                            {
                                Key = "",
                                Name = "[none]"
                            }
                        }.Union(lookupItems.Select(e => new LookupSingleValueModel
                        {
                            Key = e.Key,
                            Name = e.Name
                        }))
                        .ToList();
                    if (valueModel is EntityValueModel entityValueModel)
                    {
                        _singleValuesModel.InsertRange(1,
                            _rulesRootModel.GetAllPossibleValues(entityValueModel.EntityModel.EntityType, valueModel.Key)
                                .Where(v => _singleValuesModel.All(l => !string.Equals(l.Key, v, StringComparison.CurrentCultureIgnoreCase)))
                                .Select(v => new LookupSingleValueModel
                                {
                                    Key = v,
                                    Name = ""
                                }));
                    }
                    LoadSingleValuesGrid();
                }
            }
        }

        private void LoadTextValuesGrid(List<LookupTextValueModel>? filteredTextValuesModel = null)
        {
            _doEvents = false;
            var valuesList = filteredTextValuesModel ?? _textValuesModel!;
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            valuesGrid.DataSource = valuesList;
            valuesGrid.DisplayLayout.Bands[0].ScrollTipField = "Value";
            var selectedIndex = valuesList.Select(v => v.Value).ToList()
                .IndexOf(_valueModel.Value);
            if (selectedIndex > -1)
            {
                var selectedRow = valuesGrid.Rows[selectedIndex];
                valuesGrid.Selected.Rows.Add(selectedRow);
                valuesGrid.ActiveRowScrollRegion.ScrollRowIntoView(selectedRow);
            }
            _doEvents = true;
        }

        private void LoadSingleValuesGrid(List<LookupSingleValueModel>? filteredSingleValuesModel = null)
        {
            _doEvents = false;
            var valuesList = filteredSingleValuesModel ?? _singleValuesModel!;
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            valuesGrid.DataSource = valuesList;
            valuesGrid.DisplayLayout.Bands[0].ScrollTipField = "Key";
            var selectedIndex = valuesList!.Select(v => v.Key).ToList()
                .IndexOf(_valueModel.Value);
            if (selectedIndex > -1)
            {
                var selectedRow = valuesGrid.Rows[selectedIndex];
                valuesGrid.Selected.Rows.Add(selectedRow);
                valuesGrid.ActiveRowScrollRegion.ScrollRowIntoView(selectedRow);
                PlaySelectedSound(_valueModel.Value);
            }
            _doEvents = true;
        }

        private void LoadMultiTextValuesGrid(List<LookupMultiTextValueModel>? filteredMultiTextValuesModel = null)
        {
            _doEvents = false;
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.Default;
            valuesGrid.DataSource = (filteredMultiTextValuesModel ?? _multiTextValuesModel)?
                .OrderBy(v => v.Selected ? 0 : 1).ToList();
            valuesGrid.DisplayLayout.Bands[0].ScrollTipField = "Value";
            _doEvents = true;
        }

        private void LoadMultiValuesGrid(List<LookupMultiValueModel>? filteredMultiValuesModel = null)
        {
            _doEvents = false;
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.Default;
            var dataSource = (filteredMultiValuesModel ?? _multiValuesModel)?
                .OrderBy(v => v.Selected ? 0 : 1).ToList();
            valuesGrid.DataSource = dataSource;
            valuesGrid.DisplayLayout.Bands[0].ScrollTipField = "Key";
            var firstSelectedValue = dataSource?.FirstOrDefault(d => d.Selected);
            if (firstSelectedValue != null)
            {
                PlaySelectedSound(firstSelectedValue.Key);
            }
            _doEvents = true;
        }

        private void PlaySelectedSound(string key)
        {
            switch (_lookupType)
            {
                case "Sounds":
                case "WeaponSounds":
                    var audStream = CCGameRepository.Instance.GetAudioStream(key);
                    if (audStream != null)
                    {
                        AudioPlayerService.PlaySound(audStream);
                    }
                    else
                    {
                        CCGameRepository.Instance.TryPlayRaAudio(key);
                    }
                    break;
            }
        }

        private void OpenPopup(string key, RowUIElement hoverRow)
        {
            switch (_lookupType)
            {
                case "Sounds":
                case "WeaponSounds":
                    var audStream = CCGameRepository.Instance.GetAudioStream(key);
                    if (audStream != null)
                    {
                        AudioPlayerService.PlaySound(audStream);
                    }
                    else
                    {
                        CCGameRepository.Instance.TryPlayRaAudio(key);
                    }
                    break;
                case "Animations":
                case "ProjectileAnimations":
                    ClosePopup();
                    if (!string.IsNullOrEmpty(key))
                    {
                        var animationImage = CCGameRepository.Instance.GetAnimationsImage(key);
                        if (animationImage != null)
                        {
                            var popupPosition = new Point(
                                hoverRow.RectInsideBorders.X + Width - 98,
                                hoverRow.RectInsideBorders.Y + hoverRow.Row.Height + valuesGrid.Top + 1);
                            pictureThumbnail.Location = popupPosition;
                            pictureThumbnail.Image = animationImage;
                            pictureThumbnail.Visible = true;
                        }
                    }
                    break;
                default:
                    ClosePopup();
                    if (_lookupEntities != null)
                    {
                        var popupPosition = new Point(
                            hoverRow.RectInsideBorders.X + Width - 98,
                            hoverRow.RectInsideBorders.Y + hoverRow.Row.Height + valuesGrid.Top + 1);
                        var entityThumbnail = _lookupEntities.FirstOrDefault(e => e.EntityKey == key)?.Thumbnail;
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
                    break;
            }
        }

        private void ClosePopup()
        {
            pictureThumbnail.Visible = false;
            if (_popupAnimationAsyncLoadToken != null)
            {
                _popupAnimationAsyncLoadToken.StillNeeded = false;
                _popupAnimationAsyncLoadToken = null;
            }
        }

        private void valuesGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (!_doEvents || valuesGrid.Selected.Rows.Count == 0) return;
            if (_textValuesModel != null
                && valuesGrid.Selected.Rows[0].ListObject is LookupTextValueModel textValueModel)
            {
                _valueModel.Value = textValueModel.Value;
                SelectedValueChanged?.Invoke(this, EventArgs.Empty);
            }
            if (_singleValuesModel != null
                && valuesGrid.Selected.Rows[0].ListObject is LookupSingleValueModel singleValueModel)
            {
                _valueModel.Value = singleValueModel.Key;
                SelectedValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void valuesGrid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (!_doEvents) return;
            if (_multiTextValuesModel != null)
            {
                _valueModel.Value = string.Join(",",
                    _multiTextValuesModel.Where(v => v.Selected).Select(v => v.Value));
            }
            if (_multiValuesModel != null)
            {
                _valueModel.Value = string.Join(",",
                    _multiValuesModel.Where(v => v.Selected).Select(v => v.Key));
            }
            RefreshEntityValue?.Invoke(this, EventArgs.Empty);
        }

        private void valuesGrid_CellChange(object sender, CellEventArgs e)
        {
            valuesGrid.PerformAction(UltraGridAction.ExitEditMode);
        }

        private void valuesGrid_MouseEnterElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
            if (e.Element is CellUIElement cellElement)
            {
                if (cellElement.Column.Key == "Selected")
                {
                    if (_hoverRow != null)
                    {
                        _hoverRow.Appearance.BackColor = Color.White;
                        _hoverRow = null;
                        ClosePopup();
                    }
                    return;
                }
                if (_hoverRow == cellElement.Row) return;
                _hoverRow = cellElement.Row;
                _hoverRow.Appearance.BackColor = Color.DarkGray;
                if (_hoverRow.ListObject is ILookupValueModel lookupValueModel)
                {
                    OpenPopup(lookupValueModel.Value, (RowUIElement)_hoverRow.GetUIElement());
                }
            }
        }

        private void valuesGrid_MouseLeaveElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {

            if (e.Element is RowUIElement rowElement)
            {
                if (_hoverRow != null)
                {
                    _hoverRow.Appearance.BackColor = Color.White;
                    _hoverRow = null;
                    ClosePopup();
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBoxSearch.Text;
            if (_textValuesModel != null)
            {
                LoadTextValuesGrid(searchText == string.Empty
                    ? null
                    : _textValuesModel
                        .Where(v => v.Value.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                        .ToList());
            }
            else if (_singleValuesModel != null)
            {
                LoadSingleValuesGrid(searchText == string.Empty
                    ? null
                    : _singleValuesModel
                        .Where(v => v.Key.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                                    || v.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                        .ToList());
            }
            else if (_multiTextValuesModel != null)
            {
                LoadMultiTextValuesGrid(searchText == string.Empty
                    ? null
                    : _multiTextValuesModel
                        .Where(v => v.Value.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                        .ToList());
            }
            else if (_multiValuesModel != null)
            {
                LoadMultiValuesGrid(searchText == string.Empty
                    ? null
                    : _multiValuesModel
                        .Where(v => v.Key.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                                    || v.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                        .ToList());
            }
            buttonResetSearch.Enabled = searchText != string.Empty;
        }

        private void buttonResetSearch_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = string.Empty;
        }
    }
}
