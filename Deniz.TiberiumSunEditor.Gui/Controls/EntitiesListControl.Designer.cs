namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class EntitiesListControl
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntitiesListControl));
            panelLeft = new Panel();
            entitiesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            toolStripAdd = new ToolStrip();
            buttonAddNew = new ToolStripButton();
            splitterUnitPicker = new Splitter();
            panelContent = new Panel();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)entitiesGrid).BeginInit();
            toolStripAdd.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(entitiesGrid);
            panelLeft.Controls.Add(toolStripAdd);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(260, 558);
            panelLeft.TabIndex = 0;
            // 
            // entitiesGrid
            // 
            entitiesGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand1.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand1.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            entitiesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            entitiesGrid.DisplayLayout.GroupByBox.Hidden = true;
            entitiesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            entitiesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            entitiesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            entitiesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            entitiesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            entitiesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            entitiesGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            entitiesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            entitiesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            entitiesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            entitiesGrid.Dock = DockStyle.Fill;
            entitiesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            entitiesGrid.Location = new Point(0, 25);
            entitiesGrid.Margin = new Padding(4, 3, 4, 3);
            entitiesGrid.Name = "entitiesGrid";
            entitiesGrid.Size = new Size(260, 533);
            entitiesGrid.TabIndex = 8;
            entitiesGrid.AfterSelectChange += entitiesGrid_AfterSelectChange;
            // 
            // toolStripAdd
            // 
            toolStripAdd.Items.AddRange(new ToolStripItem[] { buttonAddNew });
            toolStripAdd.Location = new Point(0, 0);
            toolStripAdd.Name = "toolStripAdd";
            toolStripAdd.Size = new Size(260, 25);
            toolStripAdd.TabIndex = 4;
            toolStripAdd.Text = "toolStrip1";
            // 
            // buttonAddNew
            // 
            buttonAddNew.Image = (Image)resources.GetObject("buttonAddNew.Image");
            buttonAddNew.ImageTransparentColor = Color.Magenta;
            buttonAddNew.Name = "buttonAddNew";
            buttonAddNew.Size = new Size(49, 22);
            buttonAddNew.Text = "Add";
            buttonAddNew.Click += buttonAddNew_Click;
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Location = new Point(260, 0);
            splitterUnitPicker.Margin = new Padding(4, 3, 4, 3);
            splitterUnitPicker.Name = "splitterUnitPicker";
            splitterUnitPicker.Size = new Size(6, 558);
            splitterUnitPicker.TabIndex = 3;
            splitterUnitPicker.TabStop = false;
            // 
            // panelContent
            // 
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(266, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(579, 558);
            panelContent.TabIndex = 4;
            // 
            // EntitiesListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelContent);
            Controls.Add(splitterUnitPicker);
            Controls.Add(panelLeft);
            Name = "EntitiesListControl";
            Size = new Size(845, 558);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)entitiesGrid).EndInit();
            toolStripAdd.ResumeLayout(false);
            toolStripAdd.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private ToolStrip toolStripAdd;
        private ToolStripButton buttonAddNew;
        private Infragistics.Win.UltraWinGrid.UltraGrid entitiesGrid;
        private Splitter splitterUnitPicker;
        private Panel panelContent;
    }
}
