namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class InsertSnippetForm
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertSnippetForm));
            panelLeft = new Panel();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelLeftBottom = new Panel();
            buttonLoadFile = new Button();
            panelCenter = new Panel();
            rulesEdit = new Controls.RulesEditMainControl();
            panelBottom = new Panel();
            buttonOk = new Button();
            buttonCancel = new Button();
            splitterLeft = new Splitter();
            openFileDialog = new OpenFileDialog();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            panelLeftBottom.SuspendLayout();
            panelCenter.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(valuesGrid);
            panelLeft.Controls.Add(panelLeftBottom);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(233, 610);
            panelLeft.TabIndex = 0;
            // 
            // valuesGrid
            // 
            valuesGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand1.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand1.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            valuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            valuesGrid.DisplayLayout.GroupByBox.Hidden = true;
            valuesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            valuesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            valuesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            valuesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            valuesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            valuesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            valuesGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            valuesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            valuesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            valuesGrid.Dock = DockStyle.Fill;
            valuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            valuesGrid.Location = new Point(0, 0);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(233, 567);
            valuesGrid.TabIndex = 0;
            valuesGrid.AfterSelectChange += valuesGrid_AfterSelectChange;
            // 
            // panelLeftBottom
            // 
            panelLeftBottom.Controls.Add(buttonLoadFile);
            panelLeftBottom.Dock = DockStyle.Bottom;
            panelLeftBottom.Location = new Point(0, 567);
            panelLeftBottom.Name = "panelLeftBottom";
            panelLeftBottom.Size = new Size(233, 43);
            panelLeftBottom.TabIndex = 1;
            // 
            // buttonLoadFile
            // 
            buttonLoadFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoadFile.Image = (Image)resources.GetObject("buttonLoadFile.Image");
            buttonLoadFile.Location = new Point(9, 8);
            buttonLoadFile.Name = "buttonLoadFile";
            buttonLoadFile.Size = new Size(217, 26);
            buttonLoadFile.TabIndex = 1;
            buttonLoadFile.Text = "Load from file";
            buttonLoadFile.TextAlign = ContentAlignment.MiddleRight;
            buttonLoadFile.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonLoadFile.UseVisualStyleBackColor = true;
            buttonLoadFile.Click += buttonLoadFile_Click;
            // 
            // panelCenter
            // 
            panelCenter.Controls.Add(rulesEdit);
            panelCenter.Controls.Add(panelBottom);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(239, 0);
            panelCenter.Name = "panelCenter";
            panelCenter.Size = new Size(719, 610);
            panelCenter.TabIndex = 1;
            // 
            // rulesEdit
            // 
            rulesEdit.Dock = DockStyle.Fill;
            rulesEdit.Location = new Point(0, 0);
            rulesEdit.Margin = new Padding(4, 3, 4, 3);
            rulesEdit.Name = "rulesEdit";
            rulesEdit.ReadonlyMode = true;
            rulesEdit.Size = new Size(719, 567);
            rulesEdit.TabIndex = 0;
            rulesEdit.Visible = false;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonOk);
            panelBottom.Controls.Add(buttonCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 567);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(719, 43);
            panelBottom.TabIndex = 1;
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOk.Enabled = false;
            buttonOk.Image = (Image)resources.GetObject("buttonOk.Image");
            buttonOk.Location = new Point(521, 8);
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
            buttonCancel.Location = new Point(617, 8);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.MiddleRight;
            buttonCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // splitterLeft
            // 
            splitterLeft.BackColor = SystemColors.ActiveBorder;
            splitterLeft.Location = new Point(233, 0);
            splitterLeft.Margin = new Padding(4, 3, 4, 3);
            splitterLeft.Name = "splitterLeft";
            splitterLeft.Size = new Size(6, 610);
            splitterLeft.TabIndex = 3;
            splitterLeft.TabStop = false;
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "Supported Files|*.ini;*.mpr;*.map|All Files|*.*";
            // 
            // InsertSnippetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 610);
            Controls.Add(panelCenter);
            Controls.Add(splitterLeft);
            Controls.Add(panelLeft);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InsertSnippetForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Insert Snippet";
            Load += InsertSnippetForm_Load;
            panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            panelLeftBottom.ResumeLayout(false);
            panelCenter.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private Panel panelCenter;
        private Panel panelBottom;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private Controls.RulesEditMainControl rulesEdit;
        private Splitter splitterLeft;
        private Button buttonOk;
        private Button buttonCancel;
        private Panel panelLeftBottom;
        private Button buttonLoadFile;
        private OpenFileDialog openFileDialog;
    }
}