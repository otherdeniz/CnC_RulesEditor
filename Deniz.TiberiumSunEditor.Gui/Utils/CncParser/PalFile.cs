namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public class PalFile
    {
        public static PalFile ReadFromFile(string fileName)
        {
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                return ReadFromStream(fileStream);
            }
        }

        public static PalFile ReadFromFile(byte[] fileBytes)
        {
            using (var fileStream = new MemoryStream(fileBytes))
            {
                return ReadFromStream(fileStream);
            }
        }

        public static PalFile ReadFromStream(Stream fileStream)
        {
            var colors = new List<Color>();
            do
            {
                var r = fileStream.ReadByte();
                var g = fileStream.ReadByte();
                var b = fileStream.ReadByte();
                colors.Add(Color.FromArgb(r,g,b));
            } while (fileStream.Position < fileStream.Length - 1);

            return new PalFile(colors);
        }

        public PalFile(List<Color> colors)
        {
            Colors = colors;
        }

        public List<Color> Colors { get; }
    }
}
