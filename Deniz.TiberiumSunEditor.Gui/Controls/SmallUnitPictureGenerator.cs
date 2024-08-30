using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Controls;

public partial class SmallUnitPictureGenerator : UserControl
{
    private static SmallUnitPictureGenerator? _instance;
    public static SmallUnitPictureGenerator Instance => _instance ??= new SmallUnitPictureGenerator();

    public SmallUnitPictureGenerator()
    {
        InitializeComponent();
    }

    public Bitmap GetUnitPicture(GameEntityModel? entityModel)
    {
        LoadEntity(entityModel);
        var bitmap = new Bitmap(Width, Height);
        this.DrawToBitmap(bitmap, new Rectangle(0, 0, Width, Height));
        return bitmap;
    }

    private void LoadEntity(GameEntityModel? entityModel)
    {
        if (entityModel == null)
        {
            labelName.Text = "";
            labelKey.Text = "(none)";
            pictureThumbnail.Image = null;
            pictureThumbnail.BorderStyle = BorderStyle.FixedSingle;
        }
        else
        {
            labelName.Text = entityModel.EntityName;
            labelKey.Text = entityModel.EntityKey;
            var thumbnail = entityModel.Thumbnail;
            if (thumbnail?.Kind == ThumbnailKind.Animation)
            {
                pictureThumbnail.Image = thumbnail.LoadAnimation();
            }
            else
            {
                pictureThumbnail.Image = thumbnail?.Image
                                         ?? BitmapRepository.Instance.BlankImage;
            }
            pictureThumbnail.BorderStyle = BorderStyle.None;
        }
    }
}