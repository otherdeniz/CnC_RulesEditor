using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using ImageMagick;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class MixBrowserForm : Form
    {
        private readonly int _cameoBrightnesPercent = 380;
        private readonly int _animationMaxWidth = 240;
        private List<MixFileContent>? _palFiles;
        private double _zoomFactor = 1;
        private List<Color>? _cameoPaletteColors;
        private List<Color>? _animPaletteColors;
        private List<Color>? _otherPaletteColors;
        private ImageTools.AnimatedGifImage? _currentAnimatedGifImage;
        private Image? _currentImage;
        private bool _doEvents = true;

        public MixBrowserForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public void InitDropDowns()
        {
            comboBoxZoom.Items.AddRange(new object[] { "50 %", "75 %", "100 %", "150 %", "200 %", "300 %" });
            comboBoxZoom.SelectedIndex = 2;
            _palFiles = CCGameRepository.Instance.FileManager?.MixFilesContents.Where(f =>
                    f.FileName != null && f.FileName.EndsWith(".pal", StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            if (_palFiles != null)
            {
                //Cameo
                comboBoxPaletteCameo.Items.AddRange(_palFiles.OfType<object>().ToArray());
                var cameoIndex = _palFiles.FindIndex(f => f.FileName!.Equals("cameo.pal"));
                comboBoxPaletteCameo.SelectedIndex = cameoIndex > -1 ? cameoIndex : 0;
                //Animation
                comboBoxAnimation.Items.AddRange(_palFiles.OfType<object>().ToArray());
                var animIndex = _palFiles.FindIndex(f => f.FileName!.Equals("anim.pal"));
                comboBoxAnimation.SelectedIndex = animIndex > -1 ? animIndex : 0;
                //Other
                comboBoxOther.Items.AddRange(_palFiles.OfType<object>().ToArray());
                var otherIndex = _palFiles.FindIndex(f => f.FileName!.Contains("unit"));
                comboBoxOther.SelectedIndex = otherIndex > -1 ? otherIndex : 0;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var searchText = textBoxSearch.Text;
            var mixContents = CCGameRepository.Instance.FileManager?.MixFilesContents.Where(f =>
                    f.FileName != null && f.FileName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                .Select(f => new MixContentModel(f))
                .OrderBy(c => c.MixFile)
                .ThenBy(c => c.FileName)
                .ToList();
            valuesGrid.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            valuesGrid.DataSource = mixContents;
            valuesGrid.DisplayLayout.Bands[0].ScrollTipField = "FileName";
            valuesGrid.DisplayLayout.Bands[0].PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            valuesGrid.DisplayLayout.Bands[0].Columns["Size"].Width = 120;
        }

        private void OnItemSeleced(bool autoSelectPalette = true)
        {
            pictureBoxPreview.Image = null;
            _currentAnimatedGifImage?.ImageStream.Dispose();
            _currentAnimatedGifImage = null;
            _currentImage?.Dispose();
            _currentImage = null;
            if (valuesGrid.Selected.Rows.Count > 0
                && valuesGrid.Selected.Rows[0].ListObject is MixContentModel mixContent)
            {
                var fileData = mixContent.MixContent.Read();
                if (TryParseShpImage(mixContent.FileName, fileData, autoSelectPalette))
                {
                    pictureBoxPreview.Image = _currentImage ?? _currentAnimatedGifImage?.Image;
                }
                else if (TryParsePcxImage(mixContent.FileName, fileData))
                {
                    pictureBoxPreview.Image = _currentImage;
                }
            }
        }

        private bool TryParseShpImage(string shpName, byte[] fileData, bool autoSelectPalette)
        {
            if (_otherPaletteColors == null) return false;
            var paletteColors = _otherPaletteColors!;
            if (!autoSelectPalette)
            {
                if (radioButtonCameo.Checked)
                {
                    paletteColors = _cameoPaletteColors!;
                }
                else if (radioButtonAnimation.Checked)
                {
                    paletteColors = _animPaletteColors!;
                }
            }
            try
            {
                var shpFile = new ShpFile(shpName);
                shpFile.ParseFromBuffer(fileData);
                if (shpFile.FrameCount > 1 && shpFile.Width <= _animationMaxWidth)
                {
                    if (autoSelectPalette)
                    {
                        _doEvents = false;
                        if (shpName.Contains("mk.", StringComparison.InvariantCultureIgnoreCase))
                        {
                            paletteColors = _otherPaletteColors!;
                            radioButtonOther.Checked = true;
                        }
                        else
                        {
                            paletteColors = _animPaletteColors!;
                            radioButtonAnimation.Checked = true;
                        }
                        _doEvents = true;
                    }
                    var animationFrames = new List<Image>();
                    var blankImage = new Bitmap(Convert.ToInt32(shpFile.Width * _zoomFactor),
                        Convert.ToInt32(shpFile.Height * _zoomFactor));
                    using (var gfx = Graphics.FromImage(blankImage))
                    using (var brush = new SolidBrush(ThemeManager.Instance.CurrentTheme.ControlsBackColor))
                    {
                        gfx.FillRectangle(brush, 0, 0, blankImage.Width, blankImage.Height);
                    }
                    var shpImage = new ShpImageMultiFrame(paletteColors, shpFile, fileData);
                    foreach (var frameBitmap in shpImage.ToBitmapList())
                    {
                        frameBitmap.MakeTransparent(paletteColors[0]);
                        var brigthBitmap = frameBitmap.BrigthenUp(_cameoBrightnesPercent, true);
                        brigthBitmap.MakeTransparent(Color.Black);
                        animationFrames.Add(blankImage.OverlayImage(brigthBitmap, fixZoomFactor: _zoomFactor));
                        brigthBitmap.Dispose();
                    }
                    _currentAnimatedGifImage = animationFrames.ToAnimatedGif(50);
                    return true;
                }

                if (autoSelectPalette)
                {
                    _doEvents = false;
                    paletteColors = _cameoPaletteColors!;
                    radioButtonCameo.Checked = true;
                    _doEvents = true;
                }
                var frameImage = new ShpImageSingleFrame(paletteColors, shpFile, fileData);
                if (frameImage.FrameInfo.Width > 0
                    && frameImage.FrameInfo.Height > 0)
                {
                    var fullSizeBitmap = frameImage.ToBitmap().BrigthenUp(_cameoBrightnesPercent, true);
                    if (_zoomFactor == 1.0)
                    {
                        _currentImage = fullSizeBitmap;
                    }
                    else
                    {
                        _currentImage = new Bitmap(fullSizeBitmap, Convert.ToInt32(shpFile.Width * _zoomFactor),
                            Convert.ToInt32(shpFile.Height * _zoomFactor));
                        fullSizeBitmap.Dispose();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                // failed
            }
            return false;
        }

        private bool TryParsePcxImage(string pcxName, byte[] fileData)
        {
            if (pcxName.EndsWith(".pcx"))
            {
                try
                {
                    using var bitmapStream = new MemoryStream(fileData);
                    using var bmpImage = new MagickImage(bitmapStream, MagickFormat.Pcx);
                    var pcxImage = bmpImage.ToImage();
                    if (_zoomFactor == 1.0)
                    {
                        _currentImage = pcxImage;
                    }
                    else
                    {
                        _currentImage = new Bitmap(pcxImage, Convert.ToInt32(pcxImage.Width * _zoomFactor),
                            Convert.ToInt32(pcxImage.Height * _zoomFactor));
                        pcxImage.Dispose();
                    }
                    return true;
                }
                catch (Exception)
                {
                    // failed
                }
            }
            return false;
        }

        private void comboBoxCameo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPaletteCameo.SelectedIndex > -1
                && comboBoxPaletteCameo.SelectedItem is MixFileContent paletteFile)
            {
                _cameoPaletteColors = PalFile.ReadFromFile(paletteFile.Read()).Colors;
                if (radioButtonCameo.Checked)
                {
                    OnItemSeleced(false);
                }
            }
        }

        private void comboBoxAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAnimation.SelectedIndex > -1
                && comboBoxAnimation.SelectedItem is MixFileContent paletteFile)
            {
                _animPaletteColors = PalFile.ReadFromFile(paletteFile.Read()).Colors;
                if (radioButtonAnimation.Checked)
                {
                    OnItemSeleced(false);
                }
            }
        }

        private void comboBoxOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOther.SelectedIndex > -1
                && comboBoxOther.SelectedItem is MixFileContent paletteFile)
            {
                _otherPaletteColors = PalFile.ReadFromFile(paletteFile.Read()).Colors;
                if (radioButtonOther.Checked)
                {
                    OnItemSeleced(false);
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            if (sender is RadioButton radioButton
                && radioButton.Checked)
            {
                OnItemSeleced(false);
            }
        }

        private void comboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxZoom.SelectedIndex > -1
                && comboBoxZoom.SelectedItem is string zoomText
                && double.TryParse(zoomText.TrimEnd(" %"), out var zoomNumber))
            {
                _zoomFactor = zoomNumber / 100d;
                OnItemSeleced(false);
            }
        }

        private void valuesGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            OnItemSeleced();
        }

        private void MixBrowserForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }

    }
}
