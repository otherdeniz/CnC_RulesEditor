namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class OpenCompareFilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenCompareFilesForm));
            panelBottom = new Panel();
            panelOkCancel = new Panel();
            buttonOk = new Button();
            buttonCancel = new Button();
            panel1 = new Panel();
            label2 = new Label();
            labelFileType = new Label();
            comboBoxBaseGame = new ComboBox();
            comboBoxFileType = new ComboBox();
            groupBox1 = new GroupBox();
            buttonFile1 = new Button();
            textFile1Path = new TextBox();
            label3 = new Label();
            groupBox2 = new GroupBox();
            buttonFile2 = new Button();
            textFile2Path = new TextBox();
            label1 = new Label();
            openFileDialog = new OpenFileDialog();
            panelBottom.SuspendLayout();
            panelOkCancel.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(panelOkCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(8, 200);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(466, 35);
            panelBottom.TabIndex = 3;
            // 
            // panelOkCancel
            // 
            panelOkCancel.Controls.Add(buttonOk);
            panelOkCancel.Controls.Add(buttonCancel);
            panelOkCancel.Dock = DockStyle.Right;
            panelOkCancel.Location = new Point(258, 0);
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
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(labelFileType);
            panel1.Controls.Add(comboBoxBaseGame);
            panel1.Controls.Add(comboBoxFileType);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(8, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(466, 63);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 35);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 1;
            label2.Text = "Game Type:";
            // 
            // labelFileType
            // 
            labelFileType.AutoSize = true;
            labelFileType.Location = new Point(7, 6);
            labelFileType.Name = "labelFileType";
            labelFileType.Size = new Size(55, 15);
            labelFileType.TabIndex = 1;
            labelFileType.Text = "File Type:";
            // 
            // comboBoxBaseGame
            // 
            comboBoxBaseGame.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBaseGame.FormattingEnabled = true;
            comboBoxBaseGame.Location = new Point(89, 32);
            comboBoxBaseGame.Name = "comboBoxBaseGame";
            comboBoxBaseGame.Size = new Size(226, 23);
            comboBoxBaseGame.TabIndex = 1;
            comboBoxBaseGame.SelectedIndexChanged += comboBoxBaseGame_SelectedIndexChanged;
            // 
            // comboBoxFileType
            // 
            comboBoxFileType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFileType.FormattingEnabled = true;
            comboBoxFileType.Location = new Point(89, 3);
            comboBoxFileType.Name = "comboBoxFileType";
            comboBoxFileType.Size = new Size(226, 23);
            comboBoxFileType.TabIndex = 0;
            comboBoxFileType.SelectedIndexChanged += comboBoxFileType_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonFile1);
            groupBox1.Controls.Add(textFile1Path);
            groupBox1.Controls.Add(label3);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(8, 71);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(466, 60);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Base File (used as default values)";
            // 
            // buttonFile1
            // 
            buttonFile1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonFile1.Image = (Image)resources.GetObject("buttonFile1.Image");
            buttonFile1.Location = new Point(431, 22);
            buttonFile1.Name = "buttonFile1";
            buttonFile1.Size = new Size(24, 24);
            buttonFile1.TabIndex = 5;
            buttonFile1.TabStop = false;
            buttonFile1.UseVisualStyleBackColor = true;
            buttonFile1.Click += buttonFile1_Click;
            // 
            // textFile1Path
            // 
            textFile1Path.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textFile1Path.Location = new Point(89, 22);
            textFile1Path.Name = "textFile1Path";
            textFile1Path.ReadOnly = true;
            textFile1Path.Size = new Size(342, 23);
            textFile1Path.TabIndex = 4;
            textFile1Path.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 25);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 6;
            label3.Text = "ini File:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonFile2);
            groupBox2.Controls.Add(textFile2Path);
            groupBox2.Controls.Add(label1);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(8, 131);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(466, 60);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Compare with (show changes to base file)";
            // 
            // buttonFile2
            // 
            buttonFile2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonFile2.Image = (Image)resources.GetObject("buttonFile2.Image");
            buttonFile2.Location = new Point(431, 21);
            buttonFile2.Name = "buttonFile2";
            buttonFile2.Size = new Size(24, 24);
            buttonFile2.TabIndex = 5;
            buttonFile2.TabStop = false;
            buttonFile2.UseVisualStyleBackColor = true;
            buttonFile2.Click += buttonFile2_Click;
            // 
            // textFile2Path
            // 
            textFile2Path.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textFile2Path.Location = new Point(89, 22);
            textFile2Path.Name = "textFile2Path";
            textFile2Path.ReadOnly = true;
            textFile2Path.Size = new Size(342, 23);
            textFile2Path.TabIndex = 4;
            textFile2Path.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 25);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 6;
            label1.Text = "ini File:";
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "Supported Files|*.ini;*.mpr;*.map|All Files|*.*";
            // 
            // OpenCompareFilesForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(482, 243);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(panelBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OpenCompareFilesForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Compare Files";
            Load += OpenCompareFilesForm_Load;
            panelBottom.ResumeLayout(false);
            panelOkCancel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBottom;
        private Button buttonOk;
        private Button buttonCancel;
        private Panel panelOkCancel;
        private Panel panel1;
        private Label label2;
        private Label labelFileType;
        private ComboBox comboBoxBaseGame;
        private ComboBox comboBoxFileType;
        private GroupBox groupBox1;
        private Button buttonFile1;
        private TextBox textFile1Path;
        private Label label3;
        private GroupBox groupBox2;
        private Button buttonFile2;
        private TextBox textFile2Path;
        private Label label1;
        private OpenFileDialog openFileDialog;
    }
}