using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs;

public partial class ShowAiChangesForm : Form
{
    public ShowAiChangesForm()
    {
        InitializeComponent();
        ThemeManager.Instance.UseTheme(this);
    }

    public void LoadModel(IniFile changesFile, IniFile defaultFile, RulesRootModel rulesRootModel)
    {
        var changesModel = new AiRootModel(rulesRootModel, changesFile, defaultFile, false, false);
        aiEdit.LoadModel(changesModel);
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ShowAiChangesForm_Load(object sender, EventArgs e)
    {
        DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
    }
}