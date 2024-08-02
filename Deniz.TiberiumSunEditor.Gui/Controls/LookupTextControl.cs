using Deniz.CCAudioPlayerCore;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Infragistics.Win.UltraWinGrid;
using System.Windows.Forms;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class LookupTextControl : UserControl
    {
        private IValueModel _valueModel = null!;
        private RootModel _rootModel = null!;
        private List<LookupTextValueModel>? _textValuesModel;
        private List<LookupMultiTextValueModel>? _multiTextValuesModel;
        private List<LookupSingleValueModel>? _singleValuesModel;
        private List<LookupMultiValueModel>? _multiValuesModel;
        private bool _doEvents;
        private UltraGridRow? _hoverRow;
        private List<GameEntityModel>? _lookupEntities;
        private AnimationRequirementToken? _popupAnimationAsyncLoadToken;

        public LookupTextControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? RefreshEntityValue;
        public event EventHandler<EventArgs>? SelectedValueChanged;

        public void LoadValues(RootModel rootModel, IValueModel valueModel, string? selfLookupType = null)
        {
            ClosePopup();
            _rootModel = rootModel;
            _valueModel = valueModel;
            valuesGrid.DataSource = null;
            _textValuesModel = null;
            _multiTextValuesModel = null;
            _singleValuesModel = null;
            _multiValuesModel = null;
            _doEvents = false;
            IEnumerable<string>? valueList = null;
            if (valueModel.ValueDefinition.ValueList != null)
            {
                valueList = valueModel.ValueDefinition.ValueList.Split(",");
            }
            else if (valueModel.ValueDefinition.LookupType != null
                     && _rootModel.LookupEntities.TryGetValue(valueModel.ValueDefinition.LookupType, out var lookupEntityModels))
            {
                _lookupEntities = lookupEntityModels;
            }
            else if (valueModel.ValueDefinition.ValueType != null)
            {
                valueList = _rootModel.Datastructure.ValueTypes
                    .FirstOrDefault(v => v.ValueType == valueModel.ValueDefinition.ValueType)
                    ?.Values.Split(",");
            }
            switch (valueModel.ValueDefinition.LookupType)
            {
                case "Animations":
                    valueList = rootModel.Animations;
                    break;
                case "MovementZones":
                    valueList = rootModel.MovementZones;
                    break;
                case "WeaponSounds":
                    valueList = rootModel.WeaponSounds;
                    break;
                case "WeaponProjectiles":
                    valueList = rootModel.WeaponProjectiles;
                    break;
                case "Houses":
                    valueList = rootModel.Houses;
                    break;
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
            else if (valueModel.ValueDefinition.LookupType != null)
            {
                var lookupType = valueModel.ValueDefinition.LookupType == "self"
                    ? selfLookupType
                    : valueModel.ValueDefinition.LookupType;
                if (lookupType == "WeaponTypes")
                {
                    // only when not Ares, otherwise use 'WeaponTypes' and map all 'Weapons' to 'WeaponTypes'
                    lookupType = "Weapons";
                }
                var lookupItems = rootModel.LookupItems.Where(e => e.EntityType == lookupType).ToList();
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
                            GetAllUniqueValues(entityValueModel.EntityModel.EntityType, valueModel.Key)
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
                            GetAllUniqueValues(entityValueModel.EntityModel.EntityType, valueModel.Key)
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

        private List<string> GetAllUniqueValues(string entityType, string valueKey)
        {
            var allSectionKeys = _rootModel.LookupItems
                .Where(l => l.EntityType == entityType)
                .Select(l => l.Key)
                .ToList();
            return allSectionKeys.Select(s =>
                    _rootModel.DefaultFile.GetSection(s)?.GetValue(valueKey)?.Value)
                .Where(v => !string.IsNullOrEmpty(v))
                .SelectMany(v => v!.Split(",", StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .OrderBy(v => v)
                .ToList();
        }

        private void LoadTextValuesGrid()
        {
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            valuesGrid.DataSource = _textValuesModel;
            var selectedIndex = _textValuesModel!.Select(v => v.Value).ToList()
                .IndexOf(_valueModel.Value);
            if (selectedIndex > -1)
            {
                var selectedRow = valuesGrid.Rows[selectedIndex];
                valuesGrid.Selected.Rows.Add(selectedRow);
                valuesGrid.ActiveRowScrollRegion.ScrollRowIntoView(selectedRow);
            }
            _doEvents = true;
        }

        private void LoadSingleValuesGrid()
        {
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            valuesGrid.DataSource = _singleValuesModel;
            var selectedIndex = _singleValuesModel!.Select(v => v.Key).ToList()
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

        private void LoadMultiTextValuesGrid()
        {
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.Default;
            valuesGrid.DataSource = _multiTextValuesModel?.OrderBy(v => v.Selected ? 0 : 1).ToList();
            _doEvents = true;
        }

        private void LoadMultiValuesGrid()
        {
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.Default;
            valuesGrid.DataSource = _multiValuesModel?.OrderBy(v => v.Selected ? 0 : 1).ToList();
            _doEvents = true;
        }

        private void PlaySelectedSound(string key)
        {
            switch (_valueModel.ValueDefinition.LookupType)
            {
                case "Sounds":
                case "WeaponSounds":
                    var audStream = CCGameRepository.Instance.GetAudioStream(key);
                    if (audStream != null)
                    {
                        AudioPlayerService.PlaySound(audStream);
                    }
                    break;
            }
        }

        private void OpenPopup(string key, RowUIElement hoverRow)
        {
            switch (_valueModel.ValueDefinition.LookupType)
            {
                case "Sounds":
                case "WeaponSounds":
                    var audStream = CCGameRepository.Instance.GetAudioStream(key);
                    if (audStream != null)
                    {
                        AudioPlayerService.PlaySound(audStream);
                    }
                    break;
                case "Animations":
                    ClosePopup();
                    if (!string.IsNullOrEmpty(key))
                    {
                        var animationImage = CCGameRepository.Instance.GetAnimationsImage(key);
                        if (animationImage != null)
                        {
                            var popupPosition = new Point(
                                hoverRow.RectInsideBorders.X + Width - 98,
                                hoverRow.RectInsideBorders.Y + hoverRow.Row.Height + 1);
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
                            hoverRow.RectInsideBorders.Y + hoverRow.Row.Height + 1);
                        var entityThumbnail = _lookupEntities.FirstOrDefault(e => e.EntityKey == key)?.Thumbnail;
                        if (entityThumbnail != null)
                        {
                            pictureThumbnail.Location = popupPosition;
                            pictureThumbnail.Visible = true;
                            if (entityThumbnail.Kind == ThumbnailKind.Animation)
                            {
                                var animationImage = CCGameRepository.Instance.GetAnimationsImage(key);
                                if (animationImage != null)
                                {
                                    pictureThumbnail.Image = animationImage;
                                }
                            }
                            else
                            {
                                pictureThumbnail.Image = entityThumbnail.Image;
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

    }
}
