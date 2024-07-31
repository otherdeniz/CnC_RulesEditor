using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Extensions
{
    public static class IniFileExtensions
    {
        public static string GetNameValue(this IniFileSection section)
        {
            return section.GetValue("Name")?.Value ?? section.SectionName ?? "";
        }

        public static string GetHeaderDescription(this IniFileSection section)
        {
            return string.Join(" ", section.HeaderComments.Select(c => c.Comment));
        }
    }
}
