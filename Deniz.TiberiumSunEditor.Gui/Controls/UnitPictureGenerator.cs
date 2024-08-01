using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using System.Drawing.Drawing2D;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UnitPictureGenerator : UserControl
    {
        private static UnitPictureGenerator? _instance;
        public static UnitPictureGenerator Instance => _instance ??= new UnitPictureGenerator();

        public UnitPictureGenerator()
        {
            InitializeComponent();
        }

        public Bitmap GetUnitPicture(GameEntityModel entityModel, 
            bool isSelected, 
            Action<Image>? afterAnimationLoad, 
            out AnimationRequirementToken? animationRequirementToken)
        {
            var entityThumbnail = entityModel.Thumbnail;
            if (entityThumbnail == null)
            {
                animationRequirementToken = null;
                return GenerateUnitPicture(entityModel, isSelected, BitmapRepository.Instance.BlankImage);
            }
            if (entityThumbnail.Kind == ThumbnailKind.Image)
            {
                animationRequirementToken = null;
                return GenerateUnitPicture(entityModel, isSelected, entityThumbnail.Image);
            }
            if (afterAnimationLoad != null)
            {
                animationRequirementToken = entityThumbnail.LoadAnimationAsync(afterAnimationLoad);
            }
            else
            {
                animationRequirementToken = null;
            }
            return GenerateUnitPicture(entityModel, isSelected, entityThumbnail.Image);
        }

        private Bitmap GenerateUnitPicture(GameEntityModel entityModel, bool isSelected, Image thumbnailImage)
        {
            BackColor = isSelected
                ? Color.LightSkyBlue
                : Color.White;
            pictureThumbnail.Image = thumbnailImage;
            labelKey.Text = entityModel.EntityKey;
            labelName.Text = entityModel.EntityName;
            var bitmap = new Bitmap(ImageListComponent.Instance.Blank1.Images[0], Width, Height);
            this.DrawToBitmap(bitmap, new Rectangle(0, 0, Width, Height));
            var sideLogos = GetUnitSideImages(entityModel);
            if (isSelected || sideLogos.Any())
            {
                using (var canvas = Graphics.FromImage(bitmap))
                {
                    canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var editHeight = 16d;
                    var editWidth = 16d;
                    var logoLeft = 0;
                    foreach (var sideLogo in sideLogos)
                    {
                        var overlayHeight = Convert.ToDouble(sideLogo.Height);
                        var overlayWidth = Convert.ToDouble(sideLogo.Width);
                        if (overlayWidth > editWidth || overlayHeight > editHeight)
                        {
                            var factor = (overlayWidth / editWidth > overlayHeight / editHeight)
                                ? overlayWidth / editWidth // fit width
                                : overlayHeight / editHeight; // fit height
                            overlayHeight = overlayHeight / factor;
                            overlayWidth = overlayWidth / factor;
                        }
                        var overlayLeft = (editWidth - overlayWidth) / 2d;
                        var overlayTop = (editHeight - overlayHeight) / 2d;
                        canvas.DrawImage(sideLogo,
                            new Rectangle(Convert.ToInt32(overlayLeft) + logoLeft,
                                Convert.ToInt32(overlayTop),
                                Convert.ToInt32(overlayWidth),
                                Convert.ToInt32(overlayHeight)),
                            new Rectangle(0,
                                0,
                                sideLogo.Width,
                                sideLogo.Height),
                            GraphicsUnit.Pixel);
                        logoLeft += Convert.ToInt32(editWidth);
                    }
                    if (isSelected)
                    {
                        canvas.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.LightSkyBlue)),
                            new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    }
                }
            }
            return bitmap;
        }

        private List<Image> GetUnitSideImages(GameEntityModel entityModel)
        {
            return entityModel.Sides
                .Select(s => entityModel.RootModel.FileType.GameDefinition.Sides.FirstOrDefault(d => d.Name == s))
                .Where(l => l != null)
                .Select(l => l!.GetLogoImage())
                .ToList();
        }
    }
}
