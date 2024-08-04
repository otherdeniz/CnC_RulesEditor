namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class LookupTextControl
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupTextControl));
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            pictureThumbnail = new PictureBox();
            panelFilter = new Panel();
            textBoxSearch = new TextBox();
            label1 = new Label();
            buttonResetSearch = new Button();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            panelFilter.SuspendLayout();
            SuspendLayout();
            // 
            // valuesGrid
            // 
            valuesGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand3.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand3.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            valuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            valuesGrid.DisplayLayout.GroupByBox.Hidden = true;
            valuesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            valuesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
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
            valuesGrid.Location = new Point(0, 24);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(339, 448);
            valuesGrid.TabIndex = 7;
            valuesGrid.AfterCellUpdate += valuesGrid_AfterCellUpdate;
            valuesGrid.CellChange += valuesGrid_CellChange;
            valuesGrid.AfterSelectChange += valuesGrid_AfterSelectChange;
            valuesGrid.MouseEnterElement += valuesGrid_MouseEnterElement;
            valuesGrid.MouseLeaveElement += valuesGrid_MouseLeaveElement;
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.BorderStyle = BorderStyle.FixedSingle;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(130, 207);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(78, 58);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 8;
            pictureThumbnail.TabStop = false;
            pictureThumbnail.Visible = false;
            // 
            // panelFilter
            // 
            panelFilter.Controls.Add(buttonResetSearch);
            panelFilter.Controls.Add(label1);
            panelFilter.Controls.Add(textBoxSearch);
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Location = new Point(0, 0);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(339, 24);
            panelFilter.TabIndex = 9;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(66, 0);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(100, 23);
            textBoxSearch.TabIndex = 0;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 1;
            label1.Text = "Search:";
            // 
            // buttonResetSearch
            // 
            buttonResetSearch.Enabled = false;
            buttonResetSearch.Image = (Image)resources.GetObject("buttonResetSearch.Image");
            buttonResetSearch.Location = new Point(165, 0);
            buttonResetSearch.Name = "buttonResetSearch";
            buttonResetSearch.Size = new Size(23, 23);
            buttonResetSearch.TabIndex = 2;
            buttonResetSearch.UseVisualStyleBackColor = true;
            buttonResetSearch.Click += buttonResetSearch_Click;
            // 
            // LookupTextControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureThumbnail);
            Controls.Add(valuesGrid);
            Controls.Add(panelFilter);
            Margin = new Padding(4, 3, 4, 3);
            Name = "LookupTextControl";
            Size = new Size(339, 472);
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private PictureBox pictureThumbnail;
        private Panel panelFilter;
        private Button buttonResetSearch;
        private Label label1;
        private TextBox textBoxSearch;
    }
}
