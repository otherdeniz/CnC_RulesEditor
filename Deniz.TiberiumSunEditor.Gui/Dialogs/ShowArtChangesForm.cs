using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs;

public partial class ShowArtChangesForm : Form
{
    public ShowArtChangesForm()
    {
        InitializeComponent();
        ThemeManager.Instance.UseTheme(this);
    }

    public void LoadModel(IniFile changesFile, IniFile defaultFile, RulesRootModel rulesRootModel)
    {
        var changesModel = new ArtRootModel(rulesRootModel, changesFile, defaultFile);
        artEdit.LoadModel(changesModel);
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ShowArtChangesForm_Load(object sender, EventArgs e)
    {
        DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
    }
}