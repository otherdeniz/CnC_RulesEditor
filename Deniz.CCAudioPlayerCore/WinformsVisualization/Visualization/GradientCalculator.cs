// Decompiled with JetBrains decompiler
// Type: WinformsVisualization.Visualization.GradientCalculator
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WinformsVisualization.Visualization
{
  internal class GradientCalculator
  {
    private Color[] _colors;

    public GradientCalculator()
    {
    }

    public GradientCalculator(params Color[] colors) => this._colors = colors;

    public Color[] Colors
    {
      get => this._colors ?? (this._colors = new Color[0]);
      set => this._colors = value;
    }

    public Color GetColor(float perc)
    {
      if (this._colors.Length <= 1)
        return ((IEnumerable<Color>) this._colors).FirstOrDefault<Color>();
      int index = Convert.ToInt32((float) ((double) (this._colors.Length - 1) * (double) perc - 0.5));
      float num = perc % (1f / (float) (this._colors.Length - 1)) * (float) (this._colors.Length - 1);
      if (index + 1 >= this.Colors.Length)
        index = this.Colors.Length - 2;
      return Color.FromArgb((int) byte.MaxValue, (int) (byte) ((double) this._colors[index + 1].R * (double) num + (double) this._colors[index].R * (1.0 - (double) num)), (int) (byte) ((double) this._colors[index + 1].G * (double) num + (double) this._colors[index].G * (1.0 - (double) num)), (int) (byte) ((double) this._colors[index + 1].B * (double) num + (double) this._colors[index].B * (1.0 - (double) num)));
    }
  }
}
