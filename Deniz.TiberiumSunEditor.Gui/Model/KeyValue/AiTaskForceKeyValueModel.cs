using System.ComponentModel;
using System.Text.RegularExpressions;
using Deniz.TiberiumSunEditor.Gui.Controls;

namespace Deniz.TiberiumSunEditor.Gui.Model.KeyValue
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
