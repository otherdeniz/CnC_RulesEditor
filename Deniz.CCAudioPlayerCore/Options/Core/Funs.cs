// Decompiled with JetBrains decompiler
// Type: Options.Core.Funs
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore.CoreAudioAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Options.Core
{
  internal class Funs
  {
    private static Log _log = new Log();
    public static readonly string[] SizeSuffixes = new string[9]
    {
      "bytes",
      "KB",
      "MB",
      "GB",
      "TB",
      "PB",
      "EB",
      "ZB",
      "YB"
    };

    public static MMDevice GetDefaultRenderDevice()
    {
      using (MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator())
        return deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
    }

    public static string GetDescription(Enum value) => ((DescriptionAttribute) Attribute.GetCustomAttribute((MemberInfo) ((IEnumerable<FieldInfo>) value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public)).Single<FieldInfo>((Func<FieldInfo, bool>) (x => x.GetValue((object) null).Equals((object) value))), typeof (DescriptionAttribute)))?.Description ?? value.ToString();

    public static string SizeSuffix(long value, int decimalPlaces = 1)
    {
      if (decimalPlaces < 0)
        throw new ArgumentOutOfRangeException(nameof (decimalPlaces));
      if (value < 0L)
        return "-" + Funs.SizeSuffix(-value);
      if (value == 0L)
        return string.Format("{0:n" + decimalPlaces.ToString() + "} bytes", (object) 0);
      int index = (int) Math.Log((double) value, 1024.0);
      Decimal d = (Decimal) value / (Decimal) (1L << index * 10);
      if (Math.Round(d, decimalPlaces) >= 1000M)
      {
        ++index;
        d /= 1024M;
      }
      return string.Format("{0:n" + decimalPlaces.ToString() + "} {1}", (object) d, (object) Funs.SizeSuffixes[index]);
    }

    public static string AddExceptionToLog(Exception ex)
    {
      string str = ex.Message + " (" + ex.HResult.ToString() + ")";
      Funs._log.SaveToLog(str);
      return str;
    }

    public static string AddStringToLog(string s)
    {
      Funs._log.SaveToLog(s);
      return s;
    }

    public static string GetLogPath() => Funs._log.GetLogPath();
  }
}
