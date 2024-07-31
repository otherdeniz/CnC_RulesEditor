// Decompiled with JetBrains decompiler
// Type: Options.Core.ExplorerFileProperties
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Runtime.InteropServices;

namespace Options.Core
{
  internal class ExplorerFileProperties
  {
    private const int SW_SHOW = 5;
    private const uint SEE_MASK_INVOKEIDLIST = 12;

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern bool ShellExecuteEx(
      ref ExplorerFileProperties.SHELLEXECUTEINFO lpExecInfo);

    public static bool ShowFileProperties(string Filename)
    {
      ExplorerFileProperties.SHELLEXECUTEINFO lpExecInfo = new ExplorerFileProperties.SHELLEXECUTEINFO();
      lpExecInfo.cbSize = Marshal.SizeOf<ExplorerFileProperties.SHELLEXECUTEINFO>(lpExecInfo);
      lpExecInfo.lpVerb = "properties";
      lpExecInfo.lpFile = Filename;
      lpExecInfo.nShow = 5;
      lpExecInfo.fMask = 12U;
      return ExplorerFileProperties.ShellExecuteEx(ref lpExecInfo);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHELLEXECUTEINFO
    {
      public int cbSize;
      public uint fMask;
      public IntPtr hwnd;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpVerb;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpFile;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpParameters;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpDirectory;
      public int nShow;
      public IntPtr hInstApp;
      public IntPtr lpIDList;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpClass;
      public IntPtr hkeyClass;
      public uint dwHotKey;
      public IntPtr hIcon;
      public IntPtr hProcess;
    }
  }
}
