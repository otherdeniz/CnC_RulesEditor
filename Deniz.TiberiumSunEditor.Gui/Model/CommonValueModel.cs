using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class CommonValueModel : IValueModel
    {
        private readonly IniFileLineKeyValue? _fileKeyValue;
        private readonly IniFile? _valueFile;
        private readonly string? _valueSection;
        private readonly string? _valueKey;
        private readonly string? _sectionOverwrite;
        private readonly string? _categoryOverwrite;

        public CommonValueModel(CommonValueDefinition commonValueDefinition,
            string description,
            IniFileLineKeyValue fileKeyValue,
            string defaultValue,
            string? sectionOverwrite = null,
            string? categoryOverwrite = null)
        {
            CommonValueDefinition = commonValueDefinition;
            _fileKeyValue = fileKeyValue;
            _sectionOverwrite = sectionOverwrite;
            _categoryOverwrite = categoryOverwrite;
            Description = description;
            DefaultValue = defaultValue;
        }

        public CommonValueModel(CommonValueDefinition commonValueDefinition,
            string description,
            IniFile valueFile,
            string valueSection,
            string valueKey,
            string defaultValue,
            string? categoryOverwrite = null)
        {
            CommonValueDefinition = commonValueDefinition;
            _valueFile = valueFile;
            _valueSection = valueSection;
            _valueKey = valueKey;
            _categoryOverwrite = categoryOverwrite;
            Description = description;
            DefaultValue = defaultValue;
        }

        [DisplayName(" ")]
        public Image FavoriteImage =>
            ImageListComponent.Instance.Favorite24.Images[Favorite ? 1 : 0];

        [Browsable(false)]
        public bool Favorite
        {
            get => UserSettingsFile.Instance.CommonValuesSettings.IsFavorite($"{IniSection}:{Key}");
            set
            {
                UserSettingsFile.Instance.CommonValuesSettings.SetFavorite($"{IniSection}:{Key}", value);
                UserSettingsFile.Instance.Save();
            }
        }

        [Browsable(false)]
        public CommonValueDefinition CommonValueDefinition { get; }

        [Browsable(false)] 
        public UnitValueDefinition ValueDefinition => CommonValueDefinition;

        [Browsable(false)] 
        public string IniSection => _sectionOverwrite ?? CommonValueDefinition.Section;

        public string Category => _categoryOverwrite ?? CommonValueDefinition.Category;

        public string Key => CommonValueDefinition.Key;

        [Browsable(false)]
        public bool HasChanges
        {
            get
            {
                if (_fileKeyValue != null)
                {
                    return _fileKeyValue.Value != DefaultValue;
                }

                var currentValue = _valueFile!.GetSection(_valueSection)?.GetValue(_valueKey!)?.Value;
                return !string.IsNullOrEmpty(currentValue)
                       && currentValue != DefaultValue;
            }
        }

        public string Value
        {
            get
            {
                if (_fileKeyValue != null)
                {
                    return _fileKeyValue.Value;
                }
                return _valueFile!.GetSection(_valueSection)?.GetValue(_valueKey!)?.Value ?? "";
            }
            set
            {
                if (_fileKeyValue != null)
                {
                    _fileKeyValue.Value = value;
                }
                else
                {
                    var iniSection = _valueFile!.GetSection(_valueSection) 
                                     ?? _valueFile!.AddSection(_valueSection!);
                    iniSection.SetValue(_valueKey!, value);
                }
            }
        }

        [DisplayName(" ")]
        public Image? UseDefaultImage =>
            !HasChanges || string.IsNullOrEmpty(DefaultValue)
                ? null
                : ImageListComponent.Instance.Arrows24.Images[0];

        [DisplayName("Normal")]
        public string DefaultValue { get; }

        public string Description { get; }

    }
}
