namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class NewCustomModForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewCustomModForm));
            panelBottom = new Panel();
            buttonOk = new Button();
            buttonCancel = new Button();
            label1 = new Label();
            textName = new TextBox();
            comboBoxGameType = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            textGamePath = new TextBox();
            buttonPath = new Button();
            label4 = new Label();
            pictureBoxIcon = new PictureBox();
            buttonIcon = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            openFileIcon = new OpenFileDialog();
            label5 = new Label();
            comboBoxRulesIni = new ComboBox();
            checkBoxAres = new CheckBox();
            checkBoxPhobos = new CheckBox();
            label6 = new Label();
            label7 = new Label();
            comboBoxArtIni = new ComboBox();
            checkBoxVinifera = new CheckBox();
            label8 = new Label();
            checkBoxXnaSectionInheritance = new CheckBox();
            checkBoxPhobosSectionInheritance = new CheckBox();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            SuspendLayout();
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonOk);
            panelBottom.Controls.Add(buttonCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 301);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(527, 43);
            panelBottom.TabIndex = 10;
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOk.Enabled = false;
            buttonOk.Image = (Image)resources.GetObject("buttonOk.Image");
            buttonOk.Location = new Point(329, 8);
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
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.Image = (Image)resources.GetObject("buttonCancel.Image");
            buttonCancel.Location = new Point(425, 8);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.MiddleRight;
            buttonCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 3;
            label1.Text = "Name:";
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(121, 12);
            textName.Name = "textName";
            textName.Size = new Size(394, 23);
            textName.TabIndex = 0;
            textName.TextChanged += textName_TextChanged;
            // 
            // comboBoxGameType
            // 
            comboBoxGameType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxGameType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGameType.FormattingEnabled = true;
            comboBoxGameType.Location = new Point(121, 71);
            comboBoxGameType.Name = "comboBoxGameType";
            comboBoxGameType.Size = new Size(394, 23);
            comboBoxGameType.TabIndex = 2;
            comboBoxGameType.SelectedIndexChanged += comboBoxGameType_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 74);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 13;
            label2.Text = "Base Type:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 44);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 3;
            label3.Text = "Game Path:";
            // 
            // textGamePath
            // 
            textGamePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textGamePath.Location = new Point(121, 41);
            textGamePath.Name = "textGamePath";
            textGamePath.ReadOnly = true;
            textGamePath.Size = new Size(370, 23);
            textGamePath.TabIndex = 0;
            textGamePath.TabStop = false;
            // 
            // buttonPath
            // 
            buttonPath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonPath.Image = (Image)resources.GetObject("buttonPath.Image");
            buttonPath.Location = new Point(491, 41);
            buttonPath.Name = "buttonPath";
            buttonPath.Size = new Size(24, 24);
            buttonPath.TabIndex = 1;
            buttonPath.TabStop = false;
            buttonPath.UseVisualStyleBackColor = true;
            buttonPath.Click += buttonPath_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 104);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 13;
            label4.Text = "Icon:";
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxIcon.Location = new Point(121, 100);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(26, 26);
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxIcon.TabIndex = 14;
            pictureBoxIcon.TabStop = false;
            // 
            // buttonIcon
            // 
            buttonIcon.Image = (Image)resources.GetObject("buttonIcon.Image");
            buttonIcon.Location = new Point(151, 101);
            buttonIcon.Name = "buttonIcon";
            buttonIcon.Size = new Size(24, 24);
            buttonIcon.TabIndex = 3;
            buttonIcon.TabStop = false;
            buttonIcon.UseVisualStyleBackColor = true;
            buttonIcon.Click += buttonIcon_Click;
            // 
            // folderBrowserDialog
            // 
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 135);
            label5.Name = "label5";
            label5.Size = new Size(92, 15);
            label5.TabIndex = 13;
            label5.Text = "Default rules.ini:";
            // 
            // comboBoxRulesIni
            // 
            comboBoxRulesIni.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxRulesIni.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRulesIni.FormattingEnabled = true;
            comboBoxRulesIni.Location = new Point(121, 132);
            comboBoxRulesIni.Name = "comboBoxRulesIni";
            comboBoxRulesIni.Size = new Size(394, 23);
            comboBoxRulesIni.TabIndex = 4;
            comboBoxRulesIni.SelectedIndexChanged += comboBoxRulesIni_SelectedIndexChanged;
            // 
            // checkBoxAres
            // 
            checkBoxAres.AutoSize = true;
            checkBoxAres.Location = new Point(121, 193);
            checkBoxAres.Name = "checkBoxAres";
            checkBoxAres.Size = new Size(93, 19);
            checkBoxAres.TabIndex = 6;
            checkBoxAres.Text = "Ares support";
            checkBoxAres.UseVisualStyleBackColor = true;
            // 
            // checkBoxPhobos
            // 
            checkBoxPhobos.AutoSize = true;
            checkBoxPhobos.Location = new Point(121, 218);
            checkBoxPhobos.Name = "checkBoxPhobos";
            checkBoxPhobos.Size = new Size(110, 19);
            checkBoxPhobos.TabIndex = 7;
            checkBoxPhobos.Text = "Phobos support";
            checkBoxPhobos.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 194);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 13;
            label6.Text = "Mod Modules:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 164);
            label7.Name = "label7";
            label7.Size = new Size(81, 15);
            label7.TabIndex = 13;
            label7.Text = "Default art.ini:";
            // 
            // comboBoxArtIni
            // 
            comboBoxArtIni.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxArtIni.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxArtIni.FormattingEnabled = true;
            comboBoxArtIni.Location = new Point(121, 161);
            comboBoxArtIni.Name = "comboBoxArtIni";
            comboBoxArtIni.Size = new Size(394, 23);
            comboBoxArtIni.TabIndex = 5;
            comboBoxArtIni.SelectedIndexChanged += comboBoxArtIni_SelectedIndexChanged;
            // 
            // checkBoxVinifera
            // 
            checkBoxVinifera.AutoSize = true;
            checkBoxVinifera.Location = new Point(121, 243);
            checkBoxVinifera.Name = "checkBoxVinifera";
            checkBoxVinifera.Size = new Size(110, 19);
            checkBoxVinifera.TabIndex = 8;
            checkBoxVinifera.Text = "Vinifera support";
            checkBoxVinifera.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 269);
            label8.Name = "label8";
            label8.Size = new Size(93, 15);
            label8.TabIndex = 13;
            label8.Text = "Engine Features:";
            // 
            // checkBoxXnaSectionInheritance
            // 
            checkBoxXnaSectionInheritance.AutoSize = true;
            checkBoxXnaSectionInheritance.Location = new Point(121, 268);
            checkBoxXnaSectionInheritance.Name = "checkBoxXnaSectionInheritance";
            checkBoxXnaSectionInheritance.Size = new Size(285, 19);
            checkBoxXnaSectionInheritance.TabIndex = 9;
            checkBoxXnaSectionInheritance.Text = "XNAClient Section Inheritance ('BaseSection' tag)";
            checkBoxXnaSectionInheritance.UseVisualStyleBackColor = true;
            // 
            // checkBoxPhobosSectionInheritance
            // 
            checkBoxPhobosSectionInheritance.AutoSize = true;
            checkBoxPhobosSectionInheritance.Location = new Point(237, 218);
            checkBoxPhobosSectionInheritance.Name = "checkBoxPhobosSectionInheritance";
            checkBoxPhobosSectionInheritance.Size = new Size(252, 19);
            checkBoxPhobosSectionInheritance.TabIndex = 7;
            checkBoxPhobosSectionInheritance.Text = "Phobos Section inheritance ('$Inherits' tag)";
            checkBoxPhobosSectionInheritance.UseVisualStyleBackColor = true;
            // 
            // NewCustomModForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(527, 344);
            Controls.Add(checkBoxVinifera);
            Controls.Add(checkBoxPhobosSectionInheritance);
            Controls.Add(checkBoxPhobos);
            Controls.Add(checkBoxXnaSectionInheritance);
            Controls.Add(checkBoxAres);
            Controls.Add(pictureBoxIcon);
            Controls.Add(buttonIcon);
            Controls.Add(buttonPath);
            Controls.Add(comboBoxArtIni);
            Controls.Add(comboBoxRulesIni);
            Controls.Add(comboBoxGameType);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textGamePath);
            Controls.Add(label3);
            Controls.Add(textName);
            Controls.Add(label1);
            Controls.Add(panelBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "NewCustomModForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Custom Mod";
            Load += NewCustomModForm_Load;
            panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelBottom;
        private Button buttonOk;
        private Button buttonCancel;
        private Label label1;
        private TextBox textName;
        private ComboBox comboBoxGameType;
        private Label label2;
        private Label label3;
        private TextBox textGamePath;
        private Button buttonPath;
        private Label label4;
        private PictureBox pictureBoxIcon;
        private Button buttonIcon;
        private FolderBrowserDialog folderBrowserDialog;
        private OpenFileDialog openFileIcon;
        private Label label5;
        private ComboBox comboBoxRulesIni;
        private CheckBox checkBoxAres;
        private CheckBox checkBoxPhobos;
        private Label label6;
        private Label label7;
        private ComboBox comboBoxArtIni;
        private CheckBox checkBoxVinifera;
        private Label label8;
        private CheckBox checkBoxXnaSectionInheritance;
        private CheckBox checkBoxPhobosSectionInheritance;
    }
}