using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class BalanceSummaryValueModel : BalanceRowValueModel
    {
        private readonly List<BalanceTechnoValueModel> _values;

        public BalanceSummaryValueModel(List<BalanceTechnoValueModel> values, string category)
            : base("Average", $"{category} Summary")
        {
            TechnoCategory = category;
            _values = values;
        }

        [Browsable(false)]
        public string TechnoCategory { get; }

        public override double? Cost
        {
            get => GetAverage(v => v.Cost);
            set { /* read only */}
        }

        public override double? Strength
        {
            get => GetAverage(v => v.Strength);
            set { /* read only */}
        }

        public override double? Points
        {
            get => GetAverage(v => v.Points);
            set { /* read only */}
        }

        public override double? Speed
        {
            get => GetAverage(v => v.Speed);
            set { /* read only */}
        }

        public override double? Power
        {
            get => GetAverage(v => v.Power, false);
            set { /* read only */}
        }

        public override double? PrimaryDamage
        {
            get => GetAverage(v => v.PrimaryDamage);
            set { /* read only */}
        }

        public override double? PrimaryRof
        {
            get => GetAverage(v => v.PrimaryRof);
            set { /* read only */}
        }

        public override double? StrengthOverCost => GetAverage(v => v.StrengthOverCost);

        public override double? SpeedOverCost => GetAverage(v => v.SpeedOverCost);

        public override double? DamageOverCost => GetAverage(v => v.DamageOverCost);

        public override double? DamageOverRof => GetAverage(v => v.DamageOverRof);

        public override double? DamageOverRofOverCost => GetAverage(v => v.DamageOverRofOverCost);

        private double? GetAverage(Func<BalanceTechnoValueModel, double?> valueProperty, bool positiveValues = true)
        {
            var numberValues = _values
                .Select(valueProperty)
                .Where(i => i.HasValue && positiveValues ? i > 0 : i < 0)
                .Select(i => i!.Value)
                .ToList();
            return numberValues.Any()
                ? numberValues.Average()
                : null;
        }

    }
}
