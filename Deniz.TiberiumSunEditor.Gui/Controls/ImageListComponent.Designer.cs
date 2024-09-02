namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class ImageListComponent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageListComponent));
            Favorite24 = new ImageList(components);
            Favorite16 = new ImageList(components);
            Favorite48 = new ImageList(components);
            Symbols16 = new ImageList(components);
            Symbols24 = new ImageList(components);
            Blank1 = new ImageList(components);
            Black150 = new ImageList(components);
            // 
            // Favorite24
            // 
            Favorite24.ColorDepth = ColorDepth.Depth32Bit;
            Favorite24.ImageStream = (ImageListStreamer)resources.GetObject("Favorite24.ImageStream");
            Favorite24.TransparentColor = Color.Transparent;
            Favorite24.Images.SetKeyName(0, "star_grey.png");
            Favorite24.Images.SetKeyName(1, "star_yellow.png");
            // 
            // Favorite16
            // 
            Favorite16.ColorDepth = ColorDepth.Depth32Bit;
            Favorite16.ImageStream = (ImageListStreamer)resources.GetObject("Favorite16.ImageStream");
            Favorite16.TransparentColor = Color.Transparent;
            Favorite16.Images.SetKeyName(0, "star_grey.png");
            Favorite16.Images.SetKeyName(1, "star_yellow.png");
            // 
            // Favorite48
            // 
            Favorite48.ColorDepth = ColorDepth.Depth32Bit;
            Favorite48.ImageStream = (ImageListStreamer)resources.GetObject("Favorite48.ImageStream");
            Favorite48.TransparentColor = Color.Transparent;
            Favorite48.Images.SetKeyName(0, "star_grey.png");
            Favorite48.Images.SetKeyName(1, "star_yellow.png");
            // 
            // Symbols16
            // 
            Symbols16.ColorDepth = ColorDepth.Depth32Bit;
            Symbols16.ImageStream = (ImageListStreamer)resources.GetObject("Symbols16.ImageStream");
            Symbols16.TransparentColor = Color.Transparent;
            Symbols16.Images.SetKeyName(0, "arrow_left_blue.png");
            Symbols16.Images.SetKeyName(1, "delete2.png");
            Symbols16.Images.SetKeyName(2, "navigate_plus.png");
            Symbols16.Images.SetKeyName(3, "navigate_minus.png");
            Symbols16.Images.SetKeyName(4, "arrow_up_blue.png");
            Symbols16.Images.SetKeyName(5, "arrow_down_blue.png");
            // 
            // Symbols24
            // 
            Symbols24.ColorDepth = ColorDepth.Depth32Bit;
            Symbols24.ImageStream = (ImageListStreamer)resources.GetObject("Symbols24.ImageStream");
            Symbols24.TransparentColor = Color.Transparent;
            Symbols24.Images.SetKeyName(0, "arrow_left_blue.png");
            Symbols24.Images.SetKeyName(1, "delete2.png");
            // 
            // Blank1
            // 
            Blank1.ColorDepth = ColorDepth.Depth32Bit;
            Blank1.ImageStream = (ImageListStreamer)resources.GetObject("Blank1.ImageStream");
            Blank1.TransparentColor = Color.Transparent;
            Blank1.Images.SetKeyName(0, "blank-1x1.png");
            // 
            // Black150
            // 
            Black150.ColorDepth = ColorDepth.Depth24Bit;
            Black150.ImageStream = (ImageListStreamer)resources.GetObject("Black150.ImageStream");
            Black150.TransparentColor = Color.Transparent;
            Black150.Images.SetKeyName(0, "black-150x150.png");
        }

        #endregion

        public ImageList Favorite24;
        public ImageList Favorite16;
        public ImageList Favorite48;
        public ImageList Symbols16;
        public ImageList Symbols24;
        public ImageList Blank1;
        public ImageList Black150;
    }
}
