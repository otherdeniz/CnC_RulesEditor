using System.Text.RegularExpressions;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex _colorRegex = new Regex(@"\d+,\s*\d+,\s*\d+", RegexOptions.Compiled);

        public static bool IsYesNo(this string text)
        {
            return text == "yes" || text == "no" || text.StartsWith("yes,");
        }

        public static bool IsTrueFalse(this string text)
        {
            return text == "true" || text == "false";
        }

        public static bool IsRgbColor(this string text)
        {
            return _colorRegex.IsMatch(text);
        }

        public static string TrimStart(this string text, string trimText)
        {
            if (text.StartsWith(trimText, StringComparison.InvariantCultureIgnoreCase))
            {
                return text.Substring(trimText.Length);
            }
            return text;
        }
    }
}
