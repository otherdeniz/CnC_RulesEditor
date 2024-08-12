using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class OpenMapForm : Form
    {
        private List<GameDefinition> _gameDefinitions = null!;

        public OpenMapForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public GameDefinition? SelectedGameDefinition { get; private set; }

        public void LoadFile(string fileName)
        {
            LabelMap.Text = fileName;
            _gameDefinitions = GamesFile.Instance.Games
                .Union(UserSettingsFile.Instance.CustomMods.Select(m => m.ToGameDefinition())
                    .Where(g => g != null).Select(g => g!))
                .ToList();
            _gameDefinitions.ForEach(g => comboBoxGameType.Items.Add(g.NewMenuLabel));
        }

        private void comboBoxGameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGameType.SelectedIndex > -1)
            {
                SelectedGameDefinition = _gameDefinitions[comboBoxGameType.SelectedIndex];
            }
            buttonOk.Enabled = SelectedGameDefinition != null;
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
