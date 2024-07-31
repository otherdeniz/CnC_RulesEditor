// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.AudioMeter
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;

namespace CSAudioPlayer
{
  [ToolboxItem(true)]
  public class AudioMeter : CSAudioMeter.AudioMeter
  {
    private IContainer components = (IContainer) null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new Container();
  }
}
