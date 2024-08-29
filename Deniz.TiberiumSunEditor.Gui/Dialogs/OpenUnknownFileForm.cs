using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class OpenUnknownFileForm : Form
    {
        private List<GameDefinition> _gameDefinitions = null!;
        private List<FileBaseType> _fileTypes = null!;

        public OpenUnknownFileForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public GameDefinition? SelectedGameDefinition { get; private set; }

        public FileBaseType SelectedFileType { get; private set; } = FileBaseType.Unknown;

        public void LoadFile(string fileName, FileBaseType fileType, GameDefinition? selectedGameDefinition)
        {
            LabelFilename.Text = fileName;
            _gameDefinitions = GamesFile.Instance.Games
                .Union(UserSettingsFile.Instance.CustomMods.Select(m => m.ToGameDefinition())
                    .Where(g => g != null).Select(g => g!))
                .ToList();
            _gameDefinitions.ForEach(g => comboBoxGameType.Items.Add(g.NewMenuLabel));
            if (selectedGameDefinition != null)
            {
                comboBoxGameType.SelectedIndex =
                    _gameDefinitions.FindIndex(g => g.GameKey == selectedGameDefinition.GameKey);
            }
            _fileTypes = new List<FileBaseType>
            {
                FileBaseType.Rules,
                FileBaseType.Art,
                FileBaseType.Ai,
                FileBaseType.Map
            };
            _fileTypes.ForEach(t => comboBoxFileType.Items.Add(t.ToString()));
            if (fileType != FileBaseType.Unknown)
            {
                comboBoxFileType.SelectedIndex = _fileTypes.IndexOf(fileType);
                comboBoxFileType.Enabled = comboBoxFileType.SelectedIndex == -1;
            }
        }

        private void ApplyOkButton()
        {
            buttonOk.Enabled = SelectedGameDefinition != null
                               && SelectedFileType != FileBaseType.Unknown;
        }

        private void comboBoxGameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGameType.SelectedIndex > -1)
            {
                SelectedGameDefinition = _gameDefinitions[comboBoxGameType.SelectedIndex];
            }
            ApplyOkButton();
        }

        private void comboBoxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFileType.SelectedIndex > -1)
            {
                SelectedFileType = _fileTypes[comboBoxFileType.SelectedIndex];
            }
            ApplyOkButton();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OpenMapForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }

    }
}
