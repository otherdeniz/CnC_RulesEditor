// Decompiled with JetBrains decompiler
// Type: Options.Core.XmlColor
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Options.Core
{
  internal class XmlColor
  {
    private Color color_ = Color.Black;

    public XmlColor()
    {
    }

    public XmlColor(Color c) => this.color_ = c;

    public Color ToColor() => this.color_;

    public void FromColor(Color c) => this.color_ = c;

    public static implicit operator Color(XmlColor x) => x.ToColor();

    public static implicit operator XmlColor(Color c) => new XmlColor(c);

    [XmlText]
    public string Web
    {
      get => ColorTranslator.ToHtml(this.color_);
      set
      {
        try
        {
          if (this.Alpha == byte.MaxValue)
            this.color_ = ColorTranslator.FromHtml(value);
          else
            this.color_ = Color.FromArgb((int) this.Alpha, ColorTranslator.FromHtml(value));
        }
        catch (Exception ex)
        {
          this.color_ = Color.Black;
        }
      }
    }

    [XmlAttribute]
    public byte Alpha
    {
      get => this.color_.A;
      set
      {
        if ((int) value == (int) this.color_.A)
          return;
        this.color_ = Color.FromArgb((int) value, this.color_);
      }
    }

    public bool ShouldSerializeAlpha() => this.Alpha < byte.MaxValue;
  }
}
