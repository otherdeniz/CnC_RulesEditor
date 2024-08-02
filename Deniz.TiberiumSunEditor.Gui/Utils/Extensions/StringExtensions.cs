namespace Deniz.TiberiumSunEditor.Gui.Utils.Extensions
{
    public static class StringExtensions
    {
        public static bool IsYesNo(this string text)
        {
            return text == "yes" || text == "no" || text.StartsWith("yes,");
        }

        public static bool IsTrueFalse(this string text)
        {
            return text == "true" || text == "false";
        }
    }
}
