using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Controls;

public partial class SmallEntityControl : UserControl
{
    private GameEntityModel? _entityModel;

    public SmallEntityControl()
    {
        InitializeComponent();
        LoadEntity();
    }

    [Browsable(false)]
    [DefaultValue(null)]
    public GameEntityModel? EntityModel
    {
        get => _entityModel;
        set
        {
            _entityModel = value;
            LoadEntity();
        }
    }

    private void LoadEntity()
    {
        if (_entityModel == null)
        {
            labelName.Text = "";
            labelKey.Text = "(none)";
            pictureThumbnail.Image = null;
            pictureThumbnail.BorderStyle = BorderStyle.FixedSingle;
        }
        else
        {
            labelName.Text = _entityModel.EntityName;
            labelKey.Text = _entityModel.EntityKey;
            var thumbnail = _entityModel.Thumbnail;
            if (thumbnail?.Kind == ThumbnailKind.Animation)
            {
                pictureThumbnail.Image = BitmapRepository.Instance.BlankImage;
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

    private void pictureThumbnail_Click(object sender, EventArgs e)
    {
        OnClick(e);
    }

    private void labelKey_Click(object sender, EventArgs e)
    {
        OnClick(e);
    }

    private void labelName_Click(object sender, EventArgs e)
    {
        OnClick(e);
    }
}