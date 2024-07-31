using System.Drawing.Drawing2D;
using System.Numerics;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class BitmapRepository
    {
        private static BitmapRepository? _instance;
        public static BitmapRepository Instance => _instance ??= new BitmapRepository();

        private readonly Dictionary<string, Image?> _bitmapsCache = new();
        private readonly string _bitmapPath;
        private readonly Dictionary<string, string> _bitmapFiles = new();
        private readonly Dictionary<string, string> _overlayFiles = new();

        public BitmapRepository()
        {
            _bitmapPath = Path.Combine(Application.StartupPath, "Bitmaps");
            BlankImage = Image.FromFile(Path.Combine(_bitmapPath, "_BLANK.bmp"));
            WhiteImage = Image.FromFile(Path.Combine(_bitmapPath, "_WHITE.bmp"));
        }

        public Image BlankImage { get; }

        public Image WhiteImage { get; }

        public void Initialise(IEnumerable<string> bitmapSubFolders)
        {
            _bitmapsCache.Clear();
            _bitmapFiles.Clear();
            _overlayFiles.Clear();
            ReadDirectory();
            foreach (var bitmapSubFolder in bitmapSubFolders)
            {
                ReadDirectory(bitmapSubFolder);
            }
        }

        public Image? GetBitmap(string key)
        {
            if (_bitmapsCache.TryGetValue(key, out var bitmap))
            {
                return bitmap;
            }

            if (_bitmapFiles.TryGetValue(key.ToUpper(), out var bitmapFile))
            {
                var loadedImage = Image.FromFile(bitmapFile);
                _bitmapsCache.Add(key, loadedImage);
                return loadedImage;
            }

            if (_overlayFiles.TryGetValue(key.ToUpper(), out var overlayFile))
            {
                var loadedOverlay = Image.FromFile(overlayFile);
                var editBitmap = OverlayImage(loadedOverlay);
                _bitmapsCache.Add(key, editBitmap);
                return editBitmap;
            }

            _bitmapsCache.Add(key, null);
            return null;
        }

        public Image OverlayImage(Image overlay, double scaleUp = 1d)
        {
            var editBitmap = new Bitmap(BlankImage);
            var overlayHeight = Convert.ToDouble(overlay.Height);
            var overlayWidth = Convert.ToDouble(overlay.Width);
            var editHeight = Convert.ToDouble(editBitmap.Height);
            var editWidth = Convert.ToDouble(editBitmap.Width);
            if (overlayWidth > editBitmap.Width || overlayHeight > editBitmap.Height)
            {
                var factor = (overlayWidth / editWidth > overlayHeight / editHeight)
                    ? overlayWidth / editWidth // fit width
                    : overlayHeight / editHeight; // fit height
                overlayHeight = overlayHeight / factor;
                overlayWidth = overlayWidth / factor;
            }
            if (scaleUp > 1)
            {
                overlayHeight = overlayHeight * scaleUp;
                overlayWidth = overlayWidth * scaleUp;
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

        private void ReadDirectory(string? bitmapSubFolder = null)
        {
            var searchPath = bitmapSubFolder == null
                ? _bitmapPath
                : Path.Combine(_bitmapPath, bitmapSubFolder);
            if (!Directory.Exists(searchPath)) return;
            foreach (var bitmapFile in Directory.GetFiles(searchPath, "*.bmp"))
            {
                var bitmapName = Path.GetFileNameWithoutExtension(bitmapFile).ToUpper();
                if (!_bitmapFiles.ContainsKey(bitmapName))
                {
                    _bitmapFiles.Add(bitmapName, bitmapFile);
                }
            }

            var overlayPath = Path.Combine(searchPath, "Overlay");
            if (!Directory.Exists(overlayPath)) return;
            foreach (var overlayFile in Directory.GetFiles(overlayPath, "*.png"))
            {
                var overlayName = Path.GetFileNameWithoutExtension(overlayFile).ToUpper();
                if (!_overlayFiles.ContainsKey(overlayName))
                {
                    _overlayFiles.Add(overlayName, overlayFile);
                }
            }
        }
    }
}
