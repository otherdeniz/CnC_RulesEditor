// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.AudioPlayer
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using AppInfo;
using AudioCDReaderLib;
using CSAudioPlayer.Properties;
using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.Ffmpeg;
using CSCore.SoundOut;
using CSID3TagsLib;
using Microncode.Lic;
using Options.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Xml.Serialization;
using TagLib;
using TagLib.Id3v2;

namespace CSAudioPlayer
{
  [ToolboxItem(true)]
  public class AudioPlayer
  {
    public cAudioPlayer _audioplayer;
    private static System.Timers.Timer timerProgress;
    private int WORKER_STATE = 0;
    private const int WORKER_STATE_START = 1;
    private const int WORKER_STATE_PROGRESS = 2;
    private const int WORKER_STATE_DONE = 3;
    private const int WORKER_STATE_ABORTED = 4;
    private const int WORKER_STATE_ERROR = 5;
    private BackgroundWorker backgroundWorker;
    private bool bStopped;
    private AudioVisualization AudioVisualizationEmpty = new AudioVisualization();
    private ProgressArgs eventArgsProgress = new ProgressArgs();
    public TimeSpan _length;

    [Browsable(true)]
    [Category("CSAudioPlayer")]
    [ReadOnly(false)]
    [Description("The multimedia file name and path to play.")]
    [DisplayName("FileName")]
    public string FileName { get; set; }

    [ReadOnly(false)]
    [TypeConverter(typeof (EnumDescriptionConverter))]
    [Description("The samplerate of the destination audio file.")]
    [Category("CSAudioPlayer")]
    [Browsable(true)]
    [DefaultValue(Samplerate.esamples44100)]
    [DisplayName("Samplerate")]
    public Samplerate Samplerate { get; set; } = Samplerate.esamples44100;

    [TypeConverter(typeof (EnumDescriptionConverter))]
    [Description("The samplerate of the destination audio file by value.")]
    [DisplayName("SamplerateVal")]
    [Category("CSAudioPlayer")]
    [ReadOnly(false)]
    [DefaultValue(44100)]
    [Browsable(true)]
    public int SamplerateVal { get; set; } = 0;

    [TypeConverter(typeof (EnumDescriptionConverter))]
    [DefaultValue(Bits.bits16)]
    [Browsable(true)]
    [ReadOnly(false)]
    [Description("The bit-depth of the destination audio file (for WAV format only).")]
    [Category("CSAudioPlayer")]
    [DisplayName("Bits")]
    public Bits Bits { get; set; } = Bits.bits16;

    [ReadOnly(false)]
    [Browsable(true)]
    [TypeConverter(typeof (EnumDescriptionConverter))]
    [DefaultValue(Channels.channels2)]
    [Description("The channels of the destination audio file.")]
    [DisplayName("Channels")]
    [Category("CSAudioPlayer")]
    public Channels Channels { get; set; } = Channels.channels2;

    [DisplayName("Mode")]
    [Browsable(true)]
    [Category("CSAudioPlayer")]
    [TypeConverter(typeof (EnumDescriptionConverter))]
    [ReadOnly(false)]
    [DefaultValue(Mode.WasapiOut)]
    [Description("The mode of the capturing process.")]
    public Mode Mode { get; set; } = Mode.WasapiOut;

    [Description("Play to the selected audio device index.")]
    [DisplayName("AudioSource")]
    [Browsable(true)]
    [Category("CSAudioPlayer")]
    [ReadOnly(false)]
    public int AudioDevice { get; set; }

    [ReadOnly(false)]
    [Browsable(true)]
    [DisplayName("DecodeMode")]
    [DefaultValue(DecodeMode.LocalCodecs)]
    [Description("The decoding mode of the operation.")]
    [Category("CSAudioPlayer")]
    [TypeConverter(typeof (EnumDescriptionConverter))]
    public DecodeMode DecodeMode { get; set; } = DecodeMode.LocalCodecs;

    [ReadOnly(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public PlayingState PlayingState { get; set; } = PlayingState.Stopped;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(false)]
    [Browsable(false)]
    [ReadOnly(false)]
    public AudioVisualization AudioVisualization { get; set; } = (AudioVisualization) null;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ReadOnly(false)]
    public AudioMeter AudioMeter { get; set; }

    [Category("CSAudioPlayer")]
    [Browsable(true)]
    [DisplayName("UserName")]
    [Description("The user name.")]
    [ReadOnly(false)]
    public string UserName { get; set; } = "Your email";

