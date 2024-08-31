using System.Text;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public static class HexGenerator
    {
        private static readonly char[] HexChars
            = { '0', '1', '2', '3', '4', '5',
                '6', '7', '8', '9', 'A', 'B',
                'C', 'D', 'E', 'F' };

        public static string GenerateRandomHex(int digits)
        {
            var hexText = new StringBuilder();
            var rand = new Random();
            for (int i = 0; i < digits; i++)
            {
                hexText.Append(HexChars[rand.Next() % 16]);
            }
            return hexText.ToString();
        }
    }
}
