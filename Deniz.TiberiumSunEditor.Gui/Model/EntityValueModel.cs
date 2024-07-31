using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class EntityValueModel : IValueModel
    {
        private readonly IniFileSection _fileSection;
        private readonly IniFileSection? _defaultSection;

        public EntityValueModel(GameEntityModel entityModel,
            string category,
            IniFileSection fileSection,
            IniFileSection? defaultSection,
            string key,
            UnitValueDefinition valueDefinition)
        {
            _fileSection = fileSection;
            _defaultSection = defaultSection;
            EntityModel = entityModel;
            Category = category;
            Key = key;
            ValueDefinition = valueDefinition;
            NormalValue = _defaultSection?.GetValue(key)?.Value ?? "";
        }

        [DisplayName(" ")]
        public Image FavoriteImage =>
            ImageListComponent.Instance.Favorite24.Images[Favorite ? 1 : 0];

        [Browsable(false)]
        public bool Favorite
        {
            get => UserSettingsFile.Instance.UnitValuesSettings.IsFavorite(Key);
            set
            {
                UserSettingsFile.Instance.UnitValuesSettings.SetFavorite(Key, value);
                UserSettingsFile.Instance.Save();
            }
        }

        public string Category { get; }

        public string Key { get; }

        public string Value
        {
            get => _fileSection.GetValue(Key)?.Value ?? "";
            set
            {
                if (value == NormalValue && _defaultSection == null)
                {
                    // remove value, its the blank default
                    _fileSection.SetValue(Key, "");
                }
                else
                {
                    _fileSection.SetValue(Key, value);
                }
            }
        }

        [DisplayName(" ")]
        public Image? UseNormalImage =>
            Value == NormalValue
                ? null
                : ImageListComponent.Instance.Arrows24.Images[0];

        [DisplayName("Normal")]
        public string NormalValue { get; }

        [DisplayName("Default")] 
        public string DefaultValue => ValueDefinition.Default;

        public string Description => ValueDefinition.Description ?? "";

        [Browsable(false)]
        public UnitValueDefinition ValueDefinition { get; }

        [Browsable(false)]
        public GameEntityModel EntityModel { get; }

        [Browsable(false)] 
        public IniFileLineKeyValue? FileKeyValue => _fileSection.GetValue(Key);

        [Browsable(false)]
        public bool IsModified => _fileSection.GetValue(Key) != null
                                  && !string.Equals(Value, NormalValue, StringComparison.InvariantCultureIgnoreCase);
    }
}