    [ReadOnly(false)]
    [DisplayName("UserKey")]
    [Description("The user key.")]
    [Browsable(true)]
    [Category("CSAudioPlayer")]
    public string UserKey { get; set; } = "Your registration key";

    public MessageArgs MessageArgs { get; set; }

    public ProgressArgs ProgressArgs { get; set; }

    public event OnPlayProgressEventHandler PlayProgress;

    public event OnPlayErrorEventHandler PlayError;

    public event EventHandler PlayDone;

    public event EventHandler PlayStart;

    public event EventHandler PlayPaused;

    public event EventHandler PlayStopped;

    public int TrackIndex { get; set; } = -1;

    public int DriveIndex { get; set; } = -1;

    private float GetRatingStars()
    {
      if (this.TagRating == (short) 0)
        return 0.0f;
      if (this.TagRating == (short) 1)
        return 1f;
      if (this.TagRating >= (short) 2 && this.TagRating <= (short) 22)
        return 0.5f;
      if (this.TagRating >= (short) 23 && this.TagRating <= (short) 31)
        return 1f;
      if (this.TagRating >= (short) 32 && this.TagRating <= (short) 63)
        return 1.5f;
      if (this.TagRating >= (short) 64 && this.TagRating <= (short) 95)
        return 2f;
      if (this.TagRating >= (short) 96 && this.TagRating <= (short) sbyte.MaxValue)
        return 2.5f;
      if (this.TagRating >= (short) 128 && this.TagRating <= (short) 159)
        return 3f;
      if (this.TagRating >= (short) 160 && this.TagRating <= (short) 195)
        return 3.5f;
      if (this.TagRating >= (short) 196 && this.TagRating <= (short) 223)
        return 4f;
      if (this.TagRating >= (short) 224 && this.TagRating <= (short) 254)
        return 4.5f;
      return this.TagRating == (short) byte.MaxValue ? 5f : 0.0f;
    }

    private short SetRatingStars(float ratingstars)
    {
      if ((double) ratingstars == 0.0)
        return 0;
      if ((double) ratingstars == 1.0)
        return 1;
      if ((double) ratingstars == 0.5)
        return 11;
      if ((double) ratingstars == 1.0)
        return 28;
      if ((double) ratingstars == 1.5)
        return 48;
      if ((double) ratingstars == 2.0)
        return 80;
      if ((double) ratingstars == 2.5)
        return 110;
      if ((double) ratingstars == 3.0)
        return 140;
      if ((double) ratingstars == 3.5)
        return 180;
      if ((double) ratingstars == 4.0)
        return 210;
      if ((double) ratingstars == 4.5)
        return 240;
      return (double) ratingstars == 5.0 ? (short) byte.MaxValue : (short) 0;
    }

    [DisplayName("TagComposers")]
    [Category("CSAudioPlayer")]
    [ReadOnly(false)]
    [Browsable(true)]
    [Description("A list of strings for the TagComposers of the opened multimedia file.")]
    public List<string> TagComposers { get; set; } = new List<string>();

    [ReadOnly(false)]
    [Description("A list of strings for the TagArtists of the opened multimedia file.")]
    [Category("CSAudioPlayer")]
    [DisplayName("TagArtists")]
    [Browsable(true)]
    public List<string> TagArtists { get; set; } = new List<string>();

    [DisplayName("TagPerformers")]
    [Browsable(true)]
    [ReadOnly(false)]
    [Description("A list of strings for the TagPerformers of the opened multimedia file. In some cases this tag will be the TagArtists tag.")]
    [Category("CSAudioPlayer")]
    public List<string> TagPerformers { get; set; } = new List<string>();

    [Category("CSAudioPlayer")]
    [Description("A list of strings for the TagGenres of the opened multimedia file.")]
    [ReadOnly(false)]
    [Browsable(true)]
    [DisplayName("TagGenres")]
    public List<string> TagGenres { get; set; } = new List<string>();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [ReadOnly(false)]
    public List<ImageTag> TagImages { get; set; } = new List<ImageTag>();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Bindable(false)]
    [ReadOnly(false)]
    [Browsable(false)]
    private string FileNameID3Tags { get; set; }

    [Description("The TagTitle tag of the opened multimedia file.")]
    [DisplayName("TagTitle")]
    [Category("CSAudioPlayer")]
    [Browsable(true)]
    [ReadOnly(false)]
    public string TagTitle { get; set; }

