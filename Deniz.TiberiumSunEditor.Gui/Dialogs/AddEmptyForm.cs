using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class AddEmptyForm : Form
    {
        private IRootModel _rootModel = null!;

        public AddEmptyForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public void LoadModel(IRootModel rootModel)
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

        private void AddEmptyForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
