// Decompiled with JetBrains decompiler
// Type: WinformsVisualization.Visualization.SpectrumBase
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore;
using CSCore.DSP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace WinformsVisualization.Visualization
{
  public class SpectrumBase : INotifyPropertyChanged
  {
    private const int ScaleFactorLinear = 9;
    protected const int ScaleFactorSqr = 2;
    protected const double MinDbValue = -90.0;
    protected const double MaxDbValue = 0.0;
    protected const double DbScale = 90.0;
    private int _fftSize;
    private bool _isXLogScale;
    private int _maxFftIndex;
    private int _maximumFrequency = 20000;
    private int _maximumFrequencyIndex;
    private int _minimumFrequency = 20;
    private int _minimumFrequencyIndex;
    private ScalingStrategy _scalingStrategy;
    private int[] _spectrumIndexMax;
    private int[] _spectrumLogScaleIndexMax;
    private ISpectrumProvider _spectrumProvider;
    protected int SpectrumResolution;
    private bool _useAverage;

    public int MaximumFrequency
    {
      get => this._maximumFrequency;
      set
      {
        this._maximumFrequency = value > this.MinimumFrequency ? value : throw new ArgumentOutOfRangeException(nameof (value), "Value must not be less or equal the MinimumFrequency.");
        this.UpdateFrequencyMapping();
        this.RaisePropertyChanged(nameof (MaximumFrequency));
      }
    }

    public int MinimumFrequency
    {
      get => this._minimumFrequency;
      set
      {
        this._minimumFrequency = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
        this.UpdateFrequencyMapping();
        this.RaisePropertyChanged(nameof (MinimumFrequency));
      }
    }

    [Browsable(false)]
    public ISpectrumProvider SpectrumProvider
    {
      get => this._spectrumProvider;
      set
      {
        this._spectrumProvider = value != null ? value : throw new ArgumentNullException(nameof (value));
        this.RaisePropertyChanged(nameof (SpectrumProvider));
      }
    }

    public bool IsXLogScale
    {
      get => this._isXLogScale;
      set
      {
        this._isXLogScale = value;
        this.UpdateFrequencyMapping();
        this.RaisePropertyChanged(nameof (IsXLogScale));
      }
    }

    public ScalingStrategy ScalingStrategy
    {
      get => this._scalingStrategy;
      set
      {
        this._scalingStrategy = value;
        this.RaisePropertyChanged(nameof (ScalingStrategy));
      }
    }

    public bool UseAverage
    {
      get => this._useAverage;
      set
      {
        this._useAverage = value;
        this.RaisePropertyChanged(nameof (UseAverage));
      }
    }

    [Browsable(false)]
    public FftSize FftSize
    {
      get => (FftSize) this._fftSize;
      protected set
      {
        this._fftSize = (uint) (int) Math.Log((double) value, 2.0) % 1U <= 0U ? (int) value : throw new ArgumentOutOfRangeException(nameof (value));
        this._maxFftIndex = this._fftSize / 2 - 1;
        this.RaisePropertyChanged("FFTSize");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void UpdateFrequencyMapping()
    {
      this._maximumFrequencyIndex = Math.Min(this._spectrumProvider.GetFftBandIndex((float) this.MaximumFrequency) + 1, this._maxFftIndex);
      this._minimumFrequencyIndex = Math.Min(this._spectrumProvider.GetFftBandIndex((float) this.MinimumFrequency), this._maxFftIndex);
      int spectrumResolution = this.SpectrumResolution;
      int num1 = this._maximumFrequencyIndex - this._minimumFrequencyIndex;
      double num2 = Math.Round((double) num1 / (double) spectrumResolution, 3);
      this._spectrumIndexMax = this._spectrumIndexMax.CheckBuffer<int>((long) spectrumResolution, true);
      this._spectrumLogScaleIndexMax = this._spectrumLogScaleIndexMax.CheckBuffer<int>((long) spectrumResolution, true);
      double num3 = Math.Log((double) spectrumResolution, (double) spectrumResolution);
      for (int index = 1; index < spectrumResolution; ++index)
      {
        int num4 = (int) ((num3 - Math.Log((double) (spectrumResolution + 1 - index), (double) (spectrumResolution + 1))) * (double) num1) + this._minimumFrequencyIndex;
        this._spectrumIndexMax[index - 1] = this._minimumFrequencyIndex + (int) ((double) index * num2);
        this._spectrumLogScaleIndexMax[index - 1] = num4;
      }
      if (spectrumResolution <= 0)
        return;
      this._spectrumIndexMax[this._spectrumIndexMax.Length - 1] = this._spectrumLogScaleIndexMax[this._spectrumLogScaleIndexMax.Length - 1] = this._maximumFrequencyIndex;
    }

    protected virtual SpectrumBase.SpectrumPointData[] CalculateSpectrumPoints(
      double maxValue,
      float[] fftBuffer)
    {
      List<SpectrumBase.SpectrumPointData> spectrumPointDataList = new List<SpectrumBase.SpectrumPointData>();
      double val1 = 0.0;
      double val2 = 0.0;
      double num1 = 0.0;
      double num2 = maxValue;
      int index = 0;
      for (int minimumFrequencyIndex = this._minimumFrequencyIndex; minimumFrequencyIndex <= this._maximumFrequencyIndex; ++minimumFrequencyIndex)
      {
        switch (this.ScalingStrategy)
        {
          case ScalingStrategy.Decibel:
            val1 = (20.0 * Math.Log10((double) fftBuffer[minimumFrequencyIndex]) - -90.0) / 90.0 * num2;
            break;
          case ScalingStrategy.Linear:
            val1 = (double) fftBuffer[minimumFrequencyIndex] * 9.0 * num2;
            break;
          case ScalingStrategy.Sqrt:
            val1 = Math.Sqrt((double) fftBuffer[minimumFrequencyIndex]) * 2.0 * num2;
            break;
        }
        bool flag = true;
        val2 = Math.Max(0.0, Math.Max(val1, val2));
        while (index <= this._spectrumIndexMax.Length - 1 && minimumFrequencyIndex == (this.IsXLogScale ? this._spectrumLogScaleIndexMax[index] : this._spectrumIndexMax[index]))
        {
          if (!flag)
            val2 = num1;
          if (val2 > maxValue)
            val2 = maxValue;
          if (this._useAverage && index > 0)
            val2 = (num1 + val2) / 2.0;
          spectrumPointDataList.Add(new SpectrumBase.SpectrumPointData()
          {
            SpectrumPointIndex = index,
            Value = val2
          });
          num1 = val2;
          val2 = 0.0;
          ++index;
          flag = false;
        }
      }
      return spectrumPointDataList.ToArray();
    }

    protected void RaisePropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null || string.IsNullOrEmpty(propertyName))
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    [DebuggerDisplay("{Value}")]
    protected struct SpectrumPointData
    {
      public int SpectrumPointIndex;
      public double Value;
    }
  }
}
