using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    public partial class LoadingPopupForm : PopupFormBase
    {
        public LoadingPopupForm()
        {
            InitializeComponent();
        }

        public static LoadingPopupForm ShowPopup(Form parent)
        {
            var form = new LoadingPopupForm();
            form.Location = new Point(parent.Location.X + parent.Width / 2 - form.Width / 2, 
                parent.Location.Y + parent.Height / 2 - form.Height / 2);
            form.LoadAnimation();
            form.Show(parent);
            return form;
        }

        private void LoadAnimation()
        {
            var paletteFile = PalFile.ReadFromFile(ResourcesRepository.Instance.ReadResourcesFile("loading.pal"));
            var shpData = ResourcesRepository.Instance.ReadResourcesFile("loading.shp");
            var shpFile = new ShpFile("loading.shp");
            shpFile.ParseFromBuffer(shpData);
            pictureBoxUnitPreview.Image = shpFile.GetUnitAnimation(shpData, paletteFile.Colors, 380, 1.4);
        }
    }
}
