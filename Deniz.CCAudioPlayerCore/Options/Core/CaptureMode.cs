// Decompiled with JetBrains decompiler
// Type: Options.Core.CaptureMode
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.ComponentModel;

namespace Options.Core
{
  internal enum CaptureMode
  {
    [Description("WasapiCapture")] WasapiCapture = 1,
    [Description("WasapiLoopbackCapture")] WasapiLoopbackCapture = 2,
    [Description("LineIn")] LineIn = 3,
  }
}
