using Deniz.TiberiumSunEditor.Gui.Utils;
using System.Diagnostics;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public static void ExecuteShow(Form parentForm)
        {
            using (var form = new AboutForm())
            {
                form.LoadValues();
                form.ShowDialog(parentForm);
            }
        }

        private void LoadValues()
        {
            using (var reader = File.OpenText(Path.Combine(Application.StartupPath, "LICENSE.txt")))
            {
                textBoxLicense.Text = reader.ReadToEnd();
            }

            if (AutoUpdateManager.LatestRelease != null)
            {
                var latestRelease = AutoUpdateManager.LatestRelease;
                labelVersion.Text = latestRelease.Release.Name;
                if (AutoUpdateManager.AllReleases != null)
                {
                    var totalDownload = AutoUpdateManager.AllReleases.Sum(r => r.Asset.DownloadCount);
                    labelDownloads.Text = $"Total: {totalDownload:#,##0} downloads; Latest Version: {latestRelease.Asset.DownloadCount} downloads";
                }
                else
                {
                    labelDownloads.Text = $"Latest Version: {latestRelease.Asset.DownloadCount} downloads";
                }
                labelReleaseDate.Text = latestRelease.Release.CreatedAt.ToString("d");
                linkLabelRelease.Text = $"{linkLabelRelease.Text}/{latestRelease.Release.Name}";
            }
            else
            {
                labelVersion.Text = "?";
                labelDownloads.Text = "";
                labelReleaseDate.Text = "?";
            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel linkLabel)
            {
                var url = linkLabel.Tag is string tagText && tagText != string.Empty
                    ? tagText
                    : linkLabel.Text;
                var processInfo = new ProcessStartInfo($"https://{url}")
                {
                    UseShellExecute = true
                };
                Process.Start(processInfo);
            }
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
