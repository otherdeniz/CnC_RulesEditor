namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class UnitEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitEditControl));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            pictureThumbnail = new PictureBox();
            labelKey = new Label();
            labelName = new Label();
            labelModifications = new Label();
            pictureBoxFavorite = new PictureBox();
            panelTop = new Panel();
            labelUsedBy = new Label();
            panelTopRight = new Panel();
            ButtonTakeValues = new Button();
            ButtonCopy = new Button();
            pictureBoxUnitPreview = new PictureBox();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelValueChooser = new Panel();
            groupBoxValueChooser = new GroupBox();
            lookupValue = new LookupTextControl();
            panelUseDefault = new Panel();
            ButtonUseDefault = new Button();
            panelCloseValue = new Panel();
            ButtonCloseValue = new Button();
            panelCenter = new Panel();
            panelAddNew = new Panel();
            ultraComboAddValue = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            labelAddValue = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFavorite).BeginInit();
            panelTop.SuspendLayout();
            panelTopRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUnitPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            panelValueChooser.SuspendLayout();
            groupBoxValueChooser.SuspendLayout();
            panelUseDefault.SuspendLayout();
            panelCloseValue.SuspendLayout();
            panelCenter.SuspendLayout();
            panelAddNew.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboAddValue).BeginInit();
            SuspendLayout();
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
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.BackColor = Color.Transparent;
            labelKey.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelKey.Location = new Point(89, 2);
            labelKey.Margin = new Padding(4, 0, 4, 0);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(45, 19);
            labelKey.TabIndex = 2;
            labelKey.Text = "4TNK";
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
            // labelModifications
            // 
            labelModifications.AutoSize = true;
            labelModifications.BackColor = Color.Transparent;
            labelModifications.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelModifications.Location = new Point(89, 45);
            labelModifications.Margin = new Padding(4, 0, 4, 0);
            labelModifications.Name = "labelModifications";
            labelModifications.Size = new Size(89, 15);
            labelModifications.TabIndex = 3;
            labelModifications.Text = "0 Modifications";
            // 
            // pictureBoxFavorite
            // 
            pictureBoxFavorite.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxFavorite.Cursor = Cursors.Hand;
            pictureBoxFavorite.Location = new Point(166, 6);
            pictureBoxFavorite.Margin = new Padding(4, 3, 4, 3);
            pictureBoxFavorite.Name = "pictureBoxFavorite";
            pictureBoxFavorite.Size = new Size(56, 55);
            pictureBoxFavorite.TabIndex = 4;
            pictureBoxFavorite.TabStop = false;
            pictureBoxFavorite.Click += pictureBoxFavorite_Click;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(labelUsedBy);
            panelTop.Controls.Add(panelTopRight);
            panelTop.Controls.Add(labelModifications);
            panelTop.Controls.Add(labelName);
            panelTop.Controls.Add(labelKey);
            panelTop.Controls.Add(pictureThumbnail);
            panelTop.Controls.Add(pictureBoxUnitPreview);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 3, 4, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(747, 69);
            panelTop.TabIndex = 5;
            // 
            // labelUsedBy
            // 
            labelUsedBy.AutoSize = true;
            labelUsedBy.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            labelUsedBy.ForeColor = Color.DarkBlue;
            labelUsedBy.Location = new Point(196, 45);
            labelUsedBy.Name = "labelUsedBy";
            labelUsedBy.Size = new Size(61, 15);
            labelUsedBy.TabIndex = 6;
            labelUsedBy.Text = "Used by: 0";
            labelUsedBy.Visible = false;
            labelUsedBy.MouseEnter += labelUsedBy_MouseEnter;
            labelUsedBy.MouseLeave += labelUsedBy_MouseLeave;
            // 
            // panelTopRight
            // 
            panelTopRight.Controls.Add(ButtonTakeValues);
            panelTopRight.Controls.Add(ButtonCopy);
            panelTopRight.Controls.Add(pictureBoxFavorite);
            panelTopRight.Dock = DockStyle.Right;
            panelTopRight.Location = new Point(518, 0);
            panelTopRight.Margin = new Padding(4, 3, 4, 3);
            panelTopRight.Name = "panelTopRight";
            panelTopRight.Size = new Size(229, 69);
            panelTopRight.TabIndex = 4;
            // 
            // ButtonTakeValues
            // 
            ButtonTakeValues.Image = (Image)resources.GetObject("ButtonTakeValues.Image");
            ButtonTakeValues.Location = new Point(3, 34);
            ButtonTakeValues.Name = "ButtonTakeValues";
            ButtonTakeValues.Size = new Size(142, 27);
            ButtonTakeValues.TabIndex = 6;
            ButtonTakeValues.Text = "Take values from";
            ButtonTakeValues.TextAlign = ContentAlignment.MiddleRight;
            ButtonTakeValues.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonTakeValues.UseVisualStyleBackColor = true;
            ButtonTakeValues.Click += ButtonTakeValues_Click;
            // 
            // ButtonCopy
            // 
            ButtonCopy.Image = (Image)resources.GetObject("ButtonCopy.Image");
            ButtonCopy.Location = new Point(3, 6);
            ButtonCopy.Name = "ButtonCopy";
            ButtonCopy.Size = new Size(142, 27);
            ButtonCopy.TabIndex = 5;
            ButtonCopy.Text = "Create copy";
            ButtonCopy.TextAlign = ContentAlignment.MiddleRight;
            ButtonCopy.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonCopy.UseVisualStyleBackColor = true;
            ButtonCopy.Visible = false;
            ButtonCopy.Click += ButtonCopy_Click;
            // 
            // pictureBoxUnitPreview
            // 
            pictureBoxUnitPreview.Anchor = AnchorStyles.Top;
            pictureBoxUnitPreview.Location = new Point(324, 0);
            pictureBoxUnitPreview.Name = "pictureBoxUnitPreview";
            pictureBoxUnitPreview.Size = new Size(100, 70);
            pictureBoxUnitPreview.TabIndex = 5;
            pictureBoxUnitPreview.TabStop = false;
            // 
            // valuesGrid
            // 
            ultraGridBand4.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand4.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            valuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
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
            valuesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            valuesGrid.DisplayLayout.Override.SelectedAppearancesEnabled = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            valuesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            valuesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            valuesGrid.Dock = DockStyle.Fill;
            valuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            valuesGrid.Location = new Point(0, 0);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(747, 538);
            valuesGrid.TabIndex = 6;
            valuesGrid.AfterCellUpdate += valuesGrid_AfterCellUpdate;
            valuesGrid.InitializeRow += valuesGrid_InitializeRow;
            valuesGrid.ClickCell += valuesGrid_ClickCell;
            valuesGrid.MouseDown += valuesGrid_MouseDown;
            // 
            // panelValueChooser
            // 
            panelValueChooser.Controls.Add(groupBoxValueChooser);
            panelValueChooser.Dock = DockStyle.Right;
            panelValueChooser.Location = new Point(449, 69);
            panelValueChooser.Margin = new Padding(4, 3, 4, 3);
            panelValueChooser.Name = "panelValueChooser";
            panelValueChooser.Size = new Size(298, 577);
            panelValueChooser.TabIndex = 7;
            panelValueChooser.Visible = false;
            // 
            // groupBoxValueChooser
            // 
            groupBoxValueChooser.Controls.Add(lookupValue);
            groupBoxValueChooser.Controls.Add(panelUseDefault);
            groupBoxValueChooser.Controls.Add(panelCloseValue);
            groupBoxValueChooser.Dock = DockStyle.Fill;
            groupBoxValueChooser.Location = new Point(0, 0);
            groupBoxValueChooser.Margin = new Padding(4, 3, 4, 3);
            groupBoxValueChooser.Name = "groupBoxValueChooser";
            groupBoxValueChooser.Padding = new Padding(4, 3, 4, 3);
            groupBoxValueChooser.Size = new Size(298, 577);
            groupBoxValueChooser.TabIndex = 0;
            groupBoxValueChooser.TabStop = false;
            groupBoxValueChooser.Text = "Value";
            // 
            // lookupValue
            // 
            lookupValue.Dock = DockStyle.Fill;
            lookupValue.Location = new Point(4, 19);
            lookupValue.Margin = new Padding(5, 3, 5, 3);
            lookupValue.Name = "lookupValue";
            lookupValue.Size = new Size(290, 481);
            lookupValue.TabIndex = 3;
            lookupValue.RefreshEntityValue += lookupValue_RefreshEntityValue;
            lookupValue.SelectedValueChanged += lookupValue_SelectedValueChanged;
            // 
            // panelUseDefault
            // 
            panelUseDefault.Controls.Add(ButtonUseDefault);
            panelUseDefault.Dock = DockStyle.Bottom;
            panelUseDefault.Location = new Point(4, 500);
            panelUseDefault.Margin = new Padding(4, 3, 4, 3);
            panelUseDefault.Name = "panelUseDefault";
            panelUseDefault.Padding = new Padding(5);
            panelUseDefault.Size = new Size(290, 37);
            panelUseDefault.TabIndex = 4;
            // 
            // ButtonUseDefault
            // 
            ButtonUseDefault.Dock = DockStyle.Fill;
            ButtonUseDefault.Image = (Image)resources.GetObject("ButtonUseDefault.Image");
            ButtonUseDefault.Location = new Point(5, 5);
            ButtonUseDefault.Margin = new Padding(4, 3, 4, 3);
            ButtonUseDefault.Name = "ButtonUseDefault";
            ButtonUseDefault.Size = new Size(280, 27);
            ButtonUseDefault.TabIndex = 0;
            ButtonUseDefault.Text = "Use Default";
            ButtonUseDefault.TextAlign = ContentAlignment.MiddleRight;
            ButtonUseDefault.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonUseDefault.UseVisualStyleBackColor = true;
            ButtonUseDefault.Click += ButtonUseDefault_Click;
            // 
            // panelCloseValue
            // 
            panelCloseValue.Controls.Add(ButtonCloseValue);
            panelCloseValue.Dock = DockStyle.Bottom;
            panelCloseValue.Location = new Point(4, 537);
            panelCloseValue.Margin = new Padding(4, 3, 4, 3);
            panelCloseValue.Name = "panelCloseValue";
            panelCloseValue.Padding = new Padding(5);
            panelCloseValue.Size = new Size(290, 37);
            panelCloseValue.TabIndex = 2;
            // 
            // ButtonCloseValue
            // 
            ButtonCloseValue.Dock = DockStyle.Fill;
            ButtonCloseValue.Image = (Image)resources.GetObject("ButtonCloseValue.Image");
            ButtonCloseValue.Location = new Point(5, 5);
            ButtonCloseValue.Margin = new Padding(4, 3, 4, 3);
            ButtonCloseValue.Name = "ButtonCloseValue";
            ButtonCloseValue.Size = new Size(280, 27);
            ButtonCloseValue.TabIndex = 0;
            ButtonCloseValue.Text = "Close";
            ButtonCloseValue.TextAlign = ContentAlignment.MiddleRight;
            ButtonCloseValue.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonCloseValue.UseVisualStyleBackColor = true;
            ButtonCloseValue.Click += ButtonCloseValue_Click;
            // 
            // panelCenter
            // 
            panelCenter.Controls.Add(valuesGrid);
            panelCenter.Controls.Add(panelAddNew);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(0, 69);
            panelCenter.Name = "panelCenter";
            panelCenter.Size = new Size(747, 577);
            panelCenter.TabIndex = 8;
            // 
            // panelAddNew
            // 
            panelAddNew.Controls.Add(ultraComboAddValue);
            panelAddNew.Controls.Add(labelAddValue);
            panelAddNew.Dock = DockStyle.Bottom;
            panelAddNew.Location = new Point(0, 538);
            panelAddNew.Name = "panelAddNew";
            panelAddNew.Size = new Size(747, 39);
            panelAddNew.TabIndex = 7;
            // 
            // ultraComboAddValue
            // 
            ultraComboAddValue.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            ultraComboAddValue.Location = new Point(59, 6);
            ultraComboAddValue.MaxDropDownItems = 10;
            ultraComboAddValue.Name = "ultraComboAddValue";
            ultraComboAddValue.NullText = "Choose 'Key' to insert...";
            appearance11.ForeColor = Color.Gray;
            ultraComboAddValue.NullTextAppearance = appearance11;
            ultraComboAddValue.Size = new Size(175, 25);
            ultraComboAddValue.TabIndex = 1;
            ultraComboAddValue.ValueChanged += ultraComboAddValue_ValueChanged;
            // 
            // labelAddValue
            // 
            labelAddValue.AutoSize = true;
            labelAddValue.Location = new Point(4, 10);
            labelAddValue.Name = "labelAddValue";
            labelAddValue.Size = new Size(32, 15);
            labelAddValue.TabIndex = 0;
            labelAddValue.Text = "Add:";
            // 
            // UnitEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelValueChooser);
            Controls.Add(panelCenter);
            Controls.Add(panelTop);
            Margin = new Padding(4, 3, 4, 3);
            Name = "UnitEditControl";
            Size = new Size(747, 646);
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFavorite).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelTopRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxUnitPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            panelValueChooser.ResumeLayout(false);
            groupBoxValueChooser.ResumeLayout(false);
            panelUseDefault.ResumeLayout(false);
            panelCloseValue.ResumeLayout(false);
            panelCenter.ResumeLayout(false);
            panelAddNew.ResumeLayout(false);
            panelAddNew.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboAddValue).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureThumbnail;
        private Label labelKey;
        private Label labelName;
        private Label labelModifications;
        private PictureBox pictureBoxFavorite;
        private Panel panelTop;
        private Panel panelTopRight;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private Panel panelValueChooser;
        private GroupBox groupBoxValueChooser;
        private Panel panelCloseValue;
        private Button ButtonCloseValue;
        private LookupTextControl lookupValue;
        private Panel panelUseDefault;
        private Button ButtonUseDefault;
        private Button ButtonTakeValues;
        private Button ButtonCopy;
        private Panel panelCenter;
        private Panel panelAddNew;
        private Label labelAddValue;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboAddValue;
        private PictureBox pictureBoxUnitPreview;
        private Label labelUsedBy;
    }
}
