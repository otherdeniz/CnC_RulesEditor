using Deniz.TiberiumSunEditor.Gui.Controls;

namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public class ShpImageSingleFrame
    {
        public ShpImageSingleFrame(List<Color> colorPalette, ShpFile shpFile, byte[] shpData, int frameIndex = 0)
        {
            ColorPalette = colorPalette;
            ImageData = shpFile.GetUncompressedFrameData(frameIndex, shpData);
            FrameInfo = shpFile.GetShpFrameInfo(frameIndex);
        }

        public List<Color> ColorPalette { get; }

        public byte[] ImageData { get; }

        public ShpFrameInfo FrameInfo { get; }

        public Bitmap ToBitmap()
        {
            var blankImage = ImageListComponent.Instance.Blank1.Images[0];
            var bitmap = new Bitmap(blankImage, new Size(FrameInfo.Width, FrameInfo.Height));
            var pixelIndex = 0;
            for (int y = 0; y < FrameInfo.Height; y++)
            {
                for (int x = 0; x < FrameInfo.Width; x++)
                {
                    bitmap.SetPixel(x, y, ColorPalette[ImageData[pixelIndex]]);
                    pixelIndex++;
                }
            }
            return bitmap;
        }
    }
}
