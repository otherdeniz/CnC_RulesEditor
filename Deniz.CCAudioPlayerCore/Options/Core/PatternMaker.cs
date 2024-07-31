// Decompiled with JetBrains decompiler
// Type: Options.Core.PatternMaker
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;

namespace Options.Core
{
  internal class PatternMaker
  {
    public string format { get; set; } = "none";

    public string replacer(string pattern) => pattern.Replace("[day]", DateTime.Now.Day.ToString()).Replace("[month]", DateTime.Now.Month.ToString()).Replace("[year]", DateTime.Now.Year.ToString()).Replace("[hour]", DateTime.Now.Hour.ToString()).Replace("[minute]", DateTime.Now.Minute.ToString()).Replace("[second]", DateTime.Now.Second.ToString()).Replace("[pc_name]", Environment.MachineName).Replace("[format]", this.format);
  }
}
