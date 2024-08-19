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
                    mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = $"Save File ({openFileDialog.FileName})";
                    mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = true;
                    UserSettingsFile.Instance.AddRecentFile(openFileDialog.FileName, fileType);
                    InitializeRecentFilesMenu();
                }
            }
        }

        private void ButtonOpenRecent(string fileName)
        {
            var recentFile = UserSettingsFile.Instance.GetRecentFiles().First(f => f.Setting.FilePath == fileName);
            var iniFile = IniFile.Load(recentFile.Setting.FilePath);
            var fileType = ParseFileType(iniFile, recentFile.Definition);
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
                mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = $"Save File ({fileName})";
                mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = true;
            }
        }

        private void ButtonSaveInGame()
        {
            if (_editRulesMainControl != null)
            {
                var gamePath = _editRulesMainControl.Model.FileType.GameDefinition.GetUserGamePath();
                if (gamePath != null)
                {
                    var saveFilePath = string.IsNullOrEmpty(_editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                        ? Path.Combine(gamePath, _editRulesMainControl.Model.FileType.GameDefinition.SaveAsFilename)
                        : Path.Combine(gamePath, _editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder,
                            _editRulesMainControl.Model.FileType.GameDefinition.SaveAsFilename);
                    _editRulesMainControl.Model.File.SaveAs(saveFilePath);
                }
                return;
            }

            if (_editArtMainControl != null)
            {
                var gamePath = _editArtMainControl.Model.FileType.GameDefinition.GetUserGamePath();
                if (gamePath != null)
                {
                    var saveFilePath = string.IsNullOrEmpty(_editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                        ? Path.Combine(gamePath, _editArtMainControl.Model.FileType.GameDefinition.SaveAsArtFilename)
                        : Path.Combine(gamePath, _editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder,
                            _editArtMainControl.Model.FileType.GameDefinition.SaveAsArtFilename);
                    _editArtMainControl.Model.File.SaveAs(saveFilePath);
                }
            }
        }

        private void ButtonSaveFile()
        {
            if (_editRulesMainControl?.Model.File.OriginalFullPath != null)
            {
                _editRulesMainControl.Model.File.SaveAs(_editRulesMainControl.Model.File.OriginalFullPath);
            }

            if (_editArtMainControl?.Model.File.OriginalFullPath != null)
            {
                _editArtMainControl.Model.File.SaveAs(_editArtMainControl.Model.File.OriginalFullPath);
            }
        }

        private void ButtonSaveAs()
        {
            if (_editRulesMainControl != null)
            {
                var userGamePath = _editRulesMainControl.Model.FileType.GameDefinition.GetUserGamePath();
                if (_editRulesMainControl.Model.FileType.BaseType == FileBaseType.Rules
                    && (string.IsNullOrEmpty(userGamePath) || !Directory.Exists(userGamePath)))
                {
                    var relativeFolder = string.IsNullOrEmpty(_editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                        ? "root"
                        : _editRulesMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
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
                    mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = $"Save File ({saveFileDialog.FileName})";
                    mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = true;
                    UserSettingsFile.Instance.AddRecentFile(saveFileDialog.FileName, _editRulesMainControl.Model.FileType);
                    InitializeRecentFilesMenu();
                }
            }

            if (_editArtMainControl != null)
            {
                var userGamePath = _editArtMainControl.Model.FileType.GameDefinition.GetUserGamePath();
                if (string.IsNullOrEmpty(userGamePath) || !Directory.Exists(userGamePath))
                {
                    var relativeFolder = string.IsNullOrEmpty(_editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                        ? "root"
                        : _editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
                    saveFileDialog.Title = $"Save this file in games {relativeFolder} folder";
                    saveFileDialog.FileName = _editArtMainControl.Model.FileType.GameDefinition.SaveAsArtFilename;

                }
                else
                {
                    saveFileDialog.Title = "Save as";
                    saveFileDialog.FileName = _editArtMainControl.Model.File.OriginalFileName;
                }
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _editArtMainControl.Model.File.SaveAs(saveFileDialog.FileName);
                    mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = $"Save File ({saveFileDialog.FileName})";
                    mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = true;
                    UserSettingsFile.Instance.AddRecentFile(saveFileDialog.FileName, _editArtMainControl.Model.FileType);
                    InitializeRecentFilesMenu();
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
            ThemeManager.Instance.UseTheme(mainToolbarsManager);
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
            BitmapRepository.Instance.InitBlanks();
            CCGameRepository.Instance.ClearAnimationsCache();
            _editRulesMainControl?.LoadModels();
            _editArtMainControl?.LoadModels();
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

        private void CloseFile()
        {
            _editRulesMainControl = null;
            _editArtMainControl = null;
            mainToolbarsManager.Tools["SaveMenu"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SaveFile"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SaveFile"].SharedProps.Caption = "Save File";
            mainToolbarsManager.Tools["OnlyFavorites"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ShowChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ToolsMenu"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["MixBrowser"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = false;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = false;
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
                    Dock = DockStyle.Fill
                };
                ThemeManager.Instance.UseTheme(_editRulesMainControl);
                _editRulesMainControl.LoadModel(rulesRootModel);
                panelMain.Controls.Add(_editRulesMainControl);
                mainToolbarsManager.Tools["ToolsMenu"].SharedProps.Enabled = true;
                mainToolbarsManager.Tools["BalancingTool"].SharedProps.Enabled = true;
                mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = true;
                mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = true;
            }
            else if (compareModel is ArtRootModel artRootModel)
            {
                _editArtMainControl = new ArtEditMainControl()
                {
                    Dock = DockStyle.Fill
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
                Dock = DockStyle.Fill
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
            mainToolbarsManager.Tools["InsertSnippet"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["ExportChanges"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchLabel"].SharedProps.Enabled = true;
            mainToolbarsManager.Tools["SearchText"].SharedProps.Enabled = true;
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
                Dock = DockStyle.Fill
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
            var relativeFolder = string.IsNullOrEmpty(_editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder)
                ? "root"
                : _editArtMainControl.Model.FileType.GameDefinition.SaveAsRelativeToGameFolder;
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
                case "MixBrowser":
                    ButtonMixBrowserTool();
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
                case "About":
                    AboutForm.ExecuteShow(this);
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
                AutoUpdateManager.CheckForUpdate(this, true);
            }
            else
            {
                AutoUpdateManager.CheckForUpdate(this, false);
            }
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
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