    [Category("CSAudioPlayer")]
    [Description("The lyrics of the opened multimedia file.")]
    [DisplayName("TagLyrics")]
    [ReadOnly(false)]
    [Browsable(true)]
    public string TagLyrics { get; set; }

    [ReadOnly(false)]
    [Category("CSAudioPlayer")]
    [Browsable(true)]
    [DisplayName("TagAlbum")]
    [Description("The album name of the opened multimedia file.")]
    public string TagAlbum { get; set; }

    [Category("CSAudioPlayer")]
    [ReadOnly(false)]
    [Description("The comment of the opened multimedia file.")]
    [Browsable(true)]
    [DisplayName("TagComment")]
    public string TagComment { get; set; }

    [Category("CSAudioPlayer")]
    [DisplayName("TagCopyright")]
    [Description("The copyright of the opened multimedia file.")]
    [Browsable(true)]
    [ReadOnly(false)]
    public string TagCopyright { get; set; }

    [DisplayName("TagYear")]
    [Category("CSAudioPlayer")]
    [ReadOnly(false)]
    [Browsable(true)]
    [Description("The created year of the opened multimedia file.")]
    public string TagYear { get; set; }

    [Category("CSAudioPlayer")]
    [DisplayName("TagTrack")]
    [Browsable(true)]
    [ReadOnly(false)]
    [Description("The Track number of the opened multimedia file.")]
    public uint TagTrack { get; set; }

    [Category("CSAudioPlayer")]
    [DisplayName("TagRating")]
    [Description("The rating value of the opened multimedia file. This value can be 0 (unrated) to 255 (best).")]
    [ReadOnly(false)]
    [Browsable(true)]
    public short TagRating { get; set; } = 0;

    [Browsable(true)]
    [Description("The user of the rating tag. The default value is: \"Windows Media Player 9 Series\".")]
    [Category("CSAudioPlayer")]
    [DisplayName("RatingUser")]
    [ReadOnly(false)]
    public string RatingUser { get; set; } = "Windows Media Player 9 Series";

    public bool CheckLicense(bool DisplayRegistrationWindow = true) => LicenseLib.CheckLicense(this.UserName, this.UserKey, App.AppName, App.AppVersion, App.AppDescription, App.AppCompany, App.AppRegisterLicense, App.AppWebsite, App.AppHomepage, DisplayRegistrationWindow);

    public PlaybackState PlaybackState => this._audioplayer != null ? this._audioplayer.PlaybackState : PlaybackState.Stopped;

