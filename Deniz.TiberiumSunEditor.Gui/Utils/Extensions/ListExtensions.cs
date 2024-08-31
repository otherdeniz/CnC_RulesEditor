using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Extensions
{
    public static class ListExtensions
    {
        public static List<CategorizedValueDefinition> ToCategorizedList(this List<UnitValueDefinition> sourceList)
        {
            return sourceList.Select(v => new CategorizedValueDefinition(v, v.Category)).ToList();
        }
    }
}
