namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    partial class AiUnitEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiUnitEditControl));
            groupBox1 = new GroupBox();
            panelTop = new Panel();
            labelModifications = new Label();
            labelName = new Label();
            labelKey = new Label();
            pictureThumbnail = new PictureBox();
            entitiesListTaskForces = new EntitiesListControl();
            groupBox1.SuspendLayout();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(entitiesListTaskForces);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 69);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(864, 522);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Task Forces";
            // 
            // panelTop
            // 
            panelTop.Controls.Add(labelModifications);
            panelTop.Controls.Add(labelName);
            panelTop.Controls.Add(labelKey);
            panelTop.Controls.Add(pictureThumbnail);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(864, 69);
            panelTop.TabIndex = 8;
            // 
            // labelModifications
            // 
            labelModifications.AutoSize = true;
            labelModifications.BackColor = Color.Transparent;
            labelModifications.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelModifications.Location = new Point(89, 45);
            labelModifications.Margin = new Padding(4, 0, 4, 0);
            labelModifications.Name = "labelModifications";
            labelModifications.Size = new Size(75, 15);
            labelModifications.TabIndex = 3;
            labelModifications.Text = "0 Task Forces";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.BackColor = Color.Transparent;
            labelName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelName.Location = new Point(89, 23);
            labelName.Margin = new Padding(4, 0, 4, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(105, 19);
            labelName.TabIndex = 2;
            labelName.Text = "Mammoth Tank";
            // 
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.BackColor = Color.Transparent;
            labelKey.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelKey.Location = new Point(89, 2);
            labelKey.Margin = new Padding(4, 0, 4, 0);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(45, 19);
            labelKey.TabIndex = 2;
            labelKey.Text = "4TNK";
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(4, 3);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(78, 58);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 1;
            pictureThumbnail.TabStop = false;
            // 
            // entitiesListTaskForces
            // 
            entitiesListTaskForces.Dock = DockStyle.Fill;
            entitiesListTaskForces.Location = new Point(3, 19);
            entitiesListTaskForces.Name = "entitiesListTaskForces";
            entitiesListTaskForces.Size = new Size(858, 500);
            entitiesListTaskForces.TabIndex = 1;
            // 
            // AiUnitEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(panelTop);
            Name = "AiUnitEditControl";
            Size = new Size(864, 591);
            groupBox1.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox1;
        private Panel panelTop;
        private Label labelModifications;
        private Label labelName;
        private Label labelKey;
        private PictureBox pictureThumbnail;
        private EntitiesListControl entitiesListTaskForces;
    }
}
