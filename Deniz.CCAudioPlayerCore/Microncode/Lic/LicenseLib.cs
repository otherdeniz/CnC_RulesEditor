// Decompiled with JetBrains decompiler
// Type: Microncode.Lic.LicenseLib
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Security.Cryptography;
using System.Text;

namespace Microncode.Lic
{
  internal class LicenseLib
  {
    private static string CreateMD5(string input)
    {
      try
      {
        using (MD5 md5 = MD5.Create())
        {
          byte[] bytes = Encoding.ASCII.GetBytes(input);
          byte[] hash = md5.ComputeHash(bytes);
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < hash.Length; ++index)
            stringBuilder.Append(hash[index].ToString("X2"));
          return stringBuilder.ToString().ToLower();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
      return "";
    }

    internal static bool CheckLicense(
      string UserName,
      string UserKey,
      string AppName,
      string AppVersion,
      string AppDescription = "",
      string AppCompany = "",
      string AppRegisterLicense = "",
      string AppWebsite = "",
      string AppHomepage = "",
      bool DisplayRegistrationWindow = true,
      bool PrintToConsole = false)
    {
      try
      {
        string str = AppVersion.Split('.')[0];
        if (LicenseLib.CreateMD5(AppName + str + UserName) == UserKey)
          return true;
        if (DisplayRegistrationWindow)
          Console.WriteLine("\n\n****************************************************************\nThe " + AppName + " runs in a FREE mode for personal or FREE use.\nFor commercial or any other use please order a license at\n" + AppHomepage + "\n****************************************************************\n\n");
        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
      return false;
    }
  }
}
