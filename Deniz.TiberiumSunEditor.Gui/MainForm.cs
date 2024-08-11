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
        private RulesEditMainControl? _editRulesMainControl;
        private ArtEditMainControl? _editArtMainControl;
        private bool _doEvents;

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
                var iniFile = IniFile.Load(openFileDialog.FileName);
                var fileType = ParseFileType(iniFile);
                if (fileType != null)
                {
                    if (fileType.BaseType == FileBaseType.Art)
                    {
                        var rulesFile = fileType.GameDefinition.LoadDefaultRulesFile();
                        LoadArtFile(iniFile, fileType, rulesFile);
                    }
                    else
                    {
                        LoadRulesFile(iniFile, fileType);
                    }
                }
            }
        }

        private void ButtonSaveAs()
        {
            if (_editRulesMainControl != null)
            {
                if (_editRulesMainControl.Model.FileType.BaseType == FileBaseType.Rules)
                {
                    var relativeFolder = string.IsNullOrEmpty(_editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                        ? "root"
                        : _editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
                    var userGamePath = _editRulesMainControl.Model.FileType.GameDefinition.GetUserGamePath();
                    if (!string.IsNullOrEmpty(userGamePath) && Directory.Exists(userGamePath))
                    {
                        if (!string.IsNullOrEmpty(_editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder))
                        {
                            userGamePath = Path.Combine(userGamePath,
                                _editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder);
                        }
                        saveFileDialog.InitialDirectory = userGamePath;
                    }
                    saveFileDialog.Title = $"Save this file in games {relativeFolder} folder";
                    saveFileDialog.FileName = _editRulesMainControl.Model.FileType.GameDefinition.SaveAsFilename;
                }
                else
                {
                    saveFileDialog.Title = "Save as";
                    saveFileDialog.FileName = _editRulesMainControl.Model.File.OriginalFileName;
                }
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _editRulesMainControl.Model.File.SaveAs(saveFileDialog.FileName);
                }
            }

            if (_editArtMainControl != null)
            {
                var relativeFolder = string.IsNullOrEmpty(_editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                    ? "root"
                    : _editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
                var userGamePath = _editArtMainControl.Model.FileType.GameDefinition.GetUserGamePath();
                if (!string.IsNullOrEmpty(userGamePath) && Directory.Exists(userGamePath))
                {
                    if (!string.IsNullOrEmpty(_editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder))
                    {
                        userGamePath = Path.Combine(userGamePath,
                            _editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder);
                    }
                    saveFileDialog.InitialDirectory = userGamePath;
                }
                saveFileDialog.Title = $"Save this file in games {relativeFolder} folder";
                saveFileDialog.FileName = _editArtMainControl.Model.FileType.GameDefinition.SaveAsArtFilename;
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _editArtMainControl.Model.File.SaveAs(saveFileDialog.FileName);
                }
            }
        }

        private void ButtonExportChanges()
        {
            if (_editRulesMainControl == null) return;
            saveFileDialog.FileName = "new_snippet.ini";
            var changesFile = _editRulesMainControl.Model.File.GetChangesFile(_editRulesMainControl.Model.DefaultFile);
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
            if (_editRulesMainControl != null)
            {
                using (var changesForm = new ShowChangesForm())
                {
                    var defaultFile = _editRulesMainControl.Model.DefaultFile;
                    var changesFile = _editRulesMainControl.Model.File.GetChangesFile(defaultFile);
                    changesForm.LoadModel(changesFile, defaultFile, _editRulesMainControl.Model);
                    changesForm.ShowDialog(this);
                }
            }
            if (_editArtMainControl != null)
            {
                using (var changesForm = new ShowArtChangesForm())
                {
                    var defaultFile = _editArtMainControl.Model.DefaultFile;
                    var changesFile = _editArtMainControl.Model.File.GetChangesFile(defaultFile);
                    changesForm.LoadModel(changesFile, defaultFile, _editArtMainControl.Model.RulesModel);
                    changesForm.ShowDialog(this);
                }
            }
        }

        private void ButtonBalancingTool()
        {
            if (_editRulesMainControl == null) return;
            AnimationsAsyncLoader.Instance.Stop(true, false);
            using (var balancingForm = new BalancingToolForm())
            {
                balancingForm.LoadModel(_editRulesMainControl.Model);
                balancingForm.ShowDialog(this);
                _editRulesMainControl.LoadModels();
            }
            AnimationsAsyncLoader.Instance.Start();
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
            var instanceToolsToRemove = newMenuTool.Tools.OfType<PopupMenuTool>()
                .Where(t => t.Key.StartsWith("NewMenu:")).ToList();
            instanceToolsToRemove.ForEach(newMenuTool.Tools.Remove);
            var sharedMenusToRemove = mainToolbarsManager.Tools.OfType<PopupMenuTool>()
                .Where(t => t.Key.StartsWith("NewMenu:")).ToList();
            sharedMenusToRemove.ForEach(mainToolbarsManager.Tools.Remove);
            var sharedToolsToRemove = mainToolbarsManager.Tools.OfType<ButtonTool>()
                .Where(t => t.Key.StartsWith("NewRules:") || t.Key.StartsWith("NewArt:")).ToList();
            sharedToolsToRemove.ForEach(mainToolbarsManager.Tools.Remove);
            foreach (var gameDefinition in GamesFile.Instance.Games)
            {
                //shared tools
                var newMenuButton = new PopupMenuTool($"NewMenu:{gameDefinition.GameKey}");
                newMenuButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                {
                    Image = LogoRepository.Instance.GetLogo(gameDefinition.Logo)
                };
                newMenuButton.SharedPropsInternal.Caption = gameDefinition.NewMenuLabel;
                var newMenuRulesButton = new ButtonTool($"NewRules:{gameDefinition.GameKey}");
                newMenuRulesButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                {
                    Image = LogoRepository.Instance.GetLogo(gameDefinition.Logo)
                };
                newMenuRulesButton.SharedPropsInternal.Caption = gameDefinition.SaveAsFilename;
                mainToolbarsManager.Tools.Add(newMenuButton);
                mainToolbarsManager.Tools.Add(newMenuRulesButton);
                if (!string.IsNullOrEmpty(gameDefinition.SaveAsArtFilename))
                {
                    var newMenuArtButton = new ButtonTool($"NewArt:{gameDefinition.GameKey}");
                    newMenuArtButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                    {
                        Image = LogoRepository.Instance.GetLogo(gameDefinition.Logo)
                    };
                    newMenuArtButton.SharedPropsInternal.Caption = gameDefinition.SaveAsArtFilename;
                    mainToolbarsManager.Tools.Add(newMenuArtButton);
                }
                //instance tools
                var newMenuButtonInstance = (PopupMenuTool)newMenuTool.Tools.AddTool($"NewMenu:{gameDefinition.GameKey}");
                newMenuButtonInstance.InstanceProps.IsFirstInGroup = gameDefinition.NewMenuSeparator;
                newMenuButtonInstance.Tools.AddTool($"NewRules:{gameDefinition.GameKey}");
                if (!string.IsNullOrEmpty(gameDefinition.SaveAsArtFilename))
                {
                    newMenuButtonInstance.Tools.AddTool($"NewArt:{gameDefinition.GameKey}");
                }
            }

            var isFirst = true;
            foreach (var customMod in UserSettingsFile.Instance.CustomMods)
            {
                var baseGame = GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == customMod.BaseGameKey);
                if (baseGame == null) continue;
                //shared tools
                var newMenuButton = new PopupMenuTool($"NewMenu:{customMod.Key}");
                newMenuButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                {
                    Image = LogoRepository.Instance.GetLogo(customMod.LogoFile)
                };
                newMenuButton.SharedPropsInternal.Caption = $"{baseGame.GameKey} - {customMod.Name.Replace("&", "&&")}";
                var newMenuRulesButton = new ButtonTool($"NewRules:Mod:{customMod.Key}");
                newMenuRulesButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                {
                    Image = LogoRepository.Instance.GetLogo(customMod.LogoFile)
                };
                newMenuRulesButton.SharedPropsInternal.Caption = baseGame.SaveAsFilename;
                mainToolbarsManager.Tools.Add(newMenuButton);
                mainToolbarsManager.Tools.Add(newMenuRulesButton);
                if (!string.IsNullOrEmpty(baseGame.SaveAsArtFilename)
                    && !string.IsNullOrEmpty(customMod.ArtIniPath))
                {
                    var newMenuArtButton = new ButtonTool($"NewArt:Mod:{customMod.Key}");
                    newMenuArtButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                    {
                        Image = LogoRepository.Instance.GetLogo(customMod.LogoFile)
                    };
                    newMenuArtButton.SharedPropsInternal.Caption = baseGame.SaveAsArtFilename;
                    mainToolbarsManager.Tools.Add(newMenuArtButton);
                }
                //instance tools
                var newMenuButtonInstance = (PopupMenuTool)newMenuTool.Tools.AddTool($"NewMenu:{customMod.Key}");
                newMenuButtonInstance.InstanceProps.IsFirstInGroup = isFirst;
                newMenuButtonInstance.Tools.AddTool($"NewRules:Mod:{customMod.Key}");
                if (!string.IsNullOrEmpty(baseGame.SaveAsArtFilename)
                    && !string.IsNullOrEmpty(customMod.ArtIniPath))
                {
                    newMenuButtonInstance.Tools.AddTool($"NewArt:Mod:{customMod.Key}");
                }
                isFirst = false;
            }
        }

        private FileTypeModel? ParseFileType(IniFile iniFile, GameDefinition? overrideGameDefinition = null)
        {
            return FileTypeModel.ParseFile(iniFile, overrideGameDefinition, m =>
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
                    openMapForm.LoadFile(m);
                    if (openMapForm.ShowDialog(this) == DialogResult.OK)
                    {
                        return openMapForm.SelectedGameDefinition;
                    }
                }
                return null;
            });
        }

        private void LoadRulesFile(IniFile rulesFile, FileTypeModel? fileType)
        {
            if (fileType == null) return;
            Cursor = Cursors.WaitCursor;
            _editRulesMainControl = null;
            _editArtMainControl = null;
            mainToolbarsManager.Tools["SaveAs"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = false;
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
            CCGameRepository.Instance.Initialise(fileType.GameDefinition,
                fileType.GameDefinition.IsCustomMod
                    ? null
                    : fileType.GameDefinition.MixFiles);
            BitmapRepository.Instance.Initialise(fileType.GetBitmapSubFolders());
            var rootModel = new RulesRootModel(rulesFile, fileType, 
                showMissingValues: true, 
                useAres: fileType.GameDefinition.UseAres,
                usePhobos: fileType.GameDefinition.UsePhobos);
            _editRulesMainControl = new RulesEditMainControl()
            {
                Dock = DockStyle.Fill
            };
            _editRulesMainControl.LoadModel(rootModel);
            MainForm_Fill_Panel.ClientArea.Controls.Add(_editRulesMainControl);
            mainToolbarsManager.Tools["SaveAs"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void LoadArtFile(IniFile artFile, FileTypeModel? fileType, IniFile rulesFile)
        {
            if (fileType == null) return;
            Cursor = Cursors.WaitCursor;
            _editRulesMainControl = null;
            _editArtMainControl = null;
            mainToolbarsManager.Tools["SaveAs"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = false;
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
            CCGameRepository.Instance.Initialise(fileType.GameDefinition,
                fileType.GameDefinition.IsCustomMod
                    ? null
                    : fileType.GameDefinition.MixFiles);
            BitmapRepository.Instance.Initialise(fileType.GetBitmapSubFolders());
            var rulesRootModel = new RulesRootModel(rulesFile, fileType,
                showMissingValues: true,
                useAres: fileType.GameDefinition.UseAres,
                usePhobos: fileType.GameDefinition.UsePhobos);
            var artRootMdel = new ArtRootModel(rulesRootModel, artFile,
                showMissingValues: true);
            _editArtMainControl = new ArtEditMainControl()
            {
                Dock = DockStyle.Fill
            };
            _editArtMainControl.LoadModel(artRootMdel);
            MainForm_Fill_Panel.ClientArea.Controls.Add(_editArtMainControl);
            mainToolbarsManager.Tools["SaveAs"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void SearchValues(string searchText)
        {
            if (SearchText == searchText) return;
            SearchText = searchText;
            mainToolbarsManager.Tools["SearchClear"].SharedProps.Enabled = searchText != "";
            if (_editRulesMainControl != null)
            {
                _editRulesMainControl.SearchText = searchText.Length > 2 ? searchText : "";
                if (searchText.Length > 2 || searchText == "")
                {
                    _editRulesMainControl.LoadModels();
                }
            }
            else if (_editArtMainControl != null)
            {
                _editArtMainControl.SearchText = searchText.Length > 2 ? searchText : "";
                if (searchText.Length > 2 || searchText == "")
                {
                    _editArtMainControl.LoadModels();
                }
            }
        }

        private void mainToolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
        {
            if (!_doEvents) return;
            if (e.Tool.Key.StartsWith("NewRules:"))
            {
                if (e.Tool.Key.StartsWith("NewRules:Mod:"))
                {
                    var customMod = UserSettingsFile.Instance.CustomMods.FirstOrDefault(m => m.Key == e.Tool.Key.Substring(13));
                    if (customMod != null)
                    {
                        var rulesFile = customMod.LoadRulesIniFile();
                        LoadRulesFile(rulesFile, ParseFileType(rulesFile, customMod.ToGameDefinition()));
                    }
                    return;
                }
                var gameDefinition = GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == e.Tool.Key.Substring(9));
                if (gameDefinition != null)
                {
                    var rulesFile = gameDefinition.LoadDefaultRulesFile();
                    LoadRulesFile(rulesFile, ParseFileType(rulesFile, gameDefinition));
                }
                return;
            }
            if (e.Tool.Key.StartsWith("NewArt:"))
            {
                if (e.Tool.Key.StartsWith("NewArt:Mod:"))
                {
                    var customMod = UserSettingsFile.Instance.CustomMods.FirstOrDefault(m => m.Key == e.Tool.Key.Substring(11));
                    if (customMod != null)
                    {
                        var artFile = customMod.LoadArtIniFile();
                        if (artFile != null)
                        {
                            var rulesFile = customMod.LoadRulesIniFile();
                            LoadArtFile(artFile, ParseFileType(artFile, customMod.ToGameDefinition()), rulesFile);
                        }
                    }
                    return;
                }
                var gameDefinition = GamesFile.Instance.Games.FirstOrDefault(g => g.GameKey == e.Tool.Key.Substring(7));
                if (gameDefinition != null)
                {
                    var artFile = gameDefinition.LoadDefaultArtFile();
                    var rulesFile = gameDefinition.LoadDefaultRulesFile();
                    LoadArtFile(artFile, ParseFileType(artFile, gameDefinition), rulesFile);
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
                case "SettingOpeningSound":
                    UserSettingsFile.Instance.SettingPlayOpeningSound =
                        !UserSettingsFile.Instance.SettingPlayOpeningSound;
                    UserSettingsFile.Instance.Save();
                    ((StateButtonTool)e.Tool).Checked = UserSettingsFile.Instance.SettingPlayOpeningSound;
                    break;
                case "SettingCheckUpdates":
                    UserSettingsFile.Instance.SettingAutoUpdate =
                        !UserSettingsFile.Instance.SettingAutoUpdate;
                    UserSettingsFile.Instance.Save();
                    ((StateButtonTool)e.Tool).Checked = UserSettingsFile.Instance.SettingAutoUpdate;
                    break;
                case "OnlyFavoriteValues":
                    ShowOnlyFavoriteValues = !ShowOnlyFavoriteValues;
                    ((StateButtonTool)e.Tool).Checked = ShowOnlyFavoriteValues;
                    if (_editRulesMainControl != null)
                    {
                        _editRulesMainControl.ShowOnlyFavoriteValues = ShowOnlyFavoriteValues;
                        _editRulesMainControl.LoadModels();
                    }
                    else if (_editArtMainControl != null)
                    {
                        _editArtMainControl.ShowOnlyFavoriteValues = ShowOnlyFavoriteValues;
                        _editArtMainControl.LoadModels();
                    }
                    break;
                case "OnlyFavoriteUnits":
                    ShowOnlyFavoriteUnits = !ShowOnlyFavoriteUnits;
                    ((StateButtonTool)e.Tool).Checked = ShowOnlyFavoriteUnits;
                    if (_editRulesMainControl != null)
                    {
                        _editRulesMainControl.ShowOnlyFavoriteUnits = ShowOnlyFavoriteUnits;
                        _editRulesMainControl.LoadModels();
                    }
                    else if (_editArtMainControl != null)
                    {
                        _editArtMainControl.ShowOnlyFavoriteUnits = ShowOnlyFavoriteUnits;
                        _editArtMainControl.LoadModels();
                    }
                    break;
                case "ShowChanges":
                    ButtonShowChanges();
                    break;
                case "BalancingTool":
                    ButtonBalancingTool();
                    break;
                case "InsertSnippet":
                    if (_editRulesMainControl != null)
                    {
                        Cursor = Cursors.WaitCursor;
                        if (InsertSnippetForm.InsertSnippetToModel(this, _editRulesMainControl.Model))
                        {
                            _editRulesMainControl.LoadModels();
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
            if (UserSettingsFile.Instance.SettingPlayOpeningSound)
            {
                ((StateButtonTool)mainToolbarsManager.Tools["SettingOpeningSound"]).Checked = true;
                var wavStream = ResourcesRepository.Instance.ReadRandomResourcesFileStream("startup_*.wav");
                if (wavStream != null)
                {
                    AudioPlayerService.PlaySound(StupidStream.FromFileStream(wavStream));
                }
            }
            if (UserSettingsFile.Instance.SettingAutoUpdate)
            {
                ((StateButtonTool)mainToolbarsManager.Tools["SettingCheckUpdates"]).Checked = true;
                AutoUpdateManager.CheckForUpdate(this);
            }
            _doEvents = true;
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
