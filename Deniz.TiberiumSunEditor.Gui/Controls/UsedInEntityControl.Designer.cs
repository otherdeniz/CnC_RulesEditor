namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class UsedInEntityControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsedInEntityControl));
            labelTags = new Label();
            label3 = new Label();
            labelKey = new Label();
            labelName = new Label();
            pictureThumbnail = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            SuspendLayout();
            // 
            // labelTags
            // 
            labelTags.AutoSize = true;
            labelTags.Location = new Point(159, 17);
            labelTags.Name = "labelTags";
            labelTags.Size = new Size(63, 15);
            labelTags.TabIndex = 9;
            labelTags.Text = "Tag1, Tag2";
            labelTags.Click += labelTags_Click;
            labelTags.MouseEnter += labelTags_MouseEnter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(159, 1);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 12;
            label3.Text = "used in:";
            label3.Click += label3_Click;
            label3.MouseEnter += label3_MouseEnter;
            // 
            // labelKey
            // 
            labelKey.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelKey.Location = new Point(44, 1);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(109, 15);
            labelKey.TabIndex = 10;
            labelKey.Text = "Key";
            labelKey.Click += labelKey_Click;
            labelKey.MouseEnter += labelKey_MouseEnter;
            // 
            // labelName
            // 
            labelName.Location = new Point(44, 17);
            labelName.Name = "labelName";
            labelName.Size = new Size(109, 15);
            labelName.TabIndex = 11;
            labelName.Text = "Name";
            labelName.Click += labelName_Click;
            labelName.MouseEnter += labelName_MouseEnter;
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(2, 2);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(39, 29);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 8;
            pictureThumbnail.TabStop = false;
            pictureThumbnail.Click += pictureThumbnail_Click;
            pictureThumbnail.MouseEnter += pictureThumbnail_MouseEnter;
            // 
            // UsedInEntityControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(labelTags);
            Controls.Add(label3);
            Controls.Add(labelKey);
            Controls.Add(labelName);
            Controls.Add(pictureThumbnail);
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MinimumSize = new Size(300, 35);
            Name = "UsedInEntityControl";
            Size = new Size(303, 35);
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTags;
        private Label label3;
        private Label labelKey;
        private Label labelName;
        private PictureBox pictureThumbnail;
    }
}
