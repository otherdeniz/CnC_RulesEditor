using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class GameSettingIntegratedControl : UserControl
    {
        public GameSettingIntegratedControl()
        {
            InitializeComponent();
        }

        public event EventHandler<GameSettingChangdEventArgs>? GameSettingChanged;

        [Browsable(false)] 
        public GameDefinition GameDefinition { get; private set; } = null!;

        public void LoadGame(GameDefinition gameDefinition)
        {
            GameDefinition = gameDefinition;
            labelGame.Text = gameDefinition.NewMenuLabel;
            pictureGameIcon.Image = LogoRepository.Instance.GetLogo(gameDefinition.Logo);
            labelPath.Text = "(not defined)";
            var gamePath = UserSettingsFile.Instance.GamePaths.FirstOrDefault(g => g.GameKey == gameDefinition.GameKey)
                ?.GamePath;
            if (gamePath != null)
            {
                labelPath.Text = Directory.Exists(gamePath)
                    ? gamePath
                    : $"(path not exists) {gamePath}";
            }
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(Path.Combine(folderBrowserDialog.SelectedPath, GameDefinition.GameExecutable)))
                {
                    if (MessageBox.Show(
                            $"The Directory does not contain the game executable '{GameDefinition.GameExecutable}'\nDo you still want to use this directory?",
                            this.Text, MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }

                labelPath.Text = folderBrowserDialog.SelectedPath;
                var gamePathSetting =
                    UserSettingsFile.Instance.GamePaths.FirstOrDefault(g => g.GameKey == GameDefinition.GameKey);
                if (gamePathSetting == null)
                {
                    gamePathSetting = new GamePathSetting { GameKey = GameDefinition.GameKey };
                    UserSettingsFile.Instance.GamePaths.Add(gamePathSetting);
                }
                gamePathSetting.GamePath = folderBrowserDialog.SelectedPath;
                UserSettingsFile.Instance.Save();

                GameSettingChanged?.Invoke(this, new GameSettingChangdEventArgs(GameDefinition.GameKey));
            }
        }
    }

    public class GameSettingChangdEventArgs : EventArgs
    {
        public GameSettingChangdEventArgs(string gameKey)
        {
            GameKey = gameKey;
        }

        public string GameKey { get; }

    }
}
