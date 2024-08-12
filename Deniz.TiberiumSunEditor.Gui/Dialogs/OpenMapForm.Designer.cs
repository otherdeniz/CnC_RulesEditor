namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class OpenMapForm
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
            buttonOk = new Button();
            buttonCancel = new Button();
            LabelMap = new Label();
            label1 = new Label();
            comboBoxGameType = new ComboBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Enabled = false;
            buttonOk.Location = new Point(279, 82);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 23);
            buttonOk.TabIndex = 10;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(360, 82);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 11;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // LabelMap
            // 
            LabelMap.AutoSize = true;
            LabelMap.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LabelMap.Location = new Point(130, 9);
            LabelMap.Name = "LabelMap";
            LabelMap.Size = new Size(33, 15);
            LabelMap.TabIndex = 9;
            LabelMap.Text = "MAP";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
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
            comboBoxGameType.Location = new Point(130, 36);
            comboBoxGameType.Name = "comboBoxGameType";
            comboBoxGameType.Size = new Size(305, 23);
            comboBoxGameType.TabIndex = 12;
            comboBoxGameType.SelectedIndexChanged += comboBoxGameType_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 39);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 7;
            label2.Text = "Game Type:";
            // 
            // OpenMapForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(458, 120);
            ControlBox = false;
            Controls.Add(comboBoxGameType);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Controls.Add(LabelMap);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "OpenMapForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Open File";
            Load += OpenMapForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOk;
        private Button buttonCancel;
        public Label LabelMap;
        private Label label1;
        private ComboBox comboBoxGameType;
        private Label label2;
    }
}