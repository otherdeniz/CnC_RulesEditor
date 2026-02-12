using Deniz.CCAudioPlayerCore;
using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Exceptions;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using Infragistics.Win.UltraWinToolbars;
using System.Diagnostics;
using System.Security.Policy;

namespace Deniz.TiberiumSunEditor.Gui
{
    public partial class MainForm : Form
    {
        private SynchronizationContext _uiSyncContext = null!;
        private RulesEditMainControl? _editRulesMainControl;
        private ArtEditMainControl? _editArtMainControl;
        private AiEditMainControl? _editAiMainControl;
        private bool _filterEnabled;
        private bool _iniEditorEnabled;
        private bool _doEvents;

        public MainForm()
        {
            InitializeComponent();
            Text = $"{Text} v{typeof(MainForm).Assembly.GetName().Version?.ToString(3)}";
            InitialiseThemes();
            InitializeNewMenu();
            InitializeRecentFilesMenu();
        }

        public bool ShowOnlyFavoriteUnits { get; set; }

        public bool ShowOnlyFavoriteValues { get; set; }

        public string SearchText { get; set; } = "";

        private void ButtonOpen()
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var fileType = OpenFile(openFileDialog.FileName);
                if (fileType != null 
                    && fileType.BaseType != FileBaseType.Unknown)
                {
                    UserSettingsFile.Instance.AddRecentFile(openFileDialog.FileName, fileType);
                    InitializeRecentFilesMenu();
                }
            }
        }

        private void ButtonOpenRecent(string fileName)
        {
            var recentFile = UserSettingsFile.Instance.OpenRecentFile(fileName);
            InitializeRecentFilesMenu();
            OpenFile(recentFile.Setting.FilePath, recentFile.Definition);
        }

        private FileTypeModel? OpenFile(string filePath, GameDefinition? overrideGameDefinition = null)
        {
            var iniFile = IniFile.Load(filePath);
            var fileType = ParseFileType(iniFile, overrideGameDefinition);
            if (fileType != null)
            {
                if (fileType.BaseType == FileBaseType.Art)
                {
                    var rulesFile = fileType.GameDefinition.LoadCurrentRulesFile();
                    LoadArtFile(iniFile, fileType, rulesFile);
                }
                else if (fileType.BaseType == FileBaseType.Ai)
                {
                    var rulesFile = fileType.GameDefinition.LoadCurrentRulesFile();
                    LoadAiFile(iniFile, fileType, rulesFile);
                }
                else
                {
                    LoadRulesFile(iniFile, fileType);
                }
                mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = $"Save File ({filePath})";
                mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = true;
            }
            return fileType;
        }

        private void ButtonSaveInGame()
        {
            if (_editRulesMainControl != null)
            {
                var gameDefinition = _editRulesMainControl.Model.FileType.GameDefinition;
                var gamePath = gameDefinition.GetUserGamePath();
                if (gamePath != null)
                {
                    var saveFilePath = string.IsNullOrEmpty(gameDefinition.SaveAsRelativeToGameFolder)
                        ? Path.Combine(gamePath, gameDefinition.SaveAsFilename)
                        : Path.Combine(gamePath, gameDefinition.SaveAsRelativeToGameFolder,
                            gameDefinition.SaveAsFilename);
                    _editRulesMainControl.Model.File.SaveAs(saveFilePath);
                    var relativeFolder = string.IsNullOrEmpty(gameDefinition.SaveAsRelativeToGameFolder)
                        ? "root"
                        : gameDefinition.SaveAsRelativeToGameFolder;
                    _editRulesMainControl.UpdateHeaderFilePath($"saved in game's {relativeFolder}");
                }
                return;
            }

            if (_editArtMainControl != null)
            {
                var gameDefinition = _editArtMainControl.Model.FileType.GameDefinition;
                var gamePath = gameDefinition.GetUserGamePath();
                if (gamePath != null)
                {
                    var saveFilePath = string.IsNullOrEmpty(gameDefinition.SaveAsRelativeToGameFolder)
                        ? Path.Combine(gamePath, gameDefinition.SaveAsArtFilename)
                        : Path.Combine(gamePath, gameDefinition.SaveAsRelativeToGameFolder,
                            gameDefinition.SaveAsArtFilename);
                    _editArtMainControl.Model.File.SaveAs(saveFilePath);
                    var relativeFolder = string.IsNullOrEmpty(gameDefinition.SaveAsRelativeToGameFolder)
                        ? "root"
                        : gameDefinition.SaveAsRelativeToGameFolder;
                    _editArtMainControl.UpdateHeaderFilePath($"saved in game's {relativeFolder}");
                }
            }

            if (_editAiMainControl != null)
            {
                var gameDefinition = _editAiMainControl.Model.FileType.GameDefinition;
                var gamePath = gameDefinition.GetUserGamePath();
                if (gamePath != null)
                {
                    var saveFilePath = string.IsNullOrEmpty(gameDefinition.SaveAsRelativeToGameFolder)
                        ? Path.Combine(gamePath, gameDefinition.SaveAsAiFilename)
                        : Path.Combine(gamePath, gameDefinition.SaveAsRelativeToGameFolder,
                            gameDefinition.SaveAsAiFilename);
                    _editAiMainControl.Model.File.SaveAs(saveFilePath);
                    var relativeFolder = string.IsNullOrEmpty(gameDefinition.SaveAsRelativeToGameFolder)
                        ? "root"
                        : gameDefinition.SaveAsRelativeToGameFolder;
                    _editAiMainControl.UpdateHeaderFilePath($"saved in game's {relativeFolder}");
                }
            }
        }

        private void ButtonSaveFile()
        {
            _editRulesMainControl?.Model.File.Save();
            _editArtMainControl?.Model.File.Save();
            _editAiMainControl?.Model.File.Save();
        }

        private void ButtonSaveAs()
        {
            if (_editRulesMainControl != null)
            {
                SaveAs(_editRulesMainControl.Model, _editRulesMainControl.Model.FileType.GameDefinition.SaveAsFilename);
                _editRulesMainControl.UpdateHeaderFilePath();
            }
            else if (_editArtMainControl != null)
            {
                SaveAs(_editArtMainControl.Model, _editArtMainControl.Model.FileType.GameDefinition.SaveAsArtFilename);
                _editArtMainControl.UpdateHeaderFilePath();
            }
            else if (_editAiMainControl != null)
            {
                SaveAs(_editAiMainControl.Model, _editAiMainControl.Model.FileType.GameDefinition.SaveAsAiFilename);
                _editAiMainControl.UpdateHeaderFilePath();
            }
        }

        private void SaveAs(IRootModel model, string saveAsFilename)
        {
            var userGamePath = model.FileType.GameDefinition.GetUserGamePath();
            if (string.IsNullOrEmpty(userGamePath) || !Directory.Exists(userGamePath))
            {
                var relativeFolder = string.IsNullOrEmpty(model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                    ? "root"
                    : model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
                saveFileDialog.Title = $"Save this file in games {relativeFolder} folder";
                saveFileDialog.FileName = saveAsFilename;

            }
            else
            {
                saveFileDialog.Title = "Save as";
                saveFileDialog.FileName = model.File.FileName;
            }
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                model.File.SaveAs(saveFileDialog.FileName, true);
                mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = $"Save File ({saveFileDialog.FileName})";
                mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = true;
                UserSettingsFile.Instance.AddRecentFile(saveFileDialog.FileName, model.FileType);
                InitializeRecentFilesMenu();
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
            else if (_editArtMainControl != null)
            {
                using (var changesForm = new ShowArtChangesForm())
                {
                    var defaultFile = _editArtMainControl.Model.DefaultFile;
                    var changesFile = _editArtMainControl.Model.File.GetChangesFile(defaultFile);
                    changesForm.LoadModel(changesFile, defaultFile, _editArtMainControl.Model.RulesModel);
                    changesForm.ShowDialog(this);
                }
            }
            else if (_editAiMainControl != null)
            {
                using (var changesForm = new ShowAiChangesForm())
                {
                    var defaultFile = _editAiMainControl.Model.DefaultFile;
                    var changesFile = _editAiMainControl.Model.File.GetChangesFile(defaultFile);
                    changesForm.LoadModel(changesFile, defaultFile, _editAiMainControl.Model.RulesModel);
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
                _editRulesMainControl.ReloadModels();
            }
        }

        private void ButtonMixBrowserTool()
        {
            var form = new MixBrowserForm();
            form.InitDropDowns();
            form.Show(this);
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

        private void InitialiseThemes()
        {
            var themesMenuTool = (PopupMenuTool)mainToolbarsManager.Tools["ThemeMenu"];
            foreach (var themeDefinition in ThemesFile.Instance.Themes)
            {
                var themesButton = new StateButtonTool($"UseTheme:{themeDefinition.Name}");
                themesButton.SharedPropsInternal.Caption = themeDefinition.Name;
                mainToolbarsManager.Tools.Add(themesButton);
                var instancceButton = themesMenuTool.Tools.AddTool($"UseTheme:{themeDefinition.Name}");
                if (ThemeManager.Instance.CurrentTheme.Name == themeDefinition.Name)
                {
                    ((StateButtonTool)instancceButton).Checked = true;
                    ThemeManager.Instance.UseTheme(this);
                    ThemeManager.Instance.UseTheme(UnitPictureGenerator.Instance);
                    ThemeManager.Instance.UseTheme(SmallUnitPictureGenerator.Instance);
                    ThemeManager.Instance.UseTheme(mainToolbarsManager);
                }
            }
        }

        private void UseTheme(string themeName)
        {
            _doEvents = false;
            var themesMenuTool = (PopupMenuTool)mainToolbarsManager.Tools["ThemeMenu"];
            foreach (var stateButtonTool in themesMenuTool.Tools.OfType<StateButtonTool>())
            {
                stateButtonTool.Checked = stateButtonTool.SharedPropsInternal.Caption == themeName;
            }
            _doEvents = true;
            ThemeManager.Instance.LoadSelectedTheme(themeName);
            ThemeManager.Instance.UseTheme(this);
            ThemeManager.Instance.UseTheme(UnitPictureGenerator.Instance);
            ThemeManager.Instance.UseTheme(SmallUnitPictureGenerator.Instance);
            ThemeManager.Instance.UseTheme(mainToolbarsManager);
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
            BitmapRepository.Instance.InitBlanks();
            CCGameRepository.Instance.ClearAnimationsCache();
            _editRulesMainControl?.ReloadModels();
            _editArtMainControl?.ReloadModels();
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
                .Where(t => t.Key.StartsWith("NewRules:") 
                            || t.Key.StartsWith("NewArt:") 
                            || t.Key.StartsWith("NewAi:")).ToList();
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
                if (!string.IsNullOrEmpty(gameDefinition.SaveAsAiFilename))
                {
                    var newMenuAiButton = new ButtonTool($"NewAi:{gameDefinition.GameKey}");
                    newMenuAiButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                    {
                        Image = LogoRepository.Instance.GetLogo(gameDefinition.Logo)
                    };
                    newMenuAiButton.SharedPropsInternal.Caption = gameDefinition.SaveAsAiFilename;
                    mainToolbarsManager.Tools.Add(newMenuAiButton);
                }
                //instance tools
                var newMenuButtonInstance = (PopupMenuTool)newMenuTool.Tools.AddTool($"NewMenu:{gameDefinition.GameKey}");
                newMenuButtonInstance.InstanceProps.IsFirstInGroup = gameDefinition.NewMenuSeparator;
                newMenuButtonInstance.Tools.AddTool($"NewRules:{gameDefinition.GameKey}");
                if (!string.IsNullOrEmpty(gameDefinition.SaveAsArtFilename))
                {
                    newMenuButtonInstance.Tools.AddTool($"NewArt:{gameDefinition.GameKey}");
                }
                if (!string.IsNullOrEmpty(gameDefinition.SaveAsAiFilename))
                {
                    newMenuButtonInstance.Tools.AddTool($"NewAi:{gameDefinition.GameKey}");
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
                if (!string.IsNullOrEmpty(baseGame.SaveAsAiFilename)
                    && !string.IsNullOrEmpty(customMod.AiIniPath))
                {
                    var newMenuAiButton = new ButtonTool($"NewAi:Mod:{customMod.Key}");
                    newMenuAiButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                    {
                        Image = LogoRepository.Instance.GetLogo(customMod.LogoFile)
                    };
                    newMenuAiButton.SharedPropsInternal.Caption = baseGame.SaveAsAiFilename;
                    mainToolbarsManager.Tools.Add(newMenuAiButton);
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
                if (!string.IsNullOrEmpty(baseGame.SaveAsAiFilename)
                    && !string.IsNullOrEmpty(customMod.AiIniPath))
                {
                    newMenuButtonInstance.Tools.AddTool($"NewAi:Mod:{customMod.Key}");
                }
                isFirst = false;
            }
        }

        private void InitializeRecentFilesMenu()
        {
            var recentMenuTool = (PopupMenuTool)mainToolbarsManager.Tools["RecentFilesMenu"];
            var instanceToolsToRemove = recentMenuTool.Tools.OfType<ButtonTool>()
                .Where(t => t.Key.StartsWith("OpenRecent:")).ToList();
            instanceToolsToRemove.ForEach(recentMenuTool.Tools.Remove);
            var sharedToolsToRemove = mainToolbarsManager.Tools.OfType<ButtonTool>()
                .Where(t => t.Key.StartsWith("OpenRecent:")).ToList();
            sharedToolsToRemove.ForEach(mainToolbarsManager.Tools.Remove);
            foreach (var recentFile in UserSettingsFile.Instance.GetRecentFiles())
            {
                var recentButton = new ButtonTool($"OpenRecent:{recentFile.Setting.FilePath}");
                recentButton.SharedPropsInternal.Caption = $"[{recentFile.Setting.FileType}] {recentFile.Setting.FilePath}";
                recentButton.SharedPropsInternal.AppearancesSmall.Appearance = new Infragistics.Win.Appearance
                {
                    Image = LogoRepository.Instance.GetLogo(recentFile.Definition.CustomMod != null
                        ? recentFile.Definition.CustomMod.LogoFile
                        : recentFile.Definition.Logo)
                };
                mainToolbarsManager.Tools.Add(recentButton);
                recentMenuTool.Tools.AddTool($"OpenRecent:{recentFile.Setting.FilePath}");
            }

        }

        private FileTypeModel? ParseFileType(IniFile iniFile, GameDefinition? overrideGameDefinition = null)
        {
            return FileTypeModel.ParseFile(iniFile, overrideGameDefinition, (s, t) =>
            {
                // detect game of the file
                GameDefinition? gameDefinition = null;
                if (iniFile.OriginalFullPath != null)
                {
                    var integratedGame = GamesFile.Instance.Games.FirstOrDefault(g =>
                    {
                        var gamePath = g.GetUserGamePath();
                        return gamePath != null && iniFile.OriginalFullPath.StartsWith(gamePath);
                    });
                    if (integratedGame != null)
                    {
                        gameDefinition = integratedGame;
                    }
                    else
                    {
                        var customMod = UserSettingsFile.Instance.CustomMods.FirstOrDefault(m =>
                            iniFile.OriginalFullPath.StartsWith(m.GamePath));
                        gameDefinition = customMod?.ToGameDefinition();
                    }
                }

                if (gameDefinition != null && t != FileBaseType.Unknown)
                {
                    return (gameDefinition, t);
                }

                // let the user choose the game and type of the file
                using (var openMapForm = new OpenUnknownFileForm())
                {
                    openMapForm.LoadFile(s, t, gameDefinition);
                    if (openMapForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (openMapForm.SelectedGameDefinition != null)
                        {
                            return (openMapForm.SelectedGameDefinition, openMapForm.SelectedFileType);
                        }
                    }
                }
                return null;
            });
        }

        private void CloseFile()
        {
            _editRulesMainControl = null;
            _editArtMainControl = null;
            _editAiMainControl = null;
            mainToolbarsManager.Tools["SaveMenu"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = "Save File";
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ToolsMenu"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["MixBrowser"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SnippetsMenu"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["Filter"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["INI-Editor"].SharedProps.Enabled = false;
            ((TextBoxTool)mainToolbarsManager.Tools["SearchText"]).Text = "";
            Application.DoEvents();
            foreach (var control in panelMain.Controls
                         .OfType<Control>().ToList())
            {
                panelMain.Controls.Remove(control);
                control.Dispose();
            }
        }

        private void ButtonOpenCompareFiles()
        {
            var compareModel = OpenCompareFilesForm.ExecuteOpen(this);
            if (compareModel == null) return;
            Cursor = Cursors.WaitCursor;
            CloseFile();
            CCGameRepository.Instance.Initialise(compareModel.FileType.GameDefinition,
                compareModel.FileType.GameDefinition.IsCustomMod
                    ? null
                    : compareModel.FileType.GameDefinition.MixFiles);
            BitmapRepository.Instance.Initialise(compareModel.FileType.GetBitmapSubFolders());
            if (compareModel is RulesRootModel rulesRootModel)
            {
                _editRulesMainControl = new RulesEditMainControl()
                {
                    Dock = DockStyle.Fill,
                    FilterVisible = _filterEnabled
                };
                _editRulesMainControl.ReloadFile += (sender, args) =>
                {
                    MessageBox.Show("Auto-Reload of compared files not supported. Please Re-Open manually", "Not supported", 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                };
                ThemeManager.Instance.UseTheme(_editRulesMainControl);
                _editRulesMainControl.LoadModel(rulesRootModel);
                panelMain.Controls.Add(_editRulesMainControl);
                mainToolbarsManager.Tools["ToolsMenu"].SharedProps.Enabled = true;
                mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = true;
                mainToolbarsManager.Tools["SnippetsMenu"].SharedProps.Enabled = true;
                mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = true;
                mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = true;
            }
            else if (compareModel is ArtRootModel artRootModel)
            {
                _editArtMainControl = new ArtEditMainControl()
                {
                    Dock = DockStyle.Fill,
                    FilterVisible = _filterEnabled
                };
                _editArtMainControl.ReloadFile += (sender, args) =>
                {
                    MessageBox.Show("Auto-Reload of compared files not supported. Please Re-Open manually", "Not supported",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                };
                ThemeManager.Instance.UseTheme(_editArtMainControl);
                _editArtMainControl.LoadModel(artRootModel);
                panelMain.Controls.Add(_editArtMainControl);
            }
            else
            {
                return;
            }
            mainToolbarsManager.Tools["SaveMenu"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["Filter"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["INI-Editor"].SharedProps.Enabled = false;
            var relativeFolder = string.IsNullOrEmpty(compareModel.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                ? "root"
                : compareModel.FileType.GameDefinition.SaveAsRelativeToGameFolder;
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Caption = $"Save in Game's {relativeFolder}";
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Enabled =
                compareModel.FileType.GameDefinition.GetUserGamePath() != null;
            Cursor = Cursors.Default;
        }

        private void LoadRulesFile(IniFile rulesFile, FileTypeModel? fileType)
        {
            if (fileType == null) return;
            Cursor = Cursors.WaitCursor;
            CloseFile();
            CCGameRepository.Instance.Initialise(fileType.GameDefinition,
                fileType.GameDefinition.IsCustomMod
                    ? null
                    : fileType.GameDefinition.MixFiles);
            BitmapRepository.Instance.Initialise(fileType.GetBitmapSubFolders());
            var rootModel = new RulesRootModel(rulesFile, fileType, 
                showMissingValues: true, 
                useAres: fileType.GameDefinition.UseAres,
                usePhobos: fileType.GameDefinition.UsePhobos,
                usePhobosSectionInheritance: fileType.GameDefinition.UsePhobosSectionInheritance,
                useVinifera: fileType.GameDefinition.UseVinifera,
                useSectionInheritance: fileType.GameDefinition.UseSectionInheritance);
            _editRulesMainControl = new RulesEditMainControl()
            {
                Dock = DockStyle.Fill,
                FilterVisible = _filterEnabled,
                IniEditorVisible = _iniEditorEnabled
            };
            _editRulesMainControl.ReloadFile += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(rulesFile.OriginalFullPath))
                {
                    OpenFile(rulesFile.OriginalFullPath, fileType.GameDefinition);
                }
            };
            ThemeManager.Instance.UseTheme(_editRulesMainControl);
            _editRulesMainControl.LoadModel(rootModel);
            panelMain.Controls.Add(_editRulesMainControl);
            mainToolbarsManager.Tools["SaveMenu"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ToolsMenu"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["MixBrowser"].SharedProps.Enabled = CCGameRepository.Instance.IsLoaded;
            mainToolbarsManager.Tools["SnippetsMenu"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["Filter"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["INI-Editor"].SharedProps.Enabled = true;
            var relativeFolder = string.IsNullOrEmpty(_editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                ? "root"
                : _editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Caption = $"Save in Game's {relativeFolder}";
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Enabled =
                fileType.GameDefinition.GetUserGamePath() != null
                && fileType.BaseType == FileBaseType.Rules;
            Cursor = Cursors.Default;
        }

        private void LoadArtFile(IniFile artFile, FileTypeModel? fileType, IniFile rulesFile)
        {
            if (fileType == null) return;
            Cursor = Cursors.WaitCursor;
            CloseFile();
            CCGameRepository.Instance.Initialise(fileType.GameDefinition,
                fileType.GameDefinition.IsCustomMod
                    ? null
                    : fileType.GameDefinition.MixFiles);
            BitmapRepository.Instance.Initialise(fileType.GetBitmapSubFolders());
            var rulesRootModel = new RulesRootModel(rulesFile, fileType,
                showMissingValues: true,
                useAres: fileType.GameDefinition.UseAres,
                usePhobos: fileType.GameDefinition.UsePhobos,
                useVinifera: fileType.GameDefinition.UseVinifera);
            var artRootModel = new ArtRootModel(rulesRootModel, artFile,
                showMissingValues: true);
            _editArtMainControl = new ArtEditMainControl()
            {
                Dock = DockStyle.Fill,
                FilterVisible = _filterEnabled,
                IniEditorVisible = _iniEditorEnabled
            };
            _editArtMainControl.ReloadFile += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(rulesFile.OriginalFullPath))
                {
                    OpenFile(rulesFile.OriginalFullPath, fileType.GameDefinition);
                }
            };
            ThemeManager.Instance.UseTheme(_editArtMainControl);
            _editArtMainControl.LoadModel(artRootModel);
            panelMain.Controls.Add(_editArtMainControl);
            mainToolbarsManager.Tools["SaveMenu"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ToolsMenu"].SharedProps.Enabled = CCGameRepository.Instance.IsLoaded;
            mainToolbarsManager.Tools["MixBrowser"].SharedProps.Enabled = CCGameRepository.Instance.IsLoaded;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["Filter"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["INI-Editor"].SharedProps.Enabled = true;
            var relativeFolder = string.IsNullOrEmpty(_editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                ? "root"
                : _editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Caption = $"Save in Game's {relativeFolder}";
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Enabled =
                fileType.GameDefinition.GetUserGamePath() != null;
            Cursor = Cursors.Default;
        }

        private void LoadAiFile(IniFile aiFile, FileTypeModel? fileType, IniFile rulesFile)
        {
            if (fileType == null) return;
            Cursor = Cursors.WaitCursor;
            CloseFile();
            CCGameRepository.Instance.Initialise(fileType.GameDefinition,
                fileType.GameDefinition.IsCustomMod
                    ? null
                    : fileType.GameDefinition.MixFiles);
            BitmapRepository.Instance.Initialise(fileType.GetBitmapSubFolders());
            var rulesRootModel = new RulesRootModel(rulesFile, fileType,
                showMissingValues: true,
                useAres: fileType.GameDefinition.UseAres,
                usePhobos: fileType.GameDefinition.UsePhobos,
                useVinifera: fileType.GameDefinition.UseVinifera);
            var aiRootModel = new AiRootModel(rulesRootModel, aiFile,
                showMissingValues: false);
            _editAiMainControl = new AiEditMainControl()
            {
                Dock = DockStyle.Fill
                //FilterVisible = _filterEnabled
            };
            _editAiMainControl.ReloadFile += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(rulesFile.OriginalFullPath))
                {
                    OpenFile(rulesFile.OriginalFullPath, fileType.GameDefinition);
                }
            };
            ThemeManager.Instance.UseTheme(_editAiMainControl);
            _editAiMainControl.LoadModel(aiRootModel);
            panelMain.Controls.Add(_editAiMainControl);
            mainToolbarsManager.Tools["SaveMenu"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ToolsMenu"].SharedProps.Enabled = CCGameRepository.Instance.IsLoaded;
            mainToolbarsManager.Tools["MixBrowser"].SharedProps.Enabled = CCGameRepository.Instance.IsLoaded;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["Filter"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["INI-Editor"].SharedProps.Enabled = false;
            var relativeFolder = string.IsNullOrEmpty(_editAiMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                ? "root"
                : _editAiMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Caption = $"Save in Game's {relativeFolder}";
            mainToolbarsManager.Tools["SaveInGame"].SharedProps.Enabled =
                fileType.GameDefinition.GetUserGamePath() != null;
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
                    _editRulesMainControl.ReloadModels();
                }
            }
            else if (_editArtMainControl != null)
            {
                _editArtMainControl.SearchText = searchText.Length > 2 ? searchText : "";
                if (searchText.Length > 2 || searchText == "")
                {
                    _editArtMainControl.ReloadModels();
                }
            }
            else if (_editAiMainControl != null)
            {
                _editAiMainControl.SearchText = searchText.Length > 2 ? searchText : "";
                if (searchText.Length > 2 || searchText == "")
                {
                    _editAiMainControl.ReloadModels();
                }
            }
        }

        private void InitUserSettings()
        {
            _uiSyncContext = SynchronizationContext.Current
                             ?? throw new RuntimeException("Fatal: no Current SynchronizationContext");
            UserSettingsFile.ExternalChanged += (sender, args) =>
            {
                _uiSyncContext.Send(state =>
                {
                    _doEvents = false;
                    InitializeNewMenu();
                    InitializeRecentFilesMenu();
                    LoadUserSettings();
                    if (ThemeManager.Instance.CurrentTheme.Name != UserSettingsFile.Instance.SelectedTheme)
                    {
                        UseTheme(UserSettingsFile.Instance.SelectedTheme);
                    }
                    _doEvents = true;
                }, null);
            };
            LoadUserSettings();
        }

        private void ButtonShowFilter()
        {
            _filterEnabled = !_filterEnabled;
            if (_editRulesMainControl != null)
            {
                _editRulesMainControl.FilterVisible = _filterEnabled;
            }
            else if (_editArtMainControl != null)
            {
                _editArtMainControl.FilterVisible = _filterEnabled;
            }

            _doEvents = false;
            ((StateButtonTool)mainToolbarsManager.Tools["Filter"]).Checked = _filterEnabled;
            _doEvents = true;
        }

        private void ButtonShowIniEditor(bool enabled)
        {
            _iniEditorEnabled = enabled;
            if (_editRulesMainControl != null)
            {
                _editRulesMainControl.IniEditorVisible = _iniEditorEnabled;
            }
            else if (_editArtMainControl != null)
            {
                _editArtMainControl.IniEditorVisible = _iniEditorEnabled;
            }

            _doEvents = false;
            ((StateButtonTool)mainToolbarsManager.Tools["INI-Editor"]).Checked = _iniEditorEnabled;
            _doEvents = true;
        }

        private void LoadUserSettings()
        {
            _doEvents = false;
            ((StateButtonTool)mainToolbarsManager.Tools["INI-Editor"]).Checked = _iniEditorEnabled;

            ((StateButtonTool)mainToolbarsManager.Tools["SettingOpeningSound"]).Checked = 
                UserSettingsFile.Instance.SettingPlayOpeningSound;

            ((StateButtonTool)mainToolbarsManager.Tools["SettingCheckUpdates"]).Checked =
                UserSettingsFile.Instance.SettingAutoUpdate;

            ((StateButtonTool)mainToolbarsManager.Tools["SettingsPickerColumns2"]).Checked =
                UserSettingsFile.Instance.SettingUnitPickerColumns == 2;
            ((StateButtonTool)mainToolbarsManager.Tools["SettingsPickerColumns3"]).Checked =
                UserSettingsFile.Instance.SettingUnitPickerColumns == 3;
            ((StateButtonTool)mainToolbarsManager.Tools["SettingsPickerColumns4"]).Checked =
                UserSettingsFile.Instance.SettingUnitPickerColumns == 4;
            _doEvents = true;
        }

        private void mainToolbarsManager_ToolClick(object sender, ToolClickEventArgs e)
        {
            if (!_doEvents) return;
            if (e.Tool.Key.StartsWith("NewRules:"))
            {
                GameDefinition? gameDefinition = null;
                if (e.Tool.Key.StartsWith("NewRules:Mod:"))
                {
                    gameDefinition = UserSettingsFile.Instance.CustomMods
                        .FirstOrDefault(m => m.Key == e.Tool.Key.Substring(13))
                        ?.ToGameDefinition();
                }
                else
                {
                    gameDefinition = GamesFile.Instance.Games
                        .FirstOrDefault(g => g.GameKey == e.Tool.Key.Substring(9));
                }
                if (gameDefinition != null)
                {
                    var rulesFile = gameDefinition.LoadDefaultRulesFile(true);
                    LoadRulesFile(rulesFile, ParseFileType(rulesFile, gameDefinition));
                }
                return;
            }
            if (e.Tool.Key.StartsWith("NewArt:"))
            {
                GameDefinition? gameDefinition = null;
                if (e.Tool.Key.StartsWith("NewArt:Mod:"))
                {
                    gameDefinition = UserSettingsFile.Instance.CustomMods
                        .FirstOrDefault(m => m.Key == e.Tool.Key.Substring(11))
                        ?.ToGameDefinition();
                }
                else
                {
                    gameDefinition = GamesFile.Instance.Games
                        .FirstOrDefault(g => g.GameKey == e.Tool.Key.Substring(7));
                }
                if (gameDefinition != null)
                {
                    var artFile = gameDefinition.LoadDefaultArtFile(true);
                    var rulesFile = gameDefinition.LoadCurrentRulesFile();
                    LoadArtFile(artFile, ParseFileType(artFile, gameDefinition), rulesFile);
                }
                return;
            }
            if (e.Tool.Key.StartsWith("NewAi:"))
            {
                GameDefinition? gameDefinition = null;
                if (e.Tool.Key.StartsWith("NewAi:Mod:"))
                {
                    gameDefinition = UserSettingsFile.Instance.CustomMods
                        .FirstOrDefault(m => m.Key == e.Tool.Key.Substring(10))
                        ?.ToGameDefinition();
                }
                else
                {
                    gameDefinition = GamesFile.Instance.Games
                        .FirstOrDefault(g => g.GameKey == e.Tool.Key.Substring(6));
                }
                if (gameDefinition != null)
                {
                    var aiFile = gameDefinition.LoadDefaultAiFile(true);
                    var rulesFile = gameDefinition.LoadCurrentRulesFile();
                    LoadAiFile(aiFile, ParseFileType(aiFile, gameDefinition), rulesFile);
                }
                return;
            }
            if (e.Tool.Key.StartsWith("UseTheme:"))
            {
                UseTheme(e.Tool.Key.Substring(9));
                return;
            }
            if (e.Tool.Key.StartsWith("OpenRecent:"))
            {
                ButtonOpenRecent(e.Tool.Key.Substring(11));
                return;
            }
            switch (e.Tool.Key)
            {
                case "Open":
                    ButtonOpen();
                    break;
                case "CompareFiles":
                    ButtonOpenCompareFiles();
                    break;
                case "SaveAs":
                    ButtonSaveAs();
                    break;
                case "SaveFile":
                    ButtonSaveFile();
                    break;
                case "SaveInGame":
                    ButtonSaveInGame();
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
                case "SettingsPickerColumns2":
                    UserSettingsFile.Instance.SettingUnitPickerColumns = 2;
                    UserSettingsFile.Instance.Save();
                    LoadUserSettings();
                    break;
                case "SettingsPickerColumns3":
                    UserSettingsFile.Instance.SettingUnitPickerColumns = 3;
                    UserSettingsFile.Instance.Save();
                    LoadUserSettings();
                    break;
                case "SettingsPickerColumns4":
                    UserSettingsFile.Instance.SettingUnitPickerColumns = 4;
                    UserSettingsFile.Instance.Save();
                    LoadUserSettings();
                    break;
                case "OnlyFavoriteValues":
                    ShowOnlyFavoriteValues = !ShowOnlyFavoriteValues;
                    ((StateButtonTool)e.Tool).Checked = ShowOnlyFavoriteValues;
                    if (_editRulesMainControl != null)
                    {
                        _editRulesMainControl.ShowOnlyFavoriteValues = ShowOnlyFavoriteValues;
                        _editRulesMainControl.ReloadModels();
                    }
                    else if (_editArtMainControl != null)
                    {
                        _editArtMainControl.ShowOnlyFavoriteValues = ShowOnlyFavoriteValues;
                        _editArtMainControl.ReloadModels();
                    }
                    break;
                case "OnlyFavoriteUnits":
                    ShowOnlyFavoriteUnits = !ShowOnlyFavoriteUnits;
                    ((StateButtonTool)e.Tool).Checked = ShowOnlyFavoriteUnits;
                    if (_editRulesMainControl != null)
                    {
                        _editRulesMainControl.ShowOnlyFavoriteUnits = ShowOnlyFavoriteUnits;
                        _editRulesMainControl.ReloadModels();
                    }
                    else if (_editArtMainControl != null)
                    {
                        _editArtMainControl.ShowOnlyFavoriteUnits = ShowOnlyFavoriteUnits;
                        _editArtMainControl.ReloadModels();
                    }
                    break;
                case "ShowChanges":
                    ButtonShowChanges();
                    break;
                case "BalancingTool":
                    ButtonBalancingTool();
                    break;
                case "MixBrowser":
                    ButtonMixBrowserTool();
                    break;
                case "InsertSnippet":
                    if (_editRulesMainControl != null)
                    {
                        Cursor = Cursors.WaitCursor;
                        if (InsertSnippetForm.InsertSnippetToModel(this, _editRulesMainControl.Model))
                        {
                            _editRulesMainControl.ReloadModels();
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
                case "Filter":
                    ButtonShowFilter();
                    break;
                case "INI-Editor":
                    var showEditor = !_iniEditorEnabled;
                    UserSettingsFile.Instance.ShowIniEditor = showEditor;
                    UserSettingsFile.Instance.Save();
                    ButtonShowIniEditor(showEditor);
                    break;
                case "About":
                    AboutForm.ExecuteShow(this);
                    break;
                case "OnlineDocu":
                    var docuLinkProcessInfo = new ProcessStartInfo("https://github.com/otherdeniz/CnC_RulesEditor/blob/master/README_DOCS.md")
                    {
                        UseShellExecute = true
                    };
                    Process.Start(docuLinkProcessInfo);
                    break;
            }
        }

        private void mainToolbarsManager_ToolValueChanged(object sender, ToolEventArgs e)
        {
            if (e.Tool.Key == "SearchText")
            {
                SearchValues(((TextBoxTool)e.Tool).Text);
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
                var wavStream = ResourcesRepository.Instance.ReadRandomResourcesFileStream("startup_*.wav");
                if (wavStream != null)
                {
                    AudioPlayerService.PlaySound(StupidStream.FromFileStream(wavStream));
                }
            }
            if (UserSettingsFile.Instance.SettingAutoUpdate)
            {
                AutoUpdateManager.CheckForUpdate(this, true);
            }
            else
            {
                AutoUpdateManager.CheckForUpdate(this, false);
            }

            if (UserSettingsFile.Instance.ShowIniEditor)
            {
                ButtonShowIniEditor(true);
            }
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
            InitUserSettings();
            _doEvents = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimationsAsyncLoader.Instance.Stop(false, true);
        }

    }
}
