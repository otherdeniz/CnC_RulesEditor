using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class ShowChangesForm : Form
    {
        public ShowChangesForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public void LoadModel(IniFile changesFile, IniFile defaultFile, RulesRootModel rulesRootModel)
        {
            var changesModel = new RulesRootModel(changesFile, FileTypeModel.Empty, defaultFile,
                useAres: rulesRootModel.UseAres,
                usePhobos: rulesRootModel.UsePhobos,
                useVinifera: rulesRootModel.UseVinifera);
            rulesEdit.LoadModel(changesModel, "", "");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowChangesForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
