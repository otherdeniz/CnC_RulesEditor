// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.ProgressArgs
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;

namespace CSAudioPlayer
{
  public class ProgressArgs : EventArgs
  {
    public TimeSpan Position { get; set; }
  }
}
