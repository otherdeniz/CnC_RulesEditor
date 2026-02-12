namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class RulesToArtFileAssignmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RulesToArtFileAssignmentForm));
            panelBottom = new Panel();
            panelOkCancel = new Panel();
            buttonOk = new Button();
            buttonCancel = new Button();
            radioButtonOriginal = new RadioButton();
            radioButtonCustom = new RadioButton();
            label1 = new Label();
            labelModify = new Label();
            label2 = new Label();
            panelBottom.SuspendLayout();
            panelOkCancel.SuspendLayout();
            SuspendLayout();
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(panelOkCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(8, 189);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(656, 35);
            panelBottom.TabIndex = 11;
            // 
            // panelOkCancel
            // 
            panelOkCancel.Controls.Add(buttonOk);
            panelOkCancel.Controls.Add(buttonCancel);
            panelOkCancel.Dock = DockStyle.Right;
            panelOkCancel.Location = new Point(448, 0);
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
            // 
            // radioButtonOriginal
            // 
            radioButtonOriginal.AutoSize = true;
            radioButtonOriginal.Location = new Point(11, 60);
            radioButtonOriginal.Name = "radioButtonOriginal";
            radioButtonOriginal.Size = new Size(147, 19);
            radioButtonOriginal.TabIndex = 12;
            radioButtonOriginal.TabStop = true;
            radioButtonOriginal.Text = "use Art-File of the mod";
            radioButtonOriginal.UseVisualStyleBackColor = true;
            // 
            // radioButtonCustom
            // 
            radioButtonCustom.AutoSize = true;
            radioButtonCustom.Location = new Point(11, 88);
            radioButtonCustom.Name = "radioButtonCustom";
            radioButtonCustom.Size = new Size(136, 19);
            radioButtonCustom.TabIndex = 12;
            radioButtonCustom.TabStop = true;
            radioButtonCustom.Text = "use CUSTOM Art-File";
            radioButtonCustom.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(8, 8);
            label1.Name = "label1";
            label1.Size = new Size(656, 49);
            label1.TabIndex = 13;
            label1.Text = resources.GetString("label1.Text");
            // 
            // labelModify
            // 
            labelModify.AutoSize = true;
            labelModify.Cursor = Cursors.Hand;
            labelModify.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            labelModify.ForeColor = Color.DarkBlue;
            labelModify.Location = new Point(30, 110);
            labelModify.Name = "labelModify";
            labelModify.Size = new Size(55, 15);
            labelModify.TabIndex = 16;
            labelModify.Text = "NOT SET";
            labelModify.Visible = false;
            labelModify.Click += labelModify_Click;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Bottom;
            label2.Location = new Point(8, 151);
            label2.Name = "label2";
            label2.Size = new Size(656, 38);
            label2.TabIndex = 17;
            label2.Text = "Changing this will effect imediatelly\r\nChanges on the Art-File at runtime will trigger an hot-reloaded of the Art-File";
            // 
            // RulesToArtFileAssignmentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(672, 232);
            ControlBox = false;
            Controls.Add(label2);
            Controls.Add(labelModify);
            Controls.Add(label1);
            Controls.Add(radioButtonCustom);
            Controls.Add(radioButtonOriginal);
            Controls.Add(panelBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "RulesToArtFileAssignmentForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Art-File assignment";
            panelBottom.ResumeLayout(false);
            panelOkCancel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelBottom;
        private Panel panelOkCancel;
        private Button buttonOk;
        private Button buttonCancel;
        private RadioButton radioButtonOriginal;
        private RadioButton radioButtonCustom;
        private Label label1;
        private Label labelModify;
        private Label label2;
    }
}