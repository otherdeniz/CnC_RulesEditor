// Decompiled with JetBrains decompiler
// Type: WinformsVisualization.Visualization.LineSpectrum
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore.DSP;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace WinformsVisualization.Visualization
{
  public class LineSpectrum : SpectrumBase
  {
    private int _barCount;
    private double _barSpacing;
    private double _barWidth;
    private Size _currentSize;

    public float MeterOutAverage { get; set; }

    public LineSpectrum(FftSize fftSize) => this.FftSize = fftSize;

    [Browsable(false)]
    public double BarWidth => this._barWidth;

    public double BarSpacing
    {
      get => this._barSpacing;
      set
      {
        this._barSpacing = value >= 0.0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
        this.UpdateFrequencyMapping();
        this.RaisePropertyChanged(nameof (BarSpacing));
        this.RaisePropertyChanged("BarWidth");
      }
    }

    public int BarCount
    {
      get => this._barCount;
      set
      {
        this._barCount = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
        this.SpectrumResolution = value;
        this.UpdateFrequencyMapping();
        this.RaisePropertyChanged(nameof (BarCount));
        this.RaisePropertyChanged("BarWidth");
      }
    }

    [Browsable(false)]
    public Size CurrentSize
    {
      get => this._currentSize;
      protected set
      {
        this._currentSize = value;
        this.RaisePropertyChanged(nameof (CurrentSize));
      }
    }

    public Bitmap CreateSpectrumLine(
      Size size,
      Brush brush,
      Color background,
      bool highQuality)
    {
      if (!this.UpdateFrequencyMappingIfNessesary(size))
        return (Bitmap) null;
      float[] fftBuffer = new float[(int) this.FftSize];
      if (!this.SpectrumProvider.GetFftData(fftBuffer, (object) this))
        return (Bitmap) null;
      using (Pen pen = new Pen(brush, (float) this._barWidth))
      {
        Bitmap spectrumLine = new Bitmap(size.Width, size.Height);
        using (Graphics graphics = Graphics.FromImage((Image) spectrumLine))
        {
          this.PrepareGraphics(graphics, highQuality);
          graphics.Clear(background);
          this.CreateSpectrumLineInternal(graphics, pen, fftBuffer, size);
        }
        return spectrumLine;
      }
    }

    public Bitmap CreateSpectrumLine(
      Size size,
      Color color1,
      Color color2,
      Color background,
      bool highQuality)
    {
      if (!this.UpdateFrequencyMappingIfNessesary(size))
        return (Bitmap) null;
      using (Brush brush = (Brush) new LinearGradientBrush(new RectangleF(0.0f, 0.0f, (float) this._barWidth, (float) size.Height), color2, color1, LinearGradientMode.Vertical))
        return this.CreateSpectrumLine(size, brush, background, highQuality);
    }

    private void CreateSpectrumLineInternal(
      Graphics graphics,
      Pen pen,
      float[] fftBuffer,
      Size size)
    {
      int height = size.Height;
      SpectrumBase.SpectrumPointData[] spectrumPoints = this.CalculateSpectrumPoints((double) height, fftBuffer);
      float num = 0.0f;
      for (int index = 0; index < spectrumPoints.Length; ++index)
      {
        SpectrumBase.SpectrumPointData spectrumPointData = spectrumPoints[index];
        int spectrumPointIndex = spectrumPointData.SpectrumPointIndex;
        double x = this.BarSpacing * (double) (spectrumPointIndex + 1) + this._barWidth * (double) spectrumPointIndex + this._barWidth / 2.0;
        PointF pt1 = new PointF((float) x, (float) height);
        PointF pt2 = new PointF((float) x, (float) ((double) height - spectrumPointData.Value - 1.0));
        graphics.DrawLine(pen, pt1, pt2);
        num += pt2.Y;
      }
      this.MeterOutAverage = (float) (((double) height - (double) num / (double) spectrumPoints.Length) / (double) height * 100.0);
    }

    protected override void UpdateFrequencyMapping()
    {
      this._barWidth = Math.Max(((double) this._currentSize.Width - this.BarSpacing * (double) (this.BarCount + 1)) / (double) this.BarCount, 1E-05);
      base.UpdateFrequencyMapping();
    }

    private bool UpdateFrequencyMappingIfNessesary(Size newSize)
    {
      if (newSize != this.CurrentSize)
      {
        this.CurrentSize = newSize;
        this.UpdateFrequencyMapping();
      }
      return newSize.Width > 0 && newSize.Height > 0;
    }

    private void PrepareGraphics(Graphics graphics, bool highQuality)
    {
      if (highQuality)
      {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.CompositingQuality = CompositingQuality.AssumeLinear;
        graphics.PixelOffsetMode = PixelOffsetMode.Default;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      }
      else
      {
        graphics.SmoothingMode = SmoothingMode.HighSpeed;
        graphics.CompositingQuality = CompositingQuality.HighSpeed;
        graphics.PixelOffsetMode = PixelOffsetMode.None;
        graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
      }
    }
  }
}
