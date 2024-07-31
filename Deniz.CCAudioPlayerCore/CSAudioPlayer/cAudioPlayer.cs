// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.cAudioPlayer
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore.SoundOut;
using System;

namespace CSAudioPlayer
{
  public interface cAudioPlayer
  {
    PlaybackState PlaybackState { get; }

    TimeSpan Position { get; set; }

    TimeSpan Length { get; }

    float Volume { get; set; }

    AudioVisualization AudioVisualization { get; set; }

    int TrackIndex { get; set; }

    int DriveIndex { get; set; }

    float MeterOut { get; set; }

    bool Open(string FileName, DecodeMode DecodeMode = DecodeMode.LocalCodecs);

    void Play(
      string FileName,
      int DeviceIndex,
      DecodeMode DecodeMode = DecodeMode.LocalCodecs,
      int Samplerate = 0,
      short Bits = 0,
      short Channels = 0);

    void Pause();

    void Resume();

    void Stop();

    PlaybackState PlayState();

    event EventHandler PlayDone;

    event EventHandler PlayStart;

    event EventHandler PlayPaused;

    event OnPlayErrorEventHandler PlayError;
  }
}
