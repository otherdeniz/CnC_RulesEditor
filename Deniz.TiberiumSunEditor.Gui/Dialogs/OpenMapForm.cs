using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class OpenMapForm : Form
    {
        private List<GameDefinition> _gameDefinitions = null!;

        public OpenMapForm()
        {
            InitializeComponent();
        }

        public GameDefinition? SelectedGameDefinition { get; private set; }

        public void LoadMap(string mapName)
        {
            LabelMap.Text = mapName;
            _gameDefinitions = GamesFile.Instance.Games;
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
    }
}
