// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.DecodeMode
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;

namespace CSAudioPlayer
{
  public enum DecodeMode
  {
    [Description("LocalCodecs")] LocalCodecs = 1,
    [Description("FFMpeg")] FFMpeg = 2,
  }
}
