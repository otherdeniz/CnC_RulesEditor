// Decompiled with JetBrains decompiler
// Type: WinformsVisualization.Visualization.AudioVisualization
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinformsVisualization.Visualization
{
  [ToolboxItem(false)]
  public class AudioVisualization : UserControl
  {
    private IContainer components = (IContainer) null;
    public PictureBox pictureBoxGraph;

    [Category("AudioVisualization")]
    [ReadOnly(false)]
    [DisplayName("ColorBase")]
    [Description("The base color of the AudioVisualization.")]
    [Browsable(true)]
    public Color ColorBase { get; set; } = Color.DarkRed;

    [ReadOnly(false)]
    [Description("The max color of the AudioVisualization.")]
    [DisplayName("ColorMax")]
    [Category("AudioVisualization")]
    [Browsable(true)]
    public Color ColorMax { get; set; } = Color.Snow;

    [Category("AudioVisualization")]
    [Description("The quality of the AudioVisualization.")]
    [DisplayName("HighQuality")]
    [Browsable(true)]
    [ReadOnly(false)]
    public bool HighQuality { get; set; } = true;

    [ReadOnly(false)]
    [Description("The interval value of the timer of the AudioVisualization.")]
    [Browsable(true)]
    [Category("AudioVisualization")]
    [DisplayName("Interval")]
    public int Interval { get; set; } = 40;

    [Category("AudioVisualization")]
    [DisplayName("UseAverage")]
    [Description("The UseAverage of the AudioVisualization.")]
    [ReadOnly(false)]
    [Browsable(true)]
    public bool UseAverage { get; set; } = true;

    [ReadOnly(false)]
    [Browsable(true)]
    [Category("AudioVisualization")]
    [DisplayName("UseAverage")]
    [Description("The number of bars of the AudioVisualization.")]
    public int BarCount { get; set; } = 50;

    [Browsable(true)]
    [Category("AudioVisualization")]
    [DisplayName("BarSpacing")]
    [Description("The bar spacing of the AudioVisualization.")]
    [ReadOnly(false)]
    public int BarSpacing { get; set; } = 2;

    [ReadOnly(false)]
    [Description("The IsXLogScale of the AudioVisualization.")]
    [DisplayName("IsXLogScale")]
    [Category("AudioVisualization")]
    [Browsable(true)]
    public bool IsXLogScale { get; set; } = true;

    [Category("AudioVisualization")]
    [Browsable(true)]
    [Description("The maximum frequency of the AudioVisualization.")]
    [ReadOnly(false)]
    [DisplayName("MaximumFrequency")]
    public int MaximumFrequency { get; set; } = 10000;

    public AudioVisualization() => this.InitializeComponent();

    private void AudioVisualization_Load(object sender, EventArgs e) => this.pictureBoxGraph.Dock = DockStyle.Fill;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pictureBoxGraph = new PictureBox();
      ((ISupportInitialize) this.pictureBoxGraph).BeginInit();
      this.SuspendLayout();
      this.pictureBoxGraph.BackColor = Color.Black;
      this.pictureBoxGraph.Location = new Point(35, 63);
      this.pictureBoxGraph.Name = "pictureBoxGraph";
      this.pictureBoxGraph.Size = new Size(358, 89);
      this.pictureBoxGraph.TabIndex = 2;
      this.pictureBoxGraph.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.pictureBoxGraph);
      this.Name = nameof (AudioVisualization);
      this.Size = new Size(426, 201);
      this.Load += new EventHandler(this.AudioVisualization_Load);
      ((ISupportInitialize) this.pictureBoxGraph).EndInit();
      this.ResumeLayout(false);
    }
  }
}
