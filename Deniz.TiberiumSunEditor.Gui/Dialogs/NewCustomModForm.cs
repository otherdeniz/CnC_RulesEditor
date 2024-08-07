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
        private GameTypeDetector? _gameTypeDetector;

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
            var gameDefinitionIndex = _gameDefinitions.FindIndex(g => g.GameKey == customModSetting.BaseGameKey);
            SelectedGameDefinition = _gameDefinitions[gameDefinitionIndex];
            comboBoxGameType.SelectedIndex = gameDefinitionIndex;
            textGamePath.Text = customModSetting.GamePath;
            IconImagePath = customModSetting.LogoFile;
            checkBoxAres.Checked = customModSetting.HasAres;
            checkBoxPhobos.Checked = customModSetting.HasPhobos;
            SelectedImage = LogoRepository.Instance.GetLogo(IconImagePath);
            _gameTypeDetector = new GameTypeDetector(customModSetting.GamePath);
            LoadRulesIniSources();
            LoadArtIniSources();
            _doEvents = true;
        }

        private void SetButtonOkEnabled()
        {
            buttonOk.Enabled = !string.IsNullOrEmpty(textName.Text)
                               && !string.IsNullOrEmpty(textGamePath.Text)
                               && SelectedGameDefinition != null
                               && comboBoxRulesIni.SelectedIndex > -1
                               && comboBoxArtIni.SelectedIndex > -1;
        }

        private void LoadRulesIniSources()
        {
            comboBoxRulesIni.Items.Clear();
            if (_gameTypeDetector == null) return;
            if (!string.IsNullOrEmpty(_gameTypeDetector.GameDirectory))
            {
                foreach (var iniFolderRule in _gameTypeDetector.IniFolderSearch("rules*.ini"))
                {
                    comboBoxRulesIni.Items.Add(iniFolderRule);
                    if (CurrentModSetting?.RulesIniPath == iniFolderRule)
                    {
                        comboBoxRulesIni.SelectedIndex = comboBoxRulesIni.Items.Count - 1;
                    }

                }
            }
            if (SelectedGameDefinition != null)
            {
                foreach (var mixFileContent in _gameTypeDetector.FileManager.MixFilesContents
                             .Where(c => c.FileName == SelectedGameDefinition.SaveAsFilename))
                {
                    var mixContentPath = mixFileContent.ToString();
                    comboBoxRulesIni.Items.Add(mixContentPath);
                    if (CurrentModSetting?.RulesIniMixSource == mixContentPath)
                    {
                        comboBoxRulesIni.SelectedIndex = comboBoxRulesIni.Items.Count - 1;
                    }
                }
            }
            if (comboBoxRulesIni.SelectedIndex == -1)
            {
                comboBoxRulesIni.SelectedIndex = 0;
            }
        }

        private void LoadArtIniSources()
        {
            comboBoxArtIni.Items.Clear();
            if (_gameTypeDetector == null) return;
            if (!string.IsNullOrEmpty(_gameTypeDetector.GameDirectory))
            {
                foreach (var iniFolderRule in _gameTypeDetector.IniFolderSearch("art*.ini"))
                {
                    comboBoxArtIni.Items.Add(iniFolderRule);
                    if (CurrentModSetting?.RulesIniPath == iniFolderRule)
                    {
                        comboBoxArtIni.SelectedIndex = comboBoxArtIni.Items.Count - 1;
                    }

                }
            }
            if (SelectedGameDefinition != null)
            {
                foreach (var mixFileContent in _gameTypeDetector.FileManager.MixFilesContents
                             .Where(c => c.FileName == SelectedGameDefinition.SaveAsArtFilename))
                {
                    var mixContentPath = mixFileContent.ToString();
                    comboBoxArtIni.Items.Add(mixContentPath);
                    if (CurrentModSetting?.RulesIniMixSource == mixContentPath)
                    {
                        comboBoxArtIni.SelectedIndex = comboBoxArtIni.Items.Count - 1;
                    }
                }
            }
            if (comboBoxArtIni.SelectedIndex == -1)
            {
                comboBoxArtIni.SelectedIndex = 0;
            }
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

        private void comboBoxRulesIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonOkEnabled();
        }

        private void comboBoxArtIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonOkEnabled();
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                textGamePath.Text = folderBrowserDialog.SelectedPath;
                _gameTypeDetector = new GameTypeDetector(folderBrowserDialog.SelectedPath);
                if (_gameTypeDetector.BaseType != null)
                {
                    comboBoxGameType.SelectedIndex =
                        _gameDefinitions.FindIndex(g => g.GameKey == _gameTypeDetector.BaseType.GameKey);
                }
                var clientLogoPath = _gameTypeDetector.GetClientLogoPath();
                if (clientLogoPath != null)
                {
                    SelectedImage = LogoRepository.Instance.GetLogo(clientLogoPath);
                    IconImagePath = clientLogoPath;
                }

                checkBoxAres.Checked = _gameTypeDetector.HasModuleAres();
                checkBoxPhobos.Checked = _gameTypeDetector.HasModulePhobos();

                LoadRulesIniSources();
                LoadArtIniSources();
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
                    Key = Guid.NewGuid().ToString("N")
                };
            }
            CurrentModSetting.Name = textName.Text;
            CurrentModSetting.BaseGameKey = SelectedGameDefinition!.GameKey;
            CurrentModSetting.GamePath = textGamePath.Text;
            CurrentModSetting.HasAres = checkBoxAres.Checked;
            CurrentModSetting.HasPhobos = checkBoxPhobos.Checked;
            // Rules.ini
            var selectedRulesIni = (string)comboBoxRulesIni.SelectedItem;
            if (selectedRulesIni.Contains(":"))
            {
                // mix content
                CurrentModSetting.RulesIniMixSource = selectedRulesIni;
                var mixFileContent =
                    _gameTypeDetector!.FileManager.MixFilesContents.First(c =>
                        c.ToString() == selectedRulesIni);
                CurrentModSetting.RulesIniPath = UserSettingsFolder.Instance
                    .SaveFile($"{CurrentModSetting.Key}.ini", mixFileContent.Read());
            }
            else
            {
                CurrentModSetting.RulesIniMixSource = null;
                CurrentModSetting.RulesIniPath = selectedRulesIni;
            }
            // Art.ini
            var selectedArtIni = (string)comboBoxArtIni.SelectedItem;
            if (selectedArtIni.Contains(":"))
            {
                // mix content
                CurrentModSetting.ArtIniMixSource = selectedArtIni;
                var mixFileContent =
                    _gameTypeDetector!.FileManager.MixFilesContents.First(c =>
                        c.ToString() == selectedArtIni);
                CurrentModSetting.ArtIniPath = UserSettingsFolder.Instance
                    .SaveFile($"{CurrentModSetting.Key}_art.ini", mixFileContent.Read());
            }
            else
            {
                CurrentModSetting.ArtIniMixSource = null;
                CurrentModSetting.ArtIniPath = selectedArtIni;
            }
            // Logo
            if (IconImagePath != null)
            {
                CurrentModSetting.LogoFile = IconImagePath;
            }
            var nameValue = CurrentModSetting.LoadRulesIniFile().GetSection("General")?.GetValue("Name")?.Value;
            if (nameValue == null)
            {
                MessageBox.Show(@"The selected mod rules.ini file does not have a 'name' value in the 'General' section",
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
