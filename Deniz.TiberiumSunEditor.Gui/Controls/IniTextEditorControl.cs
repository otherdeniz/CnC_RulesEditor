using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using ICSharpCode.TextEditor.Document;
using System.Text;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class IniTextEditorControl : UserControl
    {
        private bool _syntaxModeApplyed;
        private bool _iniHasChanged;
        private Action? _afterIniChangedAction;
        private IniFile? _file;
        private Panel? _lockScreen;
        private bool _doEvents;

        public IniTextEditorControl()
        {
            InitializeComponent();
        }

        public Panel? LockPanel { get; set; }

        public void LoadIniFile(IniFile file, Action afterIniChanged)
        {
            _doEvents = false;
            _file = file;
            _afterIniChangedAction = afterIniChanged;
            textEditorIni.Text = file.SaveAsString();
            file.ValueChanged += (sender, args) =>
            {
                _doEvents = false;
                try
                {
                    var firstLine = textEditorIni.ActiveTextAreaControl.TextArea.TextView.FirstVisibleLine;
                    textEditorIni.Text = file.SaveAsString();
                    textEditorIni.ActiveTextAreaControl.TextArea.TextView.FirstVisibleLine = firstLine;
                }
                catch (Exception e)
                {
                    //disposed control
                }
                _doEvents = true;
            };
            ApplyIniSyntaxHighligthing();
            file.SectionTracked += (sender, args) =>
            {
                try
                {
                    TrackSection(args.Section);
                }
                catch (Exception )
                {
                    //disposed control
                }
            };
            _iniHasChanged = false;
            _doEvents = true;
        }

        public void ApplyIniSyntaxHighligthing(bool force = false)
        {
            if (_syntaxModeApplyed && !force) return;

            // Load and apply the syntax modes
            FileSyntaxModeProvider provider = new FileSyntaxModeProvider(Path.Combine(ResourcesRepository.Instance.ResourcesPath, "SyntaxModes"));
            HighlightingManager.Manager.AddSyntaxModeFileProvider(provider);

            // Apply to the editor  
            textEditorIni.SetHighlighting(ThemeManager.Instance.CurrentTheme.SyntaxMode);
            _syntaxModeApplyed = true;
        }

        private void TrackSection(IniFileSection section)
        {
            textEditorIni.ActiveTextAreaControl.TextArea.TextView.FirstVisibleLine = section.SectionLine;
        }

        private Bitmap GetPrintScreenOfControl(Control mainControl)
        {
            var bitmap = new Bitmap(ImageListComponent.Instance.Blank1.Images[0], mainControl.Width, mainControl.Height);
            mainControl.DrawToBitmap(bitmap, new Rectangle(0, 0, mainControl.Width, mainControl.Height));
            return bitmap;
        }

        private void LockMainControl()
        {
            if (LockPanel != null)
            {
                var controlPrintScreenBitmap = GetPrintScreenOfControl(LockPanel);
                controlPrintScreenBitmap = controlPrintScreenBitmap.SetBitmapOpacity(50);
                LockPanel.Controls[0].Visible = false;
                _lockScreen = new Panel
                {
                    Dock = DockStyle.Fill,
                    Visible = true,
                };
                LockPanel.Controls.Add(_lockScreen);
                var printScrerenpictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    Visible = true,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                _lockScreen.Controls.Add(printScrerenpictureBox);
                var imgTmp = controlPrintScreenBitmap.GetThumbnailImage(controlPrintScreenBitmap.Width / 5, controlPrintScreenBitmap.Height / 5, null, IntPtr.Zero);
                printScrerenpictureBox.Click += (sender, args) => UnlockMainControl();
                printScrerenpictureBox.Image = imgTmp;

                //LockPanel.Controls.SetChildIndex(_lockScreen, 0);
            }
        }

        private void UnlockMainControl()
        {
            if (LockPanel != null)
            {
                if (_lockScreen != null)
                {
                    _lockScreen.Cursor = Cursors.WaitCursor;
                }
                SaveIniFile();
                LockPanel.Controls[0].Visible = true;
                if (_lockScreen != null)
                {
                    LockPanel.Controls.Remove(_lockScreen);
                    _lockScreen.Dispose();
                    _lockScreen = null;
                }
            }
        }

        private void SaveIniFile()
        {
            if (_iniHasChanged && _file != null)
            {
                using var iniStream = new MemoryStream();
                using var writer = new StreamWriter(iniStream, Encoding.UTF8);
                writer.Write(textEditorIni.Text);
                writer.Flush();
                iniStream.Position = 0;
                _file.ApplyFromFile(IniFile.LoadStream(iniStream));
                _afterIniChangedAction?.Invoke();
                _iniHasChanged = false;
            }
        }

        private void textEditorIni_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _iniHasChanged = true;
            LockMainControl();
        }

        private void groupTextEditor_Leave(object sender, EventArgs e)
        {
            //SaveIniFile();
        }
    }
}
