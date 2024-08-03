using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;
using AnimatedGif;
using ImageMagick;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public static class ImageTools
    {
        public static AnimatedGifImage ToAnimatedGif(this List<Image> images, int frameDelay)
        {
            var finalStream = new MemoryStream();
            using (var gifCreator = new AnimatedGifCreator(finalStream))
            {
                foreach (var frameImage in images)
                {
                    gifCreator.AddFrame(frameImage, frameDelay);
                }
            }
            finalStream.Flush();
            finalStream.Seek(0, SeekOrigin.Begin);
            return new AnimatedGifImage(finalStream, Image.FromStream(finalStream));
        }

        public static MagickImage ToMagickImage(this Image image, bool disposeOriginal = false)
        {
            using (var bitmapStream = new MemoryStream())
            {
                image.Save(bitmapStream, ImageFormat.Png);
                if (disposeOriginal)
                {
                    image.Dispose();
                }
                bitmapStream.Seek(0, SeekOrigin.Begin);
                return new MagickImage(bitmapStream, MagickFormat.Png);
            }
        }

        public static Image ToImage(this MagickImage magickImage)
        {
            using (var bitmapStream = new MemoryStream())
            {
                magickImage.Write(bitmapStream, MagickFormat.Png);
                bitmapStream.Seek(0, SeekOrigin.Begin);
                return Image.FromStream(bitmapStream);
            }
        }

        public static Bitmap OverlayImage(this Image background, 
            Image overlay, 
            double autoStrechToFactor = 0d, 
            double fixZoomFactor = 0d)
        {
            var editBitmap = new Bitmap(background);
            var overlayHeight = Convert.ToDouble(overlay.Height);
            var overlayWidth = Convert.ToDouble(overlay.Width);
            var editHeight = Convert.ToDouble(editBitmap.Height);
            var editWidth = Convert.ToDouble(editBitmap.Width);
            if (fixZoomFactor > 0)
            {
                overlayHeight = overlayHeight * fixZoomFactor;
                overlayWidth = overlayWidth * fixZoomFactor;
            }
            else if (overlayWidth > editBitmap.Width || overlayHeight > editBitmap.Height)
            {
                var factor = (overlayWidth / editWidth > overlayHeight / editHeight)
                    ? overlayWidth / editWidth // fit width
                    : overlayHeight / editHeight; // fit height
                overlayHeight = overlayHeight / factor;
                overlayWidth = overlayWidth / factor;
            }
            else if (autoStrechToFactor > 0
                && overlayWidth < editWidth * autoStrechToFactor
                && overlayHeight < editHeight * autoStrechToFactor)
            {
                var factor = (overlayWidth / editWidth > overlayHeight / editHeight)
                    ? editWidth / overlayWidth * autoStrechToFactor // fit width
                    : editHeight / overlayHeight * autoStrechToFactor; // fit height
                overlayHeight = overlayHeight * factor;
                overlayWidth = overlayWidth * factor;
            }
            var overlayLeft = (editWidth - overlayWidth) / 2d;
            var overlayTop = (editHeight - overlayHeight) / 2d;
            using (var canvas = Graphics.FromImage(editBitmap))
            {
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.DrawImage(overlay,
                    new Rectangle(Convert.ToInt32(overlayLeft),
                        Convert.ToInt32(overlayTop),
                        Convert.ToInt32(overlayWidth),
                        Convert.ToInt32(overlayHeight)),
                    new Rectangle(0,
                        0,
                        overlay.Width,
                        overlay.Height),
                    GraphicsUnit.Pixel);
                canvas.Save();
                return editBitmap;
            }
        }

        public static Bitmap BrigthenUp(this Image original, int brigthnesPercent, bool disposeOriginal = false)
        {
            using (var bitmapStream = new MemoryStream())
            {
                original.Save(bitmapStream, ImageFormat.Bmp);
                if (disposeOriginal)
                {
                    original.Dispose();
                }
                bitmapStream.Seek(0, SeekOrigin.Begin);
                var bmpImage = new MagickImage(bitmapStream, MagickFormat.Bmp);
                bmpImage.Modulate(new Percentage(brigthnesPercent));
                using (var finalStream = new MemoryStream())
                {
                    bmpImage.Write(finalStream, MagickFormat.Png);
                    finalStream.Seek(0, SeekOrigin.Begin);
                    return new Bitmap(finalStream);
                }
            }
        }

        public class AnimatedGifImage : IDisposable
        {
            public AnimatedGifImage(Stream imageStream, Image image)
            {
                ImageStream = imageStream;
                Image = image;
            }

            public Stream ImageStream { get; }

            public Image Image { get; }

            public void Dispose()
            {
                ImageStream.Dispose();
                Image.Dispose();
            }
        }
    }
}
