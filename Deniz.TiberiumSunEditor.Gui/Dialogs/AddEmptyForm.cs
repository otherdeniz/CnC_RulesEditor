using Deniz.TiberiumSunEditor.Gui.Model;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class AddEmptyForm : Form
    {
        private RootModel _rootModel = null!;

        public AddEmptyForm()
        {
            InitializeComponent();
        }

        public void LoadModel(RootModel rootModel)
        {
            _rootModel = rootModel;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (_rootModel.File.Sections.Any(s =>
                    string.Equals(s.SectionName ?? "_", TextNewKey.Text,
                        StringComparison.InvariantCultureIgnoreCase)))
            {
                MessageBox.Show($"The key '{TextNewKey.Text}' already exists. Please choose a unique key.");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TextNewKey_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = !string.IsNullOrWhiteSpace(TextNewKey.Text);
        }
    }
}
