// Decompiled with JetBrains decompiler
// Type: Options.Core.CustomFileCopier
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.IO;

namespace Options.Core
{
  internal class CustomFileCopier
  {
    public CustomFileCopier(string Source, string Dest)
    {
      this.SourceFilePath = Source;
      this.DestFilePath = Dest;
      this.OnProgressChanged += (ProgressChangeDelegate) ((double _param1, ref bool _param2) => { });
      this.OnComplete += (Completedelegate) (() => { });
    }

    public void Copy()
    {
      try
      {
        byte[] buffer = new byte[1048576];
        using (FileStream fileStream1 = new FileStream(this.SourceFilePath, FileMode.Open, FileAccess.Read))
        {
          long length = fileStream1.Length;
          using (FileStream fileStream2 = new FileStream(this.DestFilePath, FileMode.CreateNew, FileAccess.Write))
          {
            long num = 0;
            int count;
            while ((count = fileStream1.Read(buffer, 0, buffer.Length)) > 0)
            {
              num += (long) count;
              double Persentage = (double) num * 100.0 / (double) length;
              fileStream2.Write(buffer, 0, count);
              bool Cancel = false;
              if (this.OnProgressChanged != null)
                this.OnProgressChanged(Persentage, ref Cancel);
              if (Cancel)
                break;
            }
          }
        }
        this.OnComplete();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message, ex);
      }
      finally
      {
      }
    }

    public string SourceFilePath { get; set; }

    public string DestFilePath { get; set; }

    public event ProgressChangeDelegate OnProgressChanged;

    public event Completedelegate OnComplete;
  }
}
