// Decompiled with JetBrains decompiler
// Type: WinformsVisualization.Visualization.BasicSpectrumProvider
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore.DSP;
using System;
using System.Collections.Generic;

namespace WinformsVisualization.Visualization
{
  public class BasicSpectrumProvider : FftProvider, ISpectrumProvider
  {
    private readonly int _sampleRate;
    private readonly List<object> _contexts = new List<object>();

    public BasicSpectrumProvider(int channels, int sampleRate, FftSize fftSize)
      : base(channels, fftSize)
    {
      this._sampleRate = sampleRate > 0 ? sampleRate : throw new ArgumentOutOfRangeException(nameof (sampleRate));
    }

    public int GetFftBandIndex(float frequency)
    {
      int fftSize = (int) this.FftSize;
      double num = (double) this._sampleRate / 2.0;
      return (int) ((double) frequency / num * (double) (fftSize / 2));
    }

    public bool GetFftData(float[] fftResultBuffer, object context)
    {
      if (this._contexts.Contains(context))
        return false;
      this._contexts.Add(context);
      this.GetFftData(fftResultBuffer);
      return true;
    }

    public override void Add(float[] samples, int count)
    {
      base.Add(samples, count);
      if (count <= 0)
        return;
      this._contexts.Clear();
    }

    public override void Add(float left, float right)
    {
      base.Add(left, right);
      this._contexts.Clear();
    }
  }
}
