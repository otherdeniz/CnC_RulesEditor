using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public void LoadGames()
        {
            // clear controls
            var controlsToDispose = panelGames.Controls
                .OfType<Control>().ToList();
            panelGames.Controls.Clear();
            controlsToDispose.ForEach(c => c.Dispose());
            // add controls
            foreach (var customMod in UserSettingsFile.Instance.CustomMods.AsEnumerable().Reverse())
            {
                var customModControl = new GameSettingCustomModControl();
                customModControl.Dock = DockStyle.Top;
                customModControl.LoadCustomMod(customMod);
                customModControl.RemoveButtonClicked += (sender, args) =>
                {
                    UserSettingsFile.Instance.CustomMods.Remove(customMod);
                    UserSettingsFile.Instance.Save();
                    LoadGames();
                };
                ThemeManager.Instance.UseTheme(customModControl);
                panelGames.Controls.Add(customModControl);
            }
            foreach (var gameDefinition in GamesFile.Instance.Games.AsEnumerable().Reverse())
            {
                var gameGontrol = new GameSettingIntegratedControl();
                gameGontrol.Dock = DockStyle.Top;
                gameGontrol.LoadGame(gameDefinition);
                ThemeManager.Instance.UseTheme(gameGontrol);
                panelGames.Controls.Add(gameGontrol);
            }
        }

        private void buttonAddCustomMod_Click(object sender, EventArgs e)
        {
            var newCustomMod = NewCustomModForm.ExecuteNew(this);
            if (newCustomMod != null)
            {
                UserSettingsFile.Instance.CustomMods.Add(newCustomMod);
                UserSettingsFile.Instance.Save();
                LoadGames();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
