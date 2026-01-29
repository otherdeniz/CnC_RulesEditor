using System.Diagnostics;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using System.Drawing.Imaging;
using System.Globalization;
using Deniz.CCAudioPlayerCore;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
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
        private RaAudioManager? _raAudioManager;
        private IniFile? _artIniFile;
        private IniFile? _soundIniFile;
        private List<Color>? _cameoPaletteColors;
        private List<Color>? _animPaletteColors;
        private List<Color>? _unitPaletteColors;

        public bool IsLoaded => _fileManager != null;

        public CCFileManager? FileManager => _fileManager;

        public IniFile? ArtFile => _artIniFile;

        public void Initialise(GameDefinition gameDefinition, string? mixFiles = null)
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
            _raAudioManager?.Dispose();
            _raAudioManager = null;
            var gameDirectory = gameDefinition.GetUserGamePath();
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

                if (!string.IsNullOrEmpty(gameDefinition.ResourcesDefaultArtIniFile))
                {
                    _artIniFile = gameDefinition.LoadCurrentArtFile();
                }
                else
                {
                    var artIniBytes = _fileManager.LoadFile("artmd.ini")
                                      ?? _fileManager.LoadFile("art.ini");
                    _artIniFile = artIniBytes != null
                        ? IniFile.Load(artIniBytes)
                        : null;
                }

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

                var soundIniBytes = _fileManager.LoadFile(gameDefinition.SoundIni ?? "sound.ini");
                _soundIniFile = soundIniBytes != null
                    ? IniFile.Load(soundIniBytes)
                    : null;

                if (gameDefinition.GameKey.StartsWith("RA2"))
                {
                    var ra2AudioMixFile = _fileManager.LoadFile("audiomd.mix")
                                          ?? _fileManager.LoadFile("audio.mix");
                    if (ra2AudioMixFile != null)
                    {
                        _raAudioManager = new RaAudioManager(ra2AudioMixFile);
                    }
                }
            }
        }

        public void ClearAnimationsCache()
        {
            _animationsCache.Clear();
            _infantryAnimationsCache.Clear();
        }

        public bool TryPlayRaAudio(string soundKey)
        {
            if (_raAudioManager != null && _soundIniFile != null)
            {
                try
                {
                    var soundValues = _soundIniFile.GetSection(soundKey)?
                        .GetValue("Sounds")?.Value
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (soundValues?.Length > 0)
                    {
                        var audioEntry = _raAudioManager.IdxFile.Entries.FirstOrDefault(e =>
                            e.Name.Equals(soundValues[0].TrimStart('$'), StringComparison.InvariantCultureIgnoreCase));
                        if (audioEntry != null)
                        {
                            _raAudioManager.PlayEntry(audioEntry);
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"TryPlayRaAudio failed: {e.Message}");
                }
            }
            return false;
        }

        public List<KeyValuePair<string, string>> GetAllSounds()
        {
            var result = new List<KeyValuePair<string, string>>();
            var soundListSection = _soundIniFile?.GetSection("SoundList");
            if (soundListSection != null)
            {
                foreach (var soundValue in soundListSection.KeyValues.Where(k => k.Value != ""))
                {
                    result.Add(new KeyValuePair<string, string>(
                        soundValue.Value.ToUpper(CultureInfo.InvariantCulture),
                        soundValue.Comment ?? ""));
                }
            }
            if (_fileManager != null)
            {
                return result.UnionBy(_fileManager.MixFilesContents
                            .Where(c => c.FileName?.EndsWith(".aud") == true && c.FileLocationInfo.Size < 50000)
                            .Select(c =>
                                new KeyValuePair<string, string>(
                                    c.FileName!.Substring(0, c.FileName!.Length - 4).ToUpper(CultureInfo.InvariantCulture),
                                    "[not in sound.ini]"))
                        , k => k.Key)
                    .OrderBy(k => k.Key)
                    .ToList();
            }
            return result.OrderBy(k => k.Key).ToList();
        }

        public StupidStream? GetAudioStream(string key)
        {
            var audBytes = _fileManager?.LoadFile($"{key.ToLowerInvariant()}.aud");
            if (audBytes != null)
            {
                return new StupidStream(new AudFile(audBytes).ToWav());
            }
            return null;
        }

        public Image? GetCameoByShp(string shpName, bool forceArtReload)
        {
            if (_fileManager == null || _cameoPaletteColors == null) return null;
            if (!forceArtReload 
                && _cameosCache.TryGetValue(shpName, out var bitmap))
            {
                return bitmap;
            }

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
                        _cameosCache[shpName] = finalImage;
                        return finalImage;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Shp load failed: {shpName} - error: {e.Message}");
            }

            _cameosCache[shpName] = null;
            return null;
        }

        public Image? GetCameoByPcx(string pcxName, bool forceArtReload)
        {
            if (_fileManager == null || _cameoPaletteColors == null) return null;
            if (!forceArtReload
                && _cameosCache.TryGetValue(pcxName, out var bitmap))
            {
                return bitmap;
            }

            try
            {
                var pcxData = _fileManager.LoadFile(pcxName);
                if (pcxData != null)
                {
                    using var bitmapStream = new MemoryStream(pcxData);
                    using var bmpImage = new MagickImage(bitmapStream, MagickFormat.Pcx);
                    var pcxImage = bmpImage.ToImage();
                    _cameosCache[pcxName] = pcxImage;
                    return pcxImage;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"PCX load failed: {pcxName} - error: {e.Message}");
            }

            _cameosCache[pcxName] = null;
            return null;
        }

        public Image? GetCameo(string key, bool forceArtReload, IniFileSection? artSection = null)
        {
            if (_fileManager == null || _cameoPaletteColors == null) return null;
            if (!forceArtReload
                && _cameosCache.TryGetValue(key, out var bitmap))
            {
                return bitmap;
            }

            artSection ??= _artIniFile?.GetSection(key);

            var pcxName = artSection?.GetValue("CameoPCX")?.Value.ToLowerInvariant();
            if (pcxName != null)
            {
                return GetCameoByPcx(pcxName, forceArtReload);
            }

            var shpName = artSection?.GetValue("Cameo")?.Value.ToLowerInvariant();
            if (shpName != null)
            {
                return GetCameoByShp(shpName, forceArtReload);
            }

            _cameosCache[key] = null;
            return null;
        }

        public Image? GetAnimationsImage(string animationKeys, double autoStretchToFactor = 0.2d, float opacity = 1)
        {
            if (_fileManager == null || _animPaletteColors == null) return null;
            var cacheKey = $"{animationKeys}:{opacity:0.0}";
            if (_animationsCache.TryGetValue(cacheKey, out var animatedGifImage))
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
                        var shpImage = new ShpImageMultiFrame(_animPaletteColors, shpFile, shpData);
                        foreach (var frameBitmap in shpImage.ToBitmapList())
                        {
                            frameBitmap.MakeTransparent(_animPaletteColors[0]);
                            var brigthBitmap = frameBitmap.BrigthenUp(_cameoBrightnesPercent, true);
                            brigthBitmap.MakeTransparent(Color.Black);
                            var animationFrame = blankImage.OverlayImage(brigthBitmap, autoStretchToFactor);
                            if (opacity != 1)
                            {
                                animationFrame = animationFrame.SetBitmapOpacity(opacity);
                            }
                            animationFrames.Add(animationFrame);
                            brigthBitmap.Dispose();
                        }
                    }
                }
            }

            if (animationFrames.Count > 0)
            {
                var result = animationFrames.ToAnimatedGif(50);
                _animationsCache.Add(cacheKey, result);
                return result?.Image;
            }

            _animationsCache.Add(cacheKey, null);
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
