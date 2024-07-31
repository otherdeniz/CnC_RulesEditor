using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class NewCustomModForm : Form
    {
        private List<GameDefinition> _gameDefinitions = null!;
        private Image? _selectedImage;
        private bool _doEvents = true;

        public NewCustomModForm()
        {
            InitializeComponent();
            InitialiseGames();
        }

        public static CustomModSetting? ExecuteNew(Form parentForm)
        {
            using (var form = new NewCustomModForm())
            {
                if (form.ShowDialog(parentForm) == DialogResult.OK)
                {
                    return form.CurrentModSetting;
                }
            }
            return null;
        }

        public static bool ExecuteEdit(Form parentForm, CustomModSetting customModSetting)
        {
            using (var form = new NewCustomModForm())
            {
                form.LoadCustomModSetting(customModSetting);
                if (form.ShowDialog(parentForm) == DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }

        public CustomModSetting? CurrentModSetting { get; private set; }

        public GameDefinition? SelectedGameDefinition { get; private set; }

        public string? IconImagePath { get; private set; }

        public Image? SelectedImage
        {
            get => _selectedImage;
            private set
            {
                _selectedImage = value;
                pictureBoxIcon.Image = value;
            }
        }

        private void InitialiseGames()
        {
            _gameDefinitions = GamesFile.Instance.Games;
            _gameDefinitions.ForEach(g => comboBoxGameType.Items.Add(g.NewMenuLabel));
        }

        private void LoadCustomModSetting(CustomModSetting customModSetting)
        {
            CurrentModSetting = customModSetting;
            _doEvents = false;
            textName.Text = customModSetting.Name;
            comboBoxGameType.SelectedIndex = _gameDefinitions.FindIndex(g => g.GameKey == customModSetting.BaseGameKey);
            textGamePath.Text = customModSetting.GamePath;
            IconImagePath = customModSetting.LogoFile;
            SelectedImage = LogoRepository.Instance.GetLogo(IconImagePath);
            _doEvents = true;
        }

        private void SetButtonOkEnabled()
        {
            buttonOk.Enabled = !string.IsNullOrEmpty(textName.Text)
                               && !string.IsNullOrEmpty(textGamePath.Text)
                               && SelectedGameDefinition != null;
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            SetButtonOkEnabled();
        }

        private void comboBoxGameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGameType.SelectedIndex > -1
                && _doEvents)
            {
                SelectedGameDefinition = _gameDefinitions[comboBoxGameType.SelectedIndex];
                SelectedImage = LogoRepository.Instance.GetLogo(SelectedGameDefinition.Logo);
                IconImagePath = SelectedGameDefinition.Logo;
            }
            SetButtonOkEnabled();
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(Path.Combine(folderBrowserDialog.SelectedPath, @"INI\rules.ini")))
                {
                    MessageBox.Show(@"The selected Directory does not contain the mandatory mod-file: 'INI\rules.ini'",
                        "Not supported mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                textGamePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonIcon_Click(object sender, EventArgs e)
        {
            if (openFileIcon.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    SelectedImage = Image.FromFile(openFileIcon.FileName);
                    IconImagePath = openFileIcon.FileName;
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Failed to load image: {exception.Message}", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (CurrentModSetting == null)
            {
                CurrentModSetting = new CustomModSetting
                {
                    Key = Guid.NewGuid().ToString("N"),
                    RulesIniPath = @"INI\rules.ini"
                };
            }
            CurrentModSetting.Name = textName.Text;
            CurrentModSetting.BaseGameKey = SelectedGameDefinition!.GameKey;
            CurrentModSetting.GamePath = textGamePath.Text;
            if (IconImagePath != null)
            {
                CurrentModSetting.LogoFile = IconImagePath;
            }
            var nameValue = CurrentModSetting.LoadRulesIniFile().GetSection("General")?.GetValue("Name")?.Value;
            if (nameValue == null)
            {
                MessageBox.Show(@"The selected mod rules.ini file (INI\rules.ini) does not have a 'name' value in the 'General' section",
                    "Not supported mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CurrentModSetting.IniNameMatchDetection = nameValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
