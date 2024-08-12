namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    public partial class UnitPopupForm : PopupFormBase
    {
        public UnitPopupForm()
        {
            InitializeComponent();
        }

        public static UnitPopupForm? ShowPopup(Form parent, Point location, Image? entityThumbnail)
        {
            if (entityThumbnail == null) return null;
            var form = new UnitPopupForm
            {
                Location = location
            };
            form.pictureThumbnail.Image = entityThumbnail;
            form.Show(parent);
            return form;
        }

    }
}
