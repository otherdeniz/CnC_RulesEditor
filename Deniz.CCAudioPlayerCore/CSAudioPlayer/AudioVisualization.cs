// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.AudioVisualization
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CSAudioPlayer
{
  [ToolboxItem(true)]
  public class AudioVisualization : WinformsVisualization.Visualization.AudioVisualization
  {
    private IContainer components = (IContainer) null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ((ISupportInitialize) this.pictureBoxGraph).BeginInit();
      this.SuspendLayout();
      this.pictureBoxGraph.Dock = DockStyle.Fill;
      this.pictureBoxGraph.Location = new Point(0, 0);
      this.pictureBoxGraph.Size = new Size(565, 274);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = nameof (AudioVisualization);
      this.Size = new Size(565, 274);
      ((ISupportInitialize) this.pictureBoxGraph).EndInit();
      this.ResumeLayout(false);
    }
  }
}
