// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.Bits
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;

namespace CSAudioPlayer
{
  public enum Bits
  {
    [Description("8")] bits8 = 8,
    [Description("16")] bits16 = 16, // 0x00000010
    [Description("24")] bits24 = 24, // 0x00000018
    [Description("32")] bits32 = 32, // 0x00000020
  }
}
