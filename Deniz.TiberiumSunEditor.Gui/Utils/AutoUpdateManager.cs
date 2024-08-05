using System.Diagnostics;
using Infragistics.Win.CalcEngine;
using Octokit;
using System.Text.RegularExpressions;
using Application = System.Windows.Forms.Application;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class AutoUpdateManager
    {
        private const string GithubUserAgent = "Deniz.TiberiumSunEditor";
        private static readonly Regex VersionRegex = new Regex(@"v\d+\.\d+\.(\d+)", RegexOptions.Compiled);
        private readonly Form _mainForm;
        private SynchronizationContext _uiContext;

        public AutoUpdateManager(Form mainForm)
        {
            _mainForm = mainForm;
            _uiContext = SynchronizationContext.Current!;
        }

        public static void CheckForUpdate(Form mainForm)
        {
            var manager = new AutoUpdateManager(mainForm);
            manager.RunCheck();
        }

        private void RunCheck()
        {
            var currentVeresion = GetType().Assembly.GetName().Version;
            if (currentVeresion != null)
            {
                Task.Run(() =>
                {
                    try
                    {
                        var latestRelease = GetLatestRelease();
                        if (latestRelease != null)
                        {
                            var buildNumber = int.Parse(VersionRegex.Match(latestRelease.Release.Name).Groups[1].Value);
                            if (buildNumber > currentVeresion.Build)
                            {
                                _uiContext.Send(args =>
                                {
                                    if (MessageBox.Show(
                                            "There is an update available on github releaes:\n" +
                                            $"Current version: v{currentVeresion.ToString(3)}\n" +
                                            $"New Version: {latestRelease.Release.Name}\n\n" +
                                            "Do you want to download and install this update?",
                                            "Update available",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        Process.Start(
                                            Path.Combine(Application.StartupPath, "Updater\\Deniz.Updater.exe"),
                                            $"{latestRelease.Asset.BrowserDownloadUrl} Deniz.TiberiumSunEditor.Gui.exe");
                                        _mainForm.Close();
                                    }
                                }, null);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // ignore update error
                    }
                });
            }
        }

        private GitHubReleaseInfo? GetLatestRelease()
        {
            var github = new GitHubClient(new ProductHeaderValue(GithubUserAgent));

            return Task.Run(async () =>
            {
                var release = await github.Repository.Release.GetLatest("otherdeniz", "CnC_RulesEditor");
                if (release != null && VersionRegex.IsMatch(release.Name))
                {
                    var asset = release.Assets.FirstOrDefault(a => a.Name.EndsWith(".zip"));
                    if (asset != null)
                    {
                        return new GitHubReleaseInfo(asset, release);
                    }
                }
                return null;
            }).GetAwaiter().GetResult();
        }

        private class GitHubReleaseInfo
        {
            public GitHubReleaseInfo(ReleaseAsset asset, Release release)
            {
                Asset = asset;
                Release = release;
            }

            public ReleaseAsset Asset { get; }

            public Release Release { get; }
        }
    }
}
