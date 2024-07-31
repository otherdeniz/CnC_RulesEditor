namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class GameSettingCustomModControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettingCustomModControl));
            label2 = new Label();
            buttonEdit = new Button();
            labelPath = new Label();
            label1 = new Label();
            labelGame = new Label();
            pictureGameIcon = new PictureBox();
            buttonRemove = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureGameIcon).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Blue;
            label2.Location = new Point(531, 3);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 11;
            label2.Text = "CUSTOM MOD";
            // 
            // buttonEdit
            // 
            buttonEdit.Image = (Image)resources.GetObject("buttonEdit.Image");
            buttonEdit.Location = new Point(109, 22);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(24, 24);
            buttonEdit.TabIndex = 10;
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // labelPath
            // 
            labelPath.AutoSize = true;
            labelPath.Location = new Point(139, 27);
            labelPath.Name = "labelPath";
            labelPath.Size = new Size(76, 15);
            labelPath.TabIndex = 9;
            labelPath.Text = "(not defined)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 27);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 8;
            label1.Text = "Install Path:";
            // 
            // labelGame
            // 
            labelGame.AutoSize = true;
            labelGame.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelGame.Location = new Point(31, 4);
            labelGame.Name = "labelGame";
            labelGame.Size = new Size(220, 15);
            labelGame.TabIndex = 7;
            labelGame.Text = "Game Game Game Game Game Game";
            // 
            // pictureGameIcon
            // 
            pictureGameIcon.Location = new Point(3, 3);
            pictureGameIcon.Name = "pictureGameIcon";
            pictureGameIcon.Size = new Size(24, 24);
            pictureGameIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureGameIcon.TabIndex = 6;
            pictureGameIcon.TabStop = false;
            // 
            // buttonRemove
            // 
            buttonRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRemove.Image = (Image)resources.GetObject("buttonRemove.Image");
            buttonRemove.Location = new Point(531, 22);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(89, 24);
            buttonRemove.TabIndex = 10;
            buttonRemove.Text = "Remove";
            buttonRemove.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // GameSettingCustomModControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(label2);
            Controls.Add(buttonRemove);
            Controls.Add(buttonEdit);
            Controls.Add(labelPath);
            Controls.Add(label1);
            Controls.Add(labelGame);
            Controls.Add(pictureGameIcon);
            Name = "GameSettingCustomModControl";
            Size = new Size(623, 48);
            ((System.ComponentModel.ISupportInitialize)pictureGameIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Button buttonEdit;
        private Label labelPath;
        private Label label1;
        private Label labelGame;
        private PictureBox pictureGameIcon;
        private Button buttonRemove;
    }
}
