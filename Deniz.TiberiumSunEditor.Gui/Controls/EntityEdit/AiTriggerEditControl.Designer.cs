namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    partial class AiTriggerEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiTriggerEditControl));
            panelTop = new Panel();
            buttonRefreshName = new Button();
            panelButtons = new Panel();
            ButtonDelete = new Button();
            ButtonCopy = new Button();
            textName = new TextBox();
            labelKey = new Label();
            unitEdit = new UnitEditControl();
            panelName = new Panel();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            panelName.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(panelName);
            panelTop.Controls.Add(panelButtons);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(647, 39);
            panelTop.TabIndex = 2;
            // 
            // buttonRefreshName
            // 
            buttonRefreshName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRefreshName.Image = (Image)resources.GetObject("buttonRefreshName.Image");
            buttonRefreshName.Location = new Point(432, 6);
            buttonRefreshName.Name = "buttonRefreshName";
            buttonRefreshName.Size = new Size(27, 27);
            buttonRefreshName.TabIndex = 9;
            buttonRefreshName.TextAlign = ContentAlignment.MiddleRight;
            buttonRefreshName.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRefreshName.UseVisualStyleBackColor = true;
            buttonRefreshName.Click += buttonRefreshName_Click;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(ButtonDelete);
            panelButtons.Controls.Add(ButtonCopy);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(467, 0);
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
            ButtonDelete.Click += ButtonDelete_Click;
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
            ButtonCopy.Visible = false;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(92, 8);
            textName.Name = "textName";
            textName.Size = new Size(340, 23);
            textName.TabIndex = 1;
            textName.TextChanged += textName_TextChanged;
            // 
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.Location = new Point(3, 11);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(68, 15);
            labelKey.TabIndex = 0;
            labelKey.Text = "01234567-G";
            // 
            // unitEdit
            // 
            unitEdit.BackColor = Color.White;
            unitEdit.Dock = DockStyle.Fill;
            unitEdit.Location = new Point(0, 39);
            unitEdit.Margin = new Padding(4, 3, 4, 3);
            unitEdit.Name = "unitEdit";
            unitEdit.ShowHeaderAndFooter = false;
            unitEdit.Size = new Size(647, 461);
            unitEdit.TabIndex = 4;
            unitEdit.Tag = "PLAIN";
            unitEdit.UseValueNameColumn = true;
            unitEdit.KeyValueChanged += unitEdit_KeyValueChanged;
            // 
            // panelName
            // 
            panelName.Controls.Add(buttonRefreshName);
            panelName.Controls.Add(textName);
            panelName.Controls.Add(labelKey);
            panelName.Dock = DockStyle.Fill;
            panelName.Location = new Point(0, 0);
            panelName.Name = "panelName";
            panelName.Size = new Size(467, 39);
            panelName.TabIndex = 10;
            // 
            // AiTriggerEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(unitEdit);
            Controls.Add(panelTop);
            Name = "AiTriggerEditControl";
            Size = new Size(647, 500);
            panelTop.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            panelName.ResumeLayout(false);
            panelName.PerformLayout();
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
        private UnitEditControl unitEdit;
        private Panel panelName;
    }
}
