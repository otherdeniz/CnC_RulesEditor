using System.Diagnostics;
using RestSharp;
using System.IO.Compression;

namespace Deniz.Updater
{
    public partial class UpdateForm : Form
    {
        private string _downloadUrl = null!;
        private string? _autostartExe;
        private string[] _excludeFolders = null!;
        private SynchronizationContext _synchronizationContext = null!;
        private long _downloadSize;
        private long _downloadedBytes;
        private long _downloadedBytesBefore;
        private DateTime? _downloadStart;
        private int _extractFilesTotal;
        private int _extractFilesDone;
        private Stream? _extractStream;
        private bool _cancelDownload;

        public UpdateForm()
        {
            InitializeComponent();
        }

        public void InitialiseUpdate(string downloadUrl, string? autostartExe)
        {
            _downloadUrl = downloadUrl;
            _autostartExe = autostartExe;
            _excludeFolders = ConfigurationManager.Instance.ExcludeFolders;
        }

        private void StartUpdate()
        {
            _synchronizationContext = SynchronizationContext.Current!;
            timerDownloadStatus.Enabled = true;
            panelDownloadStatus.Visible = true;
            Task.Run(() =>
            {
                var downloadFile = Path.Combine(Path.GetTempPath(), "Deniz.Updater.zip");
                if (File.Exists(downloadFile))
                {
                    File.Delete(downloadFile);
                }
                try
                {
                    // download file
                    var downloadStream = DownloadZipStream();
                    if (downloadStream == null)
                    {
                        ExecuteSynchronized(() =>
                        {
                            MessageBox.Show("Download failed, response was not OK.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            Close();
                        });
                        return;
                    }
                    _downloadStart = DateTime.Now;
                    using (var targetStream = File.Create(downloadFile))
                    {
                        var buffer = new byte[4096];
                        bool isCompleted = false;
                        do
                        {
                            var readBytes = downloadStream.Read(buffer, 0, buffer.Length);
                            if (readBytes > 0)
                            {
                                _downloadedBytes += readBytes;
                                targetStream.Write(buffer, 0, readBytes);
                            }
                            else
                            {
                                isCompleted = true;
                            }
                        } while (!_cancelDownload && !isCompleted);
                        downloadStream.Close();
                    }
                    if (_cancelDownload)
                    {
                        File.Delete(downloadFile);
                        ExecuteSynchronized(() =>
                        {
                            Close();
                        });
                        return;
                    }

                    // wait for main app to close
                    if (_autostartExe != null)
                    {
                        ExecuteSynchronized(() =>
                        {
                            timerDownloadStatus.Enabled = false;
                            panelDownloadDetails.Visible = false;
                            labelDownloadTitle.Text = "Waiting for application to close...";
                        });
                        while (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_autostartExe)).Length > 0)
                        {
                            Thread.Sleep(250);
                        }
                    }

                    // extract file
                    ExecuteSynchronized(() =>
                    {
                        timerDownloadStatus.Enabled = false;
                        timerExtractStatus.Enabled = true;
                        buttonCancelDownload.Enabled = false;
                        panelDownloadDetails.Visible = false;
                        panelExtractDetails.Visible = true;
                        labelDownloadTitle.Text = "Extracting ZIP file...";
                    });
                    var extractionPath = ConfigurationManager.Instance.TargetPath;
                    var archiveAndStream = OpenZipArchive(downloadFile);
                    _extractStream = archiveAndStream.Stream;
                    using (var archive = archiveAndStream.Archive)
                    {
                        _extractFilesTotal = archive.Entries.Count;
                        foreach (var entry in archive.Entries)
                        {
                            if (!entry.FullName.EndsWith("/") 
                                && !_excludeFolders.Any(f => entry.FullName.StartsWith($"{f}/")))
                            {
                                var targetFile = Path.Combine(extractionPath, entry.FullName);
                                var targetDirectory = new FileInfo(targetFile).Directory;
                                if (targetDirectory != null
                                    && !targetDirectory.Exists)
                                {
                                    targetDirectory.Create();
                                }
                                entry.ExtractToFile(targetFile, true);
                            }
                            _extractFilesDone++;
                        }
                        _extractStream = null;
                    }
                    File.Delete(downloadFile);

                    // finished
                    if (_autostartExe != null)
                    {
                        Process.Start(Path.Combine(ConfigurationManager.Instance.TargetPath, _autostartExe));
                    }
                    ExecuteSynchronized(Close);
                }
                catch (Exception e)
                {
                    _extractStream = null;
                    if (File.Exists(downloadFile))
                    {
                        File.Delete(downloadFile);
                    }
                    ExecuteSynchronized(() =>
                    {
                        MessageBox.Show($"Download failed: {e.Message}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        Close();
                    });
                }
            });
        }

