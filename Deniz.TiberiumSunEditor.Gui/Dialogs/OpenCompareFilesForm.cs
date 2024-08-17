using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class OpenCompareFilesForm : Form
    {
        private List<GameDefinition> _gameDefinitions = null!;

        public OpenCompareFilesForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
            InitialiseDropDowns();
        }

        public static IRootModel? ExecuteOpen(Form parentForm)
        {
            using (var form = new OpenCompareFilesForm())
            {
                if (form.ShowDialog(parentForm) == DialogResult.OK)
                {
                    return form.LoadModel();
                }
            }
            return null;
        }

        private void InitialiseDropDowns()
        {
            _gameDefinitions = GamesFile.Instance.Games
                .Union(UserSettingsFile.Instance.CustomMods.Select(m => m.ToGameDefinition())
                    .Where(g => g != null).Select(g => g!))
                .ToList();
            _gameDefinitions.ForEach(g => comboBoxBaseGame.Items.Add(g.NewMenuLabel));
            comboBoxFileType.Items.AddRange(new object[]
            {
                "Rules or Map",
                "Art"
            });
        }

        private IRootModel LoadModel()
        {
            var gameDefinition = _gameDefinitions[comboBoxBaseGame.SelectedIndex];
            if (comboBoxFileType.SelectedIndex == 0)
            {
                // Rules or Map
                var defaultFile = IniFile.Load(textFile1Path.Text);
                var compareFile = IniFile.Load(textFile2Path.Text);
                var fileTypeModel = FileTypeModel.ParseFile(compareFile, gameDefinition, m => gameDefinition)!;
                return new RulesRootModel(compareFile, fileTypeModel, defaultFile,
                    showMissingValues: true,
                    useAres: gameDefinition.UseAres,
                    usePhobos: gameDefinition.UsePhobos,
                    useVinifera: gameDefinition.UseVinifera);
            }
            else
            {
                // Art
                var defaultFile = IniFile.Load(textFile1Path.Text);
                var compareFile = IniFile.Load(textFile2Path.Text);
                var fileTypeModel = new FileTypeModel(FileBaseType.Art, compareFile.OriginalFileName, gameDefinition);
                var rulesRootModel = new RulesRootModel(gameDefinition.LoadDefaultRulesFile(), fileTypeModel,
                    showMissingValues: true,
                    useAres: gameDefinition.UseAres,
                    usePhobos: gameDefinition.UsePhobos,
                    useVinifera: gameDefinition.UseVinifera);
                return new ArtRootModel(rulesRootModel, compareFile, defaultFile, true);
            }
        }

        private void RefreshOkButton()
        {
            buttonOk.Enabled = comboBoxFileType.SelectedIndex > -1
                               && comboBoxBaseGame.SelectedIndex > -1
                               && !string.IsNullOrEmpty(textFile1Path.Text)
                               && !string.IsNullOrEmpty(textFile2Path.Text);
        }

        private void comboBoxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOkButton();
        }

        private void comboBoxBaseGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOkButton();
        }

        private void buttonFile1_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textFile1Path.Text = openFileDialog.FileName;
                RefreshOkButton();
            }
        }

        private void buttonFile2_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textFile2Path.Text = openFileDialog.FileName;
                RefreshOkButton();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenCompareFilesForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
