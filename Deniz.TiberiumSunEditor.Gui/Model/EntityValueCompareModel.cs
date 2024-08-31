using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class EntityValueCompareModel
    {
        private readonly IniFileSection _targetSection;
        private readonly IniFileSection _sourceSection;

        public EntityValueCompareModel(string key, IniFileSection targetSection, IniFileSection sourceSection, string description)
        {
            _targetSection = targetSection;
            _sourceSection = sourceSection;
            Key = key;
            Description = description;
        }
        public string Key { get; }

        [DisplayName("Target Value")]
        public string TargetValue
        {
            get => _targetSection.GetValue(Key)?.Value ?? string.Empty;
            set => _targetSection.SetValue(Key, value);
        }

        [DisplayName(" ")]
        public Image? UseValueImage =>
            TargetValue == SourceValue
                ? null
                : ImageListComponent.Instance.Symbols24.Images[0];

        [DisplayName("Source Value")]
        public string SourceValue => _sourceSection.GetValue(Key)?.Value ?? string.Empty;

        public string Description { get; }

    }
}
