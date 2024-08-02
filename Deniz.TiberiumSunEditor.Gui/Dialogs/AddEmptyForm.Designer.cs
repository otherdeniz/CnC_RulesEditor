namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class AddEmptyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEmptyForm));
            buttonOk = new Button();
            buttonCancel = new Button();
            TextNewKey = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Enabled = false;
            buttonOk.Location = new Point(188, 54);
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
            buttonCancel.Location = new Point(269, 54);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 11;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // TextNewKey
            // 
            TextNewKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextNewKey.Location = new Point(133, 12);
            TextNewKey.Name = "TextNewKey";
            TextNewKey.Size = new Size(211, 23);
            TextNewKey.TabIndex = 0;
            TextNewKey.TextChanged += TextNewKey_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 15);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 6;
            label3.Text = "New unique key:";
            // 
            // AddEmptyForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(364, 89);
            ControlBox = false;
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Controls.Add(TextNewKey);
            Controls.Add(label3);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddEmptyForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add new item";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOk;
        private Button buttonCancel;
        public TextBox TextNewKey;
        private Label label3;
    }
}