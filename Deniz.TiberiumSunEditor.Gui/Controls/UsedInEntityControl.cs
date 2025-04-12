using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UsedInEntityControl : UserControl
    {
        public UsedInEntityControl()
        {
            InitializeComponent();
        }

        public void LoadEntity(GameEntityModel entityModel, string usesKey)
        {
            labelName.Text = entityModel.EntityName;
            labelKey.Text = entityModel.EntityKey;
            labelTags.Text = string.Join(",",
                entityModel.FileSection.KeyValues
                    .Where(k => k.Value.Split(",")
                        .Any(v => v.Equals(usesKey, StringComparison.InvariantCultureIgnoreCase)))
                    .Select(k => k.Key));
            if (entityModel.Thumbnail?.Kind == ThumbnailKind.Animation)
            {
                pictureThumbnail.Image = BitmapRepository.Instance.BlankImage;
                pictureThumbnail.Image = entityModel.Thumbnail.LoadAnimation();
            }
            else
            {
                pictureThumbnail.Image = entityModel.Thumbnail?.Image
                                         ?? BitmapRepository.Instance.BlankImage;
            }
        }

        private void pictureThumbnail_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void labelKey_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void labelName_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void labelTags_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
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

        private void label3_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void labelTags_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
    }
}
