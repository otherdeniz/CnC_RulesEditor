// Decompiled with JetBrains decompiler
// Type: CSAudioMeter.AudioMeter
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSAudioMeter
{
  [ToolboxItem(false)]
  public class AudioMeter : ProgressBar
  {
    private IContainer components = (IContainer) null;

    [Browsable(true)]
    [ReadOnly(false)]
    [Description("The color of the meter.")]
    [DisplayName("ForeColor")]
    [Category("CSAudioMeter")]
    public override Color ForeColor { get; set; } = Color.Red;

    [Browsable(true)]
    [Category("CSAudioMeter")]
    [ReadOnly(false)]
    [Description("The background color of the meter.")]
    [DisplayName("BackColor")]
    public override Color BackColor { get; set; } = Color.Red;

    [ReadOnly(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Bindable(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float Meter { get; set; }

    public AudioMeter()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
      double num = ((double) this.Value - (double) this.Minimum) / ((double) this.Maximum - (double) this.Minimum);
      if (ProgressBarRenderer.IsSupported)
        ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rectangle);
      rectangle.Width = (int) ((double) rectangle.Width * num - 4.0);
      rectangle.Height -= 4;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, this.ForeColor, this.BackColor, LinearGradientMode.Vertical);
      e.Graphics.FillRectangle((Brush) linearGradientBrush, 2, 2, rectangle.Width, rectangle.Height);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new Container();
  }
}
