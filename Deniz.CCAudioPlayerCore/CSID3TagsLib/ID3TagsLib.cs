// Decompiled with JetBrains decompiler
// Type: CSID3TagsLib.ID3TagsLib
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TagLib;
using TagLib.Id3v2;

namespace CSID3TagsLib
{
  [ToolboxItem(false)]
  public class ID3TagsLib
  {
    private TagLib.File file;
    private bool g_bLicense;

    private float GetRatingStars()
    {
      if (this.TagRating == (short) 0)
        return 0.0f;
      if (this.TagRating == (short) 1)
        return 1f;
      if (this.TagRating >= (short) 2 && this.TagRating <= (short) 22)
        return 0.5f;
      if (this.TagRating >= (short) 23 && this.TagRating <= (short) 31)
        return 1f;
      if (this.TagRating >= (short) 32 && this.TagRating <= (short) 63)
        return 1.5f;
      if (this.TagRating >= (short) 64 && this.TagRating <= (short) 95)
        return 2f;
      if (this.TagRating >= (short) 96 && this.TagRating <= (short) sbyte.MaxValue)
        return 2.5f;
      if (this.TagRating >= (short) 128 && this.TagRating <= (short) 159)
        return 3f;
      if (this.TagRating >= (short) 160 && this.TagRating <= (short) 195)
        return 3.5f;
      if (this.TagRating >= (short) 196 && this.TagRating <= (short) 223)
        return 4f;
      if (this.TagRating >= (short) 224 && this.TagRating <= (short) 254)
        return 4.5f;
      return this.TagRating == (short) byte.MaxValue ? 5f : 0.0f;
    }

    private short SetRatingStars(float ratingstars)
    {
      if ((double) ratingstars == 0.0)
        return 0;
      if ((double) ratingstars == 1.0)
        return 1;
      if ((double) ratingstars == 0.5)
        return 11;
      if ((double) ratingstars == 1.0)
        return 28;
      if ((double) ratingstars == 1.5)
        return 48;
      if ((double) ratingstars == 2.0)
        return 80;
      if ((double) ratingstars == 2.5)
        return 110;
      if ((double) ratingstars == 3.0)
        return 140;
      if ((double) ratingstars == 3.5)
        return 180;
      if ((double) ratingstars == 4.0)
        return 210;
      if ((double) ratingstars == 4.5)
        return 240;
      return (double) ratingstars == 5.0 ? (short) byte.MaxValue : (short) 0;
    }

    [Category("CSID3Editor")]
    [Description("A list of strings for the TagComposers of the opened multimedia file.")]
    [Browsable(true)]
    [DisplayName("TagComposers")]
    [ReadOnly(false)]
    public List<string> TagComposers { get; set; } = new List<string>();

    [Category("CSID3Editor")]
    [DisplayName("TagArtists")]
    [ReadOnly(false)]
    [Description("A list of strings for the TagArtists of the opened multimedia file.")]
    [Browsable(true)]
    public List<string> TagArtists { get; set; } = new List<string>();

    [ReadOnly(false)]
    [Browsable(true)]
    [Description("A list of strings for the TagPerformers of the opened multimedia file. In some cases this tag will be the TagArtists tag.")]
    [DisplayName("TagPerformers")]
    [Category("CSID3Editor")]
    public List<string> TagPerformers { get; set; } = new List<string>();

    [DisplayName("TagGenres")]
    [ReadOnly(false)]
    [Description("A list of strings for the TagComposers of the opened multimedia file.")]
    [Category("CSID3Editor")]
    [Browsable(true)]
    public List<string> TagGenres { get; set; } = new List<string>();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ReadOnly(false)]
    public List<ImageTag> TagImages { get; set; } = new List<ImageTag>();

    [Description("The file name of the opened multimedia file.")]
    [Category("CSID3Editor")]
    [DisplayName("FileName")]
    [Browsable(true)]
    [ReadOnly(false)]
    public string FileName { get; set; }

    [DisplayName("TagTitle")]
    [Category("CSID3Editor")]
    [Browsable(true)]
    [Description("The TagTitle tag of the opened multimedia file.")]
    [ReadOnly(false)]
    public string TagTitle { get; set; }

    [Browsable(true)]
    [DisplayName("TagLyrics")]
    [ReadOnly(false)]
    [Category("CSID3Editor")]
    [Description("The lyrics of the opened multimedia file.")]
    public string TagLyrics { get; set; }

