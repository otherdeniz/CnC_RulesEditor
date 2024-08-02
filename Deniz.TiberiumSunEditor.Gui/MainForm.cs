using Deniz.CCAudioPlayerCore;
using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using Infragistics.Win.UltraWinToolbars;

namespace Deniz.TiberiumSunEditor.Gui
{
    public partial class MainForm : Form
    {
        private RulesEditMainControl? _editMainControl;

        public MainForm()
        {
            InitializeComponent();
            Text = $"{Text} v{typeof(MainForm).Assembly.GetName().Version?.ToString(3)}";
            InitializeNewMenu();
        }

        public bool ShowOnlyFavoriteUnits { get; set; }

        public bool ShowOnlyFavoriteValues { get; set; }

        public string SearchText { get; set; } = "";

        private void ButtonOpen()
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                LoadFile(IniFile.Load(openFileDialog.FileName));
            }
        }

        private void ButtonSaveAs()
        {
            if (_editMainControl == null) return;
            if (_editMainControl.Model.FileType.BaseType == FileBaseType.Rules)
            {
                var relativeFolder = string.IsNullOrEmpty(_editMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                    ? "root"
                    : _editMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
                var userGamePath = _editMainControl.Model.FileType.GameDefinition.GetUserGamePath();
                if (!string.IsNullOrEmpty(userGamePath) && Directory.Exists(userGamePath))
                {
                    if (!string.IsNullOrEmpty(_editMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder))
                    {
                        userGamePath = Path.Combine(userGamePath,
                            _editMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder);
                    }
                    saveFileDialog.InitialDirectory = userGamePath;
                }
                saveFileDialog.Title = $"Save this file in games {relativeFolder} folder";
                saveFileDialog.FileName = _editMainControl.Model.FileType.GameDefinition.SaveAsFilename;
            }
            else
            {
                saveFileDialog.Title = "Save as";
                saveFileDialog.FileName = _editMainControl.Model.File.OriginalFileName;
            }
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                _editMainControl.Model.File.SaveAs(saveFileDialog.FileName);
            }
        }

        private void ButtonExportChanges()
        {
            if (_editMainControl == null) return;
            saveFileDialog.FileName = "new_snippet.ini";
            var changesFile = _editMainControl.Model.File.GetChangesFile(_editMainControl.Model.DefaultFile);
            if (!changesFile.Sections.Any())
            {
                MessageBox.Show("The current file has no changes yet", this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                changesFile.SaveAs(saveFileDialog.FileName);
            }
        }

        private void ButtonShowChanges()
        {
            if (_editMainControl == null) return;
            using (var changesForm = new ShowChangesForm())
            {
                var defaultFile = _editMainControl.Model.DefaultFile;
                var changesFile = _editMainControl.Model.File.GetChangesFile(defaultFile);
                changesForm.LoadModel(changesFile, defaultFile, _editMainControl.Model);
                changesForm.ShowDialog(this);
            }
        }

        private void ButtonGamesSettings()
        {
            using (var gamesForm = new SettingsForm())
            {
                gamesForm.LoadGames();
                gamesForm.ShowDialog(this);
                InitializeNewMenu();
            }
        }

        private void InitializeNewMenu()
        {
            var newMenuTool = (PopupMenuTool)mainToolbarsManager.Tools["New"];
            var instanceToolsToRemove = newMenuTool.Tools.OfType<ButtonTool>().Where(t => t.Key.StartsWith("NewRules:")).ToList();
            instanceToolsToRemove.ForEach(newMenuTool.Tools.Remove);
            var sharedToolsToRemove = mainToolbarsManager.Tools.OfType<ButtonTool>().Where(t => t.Key.StartsWith("NewRules:")).ToList();
            sharedToolsToRemove.ForEach(mainToolbarsManager.Tools.Remove);
            foreach (var gameDefinition in GamesFile.Instance.Games)
            {
                var newMenuButton = new ButtonTool($"NewRules:{gameDefinition.GameKey}");
                newMenuButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                {
                    Image = LogoRepository.Instance.GetLogo(gameDefinition.Logo)
                };
                newMenuButton.SharedPropsInternal.Caption = gameDefinition.NewMenuLabel;
                mainToolbarsManager.Tools.Add(newMenuButton);
                var newMenuButtonInstance = new ButtonTool($"NewRules:{gameDefinition.GameKey}");
                newMenuButtonInstance.InstanceProps.IsFirstInGroup = gameDefinition.NewMenuSeparator;
                newMenuTool.Tools.Add(newMenuButtonInstance);
            }

            var isFirst = true;
            foreach (var customMod in UserSettingsFile.Instance.CustomMods)
            {
                var baseGame = GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == customMod.BaseGameKey);
                if (baseGame == null) continue;
                var newMenuButton = new ButtonTool($"NewRules:Mod:{customMod.Key}");
                newMenuButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                {
                    Image = LogoRepository.Instance.GetLogo(customMod.LogoFile)
                };
                newMenuButton.SharedPropsInternal.Caption = $"{baseGame.GameKey} - {customMod.Name}";
                mainToolbarsManager.Tools.Add(newMenuButton);
                var newMenuButtonInstance = new ButtonTool($"NewRules:Mod:{customMod.Key}");
                newMenuButtonInstance.InstanceProps.IsFirstInGroup = isFirst;
                newMenuTool.Tools.Add(newMenuButtonInstance);
                isFirst = false;
            }
        }

        private void LoadFile(IniFile iniFile)
        {
            Cursor = Cursors.WaitCursor;
            _editMainControl = null;
            mainToolbarsManager.Tools["SaveAs"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = false;
            Application.DoEvents();
            foreach (var control in MainForm_Fill_Panel.ClientArea.Controls
                         .OfType<Control>().ToList())
            {
                MainForm_Fill_Panel.ClientArea.Controls.Remove(control);
                control.Dispose();
            }

            var fileType = FileTypeModel.ParseFile(iniFile, m =>
            {
                // detect game of the map 
                if (iniFile.OriginalFullPath != null)
                {
                    var integratedGame = GamesFile.Instance.Games.FirstOrDefault(g =>
                    {
                        var gamePath = g.GetUserGamePath();
                        return gamePath != null && iniFile.OriginalFullPath.StartsWith(gamePath);
                    });
                    if (integratedGame != null)
                    {
                        return integratedGame;
                    }
                    var customMod = UserSettingsFile.Instance.CustomMods.FirstOrDefault(m => 
                        iniFile.OriginalFullPath.StartsWith(m.GamePath));
                    if (customMod != null)
                    {
                        return customMod.ToGameDefinition();
                    }
                }
                // let the user choose the game of the map
                using (var openMapForm = new OpenMapForm())
                {
                    openMapForm.LoadMap(m);
                    if (openMapForm.ShowDialog(this) == DialogResult.OK)
                    {
                        return openMapForm.SelectedGameDefinition;
                    }
                }
                return null;
            });
            if (fileType == null)
            {
                Cursor = Cursors.Default;
                return;
            }
            CCGameRepository.Instance.Initialise(fileType.GameDefinition.GetUserGamePath(),
                fileType.GameDefinition.IsCustomMod
                    ? null
                    : fileType.GameDefinition.MixFiles);
            BitmapRepository.Instance.Initialise(fileType.GetBitmapSubFolders());
            var rootModel = new RootModel(iniFile, fileType, 
                showMissingValues: true, 
                useAres: fileType.GameDefinition.UseAres,
                usePhobos: fileType.GameDefinition.UsePhobos);
            _editMainControl = new RulesEditMainControl()
            {
                Dock = DockStyle.Fill
            };
            _editMainControl.LoadModel(rootModel);
            MainForm_Fill_Panel.ClientArea.Controls.Add(_editMainControl);
            mainToolbarsManager.Tools["SaveAs"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void SearchValues(string searchText)
        {
            if (SearchText == searchText) return;
            SearchText = searchText;
            mainToolbarsManager.Tools["SearchClear"].SharedProps.Enabled = searchText != "";
            if (_editMainControl != null)
            {
                _editMainControl.SearchText = searchText.Length > 2 ? searchText : "";
                if (searchText.Length > 2 || searchText == "")
                {
                    _editMainControl.LoadModels();
                }
            }
        }

        private void mainToolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
        {
            if (e.Tool.Key.StartsWith("NewRules:"))
            {
                if (e.Tool.Key.StartsWith("NewRules:Mod:"))
                {
                    var customMod = UserSettingsFile.Instance.CustomMods.FirstOrDefault(m => m.Key == e.Tool.Key.Substring(13));
                    if (customMod != null)
                    {
                        LoadFile(customMod.LoadRulesIniFile());
                    }
                    return;
                }
                var gameDefinition = GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == e.Tool.Key.Substring(9));
                if (gameDefinition != null)
                {
                    LoadFile(IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(gameDefinition.ResourcesDefaultIniFile)));
                }
                return;
            }
            switch (e.Tool.Key)
            {
                case "Open":
                    ButtonOpen();
                    break;
                case "SaveAs":
                    ButtonSaveAs();
                    break;
                case "Games":
                    ButtonGamesSettings();
                    break;
                case "OnlyFavoriteValues":
                    ShowOnlyFavoriteValues = !ShowOnlyFavoriteValues;
                    ((StateButtonTool)e.Tool).Checked = ShowOnlyFavoriteValues;
                    if (_editMainControl != null)
                    {
                        _editMainControl.ShowOnlyFavoriteValues = ShowOnlyFavoriteValues;
                        _editMainControl.LoadModels();
                    }
                    break;
                case "OnlyFavoriteUnits":
                    ShowOnlyFavoriteUnits = !ShowOnlyFavoriteUnits;
                    ((StateButtonTool)e.Tool).Checked = ShowOnlyFavoriteUnits;
                    if (_editMainControl != null)
                    {
                        _editMainControl.ShowOnlyFavoriteUnits = ShowOnlyFavoriteUnits;
                        _editMainControl.LoadModels();
                    }
                    break;
                case "ShowChanges":
                    ButtonShowChanges();
                    break;
                case "InsertSnippet":
                    if (_editMainControl != null)
                    {
                        Cursor = Cursors.WaitCursor;
                        if (InsertSnippetForm.InsertSnippetToModel(this, _editMainControl.Model))
                        {
                            _editMainControl.LoadModels();
                        }
                        Cursor = Cursors.Default;
                    }
                    break;
                case "ExportChanges":
                    ButtonExportChanges();
                    break;
                case "SearchClear":
                    ((TextBoxTool)mainToolbarsManager.Tools["SearchText"]).Text = "";
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var screen = Screen.FromControl(this);
            var formWidth = Convert.ToInt32(Convert.ToDecimal(screen.Bounds.Width) * 0.85m);
            if (formWidth > 2000)
            {
                formWidth = 2000;
            }
            var formHeight = Convert.ToInt32(Convert.ToDecimal(screen.Bounds.Height) * 0.85m);
            if (formHeight > 1000)
            {
                formHeight = 1000;
            }
            Top = Convert.ToInt32(Convert.ToDecimal(screen.Bounds.Height - formHeight) / 2m);
            var left = Convert.ToInt32(Convert.ToDecimal(screen.Bounds.Width - formWidth) / 2m);
            Left = left < 250 ? left : 250;
            Height = formHeight;
            Width = formWidth;
            AnimationsAsyncLoader.Instance.InitialiseUiSyncContext();
            var audStream = ResourcesRepository.Instance.ReadRandomResourcesFileStream("startup*.aud");
            AudioPlayerService.PlaySound(StupidStream.FromFileStream(audStream));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimationsAsyncLoader.Instance.Stop(false, true);
        }

        private void mainToolbarsManager_ToolValueChanged(object sender, ToolEventArgs e)
        {
            if (e.Tool.Key == "SearchText")
            {
                SearchValues(((TextBoxTool)e.Tool).Text);
            }
        }

    }
}
