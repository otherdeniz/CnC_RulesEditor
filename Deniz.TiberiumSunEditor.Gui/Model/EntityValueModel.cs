using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Infragistics.Win.Printing;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class EntityValueModel : IValueModel
    {
        private readonly IniFileSection _fileSection;
        private readonly IniFileSection? _defaultSection;
        private string? _resolvedValue;

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
            Category = valueDefinition.ModuleCategory + category;
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

        [DisplayName("Value")]
        public string ValueResolved
        {
            get
            {
                var valueResolved = Value;
                if (string.IsNullOrEmpty(valueResolved))
                {
                    valueResolved = ResolveBaseValue(_fileSection);
                }
                return valueResolved;
            }
            set => Value = value;
        }

        [Browsable(false)]
        public bool IsValueResolved => ValueResolved != Value;

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

        [DisplayName("Initial (absolute)")]
        public string NormalValue { get; }

        [DisplayName("Default")] 
        public string DefaultValue => ValueDefinition.Default;

        public string Description => ValueDefinition.Description ?? "";

        [Browsable(false)]
        public bool IsColorValue => Key.EndsWith("Color") && Value.IsRgbColor();

        [Browsable(false)]
        public UnitValueDefinition ValueDefinition { get; set; }

        [Browsable(false)]
        public GameEntityModel EntityModel { get; }

        [Browsable(false)] 
        public IniFileLineKeyValue? FileKeyValue => _fileSection.GetValue(Key);

        [Browsable(false)]
        public bool IsModified => _fileSection.GetValue(Key) != null
                                  && !string.Equals(Value, NormalValue, StringComparison.InvariantCultureIgnoreCase);

        private string ResolveBaseValue(IniFileSection thisFileSection)
        {
            if (_resolvedValue != null)
            {
                return _resolvedValue;
            }

            if (EntityModel.RulesRootModel.UseSectionInheritance)
            {
                var baseSectionValue = thisFileSection.GetValue("BaseSection")
                                       ?? EntityModel.RootModel.DefaultFile.GetSection(thisFileSection.SectionName)?.GetValue("BaseSection");
                if (!string.IsNullOrEmpty(baseSectionValue?.Value))
                {
                    baseSectionValue.ValueChanged += (sender, args) => _resolvedValue = null;
                    var baseSection = EntityModel.RootModel.File.GetSection(baseSectionValue.Value)
                                      ?? EntityModel.RootModel.DefaultFile.GetSection(baseSectionValue.Value);
                    if (baseSection != null)
                    {
                        var baseValue = baseSection.GetValue(Key);
                        if (baseValue != null)
                        {
                            _resolvedValue = baseValue.Value;
                            baseValue.ValueChanged += (sender, args) => _resolvedValue = null;
                        }
                        else
                        {
                            return ResolveBaseValue(baseSection);
                        }
                    }
                    else
                    {
                        _resolvedValue = string.Empty;
                    }
                }
            }
            else if (EntityModel.RulesRootModel.UsePhobosSectionInheritance)
            {
                var inheritsValue = thisFileSection.GetValue("$Inherits")
                                       ?? EntityModel.RootModel.DefaultFile.GetSection(thisFileSection.SectionName)?.GetValue("$Inherits");
                if (!string.IsNullOrEmpty(inheritsValue?.Value))
                {
                    inheritsValue.ValueChanged += (sender, args) => _resolvedValue = null;
                    var inheritsFromList = inheritsValue.Value.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToList();
                    //depth-first
                    var digDeeper = new List<IniFileSection>();
                    foreach (var inheritsFrom in inheritsFromList)
                    {
                        var baseSection = EntityModel.RootModel.File.GetSection(inheritsFrom)
                                          ?? EntityModel.RootModel.DefaultFile.GetSection(inheritsFrom);
                        if (baseSection != null)
                        {
                            var baseValue = baseSection.GetValue(Key);
                            if (baseValue != null)
                            {
                                _resolvedValue = baseValue.Value;
                                baseValue.ValueChanged += (sender, args) => _resolvedValue = null;
                                return _resolvedValue;
                            }
                            digDeeper.Add(baseSection);
                        }
                        else
                        {
                            // base section could not be resolved, inheritance resolving canceled
                            _resolvedValue = string.Empty;
                            return string.Empty;
                        }
                    }
                    foreach (var deeperSection in digDeeper)
                    {
                        var deeperResolvedValue = ResolveBaseValue(deeperSection);
                        if (!string.IsNullOrEmpty(deeperResolvedValue))
                        {
                            return deeperResolvedValue;
                        }
                    }
                }
            }

            return _resolvedValue ?? string.Empty;
        }

    }
}
