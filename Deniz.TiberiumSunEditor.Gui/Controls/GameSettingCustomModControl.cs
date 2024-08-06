using Deniz.TiberiumSunEditor.Gui.Dialogs;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class GameSettingCustomModControl : UserControl
    {
        private CustomModSetting _customModSetting = null!;

        public GameSettingCustomModControl()
        {
            InitializeComponent();
        }

        public event EventHandler? RemoveButtonClicked;

        public void LoadCustomMod(CustomModSetting customModSetting)
        {
            _customModSetting = customModSetting;
            labelGame.Text = customModSetting.Name.Replace("&", "&&");
            pictureGameIcon.Image = LogoRepository.Instance.GetLogo(customModSetting.LogoFile);
            labelPath.Text = customModSetting.GamePath;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (NewCustomModForm.ExecuteEdit(this.ParentForm!, _customModSetting))
            {
                LoadCustomMod(_customModSetting);
                UserSettingsFile.Instance.Save();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
