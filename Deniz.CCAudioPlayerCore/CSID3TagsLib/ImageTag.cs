// Decompiled with JetBrains decompiler
// Type: CSID3TagsLib.ImageTag
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.Drawing;

namespace CSID3TagsLib
{
  public class ImageTag
  {
    private Image image;
    private string mine_type;
    private string description;
    private ImageType type;
    private string mimeType;
    private ImageType type1;

    public ImageTag(Image Image, string Mine_Type, string Description, ImageType Type)
    {
      this.image = Image;
      this.mine_type = Mine_Type;
      this.description = Description;
      this.type = Type;
    }

    public Image Image
    {
      get => this.image;
      set => this.image = value;
    }

    public string Mine_Type
    {
      get => this.mine_type;
      set => this.mine_type = value;
    }

    public string Description
    {
      get => this.description;
      set => this.description = value;
    }

    public ImageType Type
    {
      get => this.type;
      set => this.type = value;
    }
  }
}
