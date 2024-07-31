namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class GameSettingIntegratedControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettingIntegratedControl));
            pictureGameIcon = new PictureBox();
            labelGame = new Label();
            label1 = new Label();
            labelPath = new Label();
            buttonPath = new Button();
            label2 = new Label();
            folderBrowserDialog = new FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)pictureGameIcon).BeginInit();
            SuspendLayout();
            // 
            // pictureGameIcon
            // 
            pictureGameIcon.Location = new Point(3, 3);
            pictureGameIcon.Name = "pictureGameIcon";
            pictureGameIcon.Size = new Size(24, 24);
            pictureGameIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureGameIcon.TabIndex = 0;
            pictureGameIcon.TabStop = false;
            // 
            // labelGame
            // 
            labelGame.AutoSize = true;
            labelGame.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelGame.Location = new Point(31, 4);
            labelGame.Name = "labelGame";
            labelGame.Size = new Size(220, 15);
            labelGame.TabIndex = 1;
            labelGame.Text = "Game Game Game Game Game Game";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 27);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 2;
            label1.Text = "Install Path:";
            // 
            // labelPath
            // 
            labelPath.AutoSize = true;
            labelPath.Location = new Point(139, 27);
            labelPath.Name = "labelPath";
            labelPath.Size = new Size(76, 15);
            labelPath.TabIndex = 3;
            labelPath.Text = "(not defined)";
            // 
            // buttonPath
            // 
            buttonPath.Image = (Image)resources.GetObject("buttonPath.Image");
            buttonPath.Location = new Point(109, 22);
            buttonPath.Name = "buttonPath";
            buttonPath.Size = new Size(24, 24);
            buttonPath.TabIndex = 4;
            buttonPath.UseVisualStyleBackColor = true;
            buttonPath.Click += buttonPath_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(501, 4);
            label2.Name = "label2";
            label2.Size = new Size(116, 15);
            label2.TabIndex = 5;
            label2.Text = "INTEGRATED GAME";
            // 
            // folderBrowserDialog
            // 
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            // 
            // GameSettingIntegratedControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(label2);
            Controls.Add(buttonPath);
            Controls.Add(labelPath);
            Controls.Add(label1);
            Controls.Add(labelGame);
            Controls.Add(pictureGameIcon);
            Name = "GameSettingIntegratedControl";
            Size = new Size(623, 48);
            ((System.ComponentModel.ISupportInitialize)pictureGameIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureGameIcon;
        private Label labelGame;
        private Label label1;
        private Label labelPath;
        private Button buttonPath;
        private Label label2;
        private FolderBrowserDialog folderBrowserDialog;
    }
}
