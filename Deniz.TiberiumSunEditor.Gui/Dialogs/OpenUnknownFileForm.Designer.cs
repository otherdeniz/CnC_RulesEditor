namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class OpenUnknownFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenUnknownFileForm));
            LabelFilename = new Label();
            label1 = new Label();
            comboBoxGameType = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            comboBoxFileType = new ComboBox();
            panelBottom = new Panel();
            panelOkCancel = new Panel();
            buttonOk = new Button();
            buttonCancel = new Button();
            panelBottom.SuspendLayout();
            panelOkCancel.SuspendLayout();
            SuspendLayout();
            // 
            // LabelFilename
            // 
            LabelFilename.AutoSize = true;
            LabelFilename.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LabelFilename.Location = new Point(138, 15);
            LabelFilename.Name = "LabelFilename";
            LabelFilename.Size = new Size(65, 15);
            LabelFilename.TabIndex = 9;
            LabelFilename.Text = "[Filename]";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 15);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 7;
            label1.Text = "Name:";
            // 
            // comboBoxGameType
            // 
            comboBoxGameType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxGameType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGameType.FormattingEnabled = true;
            comboBoxGameType.Location = new Point(138, 70);
            comboBoxGameType.Name = "comboBoxGameType";
            comboBoxGameType.Size = new Size(355, 23);
            comboBoxGameType.TabIndex = 1;
            comboBoxGameType.SelectedIndexChanged += comboBoxGameType_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 73);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 7;
            label2.Text = "Game Type:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 44);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 7;
            label3.Text = "File Type:";
            // 
            // comboBoxFileType
            // 
            comboBoxFileType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFileType.FormattingEnabled = true;
            comboBoxFileType.Location = new Point(138, 41);
            comboBoxFileType.Name = "comboBoxFileType";
            comboBoxFileType.Size = new Size(155, 23);
            comboBoxFileType.TabIndex = 0;
            comboBoxFileType.SelectedIndexChanged += comboBoxFileType_SelectedIndexChanged;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(panelOkCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(8, 104);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(488, 35);
            panelBottom.TabIndex = 10;
            // 
            // panelOkCancel
            // 
            panelOkCancel.Controls.Add(buttonOk);
            panelOkCancel.Controls.Add(buttonCancel);
            panelOkCancel.Dock = DockStyle.Right;
            panelOkCancel.Location = new Point(280, 0);
            panelOkCancel.Name = "panelOkCancel";
            panelOkCancel.Size = new Size(208, 35);
            panelOkCancel.TabIndex = 2;
            // 
            // buttonOk
            // 
            buttonOk.Enabled = false;
            buttonOk.Image = (Image)resources.GetObject("buttonOk.Image");
            buttonOk.Location = new Point(19, 5);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(90, 26);
            buttonOk.TabIndex = 0;
            buttonOk.Text = "Ok";
            buttonOk.TextAlign = ContentAlignment.MiddleRight;
            buttonOk.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Image = (Image)resources.GetObject("buttonCancel.Image");
            buttonCancel.Location = new Point(115, 5);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.MiddleRight;
            buttonCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // OpenUnknownFileForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 147);
            ControlBox = false;
            Controls.Add(panelBottom);
            Controls.Add(comboBoxFileType);
            Controls.Add(comboBoxGameType);
            Controls.Add(label3);
            Controls.Add(LabelFilename);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "OpenUnknownFileForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Open File";
            Load += OpenMapForm_Load;
            panelBottom.ResumeLayout(false);
            panelOkCancel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public Label LabelFilename;
        private Label label1;
        private ComboBox comboBoxGameType;
        private Label label2;
        private Label label3;
        private ComboBox comboBoxFileType;
        private Panel panelBottom;
        private Panel panelOkCancel;
        private Button buttonOk;
        private Button buttonCancel;
    }
}