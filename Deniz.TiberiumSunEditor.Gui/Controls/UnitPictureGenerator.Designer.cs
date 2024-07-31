namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class UnitPictureGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitPictureGenerator));
            labelName = new Label();
            labelKey = new Label();
            pictureThumbnail = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.BackColor = Color.Transparent;
            labelName.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            labelName.Location = new Point(0, 74);
            labelName.Margin = new Padding(4, 0, 4, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(233, 16);
            labelName.TabIndex = 4;
            labelName.Text = "Unit Name";
            // 
            // labelKey
            // 
            labelKey.BackColor = Color.Transparent;
            labelKey.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            labelKey.Location = new Point(0, 58);
            labelKey.Margin = new Padding(4, 0, 4, 0);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(175, 16);
            labelKey.TabIndex = 3;
            labelKey.Text = "KEY";
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(0, 0);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(78, 58);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 2;
            pictureThumbnail.TabStop = false;
            // 
            // UnitPictureGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(labelName);
            Controls.Add(labelKey);
            Controls.Add(pictureThumbnail);
            Name = "UnitPictureGenerator";
            Size = new Size(102, 90);
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label labelName;
        private Label labelKey;
        private PictureBox pictureThumbnail;
    }
}
