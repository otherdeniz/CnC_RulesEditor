namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    partial class AiScriptEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiScriptEditControl));
            panelTop = new Panel();
            buttonRefreshName = new Button();
            panelButtons = new Panel();
            ButtonDelete = new Button();
            ButtonCopy = new Button();
            textName = new TextBox();
            labelKey = new Label();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(buttonRefreshName);
            panelTop.Controls.Add(panelButtons);
            panelTop.Controls.Add(textName);
            panelTop.Controls.Add(labelKey);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(774, 39);
            panelTop.TabIndex = 2;
            // 
            // buttonRefreshName
            // 
            buttonRefreshName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRefreshName.Image = (Image)resources.GetObject("buttonRefreshName.Image");
            buttonRefreshName.Location = new Point(555, 5);
            buttonRefreshName.Name = "buttonRefreshName";
            buttonRefreshName.Size = new Size(27, 27);
            buttonRefreshName.TabIndex = 9;
            buttonRefreshName.TextAlign = ContentAlignment.MiddleRight;
            buttonRefreshName.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRefreshName.UseVisualStyleBackColor = true;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(ButtonDelete);
            panelButtons.Controls.Add(ButtonCopy);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(594, 0);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(180, 39);
            panelButtons.TabIndex = 8;
            // 
            // ButtonDelete
            // 
            ButtonDelete.Image = (Image)resources.GetObject("ButtonDelete.Image");
            ButtonDelete.Location = new Point(92, 5);
            ButtonDelete.Name = "ButtonDelete";
            ButtonDelete.Size = new Size(76, 27);
            ButtonDelete.TabIndex = 6;
            ButtonDelete.Text = "Delete";
            ButtonDelete.TextAlign = ContentAlignment.MiddleRight;
            ButtonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonDelete.UseVisualStyleBackColor = true;
            // 
            // ButtonCopy
            // 
            ButtonCopy.Image = (Image)resources.GetObject("ButtonCopy.Image");
            ButtonCopy.Location = new Point(16, 5);
            ButtonCopy.Name = "ButtonCopy";
            ButtonCopy.Size = new Size(76, 27);
            ButtonCopy.TabIndex = 7;
            ButtonCopy.Text = "Copy";
            ButtonCopy.TextAlign = ContentAlignment.MiddleRight;
            ButtonCopy.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonCopy.UseVisualStyleBackColor = true;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(92, 7);
            textName.Name = "textName";
            textName.Size = new Size(463, 23);
            textName.TabIndex = 1;
            textName.TextChanged += textName_TextChanged;
            // 
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.Location = new Point(3, 10);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(68, 15);
            labelKey.TabIndex = 0;
            labelKey.Text = "01234567-G";
            // 
            // AiScriptEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelTop);
            Name = "AiScriptEditControl";
            Size = new Size(774, 529);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button buttonRefreshName;
        private Panel panelButtons;
        private Button ButtonDelete;
        private Button ButtonCopy;
        private TextBox textName;
        private Label labelKey;
    }
}
