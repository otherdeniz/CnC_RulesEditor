using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class ImageListComponent : Component
    {
        private static ImageListComponent? _instance;
        public static ImageListComponent Instance => 
            _instance ??= new ImageListComponent();

        public ImageListComponent()
        {
            InitializeComponent();
        }

        public ImageListComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
