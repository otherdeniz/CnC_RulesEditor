// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.cAudioPlayerWasapi
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using AudioCDReaderLib;
using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.DSP;
using CSCore.Ffmpeg;
using CSCore.SoundOut;
using CSCore.Streams;
using System;
using System.Drawing;
using System.IO;
using System.Timers;
using WinformsVisualization.Visualization;

namespace CSAudioPlayer
{
    public class cAudioPlayerWasapi : cAudioPlayer
    {
        private WasapiOut _soundOut;
        private IWaveSource _waveSource;
        private LineSpectrum _lineSpectrum;
        private VoicePrint3DSpectrum _voicePrint3DSpectrum;
        private IWaveSource _source;
        private static Timer timer1;
        public TimeSpan _length;

        private event EventHandler<PlaybackStoppedEventArgs> PlayStopped;

        public event EventHandler PlayDone;

        public event EventHandler PlayStart;

        public event EventHandler PlayPaused;

        public event OnPlayErrorEventHandler PlayError;

        public AudioVisualization AudioVisualization { get; set; }

        public float MeterOut { get; set; }

        public int TrackIndex { get; set; } = -1;

        public int DriveIndex { get; set; } = -1;

        public PlaybackState PlaybackState => this._soundOut != null ? this._soundOut.PlaybackState : PlaybackState.Stopped;

        public TimeSpan Position
        {
            get => this._waveSource != null ? this._waveSource.GetPosition() : TimeSpan.Zero;
            set
            {
                if (this._waveSource == null)
                    return;
                this._waveSource.SetPosition(value);
            }
        }

        public TimeSpan Length => this._length;

        public float Volume
        {
            get => this._soundOut != null ? this._soundOut.Volume : 100f;
            set
            {
                if (this._soundOut == null)
                    return;
                this._soundOut.Volume = Math.Min(1f, Math.Max(value / 100f, 0.0f));
            }
        }

        public PlaybackState PlayState() => this._soundOut != null ? this._soundOut.PlaybackState : PlaybackState.Stopped;

        public bool Open(string FileName, DecodeMode DecodeMode = DecodeMode.LocalCodecs)
        {
            if (this._waveSource != null)
                this._waveSource = (IWaveSource)null;
            try
            {
                if (this.DriveIndex > 1)
                {
                    this._waveSource = (IWaveSource)new AudioCDReader(this.DriveIndex, this.TrackIndex);
                    this._length = TimeSpan.FromMilliseconds((double)this._waveSource.Length);
                }
                else if (Path.GetExtension(FileName) == ".cda")
                {
                    this._waveSource = (IWaveSource)new AudioCDReader(FileName);
                    this._length = TimeSpan.FromMilliseconds((double)this._waveSource.Length);
                }
                else
                {
                    switch (DecodeMode)
                    {
                        case DecodeMode.LocalCodecs:
                            this._waveSource = CodecFactory.Instance.GetCodec(FileName);
                            this._length = this._waveSource.GetLength();
                            break;
                        case DecodeMode.FFMpeg:
                            this._waveSource = (IWaveSource)new FfmpegDecoder(FileName);
                            this._length = this._waveSource.GetLength();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
            }
            return true;
        }

        public void Play(IWaveSource waveSource)
        {
            this.CleanupPlayback();
            MMDeviceCollection deviceCollection = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active);
            if (this._soundOut != null)
                this._soundOut = (WasapiOut)null;
            this._soundOut = new WasapiOut() { Latency = 100 };
            this._soundOut.Device = deviceCollection[0];
            this._waveSource = waveSource;
            this._length = this._waveSource.GetLength();
            //if (Samplerate > 0)
            //    this._waveSource = this._waveSource.ChangeSampleRate(Samplerate);
            //if (Bits > (short)0)
            //    this._waveSource = this._waveSource.ToSampleSource().ToWaveSource((int)Bits);
            //if (Channels == (short)1)
            //    this._waveSource = this._waveSource.ToMono();
            //if (Channels == (short)2)
            //    this._waveSource = this._waveSource.ToStereo();
            this.SetupSampleSource(this._waveSource.ToSampleSource());
            this._soundOut.Initialize(this._source);
            this._soundOut.Stopped += (EventHandler<PlaybackStoppedEventArgs>)((s, args) =>
            {
                EventHandler playDone = this.PlayDone;
                if (playDone == null)
                    return;
                playDone((object)this, EventArgs.Empty);
            });
            this._soundOut.Play();
        }

        public void Play(
              string FileName,
              int DeviceIndex,
              DecodeMode DecodeMode = DecodeMode.LocalCodecs,
              int Samplerate = 0,
              short Bits = 0,
              short Channels = 0)
        {
            this.CleanupPlayback();
            MMDevice mmDevice1 = (MMDevice)null;
            MMDeviceCollection deviceCollection = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active);
            int num = 0;
            foreach (MMDevice mmDevice2 in deviceCollection)
            {
                if (DeviceIndex == num)
                    mmDevice1 = mmDevice2;
                ++num;
            }
            if (this._soundOut != null)
                this._soundOut = (WasapiOut)null;
            this._soundOut = new WasapiOut() { Latency = 100 };
            this._soundOut.Device = mmDevice1;
            if (this._waveSource != null)
                this._waveSource = (IWaveSource)null;
            if (this.DriveIndex > -1)
            {
                this._waveSource = (IWaveSource)new AudioCDReader(this.DriveIndex, this.TrackIndex);
                this._length = TimeSpan.FromMilliseconds((double)this._waveSource.Length);
            }
            else if (Path.GetExtension(FileName) == ".cda")
            {
                this._waveSource = (IWaveSource)new AudioCDReader(FileName);
                this._length = TimeSpan.FromMilliseconds((double)this._waveSource.Length);
            }
            else
            {
                switch (DecodeMode)
                {
                    case DecodeMode.LocalCodecs:
                        this._waveSource = CodecFactory.Instance.GetCodec(FileName);
                        this._length = this._waveSource.GetLength();
                        break;
                    case DecodeMode.FFMpeg:
                        this._waveSource = (IWaveSource)new FfmpegDecoder(FileName);
                        this._length = this._waveSource.GetLength();
                        break;
                }
            }
            if (Samplerate > 0)
                this._waveSource = this._waveSource.ChangeSampleRate(Samplerate);
            if (Bits > (short)0)
                this._waveSource = this._waveSource.ToSampleSource().ToWaveSource((int)Bits);
            if (Channels == (short)1)
                this._waveSource = this._waveSource.ToMono();
            if (Channels == (short)2)
                this._waveSource = this._waveSource.ToStereo();
            this.SetupSampleSource(this._waveSource.ToSampleSource());
            this._soundOut.Initialize(this._source);
            this._soundOut.Stopped += (EventHandler<PlaybackStoppedEventArgs>)((s, args) =>
            {
                EventHandler playDone = this.PlayDone;
                if (playDone == null)
                    return;
                playDone((object)this, EventArgs.Empty);
            });
            if (this._soundOut != null)
                this._soundOut.Play();
            if (cAudioPlayerWasapi.timer1 != null)
            {
                cAudioPlayerWasapi.timer1.Dispose();
                cAudioPlayerWasapi.timer1 = (Timer)null;
            }
            cAudioPlayerWasapi.timer1 = new Timer(40.0);
            cAudioPlayerWasapi.timer1.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
            cAudioPlayerWasapi.timer1.AutoReset = true;
            cAudioPlayerWasapi.timer1.Enabled = true;
            EventHandler playStart = this.PlayStart;
            if (playStart == null)
                return;
            playStart((object)this, EventArgs.Empty);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e) => this.GenerateLineSpectrum();

