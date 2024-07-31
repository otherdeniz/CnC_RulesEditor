// Decompiled with JetBrains decompiler
// Type: CSID3TagsLib.FileProperties
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;

namespace CSID3TagsLib
{
  public class FileProperties
  {
    public TimeSpan Duration { get; set; }

    public string Description { get; set; }

    public int AudioBitrate { get; set; }

    public int AudioSampleRate { get; set; }

    public int BitsPerSample { get; set; }

    public int AudioChannels { get; set; }

    public int VideoWidth { get; set; }

    public int VideoHeight { get; set; }

    public int PhotoWidth { get; set; }

    public int PhotoHeight { get; set; }

    public int PhotoQuality { get; set; }
  }
}
