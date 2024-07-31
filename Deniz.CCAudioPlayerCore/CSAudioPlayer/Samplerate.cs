// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.Samplerate
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;

namespace CSAudioPlayer
{
  public enum Samplerate
  {
    [Description("8000")] asamples8000 = 8000, // 0x00001F40
    [Description("11025")] bsamples11025 = 11025, // 0x00002B11
    [Description("22050")] csamples22050 = 22050, // 0x00005622
    [Description("44100")] esamples44100 = 44100, // 0x0000AC44
    [Description("48000")] fsamples48000 = 48000, // 0x0000BB80
  }
}
