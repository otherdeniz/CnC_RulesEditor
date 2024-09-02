using System.ComponentModel;
using System.Text.RegularExpressions;
using Deniz.TiberiumSunEditor.Gui.Controls;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class AiTaskForceKeyValueModel
    {
        private static readonly Regex KeyValueRegex = new Regex(@"(\d+),(\w+)", RegexOptions.Compiled);
        private string? _currentKeyValue;
        private int? _count;
        private string _unitKey = string.Empty;
        private readonly Action _valueChangedAction;

        public AiTaskForceKeyValueModel(GameEntityModel entityModel, string key, Action valueChangedAction)
        {
            EntityModel = entityModel;
            Key = key;
            UnitPicture = SmallUnitPictureGenerator.Instance.GetUnitPicture(null);
            _valueChangedAction = valueChangedAction;
            Read();
        }

        [Browsable(false)]
        public GameEntityModel EntityModel { get; }

        public string Key { get; }

        public int? Count
        {
            get
            {
                Read();
                return _count;
            }
            set
            {
                _count = value;
                Write();
            }
        }

        [DisplayName(" ")]
        public Image? PlusImage =>
            _count == null
                ? null
                : ImageListComponent.Instance.Symbols16.Images[2];

        [DisplayName(" ")]
        public Image? MinusImage =>
            _count == null || _count <= 1
                ? null
                : ImageListComponent.Instance.Symbols16.Images[3];

        [Browsable(false)]
        public string UnitKey
        {
            get
            {
                Read();
                return _unitKey;
            }
            set
            {
                _unitKey = value;
                if (_count == null && !string.IsNullOrEmpty(_unitKey))
                {
                    _count = 1;
                }
                Write();
            }
        }

        [DisplayName("Unit")]
        public Image UnitPicture { get; private set; }

        [DisplayName("Cost (sum)")]
        public string? Cost
        {
            get
            {
                var costValue = UnitModel?.EntityValueList.FirstOrDefault(v => v.Key == "Cost")?.ValueResolved;
                if (_count != null && int.TryParse(costValue, out var costNumber))
                {
                    return (_count.Value * costNumber).ToString("#,##0");
                }
                return costValue;
            }
        }

        public string? Speed => UnitModel?.EntityValueList.FirstOrDefault(v => v.Key == "Speed")?.ValueResolved;

        [DisplayName(" ")]
        public Image? DeleteImage =>
            Key == "0" || UnitKey == string.Empty
                ? null
                : ImageListComponent.Instance.Symbols16.Images[1];

        [Browsable(false)]
        public GameEntityModel? UnitModel { get; private set; }

        private void Read()
        {
            var keyValue = EntityModel.FileSection.GetValue(Key);
            if (_currentKeyValue == keyValue?.Value) return;
            _currentKeyValue = keyValue?.Value;
            if (keyValue != null)
            {
                var valueMatch = KeyValueRegex.Match(keyValue.Value);
                if (valueMatch.Success)
                {
                    _count = int.Parse(valueMatch.Groups[1].Value);
                    _unitKey = valueMatch.Groups[2].Value;
                    UnitModel = EntityModel.RulesRootModel.LookupEntities.Values.SelectMany(e => e)
                        .FirstOrDefault(e => e.EntityKey == _unitKey);
                    UnitPicture = SmallUnitPictureGenerator.Instance.GetUnitPicture(UnitModel);
                    return;
                }
            }
            _count = null;
            _unitKey = string.Empty;
            UnitModel = null;
            UnitPicture = SmallUnitPictureGenerator.Instance.GetUnitPicture(null);
        }

        private void Write()
        {
            if (_count != null && _unitKey != string.Empty)
            {
                EntityModel.FileSection.SetValue(Key, $"{_count},{_unitKey}");
            }
            else
            {
                EntityModel.FileSection.SetValue(Key, string.Empty);
            }
            _valueChangedAction.Invoke();
        }

    }
}
