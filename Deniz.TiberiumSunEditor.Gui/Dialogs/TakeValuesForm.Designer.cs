namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class TakeValuesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TakeValuesForm));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            splitterUnitPicker = new Splitter();
            panelLeft = new Panel();
            unitsLayoutPanel = new FlowLayoutPanel();
            unitPickerControl1 = new Controls.UnitPickerControl();
            unitPickerControl2 = new Controls.UnitPickerControl();
            unitPickerControl3 = new Controls.UnitPickerControl();
            unitPickerControl4 = new Controls.UnitPickerControl();
            labelTypeHeader = new Label();
            groupBoxSource = new GroupBox();
            panel1 = new Panel();
            labelNameSource = new Label();
            labelKeySource = new Label();
            pictureThumbnailSouce = new PictureBox();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelBottom = new Panel();
            buttonClose = new Button();
            panelTargetSource = new Panel();
            groupBoxTarget = new GroupBox();
            panelTopTarget = new Panel();
            labelName = new Label();
            labelKeyTarget = new Label();
            pictureThumbnailTarget = new PictureBox();
            panelLeft.SuspendLayout();
            unitsLayoutPanel.SuspendLayout();
            groupBoxSource.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnailSouce).BeginInit();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            panelBottom.SuspendLayout();
            panelTargetSource.SuspendLayout();
            groupBoxTarget.SuspendLayout();
            panelTopTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnailTarget).BeginInit();
            SuspendLayout();
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Location = new Point(460, 0);
            splitterUnitPicker.Margin = new Padding(4, 3, 4, 3);
            splitterUnitPicker.Name = "splitterUnitPicker";
            splitterUnitPicker.Size = new Size(6, 698);
            splitterUnitPicker.TabIndex = 8;
            splitterUnitPicker.TabStop = false;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(unitsLayoutPanel);
            panelLeft.Controls.Add(labelTypeHeader);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.MinimumSize = new Size(128, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(460, 698);
            panelLeft.TabIndex = 9;
            // 
            // unitsLayoutPanel
            // 
            unitsLayoutPanel.AutoScroll = true;
            unitsLayoutPanel.BackColor = Color.White;
            unitsLayoutPanel.Controls.Add(unitPickerControl1);
            unitsLayoutPanel.Controls.Add(unitPickerControl2);
            unitsLayoutPanel.Controls.Add(unitPickerControl3);
            unitsLayoutPanel.Controls.Add(unitPickerControl4);
            unitsLayoutPanel.Dock = DockStyle.Fill;
            unitsLayoutPanel.Location = new Point(0, 23);
            unitsLayoutPanel.Margin = new Padding(4, 3, 4, 3);
            unitsLayoutPanel.Name = "unitsLayoutPanel";
            unitsLayoutPanel.Size = new Size(460, 675);
            unitsLayoutPanel.TabIndex = 1;
            // 
            // unitPickerControl1
            // 
            unitPickerControl1.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl1.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl1.Location = new Point(5, 3);
            unitPickerControl1.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl1.Name = "unitPickerControl1";
            unitPickerControl1.Size = new Size(101, 90);
            unitPickerControl1.TabIndex = 0;
            unitPickerControl1.Tag = "KEEP";
            // 
            // unitPickerControl2
            // 
            unitPickerControl2.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl2.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl2.Location = new Point(116, 3);
            unitPickerControl2.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl2.Name = "unitPickerControl2";
            unitPickerControl2.Size = new Size(101, 90);
            unitPickerControl2.TabIndex = 1;
            unitPickerControl2.Tag = "KEEP";
            // 
            // unitPickerControl3
            // 
            unitPickerControl3.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl3.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl3.Location = new Point(227, 3);
            unitPickerControl3.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl3.Name = "unitPickerControl3";
            unitPickerControl3.Size = new Size(101, 90);
            unitPickerControl3.TabIndex = 2;
            unitPickerControl3.Tag = "KEEP";
            // 
            // unitPickerControl4
            // 
            unitPickerControl4.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl4.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl4.Location = new Point(338, 3);
            unitPickerControl4.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl4.Name = "unitPickerControl4";
            unitPickerControl4.Size = new Size(101, 90);
            unitPickerControl4.TabIndex = 3;
            unitPickerControl4.Tag = "KEEP";
            // 
            // labelTypeHeader
            // 
            labelTypeHeader.BackColor = SystemColors.Control;
            labelTypeHeader.BorderStyle = BorderStyle.FixedSingle;
            labelTypeHeader.Dock = DockStyle.Top;
            labelTypeHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelTypeHeader.Location = new Point(0, 0);
            labelTypeHeader.Name = "labelTypeHeader";
            labelTypeHeader.Size = new Size(460, 23);
            labelTypeHeader.TabIndex = 3;
            labelTypeHeader.Text = "Select Source";
            labelTypeHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBoxSource
            // 
            groupBoxSource.BackColor = Color.White;
            groupBoxSource.Controls.Add(panel1);
            groupBoxSource.Dock = DockStyle.Fill;
            groupBoxSource.Location = new Point(294, 0);
            groupBoxSource.Name = "groupBoxSource";
            groupBoxSource.Size = new Size(391, 88);
            groupBoxSource.TabIndex = 10;
            groupBoxSource.TabStop = false;
            groupBoxSource.Text = "Source";
            groupBoxSource.Visible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(labelNameSource);
            panel1.Controls.Add(labelKeySource);
            panel1.Controls.Add(pictureThumbnailSouce);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 19);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(385, 64);
            panel1.TabIndex = 9;
            // 
            // labelNameSource
            // 
            labelNameSource.AutoSize = true;
            labelNameSource.BackColor = Color.Transparent;
            labelNameSource.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelNameSource.Location = new Point(89, 23);
            labelNameSource.Margin = new Padding(4, 0, 4, 0);
            labelNameSource.Name = "labelNameSource";
            labelNameSource.Size = new Size(105, 19);
            labelNameSource.TabIndex = 2;
            labelNameSource.Text = "Mammoth Tank";
            // 
            // labelKeySource
            // 
            labelKeySource.AutoSize = true;
            labelKeySource.BackColor = Color.Transparent;
            labelKeySource.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelKeySource.Location = new Point(89, 2);
            labelKeySource.Margin = new Padding(4, 0, 4, 0);
            labelKeySource.Name = "labelKeySource";
            labelKeySource.Size = new Size(45, 19);
            labelKeySource.TabIndex = 2;
            labelKeySource.Text = "4TNK";
            // 
            // pictureThumbnailSouce
            // 
            pictureThumbnailSouce.BackColor = Color.Transparent;
            pictureThumbnailSouce.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnailSouce.Image = (Image)resources.GetObject("pictureThumbnailSouce.Image");
            pictureThumbnailSouce.Location = new Point(4, 3);
            pictureThumbnailSouce.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnailSouce.Name = "pictureThumbnailSouce";
            pictureThumbnailSouce.Size = new Size(78, 58);
            pictureThumbnailSouce.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnailSouce.TabIndex = 1;
            pictureThumbnailSouce.TabStop = false;
            // 
            // valuesGrid
            // 
            ultraGridBand1.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand1.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            valuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            valuesGrid.DisplayLayout.DefaultSelectedBackColor = Color.FromArgb(192, 192, 255);
            valuesGrid.DisplayLayout.DefaultSelectedForeColor = Color.Black;
            valuesGrid.DisplayLayout.GroupByBox.Hidden = true;
            valuesGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            valuesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            valuesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.Override.AutoEditMode = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            valuesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            valuesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            valuesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            valuesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            valuesGrid.DisplayLayout.Override.SelectedAppearancesEnabled = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            valuesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            valuesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            valuesGrid.Dock = DockStyle.Fill;
            valuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            valuesGrid.Location = new Point(466, 88);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(685, 567);
            valuesGrid.TabIndex = 13;
            valuesGrid.Visible = false;
            valuesGrid.ClickCell += valuesGrid_ClickCell;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = SystemColors.Control;
            panelBottom.Controls.Add(buttonClose);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(466, 655);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(685, 43);
            panelBottom.TabIndex = 12;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.Image = (Image)resources.GetObject("buttonClose.Image");
            buttonClose.Location = new Point(584, 8);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(90, 26);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Close";
            buttonClose.TextAlign = ContentAlignment.MiddleRight;
            buttonClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // panelTargetSource
            // 
            panelTargetSource.Controls.Add(groupBoxSource);
            panelTargetSource.Controls.Add(groupBoxTarget);
            panelTargetSource.Dock = DockStyle.Top;
            panelTargetSource.Location = new Point(466, 0);
            panelTargetSource.Name = "panelTargetSource";
            panelTargetSource.Size = new Size(685, 88);
            panelTargetSource.TabIndex = 14;
            // 
            // groupBoxTarget
            // 
            groupBoxTarget.BackColor = Color.White;
            groupBoxTarget.Controls.Add(panelTopTarget);
            groupBoxTarget.Dock = DockStyle.Left;
            groupBoxTarget.Location = new Point(0, 0);
            groupBoxTarget.Name = "groupBoxTarget";
            groupBoxTarget.Size = new Size(294, 88);
            groupBoxTarget.TabIndex = 8;
            groupBoxTarget.TabStop = false;
            groupBoxTarget.Text = "Target";
            // 
            // panelTopTarget
            // 
            panelTopTarget.Controls.Add(labelName);
            panelTopTarget.Controls.Add(labelKeyTarget);
            panelTopTarget.Controls.Add(pictureThumbnailTarget);
            panelTopTarget.Dock = DockStyle.Top;
            panelTopTarget.Location = new Point(3, 19);
            panelTopTarget.Margin = new Padding(4, 3, 4, 3);
            panelTopTarget.Name = "panelTopTarget";
            panelTopTarget.Size = new Size(288, 64);
            panelTopTarget.TabIndex = 9;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.BackColor = Color.Transparent;
            labelName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelName.Location = new Point(89, 23);
            labelName.Margin = new Padding(4, 0, 4, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(105, 19);
            labelName.TabIndex = 2;
            labelName.Text = "Mammoth Tank";
            // 
            // labelKeyTarget
            // 
            labelKeyTarget.AutoSize = true;
            labelKeyTarget.BackColor = Color.Transparent;
            labelKeyTarget.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelKeyTarget.Location = new Point(89, 2);
            labelKeyTarget.Margin = new Padding(4, 0, 4, 0);
            labelKeyTarget.Name = "labelKeyTarget";
            labelKeyTarget.Size = new Size(45, 19);
            labelKeyTarget.TabIndex = 2;
            labelKeyTarget.Text = "4TNK";
            // 
            // pictureThumbnailTarget
            // 
            pictureThumbnailTarget.BackColor = Color.Transparent;
            pictureThumbnailTarget.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnailTarget.Image = (Image)resources.GetObject("pictureThumbnailTarget.Image");
            pictureThumbnailTarget.Location = new Point(4, 3);
            pictureThumbnailTarget.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnailTarget.Name = "pictureThumbnailTarget";
            pictureThumbnailTarget.Size = new Size(78, 58);
            pictureThumbnailTarget.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnailTarget.TabIndex = 1;
            pictureThumbnailTarget.TabStop = false;
            // 
            // TakeValuesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 698);
            Controls.Add(valuesGrid);
            Controls.Add(panelTargetSource);
            Controls.Add(panelBottom);
            Controls.Add(splitterUnitPicker);
            Controls.Add(panelLeft);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TakeValuesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Take values form another unit";
            Load += TakeValuesForm_Load;
            panelLeft.ResumeLayout(false);
            unitsLayoutPanel.ResumeLayout(false);
            groupBoxSource.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnailSouce).EndInit();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            panelBottom.ResumeLayout(false);
            panelTargetSource.ResumeLayout(false);
            groupBoxTarget.ResumeLayout(false);
            panelTopTarget.ResumeLayout(false);
            panelTopTarget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnailTarget).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Splitter splitterUnitPicker;
        private Panel panelLeft;
        private FlowLayoutPanel unitsLayoutPanel;
        private Controls.UnitPickerControl unitPickerControl1;
        private Controls.UnitPickerControl unitPickerControl2;
        private Controls.UnitPickerControl unitPickerControl3;
        private Controls.UnitPickerControl unitPickerControl4;
        private GroupBox groupBoxSource;
        private Panel panel1;
        private Label labelNameSource;
        private Label labelKeySource;
        private PictureBox pictureThumbnailSouce;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private Panel panelBottom;
        private Button buttonClose;
        private Panel panelTargetSource;
        private GroupBox groupBoxTarget;
        private Panel panelTopTarget;
        private Label labelName;
        private Label labelKeyTarget;
        private PictureBox pictureThumbnailTarget;
        private Label labelTypeHeader;
    }
}