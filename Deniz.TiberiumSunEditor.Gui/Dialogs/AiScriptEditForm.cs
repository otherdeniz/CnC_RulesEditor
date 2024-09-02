using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class AiScriptEditForm : Form
    {
        public AiScriptEditForm()
        {
            InitializeComponent();
            panelParamNumber.Dock = DockStyle.Fill;
            paramValuesGrid.Dock = DockStyle.Fill;
            panelParamBuilding.Dock = DockStyle.Fill;
            ThemeManager.Instance.UseTheme(this);
        }

        public static bool ExecuteEdit(Form parentForm, AiRootModel aiRootModel, AiScriptKeyValueModel editItem)
        {
            using (var form = new AiScriptEditForm())
            {

            }
            return false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AiScriptEditForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }

    }
}
