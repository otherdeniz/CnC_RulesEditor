using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Extensions
{
    public static class ShpFileExtensions
    {
        public static Image? GetUnitAnimation(this ShpFile shpFile, 
            byte[] shpData, 
            List<Color> colorPalette, 
            int brightnesPercent,
            double zoomFactor)
        {
            var animationFrames = new List<Image>();
            if (shpFile.FrameCount > 1)
            {
                var blankImage = BitmapRepository.Instance.WhiteImage;
                animationFrames.Add(new Bitmap(blankImage, new Size(blankImage.Width, blankImage.Height)));

                var shpImage = new ShpImageMultiFrame(colorPalette, shpFile, shpData);
                foreach (var frameBitmap in shpImage.ToBitmapList(200))
                {
                    frameBitmap.MakeTransparent(colorPalette[0]);
                    var brigthBitmap = frameBitmap.BrigthenUp(brightnesPercent, true);
                    brigthBitmap.MakeTransparent(Color.Black);
                    animationFrames.Add(blankImage.OverlayImage(brigthBitmap, fixZoomFactor: zoomFactor));
                    brigthBitmap.Dispose();
                }
            }

            if (animationFrames.Count > 0)
            {
                return animationFrames.ToAnimatedGif(50).Image;
            }

            return null;
        }
    }
}