        private void ExecuteSynchronized(Action action)
        {
            //Send - synchronous: wait for answer(or action completed)
            //Post - asynchronous: drop off and continue
            _synchronizationContext.Send(args => action(), null);
        }

        private Stream? DownloadZipStream()
        {
            try
            {
                using (var client = new RestClient())
                {
                    var headRequest = new RestRequest(_downloadUrl);
                    var headResponse = client.Execute(headRequest, Method.Head);
                    if (headResponse.ContentLength.HasValue 
                        && headResponse.ContentLength > 0)
                    {
                        _downloadSize = headResponse.ContentLength.Value;
                        var streamRequest = new RestRequest(_downloadUrl);
                        return client.DownloadStream(streamRequest);
                    }
                }
            }
            catch (Exception)
            {
                // failed
            }
            return null;
        }

        private (ZipArchive Archive, Stream Stream) OpenZipArchive(string archiveFileName)
        {
            var fs = new FileStream(archiveFileName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 0x1000, useAsync: false);
            var archive = new ZipArchive(fs, ZipArchiveMode.Read, leaveOpen: false);
            return (archive, fs);
        }

        private void timerStartDownload_Tick(object sender, EventArgs e)
        {
            timerStartDownload.Enabled = false;
            StartUpdate();
        }

        private void timerDownloadStatus_Tick(object sender, EventArgs e)
        {
            labelDownloadedSize.Text = $"{_downloadedBytes.BytesToReadable()} / {_downloadSize.BytesToReadable()}";
            labelDownloadedProgress.Text = _downloadSize > 0
                ? $"{(100m / Convert.ToDecimal(_downloadSize) * Convert.ToDecimal(_downloadedBytes)):0.00} %"
                : "-";
            if (_downloadedBytesBefore > 0)
            {
                labelDownloadedSpeed.Text = $"{(_downloadedBytes - _downloadedBytesBefore).BytesToReadable()} / s";
            }
            _downloadedBytesBefore = _downloadedBytes;
            if (_downloadStart != null && _downloadedBytes > 0)
            {
                try
                {
                    var timeLeft = TimeSpan.FromSeconds((DateTime.Now - _downloadStart.Value).TotalSeconds /
                        Convert.ToDouble(_downloadedBytes) * Convert.ToDouble(_downloadSize - _downloadedBytes));
                    labelDownloadedTimeLeft.Text = timeLeft.TotalMinutes > 1
                        ? $"{timeLeft.TotalMinutes:0.0} minutes"
                        : $"{timeLeft.TotalSeconds:0} seconds";
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }

        private void timerExtractStatus_Tick(object sender, EventArgs e)
        {
            labelExtractedFiles.Text = $"{_extractFilesDone:#,##0} / {_extractFilesTotal:#,##0}";
            if (_extractFilesTotal > 0 && _extractStream != null && _extractStream.Length > 0)
            {
                labelExtractedProgress.Text =
                    $"{100m / Convert.ToDecimal(_extractStream.Length) * Convert.ToDecimal(_extractStream.Position):0.00} %";
            }
        }

        private void buttonCancelDownload_Click(object sender, EventArgs e)
        {
            buttonCancelDownload.Enabled = false;
            _cancelDownload = true;
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            timerStartDownload.Enabled = true;
        }

    }
}
