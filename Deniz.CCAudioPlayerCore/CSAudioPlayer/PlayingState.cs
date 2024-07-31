// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.PlayingState
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;

namespace CSAudioPlayer
{
  public enum PlayingState
  {
    [Description("Stopped")] Stopped = 1,
    [Description("Playing")] Playing = 2,
    [Description("Paused")] Paused = 3,
  }
}
