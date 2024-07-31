namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class CreateCopyForm
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
            label1 = new Label();
            LabelKey = new Label();
            TextNewKey = new TextBox();
            label3 = new Label();
            TextNewName = new TextBox();
            label4 = new Label();
            buttonCancel = new Button();
            buttonOk = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 0;
            label1.Text = "Create copy of:";
            // 
            // LabelKey
            // 
            LabelKey.AutoSize = true;
            LabelKey.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LabelKey.Location = new Point(130, 9);
            LabelKey.Name = "LabelKey";
            LabelKey.Size = new Size(28, 15);
            LabelKey.TabIndex = 1;
            LabelKey.Text = "KEY";
            // 
            // TextNewKey
            // 
            TextNewKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextNewKey.Location = new Point(130, 38);
            TextNewKey.Name = "TextNewKey";
            TextNewKey.Size = new Size(211, 23);
            TextNewKey.TabIndex = 0;
            TextNewKey.TextChanged += TextNewKey_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 41);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 0;
            label3.Text = "New unique key:";
            // 
            // TextNewName
            // 
            TextNewName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextNewName.Location = new Point(130, 67);
            TextNewName.Name = "TextNewName";
            TextNewName.Size = new Size(211, 23);
            TextNewName.TabIndex = 1;
            TextNewName.TextChanged += TextNewName_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 70);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 0;
            label4.Text = "New name:";
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(266, 121);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Enabled = false;
            buttonOk.Location = new Point(185, 121);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 23);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // CreateCopyForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(364, 159);
            ControlBox = false;
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Controls.Add(TextNewName);
            Controls.Add(TextNewKey);
            Controls.Add(LabelKey);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "CreateCopyForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create copy";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label4;
        private Button buttonCancel;
        private Button buttonOk;
        public Label LabelKey;
        public TextBox TextNewKey;
        public TextBox TextNewName;
    }
}