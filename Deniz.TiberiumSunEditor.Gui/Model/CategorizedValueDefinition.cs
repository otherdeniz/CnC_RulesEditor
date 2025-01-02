using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class CategorizedValueDefinition
    {
        public static readonly List<CategorizedValueDefinition> EmptyList = new();

        public CategorizedValueDefinition(UnitValueDefinition unitValueDefinition, string category)
        {
            UnitValueDefinition = unitValueDefinition;
            Category = string.IsNullOrEmpty(unitValueDefinition.Category) 
                ? category
                : unitValueDefinition.Category;
        }

        public UnitValueDefinition UnitValueDefinition { get; }

        public string Category { get; }

    }
}
