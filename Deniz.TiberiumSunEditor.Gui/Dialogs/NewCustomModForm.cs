using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class NewCustomModForm : Form
    {
        private const string CommandLineArgumentInheritance = "Inheritance";
        private List<GameDefinition> _gameDefinitions = null!;
        private Image? _selectedImage;
        private bool _doEvents = true;
        private GameTypeDetector? _gameTypeDetector;

        public NewCustomModForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
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
            checkBoxPhobosSectionInheritance.Checked = customModSetting.HasPhobosSectionInheritance;
            checkBoxVinifera.Checked = customModSetting.HasVinifera;
            checkBoxXnaSectionInheritance.Checked = customModSetting.HasSectionInheritance;
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
            if (comboBoxRulesIni.SelectedIndex == -1 
                && comboBoxRulesIni.Items.Count > 0)
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
                    if (CurrentModSetting?.ArtIniPath == iniFolderRule)
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
                    if (CurrentModSetting?.ArtIniMixSource == mixContentPath)
                    {
                        comboBoxArtIni.SelectedIndex = comboBoxArtIni.Items.Count - 1;
                    }
                }
            }
            if (comboBoxArtIni.SelectedIndex == -1
                && comboBoxArtIni.Items.Count > 0)
            {
                comboBoxArtIni.SelectedIndex = 0;
            }
        }

        private bool CheckClientCommandLineArgument(string gamePath, string argument)
        {
            var clientDefinitionIniPath = Path.Combine(gamePath, @"Resources\ClientDefinitions.ini");
            if (File.Exists(clientDefinitionIniPath))
            {
                var clientDefinitionsIniFile = IniFile.Load(clientDefinitionIniPath);
                return clientDefinitionsIniFile.GetSection("Settings")?
                           .GetValue("ExtraCommandLineParams")?.Value
                           .Contains($" -{argument}", StringComparison.InvariantCultureIgnoreCase)
                       ?? false;
            }

            return false;
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            SetButtonOkEnabled();
        }

        private void comboBoxGameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGameType.SelectedIndex > -1)
            {
                var gameDefinition = _gameDefinitions[comboBoxGameType.SelectedIndex];
                if (gameDefinition.GameKey == "TiberianSun" 
                    || gameDefinition.GameKey == "Firestorm"
                    || gameDefinition.GameKey == "DTA")
                {
                    checkBoxAres.Checked = false;
                    checkBoxAres.Enabled = false;
                    checkBoxPhobos.Checked = false;
                    checkBoxPhobos.Enabled = false;
                    checkBoxVinifera.Enabled = true;
                    checkBoxPhobosSectionInheritance.Checked = false;
                    checkBoxPhobosSectionInheritance.Enabled = false;
                    checkBoxXnaSectionInheritance.Enabled = true;
                }
                else if (gameDefinition.GameKey.StartsWith("RA2"))
                {
                    checkBoxAres.Enabled = true;
                    checkBoxPhobos.Enabled = true;
                    checkBoxVinifera.Checked = false;
                    checkBoxVinifera.Enabled = false;
                    checkBoxPhobosSectionInheritance.Enabled = true;
                    checkBoxXnaSectionInheritance.Checked = false;
                    checkBoxXnaSectionInheritance.Enabled = false;
                }
                if (_doEvents)
                {
                    SelectedGameDefinition = gameDefinition;
                    SelectedImage = LogoRepository.Instance.GetLogo(gameDefinition.Logo);
                    IconImagePath = gameDefinition.Logo;
                }
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
                var gameTypeDetector = new GameTypeDetector(folderBrowserDialog.SelectedPath);
                var filesToCopyAsDefault = gameTypeDetector.CheckBaseFilesToCreateDefaultVersions(new[] { "rules*.ini", "art*.ini" });
                if (filesToCopyAsDefault.Any())
                {
                    if (MessageBox.Show("The following files where found in the games 'INI\\Base'-folder:" + Environment.NewLine +
                                        string.Join(", ", filesToCopyAsDefault) + Environment.NewLine + Environment.NewLine +
                                        "It is required for this editor to have dedicated default-files," + Environment.NewLine +
                                        "the editor then saves the modified .ini files in the same location." + Environment.NewLine +
                                        "Press 'Yes' to copy these files now to a default-version in the same folder" + Environment.NewLine +
                                        "(adds the '-default' sufix to the default filename).",
                            "Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CopyBaseFilesToDefaultFile(folderBrowserDialog.SelectedPath, filesToCopyAsDefault);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    var filesToMove = gameTypeDetector.CheckFilesToMoveToIniFolder(new[] { "rules*.ini", "art*.ini" });
                    if (filesToMove.Any())
                    {
                        if (MessageBox.Show("The following files where found in the games root-folder:" + Environment.NewLine +
                                            string.Join(", ", filesToMove) + Environment.NewLine + Environment.NewLine +
                                            "It is required for this editor that the default .ini files are located in the games sub-folder 'INI'" + Environment.NewLine +
                                            "the editor then saves the modified .ini files in the games root folder." + Environment.NewLine +
                                            "Press 'Yes' to copy these files now to the 'INI' subfolder.",
                                "Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CopyFilesToIniFolder(folderBrowserDialog.SelectedPath, filesToMove);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                textGamePath.Text = folderBrowserDialog.SelectedPath;
                _gameTypeDetector = gameTypeDetector;
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

                if (checkBoxAres.Enabled)
                {
                    checkBoxAres.Checked = _gameTypeDetector.HasModule("Ares.dll");
                }
                if (checkBoxPhobos.Enabled)
                {
                    checkBoxPhobos.Checked = _gameTypeDetector.HasModule("Phobos.dll");
                }
                if (checkBoxPhobosSectionInheritance.Enabled)
                {
                    checkBoxPhobosSectionInheritance.Checked =
                        CheckClientCommandLineArgument(textGamePath.Text, CommandLineArgumentInheritance);
                }
                if (checkBoxVinifera.Enabled)
                {
                    checkBoxVinifera.Checked = _gameTypeDetector.HasModule("Vinifera.dll");
                }
                if (checkBoxXnaSectionInheritance.Enabled)
                {
                    checkBoxXnaSectionInheritance.Checked =
                        Directory.Exists(Path.Combine(folderBrowserDialog.SelectedPath, "INI", "Base"));
                }
                LoadRulesIniSources();
                LoadArtIniSources();
            }
        }

        private void CopyFilesToIniFolder(string gamePath, List<string> fileNames)
        {
            var iniPath = Path.Combine(gamePath, "INI");
            if (!Directory.Exists(iniPath))
            {
                Directory.CreateDirectory(iniPath);
            }
            foreach (var fileName in fileNames)
            {
                File.Copy(Path.Combine(gamePath, fileName), Path.Combine(iniPath, fileName), false);
            }
        }

        private void CopyBaseFilesToDefaultFile(string gamePath, List<string> fileNames)
        {
            var iniBasePath = Path.Combine(gamePath, "INI", "Base");
            if (Directory.Exists(iniBasePath))
            {
                foreach (var fileName in fileNames)
                {
                    File.Copy(Path.Combine(iniBasePath, fileName), 
                        Path.Combine(iniBasePath, Path.GetFileNameWithoutExtension(fileName) + "-default.ini"), false);
                }
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
            CurrentModSetting.HasPhobosSectionInheritance = checkBoxPhobosSectionInheritance.Checked;
            CurrentModSetting.HasVinifera = checkBoxVinifera.Checked;
            CurrentModSetting.HasSectionInheritance = checkBoxXnaSectionInheritance.Checked;
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
            // ini name match detection
            var nameValue = CurrentModSetting.LoadRulesIniFile().GetSection("General")?.GetValue("Name")?.Value;
            if (nameValue == null)
            {
                MessageBox.Show("The selected mod rules.ini file does not have a 'name' value in the 'General' section",
                    "Not supported mod", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            CurrentModSetting.IniNameMatchDetection = nameValue;
            // specific features check
            if (checkBoxPhobosSectionInheritance.Checked
                && !CheckClientCommandLineArgument(textGamePath.Text, CommandLineArgumentInheritance))
            {
                MessageBox.Show("The selected Phobos feature 'Section inheritance' is not activated in the current mod." + Environment.NewLine +
                                @"You must edit the file '[ModRoot]\Resources\ClientDefinitions.ini and add the command line argument" + Environment.NewLine +
                                $"'-{CommandLineArgumentInheritance}' to the value of 'ExtraCommandLineParams' in the section [Settings]" + Environment.NewLine +
                                "Please add this argument now, before you procceed here...",
                    "Phobos Feature not activated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void NewCustomModForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
