using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
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
        private ImageTools.AnimatedGifImage? _currentAnimatedGifImage;
        private Bitmap? _currentImage;

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
                comboBoxPalette.Items.AddRange(_palFiles.OfType<object>().ToArray());
                var cameoIndex = _palFiles.FindIndex(f => f.FileName!.Contains("cameo"));
                comboBoxPalette.SelectedIndex = cameoIndex > -1 ? cameoIndex : 0;
            }
        }

        private bool TryParseShpImage(byte[] fileData)
        {
            if (_cameoPaletteColors == null) return false;
            try
            {
                var shpFile = new ShpFile("file.shp");
                shpFile.ParseFromBuffer(fileData);
                if (shpFile.FrameCount > 1 && shpFile.Width <= _animationMaxWidth)
                {
                    var animationFrames = new List<Image>();
                    var blankImage = new Bitmap(Convert.ToInt32(shpFile.Width * _zoomFactor),
                        Convert.ToInt32(shpFile.Height * _zoomFactor));
                    using (var gfx = Graphics.FromImage(blankImage))
                    using (var brush = new SolidBrush(ThemeManager.Instance.CurrentTheme.ControlsBackColor))
                    {
                        gfx.FillRectangle(brush, 0, 0, blankImage.Width, blankImage.Height);
                    }
                    //animationFrames.Add(blankImage);

                    var shpImage = new ShpImageMultiFrame(_cameoPaletteColors, shpFile, fileData);
                    foreach (var frameBitmap in shpImage.ToBitmapList())
                    {
                        frameBitmap.MakeTransparent(_cameoPaletteColors[0]);
                        var brigthBitmap = frameBitmap.BrigthenUp(_cameoBrightnesPercent, true);
                        brigthBitmap.MakeTransparent(Color.Black);
                        animationFrames.Add(blankImage.OverlayImage(brigthBitmap, fixZoomFactor: _zoomFactor));
                        brigthBitmap.Dispose();
                    }
                    _currentAnimatedGifImage = animationFrames.ToAnimatedGif(50);
                    return true;
                }

                var frameImage = new ShpImageSingleFrame(_cameoPaletteColors, shpFile, fileData);
                if (frameImage.FrameInfo.Width > 0
                    && frameImage.FrameInfo.Height > 0)
                {
                    _currentImage = frameImage.ToBitmap().BrigthenUp(_cameoBrightnesPercent, true);
                    return true;
                }
            }
            catch (Exception)
            {
                // failed
            }

            return false;
        }

        private void OnItemSeleced()
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
                if (TryParseShpImage(fileData))
                {
                    pictureBoxPreview.Image = _currentImage ?? _currentAnimatedGifImage?.Image;
                }
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
        }

        private void comboBoxPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPalette.SelectedIndex > -1
                && comboBoxPalette.SelectedItem is MixFileContent paletteFile)
            {
                _cameoPaletteColors = PalFile.ReadFromFile(paletteFile.Read()).Colors;
                OnItemSeleced();
            }
        }

        private void comboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxZoom.SelectedIndex > -1
                && comboBoxZoom.SelectedItem is string zoomText
                && double.TryParse(zoomText.TrimEnd(" %"), out var zoomNumber))
            {
                _zoomFactor = zoomNumber / 100d;
                OnItemSeleced();
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
