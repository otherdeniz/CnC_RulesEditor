using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class BalanceTechnoValueModel : BalanceRowValueModel
    {
        private readonly GameEntityModel _technoEntityModel;

        public BalanceTechnoValueModel(GameEntityModel technoEntityModel, string category)
            : base(technoEntityModel.EntityKey, category)
        {
            _technoEntityModel = technoEntityModel;
        }

        [Browsable(false)]
        public GameEntityModel TechnoEntityModel => _technoEntityModel;

        public override double? Cost
        {
            get => GetTechnoValue("Cost");
            set => SetTechnoValue("Cost", value);
        }

        public override double? Strength
        {
            get => GetTechnoValue("Strength");
            set => SetTechnoValue("Strength", value);
        }

        public override double? Points
        {
            get => GetTechnoValue("Points");
            set => SetTechnoValue("Points", value);
        }

        public override double? Speed
        {
            get => GetTechnoValue("Speed");
            set => SetTechnoValue("Speed", value);
        }

        public override double? Power
        {
            get => GetTechnoValue("Power");
            set => SetTechnoValue("Power", value);
        }

        public override double? PrimaryDamage
        {
            get => GetTechnoRelationValue("Primary", "Damage");
            set => SetTechnoRelationValue("Primary", "Damage", value);
        }

        public override double? PrimaryRof
        {
            get => GetTechnoRelationValue("Primary", "ROF");
            set => SetTechnoRelationValue("Primary", "ROF", value);
        }

        public override string PrimaryWeapon => GetTechnoTextValue("Primary") ?? string.Empty;

        public override double? StrengthOverCost => GetValueOverValue("Strength", "Cost");

        public override double? SpeedOverCost => GetValueOverValue("Speed", "Cost");

        public override double? DamageOverCost => GetRealtedValueOverValue("Primary", "Damage", "Cost");

        public override double? DamageOverRof => GetRealtedValueOverRelatedValue("Primary", "Damage", "ROF", 15);

        public override double? DamageOverRofOverCost => 
            GetNumberOverValue(GetRealtedValueOverRelatedValue("Primary", "Damage", "ROF", 15), "Cost", 100);

        private string? GetTechnoTextValue(string valueKey)
        {
            return (_technoEntityModel.FileSection.GetValue(valueKey)
                                    ?? _technoEntityModel.DefaultSection?.GetValue(valueKey)) // for Maps
                ?.Value;
        }

        private double? GetTechnoValue(string valueKey)
        {
            return double.TryParse((_technoEntityModel.FileSection.GetValue(valueKey)
                                    ?? _technoEntityModel.DefaultSection?.GetValue(valueKey)) // for Maps
                                    ?.Value, out var numberValue)
                ? numberValue
                : null;
        }

        private void SetTechnoValue(string valueKey, double? value)
        {
            _technoEntityModel.FileSection.SetValue(valueKey, value?.ToString("0") ?? string.Empty);
        }

        private double? GetTechnoRelationValue(string relationKey, string valueKey)
        {
            var relationSectionKey = _technoEntityModel.FileSection.GetValue(relationKey)?.Value;
            if (relationSectionKey == null) return null;
            var relationSection = _technoEntityModel.RootModel.File.GetSection(relationSectionKey)
                ?? _technoEntityModel.RootModel.DefaultFile.GetSection(relationSectionKey); // for Maps
            return double.TryParse(relationSection?.GetValue(valueKey)?.Value, out var numberValue)
                ? numberValue
                : null;
        }

        private void SetTechnoRelationValue(string relationKey, string valueKey, double? value)
        {
            var relationSectionKey = _technoEntityModel.FileSection.GetValue(relationKey)?.Value;
            if (relationSectionKey == null) return;
            var relationSection = _technoEntityModel.RootModel.File.GetSection(relationSectionKey)
                                  ?? _technoEntityModel.RootModel.File.AddSection(relationSectionKey); // for Maps
            relationSection.SetValue(valueKey, value?.ToString("0") ?? string.Empty);
        }

        private double? GetValueOverValue(string valueKey, string overValueKey)
        {
            return GetNumberOverValue(double.TryParse(_technoEntityModel.FileSection.GetValue(valueKey)?.Value, out var intOverNumber)
                ? (double?)intOverNumber : null, overValueKey);
        }

        private double? GetNumberOverValue(double? number, string overValueKey, double multiplier = 1)
        {
            var numberOverValue = double.TryParse(_technoEntityModel.FileSection.GetValue(overValueKey)?.Value, out var intOverNumber)
                ? (double?)intOverNumber : null;
            if (number > 0 && numberOverValue > 0)
            {
                return (number.Value / numberOverValue.Value * multiplier);
            }
            return null;
        }

        private double? GetRealtedValueOverValue(string relationKey, string valueKey, string overValueKey)
        {
            var relationSectionKey = _technoEntityModel.FileSection.GetValue(relationKey)?.Value;
            if (relationSectionKey == null) return null;
            var relationSection = _technoEntityModel.RootModel.File.GetSection(relationSectionKey)
                                  ?? _technoEntityModel.RootModel.File.AddSection(relationSectionKey); // for Maps
            var numberValue = double.TryParse(relationSection.GetValue(valueKey)?.Value, out var intNumber)
                ? (double?)intNumber : null;
            var numberOverValue = double.TryParse(_technoEntityModel.FileSection.GetValue(overValueKey)?.Value, out var intOverNumber)
                ? (double?)intOverNumber : null;
            if (numberValue > 0 && numberOverValue > 0)
            {
                return (numberValue.Value / numberOverValue.Value);
            }
            return null;
        }

        private double? GetRealtedValueOverRelatedValue(string relationKey, string valueKey, string overValueKey, double multiplier = 1)
        {
            var relationSectionKey = _technoEntityModel.FileSection.GetValue(relationKey)?.Value;
            if (relationSectionKey == null) return null;
            var relationSection = _technoEntityModel.RootModel.File.GetSection(relationSectionKey)
                                  ?? _technoEntityModel.RootModel.File.AddSection(relationSectionKey); // for Maps
            var numberValue = double.TryParse(relationSection.GetValue(valueKey)?.Value, out var intNumber)
                ? (double?)intNumber : null;
            var numberOverValue = double.TryParse(relationSection.GetValue(overValueKey)?.Value, out var intOverNumber)
                ? (double?)intOverNumber : null;
            if (numberValue > 0 && numberOverValue > 0)
            {
                return (numberValue.Value / numberOverValue.Value * multiplier);
            }
            return null;
        }

    }
}