    public TimeSpan Position
    {
      get
      {
        try
        {
          return this._audioplayer != null ? this._audioplayer.Position : TimeSpan.Zero;
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
        return TimeSpan.Zero;
      }
      set
      {
        try
        {
          if (this._audioplayer == null)
            return;
          this._audioplayer.Position = value;
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
      }
    }

    public TimeSpan Length
    {
      get
      {
        try
        {
          return this._length;
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
        return TimeSpan.Zero;
      }
    }

    public int Volume
    {
      get
      {
        try
        {
          return this._audioplayer != null ? Math.Min(100, Math.Max((int) ((double) this._audioplayer.Volume * 100.0), 0)) : 100;
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
        return 100;
      }
      set
      {
        try
        {
          if (this._audioplayer == null)
            return;
          this._audioplayer.Volume = (float) value;
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
      }
    }

    public ObservableCollection<string> GetPlayerModes()
    {
      ObservableCollection<string> playerModes = new ObservableCollection<string>();
      try
      {
        foreach (Mode mode in Enum.GetValues(typeof (Mode)))
          playerModes.Add(this.GetEnumValue((Enum) mode));
        return playerModes;
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
      return playerModes;
    }

    public ObservableCollection<string> GetDecoderModes()
    {
      ObservableCollection<string> decoderModes = new ObservableCollection<string>();
      try
      {
        foreach (DecodeMode decodeMode in Enum.GetValues(typeof (DecodeMode)))
          decoderModes.Add(this.GetEnumValue((Enum) decodeMode));
        return decoderModes;
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
      return decoderModes;
    }

    public ObservableCollection<string> GetChannels()
    {
      ObservableCollection<string> channels1 = new ObservableCollection<string>();
      try
      {
        foreach (Channels channels2 in Enum.GetValues(typeof (Channels)))
          channels1.Add(this.GetEnumValue((Enum) channels2));
        return channels1;
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
      return channels1;
    }

    public ObservableCollection<string> GetBits()
    {
      ObservableCollection<string> bits1 = new ObservableCollection<string>();
      try
      {
        foreach (Bits bits2 in Enum.GetValues(typeof (Bits)))
          bits1.Add(this.GetEnumValue((Enum) bits2));
        return bits1;
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
      return bits1;
    }

    public ObservableCollection<string> GetSamplerates()
    {
      ObservableCollection<string> samplerates = new ObservableCollection<string>();
      try
      {
        foreach (Samplerate samplerate in Enum.GetValues(typeof (Samplerate)))
          samplerates.Add(this.GetEnumValue((Enum) samplerate));
        return samplerates;
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
      return samplerates;
    }

    public ObservableCollection<string> GetDevices(Mode mode = Mode.WasapiOut)
    {
      ObservableCollection<string> devices = new ObservableCollection<string>();
      try
      {
        if (mode == Mode.LineOut)
        {
          IEnumerable<WaveOutDevice> source = WaveOutDevice.EnumerateDevices();
          if (!source.Any<WaveOutDevice>())
            Console.WriteLine("No devices found.");
          Console.WriteLine("Select device:");
          foreach (WaveOutDevice waveOutDevice in source)
            devices.Add(waveOutDevice.Name);
          return devices;
        }
        using (MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator())
        {
          using (MMDeviceCollection deviceCollection = deviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
          {
            foreach (MMDevice mmDevice in deviceCollection)
              devices.Add(mmDevice.FriendlyName);
          }
        }
        return devices;
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
      return devices;
    }

    public int GetDeviceDefaultIndex(Mode mode = Mode.WasapiOut)
    {
      try
      {
        if (mode != Mode.WasapiOut)
          return 0;
        ObservableCollection<string> observableCollection = new ObservableCollection<string>();
        MMDevice defaultAudioEndpoint;
        using (MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator())
          defaultAudioEndpoint = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        using (MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator())
        {
          using (MMDeviceCollection deviceCollection = deviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
          {
            int deviceDefaultIndex = 0;
            foreach (MMDevice mmDevice in deviceCollection)
            {
              if (defaultAudioEndpoint.DeviceID == mmDevice.DeviceID)
                return deviceDefaultIndex;
              ++deviceDefaultIndex;
            }
          }
        }
        return 0;
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
      return -1;
    }

    private void ResetControls()
    {
      try
      {
        if (this.AudioMeter != null)
          this.AudioMeter.Value = 0;
        if (this.AudioVisualization == null)
          return;
        this.AudioVisualization.pictureBoxGraph.Image = (Image) null;
        this.AudioVisualization.pictureBoxGraph.Update();
        this.AudioVisualization = this.AudioVisualizationEmpty;
      }
      catch (Exception ex)
      {
      }
      finally
      {
      }
    }

    private void Stopped()
    {
      this.backgroundWorker.DoWork -= new DoWorkEventHandler(this.backgroundWorker_DoWork);
      this.backgroundWorker.ProgressChanged -= new ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
      this.backgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
      this.backgroundWorker = (BackgroundWorker) null;
      this.ResetControls();
    }

    public void Stop()
    {
      if (this._audioplayer == null || this._audioplayer.PlaybackState != PlaybackState.Playing)
        Console.WriteLine("Not playing.");
      else
        this.bStopped = true;
    }

    private void RaiseError(Exception ex)
    {
      try
      {
        MessageArgs e = new MessageArgs();
        e.String = ex.Message;
        e.Number = ex.HResult;
        Console.WriteLine(ex.Message);
        OnPlayErrorEventHandler playError = this.PlayError;
        if (playError == null)
          return;
        playError((object) this, e);
      }
      catch (Exception ex1)
      {
        MessageArgs e = new MessageArgs();
        e.String = ex1.Message;
        e.Number = ex1.HResult;
        Console.WriteLine(ex1.Message);
        OnPlayErrorEventHandler playError = this.PlayError;
        if (playError == null)
          return;
        playError((object) this, e);
      }
      finally
      {
      }
    }

    public bool Open(string FileName)
    {
      try
      {
        if (this.DriveIndex > -1)
          this._length = TimeSpan.FromMilliseconds((double) new AudioCDReader(this.DriveIndex, this.TrackIndex).Length);
        else if (Path.GetExtension(FileName) == ".cda")
          this._length = TimeSpan.FromMilliseconds((double) new AudioCDReader(FileName).Length);
        else if (this.DecodeMode == DecodeMode.LocalCodecs)
          this._length = CodecFactory.Instance.GetCodec(FileName).GetLength();
        else if (this.DecodeMode == DecodeMode.FFMpeg)
          this._length = new FfmpegDecoder(FileName).GetLength();
        if (this._audioplayer != null)
          this._audioplayer = (cAudioPlayer) null;
        if (this.Mode == Mode.WasapiOut)
          this._audioplayer = (cAudioPlayer) new cAudioPlayerWasapi();
        else if (this.Mode == Mode.LineOut)
          this._audioplayer = (cAudioPlayer) new cAudioPlayerLineOut();
        this._audioplayer.Open(FileName);
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
        return false;
      }
      finally
      {
      }
      return true;
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
      ProgressArgs e1 = new ProgressArgs();
      e1.Position = this._audioplayer.Position;
      this.AudioMeter.Value = (int) this._audioplayer.MeterOut;
      OnPlayProgressEventHandler playProgress = this.PlayProgress;
      if (playProgress == null)
        return;
      playProgress((object) this, e1);
    }

    public void Pause()
    {
      this._audioplayer.Pause();
      this.PlayingState = PlayingState.Paused;
    }

    public void Resume()
    {
      this._audioplayer.Resume();
      this.PlayingState = PlayingState.Playing;
    }

    public void Play()
    {
      this.CheckLicense();
      if (this.backgroundWorker != null)
      {
        this.backgroundWorker.CancelAsync();
        this.backgroundWorker = (BackgroundWorker) null;
      }
      this.backgroundWorker = new BackgroundWorker();
      this.backgroundWorker.WorkerSupportsCancellation = true;
      this.backgroundWorker.WorkerReportsProgress = true;
      this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
      this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
      this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
      this.backgroundWorker.RunWorkerAsync((object) this);
    }

    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.bStopped = false;
      if (this.PlayingState != PlayingState.Stopped)
        return;
      try
      {
        int Samplerate = this.SamplerateVal > 0 ? this.SamplerateVal : (int) Enum.Parse(typeof (Samplerate), this.Samplerate.ToString());
        int Bits = (int) Enum.Parse(typeof (Bits), this.Bits.ToString());
        int Channels = (int) Enum.Parse(typeof (Channels), this.Channels.ToString());
        if (this._audioplayer != null)
          this._audioplayer = (cAudioPlayer) null;
        if (this.Mode == Mode.WasapiOut)
          this._audioplayer = (cAudioPlayer) new cAudioPlayerWasapi();
        else if (this.Mode == Mode.LineOut)
          this._audioplayer = (cAudioPlayer) new cAudioPlayerLineOut();
        this._audioplayer.PlayDone += (EventHandler) ((s, args) =>
        {
          this.WORKER_STATE = 3;
          this.backgroundWorker.ReportProgress(0, (object) this);
          this.PlayingState = PlayingState.Stopped;
          this._audioplayer = (cAudioPlayer) null;
        });
        this._audioplayer.PlayStart += (EventHandler) ((s, args) =>
        {
          this.WORKER_STATE = 1;
          this.backgroundWorker.ReportProgress(0, (object) this);
        });
        this._audioplayer.AudioVisualization = this.AudioVisualization == null ? this.AudioVisualizationEmpty : this.AudioVisualization;
        this._audioplayer.Play(this.FileName, this.AudioDevice, this.DecodeMode, Samplerate, (short) Bits, (short) Channels);
        if (AudioPlayer.timerProgress != null)
        {
          AudioPlayer.timerProgress.Dispose();
          AudioPlayer.timerProgress = (System.Timers.Timer) null;
        }
        AudioPlayer.timerProgress = new System.Timers.Timer(100.0);
        AudioPlayer.timerProgress.Elapsed += (ElapsedEventHandler) ((s, args) =>
        {
          if (this._audioplayer == null)
            return;
          this.eventArgsProgress.Position = this._audioplayer.Position;
          OnPlayProgressEventHandler playProgress = this.PlayProgress;
          if (playProgress != null)
            playProgress((object) this, this.eventArgsProgress);
          this.WORKER_STATE = 2;
          this.backgroundWorker.ReportProgress(0, (object) this);
        });
        AudioPlayer.timerProgress.AutoReset = true;
        AudioPlayer.timerProgress.Enabled = true;
        this.PlayingState = PlayingState.Playing;
        while (!this.bStopped)
          Thread.Sleep(100);
        this._audioplayer.Stop();
        AudioPlayer.timerProgress.Enabled = false;
        AudioPlayer.timerProgress.Elapsed -= new ElapsedEventHandler(this.OnTimedEvent);
        AudioPlayer.timerProgress.Dispose();
        AudioPlayer.timerProgress = (System.Timers.Timer) null;
        this.Stopped();
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
      }
      finally
      {
      }
    }

    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      AudioPlayer userState = (AudioPlayer) e.UserState;
      switch (this.WORKER_STATE)
      {
        case 1:
          EventHandler playStart = this.PlayStart;
          if (playStart == null)
            break;
          playStart((object) this, EventArgs.Empty);
          break;
        case 2:
          if (this.AudioMeter != null)
          {
            this.AudioMeter.Meter = this._audioplayer.MeterOut;
            this.AudioMeter.Value = (int) this._audioplayer.MeterOut;
          }
          OnPlayProgressEventHandler playProgress = this.PlayProgress;
          if (playProgress == null)
            break;
          playProgress((object) this, userState.eventArgsProgress);
          break;
        case 3:
          this.ResetControls();
          EventHandler playDone = this.PlayDone;
          if (playDone == null)
            break;
          playDone((object) this, EventArgs.Empty);
          break;
      }
    }

    private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
    }

    public float RatingStars
    {
      get => this.GetRatingStars();
      set
      {
        Console.WriteLine(value);
        this.TagRating = this.SetRatingStars(value);
      }
    }

    public void ClearAllTags(string DestFile)
    {
      TagLib.File file = TagLib.File.Create(DestFile);
      file.RemoveTags(TagLib.TagTypes.AllTags);
      file.Save();
    }

    public void ClearTag(string DestFile, TagTypes TagType)
    {
      TagLib.File file = TagLib.File.Create(DestFile);
      file.RemoveTags((TagLib.TagTypes) TagType);
      file.Save();
    }

    private bool OpenID3Tags(string multimedia_file)
    {
      TagLib.File file;
      try
      {
        this.TagImages.Clear();
        this.FileNameID3Tags = multimedia_file;
        file = TagLib.File.Create(multimedia_file);
        this.TagTitle = file.Tag.Title;
        this.TagAlbum = file.Tag.Album;
        this.TagYear = file.Tag.Year.ToString();
        this.TagComment = file.Tag.Comment;
        this.TagCopyright = file.Tag.Copyright;
        this.TagLyrics = file.Tag.Lyrics;
        this.TagTrack = file.Tag.Track;
        if (((IEnumerable<string>) file.Tag.Composers).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) file.Tag.Composers).Count<string>(); ++index)
            this.TagComposers.Add(file.Tag.Composers[index].ToString());
        }
        if (((IEnumerable<string>) file.Tag.Artists).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) file.Tag.Artists).Count<string>(); ++index)
            this.TagArtists.Add(file.Tag.Artists[index].ToString());
        }
        if (((IEnumerable<string>) file.Tag.Performers).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) file.Tag.Performers).Count<string>(); ++index)
            this.TagPerformers.Add(file.Tag.Performers[index].ToString());
        }
        if (((IEnumerable<string>) file.Tag.Genres).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) file.Tag.Genres).Count<string>(); ++index)
            this.TagGenres.Add(file.Tag.Genres[index].ToString());
        }
        if (((IEnumerable<IPicture>) file.Tag.Pictures).Count<IPicture>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<IPicture>) file.Tag.Pictures).Count<IPicture>(); ++index)
            this.TagImages.Add(new ImageTag(Image.FromStream((Stream) new MemoryStream(file.Tag.Pictures[index].Data.Data)), file.Tag.Pictures[index].MimeType, file.Tag.Pictures[index].Description, (ImageType) file.Tag.Pictures[index].Type));
        }
        this.TagRating = (short) PopularimeterFrame.Get((TagLib.Id3v2.Tag) file.GetTag(TagLib.TagTypes.Id3v2), this.RatingUser, true).Rating;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
      finally
      {
      }
      file.Dispose();
      return true;
    }

    public void SetID3Tags(string DestFile)
    {
      TagLib.File file = TagLib.File.Create(DestFile);
      string tagComment = this.TagComment;
      file.Tag.Title = this.TagTitle;
      file.Tag.Album = this.TagAlbum;
      file.Tag.Comment = tagComment;
      file.Tag.Copyright = this.TagCopyright;
      file.Tag.Lyrics = this.TagLyrics;
      file.Tag.Track = this.TagTrack;
      file.Tag.Performers = this.TagPerformers.ToArray();
      file.Tag.Artists = this.TagArtists.ToArray();
      file.Tag.Genres = this.TagGenres.ToArray();
      file.Tag.Composers = this.TagComposers.ToArray();
      try
      {
        file.Tag.Year = (uint) int.Parse(this.TagYear);
      }
      catch
      {
      }
      Picture[] source = new Picture[this.TagImages.Count<ImageTag>()];
      int index = 0;
      foreach (ImageTag tagImage in this.TagImages)
      {
        Picture picture = new Picture();
        Image image = tagImage.Image;
        ImageFormat format = ImageFormat.Jpeg;
        if (tagImage.Mine_Type == "image/png")
          format = ImageFormat.Png;
        picture.Type = (PictureType) tagImage.Type;
        picture.Description = tagImage.Description;
        picture.MimeType = tagImage.Mine_Type;
        MemoryStream memoryStream = new MemoryStream();
        image.Save((Stream) memoryStream, format);
        memoryStream.Position = 0L;
        picture.Data = ByteVector.FromStream((Stream) memoryStream);
        source[index] = picture;
        memoryStream.Close();
        ++index;
      }
      try
      {
        file.Tag.Pictures = (IPicture[]) ((IEnumerable<Picture>) source).ToArray<Picture>();
        TagLib.Id3v2.Tag.DefaultVersion = (byte) 3;
        TagLib.Id3v2.Tag.ForceDefaultVersion = true;
        PopularimeterFrame.Get((TagLib.Id3v2.Tag) file.GetTag(TagLib.TagTypes.Id3v2), this.RatingUser, true).Rating = (byte) this.TagRating;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
      file.Save();
    }

    public Image AddImageFromFile(string image_file)
    {
      Image image = Image.FromFile(image_file);
      string mineType = CSID3TagsLib.Core.GetMineType(image);
      this.TagImages.Add(new ImageTag(image, mineType, "", ImageType.FrontCover));
      return image;
    }

    private static Stream GenerateStreamFromString(string s)
    {
      MemoryStream streamFromString = new MemoryStream();
      StreamWriter streamWriter = new StreamWriter((Stream) streamFromString);
      streamWriter.Write(s);
      streamWriter.Flush();
      streamFromString.Position = 0L;
      return (Stream) streamFromString;
    }

    private static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
    {
      TextWriter textWriter = (TextWriter) null;
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
        textWriter = (TextWriter) new StreamWriter(filePath, append);
        xmlSerializer.Serialize(textWriter, (object) objectToWrite);
      }
      finally
      {
        textWriter?.Close();
      }
    }

    public T GetFFMpegFormats<T>() where T : new()
    {
      Stream streamFromString = AudioPlayer.GenerateStreamFromString(Resources.Formats);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));
      TextReader textReader = (TextReader) null;
      try
      {
        textReader = (TextReader) new StreamReader(streamFromString);
        return (T) xmlSerializer.Deserialize(textReader);
      }
      catch (Exception ex)
      {
        this.RaiseError(ex);
        return (T) xmlSerializer.Deserialize(textReader);
      }
      finally
      {
        textReader?.Close();
      }
    }

    public string GetEnumValue(Enum value)
    {
      MemberInfo element = ((IEnumerable<MemberInfo>) value.GetType().GetMember(value.ToString())).FirstOrDefault<MemberInfo>();
      return (object) element != null ? element.GetCustomAttribute<DescriptionAttribute>()?.Description : (string) null;
    }

    public T GetEnumValue<T>(string str) where T : struct, IConvertible
    {
      if (!typeof (T).IsEnum)
        throw new Exception("T must be an Enumeration type.");
      T enumValue = ((T[]) Enum.GetValues(typeof (T)))[0];
      if (!string.IsNullOrEmpty(str))
      {
        foreach (T obj in (T[]) Enum.GetValues(typeof (T)))
        {
          if (obj.ToString().ToUpper().Equals(str.ToUpper()))
          {
            enumValue = obj;
            break;
          }
        }
      }
      return enumValue;
    }
  }
}
