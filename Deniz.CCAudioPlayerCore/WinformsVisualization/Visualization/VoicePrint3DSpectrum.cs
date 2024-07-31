// Decompiled with JetBrains decompiler
// Type: WinformsVisualization.Visualization.VoicePrint3DSpectrum
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore.DSP;
using System;
using System.Drawing;

namespace WinformsVisualization.Visualization
{
  public class VoicePrint3DSpectrum : SpectrumBase
  {
    private readonly GradientCalculator _colorCalculator;
    private bool _isInitialized;

    public VoicePrint3DSpectrum(FftSize fftSize)
    {
      this._colorCalculator = new GradientCalculator();
      this.Colors = new Color[6]
      {
        Color.Black,
        Color.Blue,
        Color.Cyan,
        Color.Lime,
        Color.Yellow,
        Color.Red
      };
      this.FftSize = fftSize;
    }

    public Color[] Colors
    {
      get => this._colorCalculator.Colors;
      set => this._colorCalculator.Colors = value != null && value.Length != 0 ? value : throw new ArgumentException(nameof (value));
    }

    public int PointCount
    {
      get => this.SpectrumResolution;
      set
      {
        this.SpectrumResolution = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
        this.UpdateFrequencyMapping();
      }
    }

    public bool CreateVoicePrint3D(
      Graphics graphics,
      RectangleF clipRectangle,
      float xPos,
      Color background,
      float lineThickness = 1f)
    {
      if (!this._isInitialized)
      {
        this.UpdateFrequencyMapping();
        this._isInitialized = true;
      }
      float[] fftBuffer = new float[(int) this.FftSize];
      if (!this.SpectrumProvider.GetFftData(fftBuffer, (object) this))
        return false;
      SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints(1.0, fftBuffer);
      using (Pen pen = new Pen(background, lineThickness))
      {
        float y = clipRectangle.Y + clipRectangle.Height;
        for (int index = 0; index < spectrumPoints.Length; ++index)
        {
          SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
          float x = clipRectangle.X + xPos;
          float num = clipRectangle.Height / (float) spectrumPoints.Length;
          pen.Color = this._colorCalculator.GetColor((float) spectrumPointData.Value);
          PointF pt1 = new PointF(x, y);
          PointF pt2 = new PointF(x, y - num);
          graphics.DrawLine(pen, pt1, pt2);
          y -= num;
        }
      }
      return true;
    }
  }
}
