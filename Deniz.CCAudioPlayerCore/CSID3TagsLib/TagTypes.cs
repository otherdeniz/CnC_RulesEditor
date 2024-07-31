// Decompiled with JetBrains decompiler
// Type: CSID3TagsLib.TagTypes
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

namespace CSID3TagsLib
{
  public enum TagTypes : uint
  {
    None = 0,
    Xiph = 1,
    Id3v1 = 2,
    Id3v2 = 4,
    Ape = 8,
    Apple = 16, // 0x00000010
    Asf = 32, // 0x00000020
    RiffInfo = 64, // 0x00000040
    MovieId = 128, // 0x00000080
    DivX = 256, // 0x00000100
    FlacMetadata = 512, // 0x00000200
    AudibleMetadata = 1024, // 0x00000400
    TiffIFD = 1024, // 0x00000400
    XMP = 2048, // 0x00000800
    JpegComment = 4096, // 0x00001000
    GifComment = 8192, // 0x00002000
    Png = 16384, // 0x00004000
    IPTCIIM = 32768, // 0x00008000
    AllTags = 4294967295, // 0xFFFFFFFF
  }
}