        private void GenerateLineSpectrum()
        {
            Image image = this.AudioVisualization.pictureBoxGraph.Image;
            Bitmap spectrumLine = this._lineSpectrum.CreateSpectrumLine(this.AudioVisualization.pictureBoxGraph.Size, this.AudioVisualization.ColorBase, this.AudioVisualization.ColorMax, this.AudioVisualization.BackColor, this.AudioVisualization.HighQuality);
            if (spectrumLine == null)
                return;
            this.MeterOut = this._lineSpectrum.MeterOutAverage;
            this.AudioVisualization.pictureBoxGraph.Image = (Image)spectrumLine;
            image?.Dispose();
        }

        public void Pause()
        {
            if (this._soundOut == null)
                return;
            this._soundOut.Pause();
        }

        public void Resume()
        {
            if (this._soundOut == null)
                return;
            this._soundOut.Resume();
        }

        public void Stop()
        {
            if (this._soundOut != null)
                this._soundOut.Stop();
            if (cAudioPlayerWasapi.timer1 == null)
                return;
            cAudioPlayerWasapi.timer1.Enabled = false;
            cAudioPlayerWasapi.timer1.Elapsed -= new ElapsedEventHandler(this.OnTimedEvent);
            cAudioPlayerWasapi.timer1.Dispose();
            cAudioPlayerWasapi.timer1 = (Timer)null;
        }

        private void CleanupPlayback()
        {
            if (this._soundOut != null)
            {
                this._soundOut.Dispose();
                this._soundOut = (WasapiOut)null;
            }
            if (this._waveSource == null)
                return;
            this._waveSource.Dispose();
            this._waveSource = (IWaveSource)null;
        }

        public void Dispose() => this.CleanupPlayback();

        private void SetupSampleSource(ISampleSource aSampleSource)
        {
            BasicSpectrumProvider spectrumProvider = new BasicSpectrumProvider(aSampleSource.WaveFormat.Channels, aSampleSource.WaveFormat.SampleRate, FftSize.Fft4096);
            LineSpectrum lineSpectrum = new LineSpectrum(FftSize.Fft4096);
            lineSpectrum.SpectrumProvider = (ISpectrumProvider)spectrumProvider;
            lineSpectrum.UseAverage = this.AudioVisualization?.UseAverage ?? false;
            lineSpectrum.BarCount = this.AudioVisualization?.BarCount ?? 100;
            lineSpectrum.BarSpacing = (double)(this.AudioVisualization?.BarSpacing ?? 1);
            lineSpectrum.IsXLogScale = this.AudioVisualization?.IsXLogScale ?? false;
            lineSpectrum.ScalingStrategy = ScalingStrategy.Decibel;
            this._lineSpectrum = lineSpectrum;
            VoicePrint3DSpectrum voicePrint3Dspectrum = new VoicePrint3DSpectrum(FftSize.Fft4096);
            voicePrint3Dspectrum.SpectrumProvider = (ISpectrumProvider)spectrumProvider;
            voicePrint3Dspectrum.UseAverage = true;
            voicePrint3Dspectrum.PointCount = 200;
            voicePrint3Dspectrum.IsXLogScale = true;
            voicePrint3Dspectrum.ScalingStrategy = ScalingStrategy.Decibel;
            this._voicePrint3DSpectrum = voicePrint3Dspectrum;
            SingleBlockNotificationStream sampleSource = new SingleBlockNotificationStream(aSampleSource);
            sampleSource.SingleBlockRead += (EventHandler<SingleBlockReadEventArgs>)((s, a) => spectrumProvider.Add(a.Left, a.Right));
            this._source = sampleSource.ToWaveSource(16);
        }
    }
}