    [Category("CSID3Editor")]
    [Description("The album name of the opened multimedia file.")]
    [Browsable(true)]
    [DisplayName("TagAlbum")]
    [ReadOnly(false)]
    public string TagAlbum { get; set; }

    [ReadOnly(false)]
    [Category("CSID3Editor")]
    [Browsable(true)]
    [DisplayName("TagComment")]
    [Description("The comment of the opened multimedia file.")]
    public string TagComment { get; set; }

    [DisplayName("TagCopyright")]
    [Browsable(true)]
    [Description("The copyright of the opened multimedia file.")]
    [Category("CSID3Editor")]
    [ReadOnly(false)]
    public string TagCopyright { get; set; }

    [ReadOnly(false)]
    [Category("CSID3Editor")]
    [DisplayName("TagYear")]
    [Description("The created year of the opened multimedia file.")]
    [Browsable(true)]
    public string TagYear { get; set; }

    [Category("CSID3Editor")]
    [DisplayName("TagTrack")]
    [Description("The Track number of the opened multimedia file.")]
    [ReadOnly(false)]
    [Browsable(true)]
    public uint TagTrack { get; set; }

    [Browsable(true)]
    [DisplayName("FileProperties")]
    [Category("CSID3Editor")]
    [ReadOnly(false)]
    [Description("The properties of the opened multimedia file.")]
    public FileProperties FileProperties { get; set; } = new FileProperties();

    [Browsable(true)]
    [ReadOnly(false)]
    [Description("The rating stars of the opened multimedia file. This value can be 0 (unrated) to 5 (best).")]
    [Category("CSID3Editor")]
    [DisplayName("RatingStars")]
    public float RatingStars
    {
      get => this.GetRatingStars();
      set => this.TagRating = this.SetRatingStars(value);
    }

    [ReadOnly(false)]
    [DisplayName("TagRating")]
    [Category("CSID3Editor")]
    [Description("The rating value of the opened multimedia file. This value can be 0 (unrated) to 255 (best).")]
    [Browsable(true)]
    public short TagRating { get; set; } = 0;

    [Description("The user of the rating tag. The default value is: \"Windows Media Player 9 Series\".")]
    [ReadOnly(false)]
    [DisplayName("RatingUser")]
    [Category("CSID3Editor")]
    [Browsable(true)]
    public string RatingUser { get; set; } = "Windows Media Player 9 Series";

    public void ClearAllTags()
    {
      this.file.RemoveTags(TagLib.TagTypes.AllTags);
      this.file.Save();
    }

    public void ClearTag(TagTypes TagType)
    {
      this.file.RemoveTags((TagLib.TagTypes) TagType);
      this.file.Save();
    }

    public int Init(string sKey)
    {
      if (sKey == "1DQFYPN3T8G1BHWWSIAFPM55DN2I8SXTTNNKMZP1IAYCIBAJ")
        this.g_bLicense = true;
      return 0;
    }

