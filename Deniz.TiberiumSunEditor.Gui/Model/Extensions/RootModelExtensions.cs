using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;

namespace Deniz.TiberiumSunEditor.Gui.Model.Extensions
{
    public static class RootModelExtensions
    {
        public static List<string> GetAllPossibleValues(this IRootModel rootModel, 
            string searchEntityType, string valueKey, bool orderByName = true)
        {
            return GetAllPossibleValues(rootModel, new List<string> { searchEntityType }, valueKey, orderByName);
        }

        public static List<string> GetAllPossibleValues(this IRootModel rootModel,
            List<string> searchEntityTypes, string valueKey, bool orderByName = true)
        {
            var allSectionKeys = rootModel.LookupItems
                .Where(l => searchEntityTypes.Contains(l.EntityType, StringEqualityComparer.Instance))
                .Select(l => l.Key)
                .ToList();
            var distinctValues =
                allSectionKeys.Select(s =>
                        rootModel.File.GetSection(s)?.GetValue(valueKey)?.Value)
                    .Where(v => !string.IsNullOrEmpty(v))
                    .SelectMany(v => v!.Split(",", StringSplitOptions.RemoveEmptyEntries))
                    .Union(
                        allSectionKeys.Select(s =>
                                rootModel.DefaultFile.GetSection(s)?.GetValue(valueKey)?.Value)
                            .Where(v => !string.IsNullOrEmpty(v))
                            .SelectMany(v => v!.Split(",", StringSplitOptions.RemoveEmptyEntries))
                    )
                    .Distinct();
            return orderByName
                ? distinctValues.OrderBy(s => s).ToList()
                : distinctValues.ToList();
        }

    }
}
