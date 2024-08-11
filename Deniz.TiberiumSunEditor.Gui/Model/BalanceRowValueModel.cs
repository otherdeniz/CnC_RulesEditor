using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class BalanceRowValueModel
    {
        public BalanceRowValueModel(string key, string category)
        {
            Key = key;
            Category = category;
        }

        public string Category { get; }

        public string Key { get; }

        public virtual double? Cost { get; set; }

        public virtual double? Strength { get; set; }

        [DisplayName("Strength/Cost")]
        public virtual double? StrengthOverCost => null;

        public virtual double? Points { get; set; }

        public virtual double? Speed { get; set; }

        [DisplayName("Speed/Cost")]
        public virtual double? SpeedOverCost => null;

        public virtual double? Power { get; set; }

        [DisplayName("Prim.Weapon")]
        public virtual string PrimaryWeapon => string.Empty;

        [DisplayName("Prim.Damage")]
        public virtual double? PrimaryDamage { get; set; }

        [DisplayName("Damage/Cost")]
        public virtual double? DamageOverCost => null;

        [DisplayName("Prim.ROF")]
        public virtual double? PrimaryRof { get; set; }

        [DisplayName("Damage/ROF*15")]
        public virtual double? DamageOverRof => null;

        [DisplayName("Damage/ROF*15/Cost*100")]
        public virtual double? DamageOverRofOverCost => null;
    }
}
