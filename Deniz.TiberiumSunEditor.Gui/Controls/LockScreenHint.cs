using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class LockScreenHint : UserControl
    {
        public LockScreenHint()
        {
            InitializeComponent();
        }

        public Bitmap CreateBitmap()
        {
            ThemeManager.Instance.UseTheme(this);
            var bitmap = new Bitmap(ImageListComponent.Instance.Blank1.Images[0], Width, Height);
            this.DrawToBitmap(bitmap, new Rectangle(0, 0, Width, Height));
            return bitmap;
        }
    }
}
