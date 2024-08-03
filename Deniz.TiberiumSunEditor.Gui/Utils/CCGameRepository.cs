using System.Diagnostics;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using System.Drawing.Imaging;
using Deniz.CCAudioPlayerCore;
using ImageMagick;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class CCGameRepository
    {
        private static CCGameRepository? _instance;
        public static CCGameRepository Instance => _instance ??= new CCGameRepository();

        private readonly Dictionary<string, Image?> _cameosCache = new();
        private readonly Dictionary<string, ImageTools.AnimatedGifImage?> _animationsCache = new();
        private readonly Dictionary<string, ImageTools.AnimatedGifImage?> _infantryAnimationsCache = new();
        private readonly int _cameoBrightnesPercent = 380;
        private readonly int _animationMaxWidth = 240;
        private CCFileManager? _fileManager;
        private IniFile? _artIniFile;
        private IniFile? _soundIniFile;
        private List<Color>? _cameoPaletteColors;
        private List<Color>? _animPaletteColors;
        private List<Color>? _unitPaletteColors;

        public bool IsLoaded => _fileManager != null;

        public IniFile? ArtFile => _artIniFile;

        public void Initialise(string? gameDirectory, string? mixFiles = null)
        {
            _cameosCache.Clear();
            foreach (var animatedGifImage in _animationsCache.Values)
            {
                animatedGifImage?.Dispose();
            }
            _animationsCache.Clear();
            foreach (var animatedGifImage in _infantryAnimationsCache.Values)
            {
                animatedGifImage?.Dispose();
            }
            _infantryAnimationsCache.Clear();
            _fileManager = null;
            if (!string.IsNullOrEmpty(gameDirectory) 
                && Directory.Exists(gameDirectory))
            {
                _fileManager = new CCFileManager(gameDirectory);
                if (mixFiles == null)
                {
                    // load all MIX files in root
                    _fileManager.LoadAllMixFilesInDirectory(null, true);
                }
                else
                {
                    // load only specified mix files in root
                    foreach (var mixFile in mixFiles.Split(","))
                    {
                        _fileManager.LoadMixFile(mixFile, true);
                    }
                }
                _fileManager.LoadAllMixFilesInDirectory("MIX", true);

                var artIniBytes = _fileManager.LoadFile("artmd.ini") 
                                  ?? _fileManager.LoadFile("art.ini");
                _artIniFile = artIniBytes != null
                    ? IniFile.Load(artIniBytes)
                    : null;

                var cameoPalBytes = _fileManager.LoadFile("cameo.pal");
                _cameoPaletteColors = cameoPalBytes != null
                    ? PalFile.ReadFromFile(cameoPalBytes).Colors
                    : null;

                var animPalBytes = _fileManager.LoadFile("anim.pal");
                _animPaletteColors = animPalBytes != null
                    ? PalFile.ReadFromFile(animPalBytes).Colors
                    : null;

                var unitmPalBytes = _fileManager.LoadFile("unittem.pal");
                _unitPaletteColors = unitmPalBytes != null
                    ? PalFile.ReadFromFile(unitmPalBytes).Colors
                    : null;

                var soundIniBytes = _fileManager.LoadFile("sound.ini");
                _soundIniFile = soundIniBytes != null
                    ? IniFile.Load(soundIniBytes)
                    : null;

            }
        }

        public List<KeyValuePair<string, string>> GetAllSounds()
        {
            var result = new List<KeyValuePair<string, string>>();
            var soundListSection = _soundIniFile?.GetSection("SoundList");
            if (soundListSection != null)
            {
                foreach (var soundValue in soundListSection.KeyValues.Where(k => k.Value != ""))
                {
                    result.Add(new KeyValuePair<string, string>(soundValue.Value, soundValue.Comment ?? ""));
                }
            }
            return result.OrderBy(k => k.Key).ToList();
        }

        public Stream? GetAudioStream(string key)
        {
            var audBytes = _fileManager?.LoadFile($"{key.ToLowerInvariant()}.aud");
            if (audBytes != null)
            {
                return new StupidStream(audBytes);
            }
            return null;
        }

        public Image? GetCameo(string key)
        {
            if (_fileManager == null || _cameoPaletteColors == null) return null;
            if (_cameosCache.TryGetValue(key, out var bitmap))
            {
                return bitmap;
            }

            var shpName = _artIniFile?.GetSection(key)?.GetValue("Cameo")?.Value.ToLowerInvariant();
            if (shpName != null)
            {
                try
                {
                    var shpData = _fileManager.LoadFile($"{shpName}.shp");
                    if (shpData != null)
                    {
                        var shpFile = new ShpFile(shpName);
                        shpFile.ParseFromBuffer(shpData);

                        var frameImage = new ShpImageSingleFrame(_cameoPaletteColors, shpFile, shpData);
                        if (frameImage.FrameInfo.Width > 0
                            && frameImage.FrameInfo.Height > 0)
                        {
                            using var frameBitmap = frameImage.ToBitmap();
                            using var bitmapStream = new MemoryStream();
                            frameBitmap.Save(bitmapStream, ImageFormat.Bmp);
                            bitmapStream.Seek(0, SeekOrigin.Begin);
                            using var bmpImage = new MagickImage(bitmapStream, MagickFormat.Bmp);
                            bmpImage.Modulate(new Percentage(_cameoBrightnesPercent));
                            using var finalStream = new MemoryStream();
                            bmpImage.Write(finalStream, MagickFormat.Bmp);
                            finalStream.Seek(0, SeekOrigin.Begin);
                            Image finalImage = new Bitmap(finalStream);
                            if (finalImage.Height < 40)
                            {
                                var scale = 40d / Convert.ToDouble(finalImage.Height);
                                if (Convert.ToDouble(finalImage.Width) * scale > 60)
                                {
                                    scale = 60d / Convert.ToDouble(finalImage.Width);
                                }
                                finalImage = BitmapRepository.Instance.OverlayImage(finalImage, scale);
                            }
                            _cameosCache.Add(key, finalImage);
                            return finalImage;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Shp load failed: {shpName} - error: {e.Message}");
                }
            }

            _cameosCache.Add(key, null);
            return null;
        }

        public Image? GetAnimationsImage(string animationKeys, double autoStretchToFactor = 0.2d)
        {
            if (_fileManager == null || _animPaletteColors == null) return null;
            if (_animationsCache.TryGetValue(animationKeys, out var animatedGifImage))
            {
                return animatedGifImage?.Image;
            }

            var animationFrames = new List<Image>();
            foreach (var animationKey in animationKeys.Split(","))
            {
                var shpName = $"{animationKey.ToLower()}.shp";
                var shpData = _fileManager.LoadFile(shpName);
                if (shpData != null)
                {
                    var shpFile = new ShpFile(shpName);
                    shpFile.ParseFromBuffer(shpData);
                    if (shpFile.FrameCount > 1 && shpFile.Width <= _animationMaxWidth)
                    {
                        var blankImage = BitmapRepository.Instance.BlankImage;
                        animationFrames.Add(new Bitmap(blankImage, new Size(blankImage.Width, blankImage.Height)));

                        var shpImage = new ShpImageMultiFrame(_animPaletteColors, shpFile, shpData);
                        foreach (var frameBitmap in shpImage.ToBitmapList())
                        {
                            frameBitmap.MakeTransparent(_animPaletteColors[0]);
                            var brigthBitmap = frameBitmap.BrigthenUp(_cameoBrightnesPercent, true);
                            brigthBitmap.MakeTransparent(Color.Black);
                            animationFrames.Add(blankImage.OverlayImage(brigthBitmap, autoStretchToFactor));
                            brigthBitmap.Dispose();
                        }
                    }
                }
            }

            if (animationFrames.Count > 0)
            {
                var result = animationFrames.ToAnimatedGif(50);
                _animationsCache.Add(animationKeys, result);
                return result?.Image;
            }

            _animationsCache.Add(animationKeys, null);
            return null;
        }

        public Image? GetUnitPreviewAnimation(string key)
        {
            if (_fileManager == null || _unitPaletteColors == null) return null;
            if (_infantryAnimationsCache.TryGetValue(key, out var animatedGifImage))
            {
                return animatedGifImage?.Image;
            }

            var animationFrames = new List<Image>();
            var shpName = $"{key.ToLower()}.shp";
            var shpData = _fileManager.LoadFile(shpName);
            if (shpData != null)
            {
                var shpFile = new ShpFile(shpName);
                shpFile.ParseFromBuffer(shpData);
                if (shpFile.FrameCount > 1 && shpFile.Width <= _animationMaxWidth)
                {
                    var blankImage = BitmapRepository.Instance.WhiteImage;
                    animationFrames.Add(new Bitmap(blankImage, new Size(blankImage.Width, blankImage.Height)));

                    var shpImage = new ShpImageMultiFrame(_unitPaletteColors, shpFile, shpData);
                    foreach (var frameBitmap in shpImage.ToBitmapList(200))
                    {
                        frameBitmap.MakeTransparent(_unitPaletteColors[0]);
                        var brigthBitmap = frameBitmap.BrigthenUp(_cameoBrightnesPercent, true);
                        brigthBitmap.MakeTransparent(Color.Black);
                        animationFrames.Add(blankImage.OverlayImage(brigthBitmap, fixZoomFactor:1.4));
                        brigthBitmap.Dispose();
                    }
                }
            }

            if (animationFrames.Count > 0)
            {
                var result = animationFrames.ToAnimatedGif(50);
                _infantryAnimationsCache.Add(key, result);
                return result?.Image;
            }

            _infantryAnimationsCache.Add(key, null);
            return null;
        }
    }
}
