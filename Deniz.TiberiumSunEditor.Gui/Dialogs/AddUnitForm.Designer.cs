namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class AddUnitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUnitForm));
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
            panelBottom = new Panel();
            buttonOk = new Button();
            buttonCancel = new Button();
            panelUnit = new Panel();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelTop = new Panel();
            textBoxName = new TextBox();
            labelName = new Label();
            labelKey = new Label();
            pictureThumbnail = new PictureBox();
            panelLeft.SuspendLayout();
            unitsLayoutPanel.SuspendLayout();
            panelBottom.SuspendLayout();
            panelUnit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            SuspendLayout();
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Location = new Point(460, 0);
            splitterUnitPicker.Margin = new Padding(4, 3, 4, 3);
            splitterUnitPicker.Name = "splitterUnitPicker";
            splitterUnitPicker.Size = new Size(6, 513);
            splitterUnitPicker.TabIndex = 5;
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
            panelLeft.Size = new Size(460, 513);
            panelLeft.TabIndex = 6;
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
            unitsLayoutPanel.Size = new Size(460, 490);
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
            labelTypeHeader.TabIndex = 2;
            labelTypeHeader.Text = "Available Units";
            labelTypeHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = SystemColors.Control;
            panelBottom.Controls.Add(buttonOk);
            panelBottom.Controls.Add(buttonCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(466, 470);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(388, 43);
            panelBottom.TabIndex = 8;
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOk.Enabled = false;
            buttonOk.Image = (Image)resources.GetObject("buttonOk.Image");
            buttonOk.Location = new Point(190, 8);
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
            buttonCancel.Location = new Point(286, 8);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.MiddleRight;
            buttonCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // panelUnit
            // 
            panelUnit.BackColor = Color.White;
            panelUnit.Controls.Add(valuesGrid);
            panelUnit.Controls.Add(panelTop);
            panelUnit.Dock = DockStyle.Fill;
            panelUnit.Location = new Point(466, 0);
            panelUnit.Name = "panelUnit";
            panelUnit.Size = new Size(388, 470);
            panelUnit.TabIndex = 10;
            panelUnit.Visible = false;
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
            valuesGrid.Location = new Point(0, 69);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(388, 401);
            valuesGrid.TabIndex = 11;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(textBoxName);
            panelTop.Controls.Add(labelName);
            panelTop.Controls.Add(labelKey);
            panelTop.Controls.Add(pictureThumbnail);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(388, 69);
            panelTop.TabIndex = 10;
            // 
            // textBoxName
            // 
            textBoxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxName.Location = new Point(145, 23);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(231, 23);
            textBoxName.TabIndex = 3;
            // 
            // labelName
            // 
            labelName.BackColor = Color.Transparent;
            labelName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelName.Location = new Point(89, 23);
            labelName.Margin = new Padding(4, 0, 4, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(49, 21);
            labelName.TabIndex = 2;
            labelName.Text = "Name:";
            // 
            // labelKey
            // 
            labelKey.BackColor = Color.Transparent;
            labelKey.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelKey.Location = new Point(89, 2);
            labelKey.Margin = new Padding(4, 0, 4, 0);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(292, 21);
            labelKey.TabIndex = 2;
            labelKey.Text = "4TNK";
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(4, 3);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(78, 58);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 1;
            pictureThumbnail.TabStop = false;
            // 
            // AddUnitForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            CancelButton = buttonCancel;
            ClientSize = new Size(854, 513);
            Controls.Add(panelUnit);
            Controls.Add(panelBottom);
            Controls.Add(splitterUnitPicker);
            Controls.Add(panelLeft);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddUnitForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Unit";
            Load += AddUnitForm_Load;
            panelLeft.ResumeLayout(false);
            unitsLayoutPanel.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            panelUnit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Splitter splitterUnitPicker;
        private Panel panelLeft;
        private Label labelTypeHeader;
        private FlowLayoutPanel unitsLayoutPanel;
        private Controls.UnitPickerControl unitPickerControl1;
        private Controls.UnitPickerControl unitPickerControl2;
        private Controls.UnitPickerControl unitPickerControl3;
        private Controls.UnitPickerControl unitPickerControl4;
        private Panel panelBottom;
        private Button buttonOk;
        private Button buttonCancel;
        private Panel panelUnit;
        private Panel panelTop;
        private Label labelName;
        private Label labelKey;
        private PictureBox pictureThumbnail;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private TextBox textBoxName;
    }
}