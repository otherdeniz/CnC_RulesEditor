namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class MixEditorForm
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
            panelTop = new Panel();
            labelFilePath = new Label();
            buttonNewEmpty = new Button();
            buttonOpenMix = new Button();
            listViewEntries = new ListView();
            columnFileName = new ColumnHeader();
            columnSize = new ColumnHeader();
            panelBottom = new Panel();
            labelFileCount = new Label();
            buttonCancel = new Button();
            buttonSave = new Button();
            buttonRemove = new Button();
            buttonAdd = new Button();
            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            //
            // panelTop
            //
            panelTop.Controls.Add(labelFilePath);
            panelTop.Controls.Add(buttonNewEmpty);
            panelTop.Controls.Add(buttonOpenMix);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(8, 8);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(584, 60);
            panelTop.TabIndex = 0;
            //
            // buttonOpenMix
            //
            buttonOpenMix.Location = new Point(0, 4);
            buttonOpenMix.Name = "buttonOpenMix";
            buttonOpenMix.Size = new Size(120, 25);
            buttonOpenMix.TabIndex = 0;
            buttonOpenMix.Text = "Open Mix File...";
            buttonOpenMix.UseVisualStyleBackColor = true;
            buttonOpenMix.Click += buttonOpenMix_Click;
            //
            // buttonNewEmpty
            //
            buttonNewEmpty.Location = new Point(126, 4);
            buttonNewEmpty.Name = "buttonNewEmpty";
            buttonNewEmpty.Size = new Size(110, 25);
            buttonNewEmpty.TabIndex = 1;
            buttonNewEmpty.Text = "New Empty Mix";
            buttonNewEmpty.UseVisualStyleBackColor = true;
            buttonNewEmpty.Click += buttonNewEmpty_Click;
            //
            // labelFilePath
            //
            labelFilePath.AutoSize = true;
            labelFilePath.Location = new Point(0, 36);
            labelFilePath.Name = "labelFilePath";
            labelFilePath.Size = new Size(93, 15);
            labelFilePath.TabIndex = 2;
            labelFilePath.Text = "(New MIX File)";
            //
            // listViewEntries
            //
            listViewEntries.Columns.AddRange(new ColumnHeader[] { columnFileName, columnSize });
            listViewEntries.Dock = DockStyle.Fill;
            listViewEntries.FullRowSelect = true;
            listViewEntries.GridLines = true;
            listViewEntries.Location = new Point(8, 68);
            listViewEntries.Name = "listViewEntries";
            listViewEntries.Size = new Size(584, 327);
            listViewEntries.TabIndex = 1;
            listViewEntries.UseCompatibleStateImageBehavior = false;
            listViewEntries.View = View.Details;
            listViewEntries.SelectedIndexChanged += listViewEntries_SelectedIndexChanged;
            //
            // columnFileName
            //
            columnFileName.Text = "File Name";
            columnFileName.Width = 400;
            //
            // columnSize
            //
            columnSize.Text = "Size";
            columnSize.Width = 150;
            //
            // panelBottom
            //
            panelBottom.Controls.Add(labelFileCount);
            panelBottom.Controls.Add(buttonCancel);
            panelBottom.Controls.Add(buttonSave);
            panelBottom.Controls.Add(buttonRemove);
            panelBottom.Controls.Add(buttonAdd);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(8, 395);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(584, 45);
            panelBottom.TabIndex = 2;
            //
            // buttonAdd
            //
            buttonAdd.Location = new Point(0, 12);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(110, 25);
            buttonAdd.TabIndex = 0;
            buttonAdd.Text = "Add from File...";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            //
            // buttonRemove
            //
            buttonRemove.Enabled = false;
            buttonRemove.Location = new Point(116, 12);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(115, 25);
            buttonRemove.TabIndex = 1;
            buttonRemove.Text = "Remove Selected";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            //
            // labelFileCount
            //
            labelFileCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelFileCount.Location = new Point(290, 16);
            labelFileCount.Name = "labelFileCount";
            labelFileCount.Size = new Size(80, 15);
            labelFileCount.TabIndex = 2;
            labelFileCount.Text = "0 file(s)";
            labelFileCount.TextAlign = ContentAlignment.MiddleRight;
            //
            // buttonSave
            //
            buttonSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSave.Enabled = false;
            buttonSave.Location = new Point(376, 12);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(110, 25);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save Mix File...";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            //
            // buttonCancel
            //
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.Location = new Point(492, 12);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 25);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            //
            // MixEditorForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 448);
            Controls.Add(listViewEntries);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Name = "MixEditorForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MIX File Editor";
            Load += MixEditorForm_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label labelFilePath;
        private Button buttonNewEmpty;
        private Button buttonOpenMix;
        private ListView listViewEntries;
        private ColumnHeader columnFileName;
        private ColumnHeader columnSize;
        private Panel panelBottom;
        private Label labelFileCount;
        private Button buttonCancel;
        private Button buttonSave;
        private Button buttonRemove;
        private Button buttonAdd;
    }
}
