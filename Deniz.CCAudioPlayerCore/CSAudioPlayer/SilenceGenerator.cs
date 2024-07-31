// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.SilenceGenerator
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore;
using System;

namespace CSAudioPlayer
{
  public class SilenceGenerator : IWaveSource, IReadableAudioSource<byte>, IAudioSource, IDisposable
  {
    private readonly WaveFormat _waveFormat = new WaveFormat(44100, 16, 2);

    public int Read(byte[] buffer, int offset, int count)
    {
      Array.Clear((Array) buffer, offset, count);
      return count;
    }

    public WaveFormat WaveFormat => new WaveFormat();

    public long Position
    {
      get => -1;
      set => throw new InvalidOperationException();
    }

    public long Length => -1;

    public bool CanSeek => throw new NotImplementedException();

    public void Dispose()
    {
    }
  }
}
