// Decompiled with JetBrains decompiler
// Type: Options.Core.Log
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.IO;
using System.Windows.Forms;

namespace Options.Core
{
  internal class Log
  {
    public void SaveToLog(string str)
    {
      string logPath = this.GetLogPath();
      if (!Directory.Exists(logPath))
        Directory.CreateDirectory(logPath);
      PatternMaker patternMaker = new PatternMaker();
      string path = logPath + patternMaker.replacer("[year]-[month]-[day]") + ".txt";
      str = patternMaker.replacer("[hour]-[minute]-[second]") + ":" + str;
      File.AppendAllText(path, str + Environment.NewLine);
    }

    public string GetLogPath() => Application.StartupPath + "\\Log\\";
  }
}
