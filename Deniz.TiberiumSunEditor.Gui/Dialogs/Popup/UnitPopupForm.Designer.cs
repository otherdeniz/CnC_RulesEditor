namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class UnitPopupForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitPopupForm));
            pictureThumbnail = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            SuspendLayout();
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.BorderStyle = BorderStyle.FixedSingle;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(0, 0);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(78, 58);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 3;
            pictureThumbnail.TabStop = false;
            // 
            // UnitPopupForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(78, 58);
            Controls.Add(pictureThumbnail);
            Name = "UnitPopupForm";
            Text = "UnitPopupForm";
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureThumbnail;
    }
}