namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class SideBalanceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SideBalanceControl));
            panelTop = new Panel();
            ultraComboSide = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            label1 = new Label();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            pictureThumbnail = new PictureBox();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboSide).BeginInit();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(ultraComboSide);
            panelTop.Controls.Add(label1);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(624, 30);
            panelTop.TabIndex = 0;
            // 
            // ultraComboSide
            // 
            ultraComboSide.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            ultraComboSide.Location = new Point(70, 2);
            ultraComboSide.Name = "ultraComboSide";
            ultraComboSide.Size = new Size(196, 25);
            ultraComboSide.TabIndex = 1;
            ultraComboSide.ValueChanged += ultraComboSide_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 6);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 0;
            label1.Text = "Side:";
            // 
            // valuesGrid
            // 
            ultraGridBand1.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand1.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            valuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            valuesGrid.DisplayLayout.DefaultSelectedBackColor = Color.FromArgb(192, 192, 255);
            valuesGrid.DisplayLayout.DefaultSelectedForeColor = Color.Black;
            valuesGrid.DisplayLayout.GroupByBox.Hidden = true;
            valuesGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            valuesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            valuesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            valuesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            valuesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            valuesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            valuesGrid.DisplayLayout.Override.SelectedAppearancesEnabled = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            valuesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            valuesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            valuesGrid.Dock = DockStyle.Fill;
            valuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            valuesGrid.Location = new Point(0, 30);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(624, 700);
            valuesGrid.TabIndex = 7;
            valuesGrid.InitializeRow += valuesGrid_InitializeRow;
            valuesGrid.AfterRowUpdate += valuesGrid_AfterRowUpdate;
            valuesGrid.MouseEnterElement += valuesGrid_MouseEnterElement;
            valuesGrid.MouseLeaveElement += valuesGrid_MouseLeaveElement;
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.BorderStyle = BorderStyle.FixedSingle;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(273, 336);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(78, 58);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 9;
            pictureThumbnail.TabStop = false;
            pictureThumbnail.Visible = false;
            // 
            // SideBalanceControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureThumbnail);
            Controls.Add(valuesGrid);
            Controls.Add(panelTop);
            Name = "SideBalanceControl";
            Size = new Size(624, 730);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboSide).EndInit();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboSide;
        private Label label1;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private PictureBox pictureThumbnail;
    }
}
