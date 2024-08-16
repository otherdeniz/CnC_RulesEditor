namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class MixBrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MixBrowserForm));
            buttonSearch = new Button();
            textBoxSearch = new TextBox();
            label2 = new Label();
            groupBoxRight = new GroupBox();
            pictureBoxPreview = new PictureBox();
            panel1 = new Panel();
            radioButtonOther = new RadioButton();
            radioButtonAnimation = new RadioButton();
            radioButtonCameo = new RadioButton();
            comboBoxZoom = new ComboBox();
            label3 = new Label();
            comboBoxOther = new ComboBox();
            comboBoxAnimation = new ComboBox();
            comboBoxPaletteCameo = new ComboBox();
            groupBoxResult = new GroupBox();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelTop = new Panel();
            panelLeft = new Panel();
            groupBoxRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            panel1.SuspendLayout();
            groupBoxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            panelTop.SuspendLayout();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(234, 8);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(75, 23);
            buttonSearch.TabIndex = 2;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(101, 8);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(127, 23);
            textBoxSearch.TabIndex = 1;
            textBoxSearch.Text = ".shp";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 11);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 0;
            label2.Text = "Search Term:";
            // 
            // groupBoxRight
            // 
            groupBoxRight.Controls.Add(pictureBoxPreview);
            groupBoxRight.Controls.Add(panel1);
            groupBoxRight.Dock = DockStyle.Fill;
            groupBoxRight.Location = new Point(427, 8);
            groupBoxRight.Name = "groupBoxRight";
            groupBoxRight.Padding = new Padding(8);
            groupBoxRight.Size = new Size(490, 493);
            groupBoxRight.TabIndex = 2;
            groupBoxRight.TabStop = false;
            groupBoxRight.Text = "Image / Animation Preview";
            // 
            // pictureBoxPreview
            // 
            pictureBoxPreview.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPreview.Dock = DockStyle.Fill;
            pictureBoxPreview.Location = new Point(8, 143);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new Size(474, 342);
            pictureBoxPreview.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxPreview.TabIndex = 1;
            pictureBoxPreview.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(radioButtonOther);
            panel1.Controls.Add(radioButtonAnimation);
            panel1.Controls.Add(radioButtonCameo);
            panel1.Controls.Add(comboBoxZoom);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(comboBoxOther);
            panel1.Controls.Add(comboBoxAnimation);
            panel1.Controls.Add(comboBoxPaletteCameo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(8, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(474, 119);
            panel1.TabIndex = 2;
            // 
            // radioButtonOther
            // 
            radioButtonOther.AutoSize = true;
            radioButtonOther.Location = new Point(3, 59);
            radioButtonOther.Name = "radioButtonOther";
            radioButtonOther.Size = new Size(100, 19);
            radioButtonOther.TabIndex = 2;
            radioButtonOther.TabStop = true;
            radioButtonOther.Text = "Other Palette: ";
            radioButtonOther.UseVisualStyleBackColor = true;
            radioButtonOther.CheckedChanged += radioButton_CheckedChanged;
            // 
            // radioButtonAnimation
            // 
            radioButtonAnimation.AutoSize = true;
            radioButtonAnimation.Location = new Point(3, 30);
            radioButtonAnimation.Name = "radioButtonAnimation";
            radioButtonAnimation.Size = new Size(126, 19);
            radioButtonAnimation.TabIndex = 2;
            radioButtonAnimation.TabStop = true;
            radioButtonAnimation.Text = "Animation Palette: ";
            radioButtonAnimation.UseVisualStyleBackColor = true;
            radioButtonAnimation.CheckedChanged += radioButton_CheckedChanged;
            // 
            // radioButtonCameo
            // 
            radioButtonCameo.AutoSize = true;
            radioButtonCameo.Location = new Point(3, 1);
            radioButtonCameo.Name = "radioButtonCameo";
            radioButtonCameo.Size = new Size(108, 19);
            radioButtonCameo.TabIndex = 2;
            radioButtonCameo.TabStop = true;
            radioButtonCameo.Text = "Cameo Palette: ";
            radioButtonCameo.UseVisualStyleBackColor = true;
            radioButtonCameo.CheckedChanged += radioButton_CheckedChanged;
            // 
            // comboBoxZoom
            // 
            comboBoxZoom.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxZoom.FormattingEnabled = true;
            comboBoxZoom.Location = new Point(132, 87);
            comboBoxZoom.Name = "comboBoxZoom";
            comboBoxZoom.Size = new Size(98, 23);
            comboBoxZoom.TabIndex = 1;
            comboBoxZoom.SelectedIndexChanged += comboBoxZoom_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 90);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 0;
            label3.Text = "Zoom:";
            // 
            // comboBoxOther
            // 
            comboBoxOther.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxOther.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOther.FormattingEnabled = true;
            comboBoxOther.Location = new Point(132, 58);
            comboBoxOther.Name = "comboBoxOther";
            comboBoxOther.Size = new Size(339, 23);
            comboBoxOther.TabIndex = 1;
            comboBoxOther.SelectedIndexChanged += comboBoxOther_SelectedIndexChanged;
            // 
            // comboBoxAnimation
            // 
            comboBoxAnimation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAnimation.FormattingEnabled = true;
            comboBoxAnimation.Location = new Point(132, 29);
            comboBoxAnimation.Name = "comboBoxAnimation";
            comboBoxAnimation.Size = new Size(339, 23);
            comboBoxAnimation.TabIndex = 1;
            comboBoxAnimation.SelectedIndexChanged += comboBoxAnimation_SelectedIndexChanged;
            // 
            // comboBoxPaletteCameo
            // 
            comboBoxPaletteCameo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxPaletteCameo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPaletteCameo.FormattingEnabled = true;
            comboBoxPaletteCameo.Location = new Point(132, 0);
            comboBoxPaletteCameo.Name = "comboBoxPaletteCameo";
            comboBoxPaletteCameo.Size = new Size(339, 23);
            comboBoxPaletteCameo.TabIndex = 1;
            comboBoxPaletteCameo.SelectedIndexChanged += comboBoxCameo_SelectedIndexChanged;
            // 
            // groupBoxResult
            // 
            groupBoxResult.Controls.Add(valuesGrid);
            groupBoxResult.Dock = DockStyle.Fill;
            groupBoxResult.Location = new Point(0, 44);
            groupBoxResult.Name = "groupBoxResult";
            groupBoxResult.Size = new Size(419, 449);
            groupBoxResult.TabIndex = 1;
            groupBoxResult.TabStop = false;
            groupBoxResult.Text = "Result";
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
            valuesGrid.Location = new Point(3, 19);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(413, 427);
            valuesGrid.TabIndex = 8;
            valuesGrid.AfterSelectChange += valuesGrid_AfterSelectChange;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(buttonSearch);
            panelTop.Controls.Add(label2);
            panelTop.Controls.Add(textBoxSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(419, 44);
            panelTop.TabIndex = 0;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(groupBoxResult);
            panelLeft.Controls.Add(panelTop);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(8, 8);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(419, 493);
            panelLeft.TabIndex = 3;
            // 
            // MixBrowserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 509);
            Controls.Add(groupBoxRight);
            Controls.Add(panelLeft);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MixBrowserForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mix Content Browser";
            Load += MixBrowserForm_Load;
            groupBoxRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBoxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBoxRight;
        private GroupBox groupBoxResult;
        private Label label1;
        private PictureBox pictureBoxPreview;
        private Panel panel1;
        private Label label2;
        private ComboBox comboBoxPaletteCameo;
        private Button buttonSearch;
        private TextBox textBoxSearch;
        private ComboBox comboBoxZoom;
        private Label label3;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private Panel panelTop;
        private Panel panelLeft;
        private RadioButton radioButtonOther;
        private RadioButton radioButtonAnimation;
        private RadioButton radioButtonCameo;
        private ComboBox comboBoxOther;
        private ComboBox comboBoxAnimation;
    }
}