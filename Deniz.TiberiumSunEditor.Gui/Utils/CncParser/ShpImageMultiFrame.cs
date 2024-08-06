using Deniz.TiberiumSunEditor.Gui.Controls;

namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser;

public class ShpImageMultiFrame
{
    private readonly ShpFile _shpFile;
    private readonly byte[] _shpData;

    public ShpImageMultiFrame(List<Color> colorPalette, ShpFile shpFile, byte[] shpData)
    {
        _shpFile = shpFile;
        _shpData = shpData;
        ColorPalette = colorPalette;
    }

    public List<Color> ColorPalette { get; }

    public List<Bitmap> ToBitmapList(int maxFrames = 0)
    {
        var bitmaps = new List<Bitmap>();
        var frameCount = _shpFile.FrameCount;
        if (maxFrames > 0 && maxFrames < frameCount)
        {
            frameCount = maxFrames;
        }
        for (int i = 0; i < frameCount; i++)
        {
            var imageData = _shpFile.GetUncompressedFrameData(i, _shpData);
            var frameInfo = _shpFile.GetShpFrameInfo(i);

            var blankImage = ImageListComponent.Instance.Black150.Images[0];
            var bitmap = new Bitmap(blankImage, new Size(_shpFile.Width, _shpFile.Height));
            if (imageData != null)
            {
                var pixelIndex = 0;
                for (int y = 0; y < frameInfo.Height; y++)
                {
                    for (int x = 0; x < frameInfo.Width; x++)
                    {
                        bitmap.SetPixel(x + frameInfo.XOffset, frameInfo.YOffset + y, ColorPalette[imageData[pixelIndex]]);
                        pixelIndex++;
                    }
                }
            }
            bitmaps.Add(bitmap);
        }
        return bitmaps;
    }
}