    public virtual bool Open(string multimedia_file)
    {
      try
      {
        this.TagComposers.Clear();
        this.TagArtists.Clear();
        this.TagPerformers.Clear();
        this.TagGenres.Clear();
        this.TagImages.Clear();
        this.FileName = multimedia_file;
        this.file = TagLib.File.Create(multimedia_file);
        this.TagTitle = this.file.Tag.Title;
        this.TagAlbum = this.file.Tag.Album;
        this.TagYear = this.file.Tag.Year.ToString();
        this.TagComment = this.file.Tag.Comment;
        this.TagCopyright = this.file.Tag.Copyright;
        this.TagLyrics = this.file.Tag.Lyrics;
        this.TagTrack = this.file.Tag.Track;
        if (((IEnumerable<string>) this.file.Tag.Composers).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) this.file.Tag.Composers).Count<string>(); ++index)
            this.TagComposers.Add(this.file.Tag.Composers[index].ToString());
        }
        if (((IEnumerable<string>) this.file.Tag.Artists).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) this.file.Tag.Artists).Count<string>(); ++index)
            this.TagArtists.Add(this.file.Tag.Artists[index].ToString());
        }
        if (((IEnumerable<string>) this.file.Tag.Performers).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) this.file.Tag.Performers).Count<string>(); ++index)
            this.TagPerformers.Add(this.file.Tag.Performers[index].ToString());
        }
        if (((IEnumerable<string>) this.file.Tag.Genres).Count<string>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<string>) this.file.Tag.Genres).Count<string>(); ++index)
            this.TagGenres.Add(this.file.Tag.Genres[index].ToString());
        }
        if (((IEnumerable<IPicture>) this.file.Tag.Pictures).Count<IPicture>() > 0)
        {
          for (int index = 0; index < ((IEnumerable<IPicture>) this.file.Tag.Pictures).Count<IPicture>(); ++index)
            this.TagImages.Add(new ImageTag(Image.FromStream((Stream) new MemoryStream(this.file.Tag.Pictures[index].Data.Data)), this.file.Tag.Pictures[index].MimeType, this.file.Tag.Pictures[index].Description, (ImageType) this.file.Tag.Pictures[index].Type));
        }
        this.FileProperties.AudioBitrate = this.file.Properties.AudioBitrate;
        this.FileProperties.AudioChannels = this.file.Properties.AudioChannels;
        this.FileProperties.AudioSampleRate = this.file.Properties.AudioSampleRate;
        this.FileProperties.BitsPerSample = this.file.Properties.BitsPerSample;
        this.FileProperties.Description = this.file.Properties.Description;
        this.FileProperties.Duration = this.file.Properties.Duration;
        this.FileProperties.PhotoHeight = this.file.Properties.PhotoHeight;
        this.FileProperties.PhotoQuality = this.file.Properties.PhotoQuality;
        this.FileProperties.PhotoWidth = this.file.Properties.PhotoWidth;
        this.FileProperties.VideoHeight = this.file.Properties.VideoHeight;
        this.FileProperties.VideoWidth = this.file.Properties.VideoWidth;
        this.TagRating = (short) PopularimeterFrame.Get((TagLib.Id3v2.Tag) this.file.GetTag(TagLib.TagTypes.Id3v2), this.RatingUser, true).Rating;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
      finally
      {
      }
      return true;
    }

    public virtual void Set()
    {
      string tagComment = this.TagComment;
      this.file.Tag.Title = this.TagTitle;
      this.file.Tag.Album = this.TagAlbum;
      this.file.Tag.Comment = tagComment;
      this.file.Tag.Copyright = this.TagCopyright;
      this.file.Tag.Lyrics = this.TagLyrics;
      this.file.Tag.Track = this.TagTrack;
      this.file.Tag.Performers = this.TagPerformers.ToArray();
      this.file.Tag.AlbumArtists = this.TagArtists.ToArray();
      this.file.Tag.Genres = this.TagGenres.ToArray();
      this.file.Tag.Composers = this.TagComposers.ToArray();
      try
      {
        this.file.Tag.Year = (uint) int.Parse(this.TagYear);
      }
      catch
      {
      }
      Picture[] source = new Picture[this.TagImages.Count<ImageTag>()];
      int index = 0;
      foreach (ImageTag tagImage in this.TagImages)
      {
        Picture picture = new Picture();
        Image image = tagImage.Image;
        ImageFormat format = ImageFormat.Jpeg;
        if (tagImage.Mine_Type == "image/png")
          format = ImageFormat.Png;
        picture.Type = (PictureType) tagImage.Type;
        picture.Description = tagImage.Description;
        picture.MimeType = tagImage.Mine_Type;
        MemoryStream memoryStream = new MemoryStream();
        image.Save((Stream) memoryStream, format);
        memoryStream.Position = 0L;
        picture.Data = ByteVector.FromStream((Stream) memoryStream);
        source[index] = picture;
        memoryStream.Close();
        ++index;
      }
      try
      {
        this.file.Tag.Pictures = (IPicture[]) ((IEnumerable<Picture>) source).ToArray<Picture>();
        TagLib.Id3v2.Tag.DefaultVersion = (byte) 3;
        TagLib.Id3v2.Tag.ForceDefaultVersion = true;
        PopularimeterFrame.Get((TagLib.Id3v2.Tag) this.file.GetTag(TagLib.TagTypes.Id3v2), this.RatingUser, true).Rating = (byte) this.TagRating;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
      this.file.Save();
    }

    public Image AddImageFromFile(string image_file, string comment = "microncode.com", ImageType image_type = ImageType.FrontCover)
    {
      Image image = Image.FromFile(image_file);
      string mineType = Core.GetMineType(image);
      this.TagImages.Add(new ImageTag(image, mineType, comment, image_type));
      return image;
    }
  }
}
