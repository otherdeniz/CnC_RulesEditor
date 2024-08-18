namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class UnitPickerGroupControl
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
            panel1 = new Panel();
            ultraComboColor = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            textBoxGroupName = new TextBox();
            unitsLayoutPanel = new FlowLayoutPanel();
            colorDialog = new ColorDialog();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboColor).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(ultraComboColor);
            panel1.Controls.Add(textBoxGroupName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 28);
            panel1.TabIndex = 2;
            // 
            // ultraComboColor
            // 
            ultraComboColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ultraComboColor.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            ultraComboColor.Location = new Point(176, 0);
            ultraComboColor.Name = "ultraComboColor";
            ultraComboColor.Size = new Size(55, 25);
            ultraComboColor.TabIndex = 5;
            ultraComboColor.ValueChanged += ultraComboColor_ValueChanged;
            // 
            // textBoxGroupName
            // 
            textBoxGroupName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxGroupName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxGroupName.Location = new Point(3, 2);
            textBoxGroupName.Name = "textBoxGroupName";
            textBoxGroupName.Size = new Size(172, 23);
            textBoxGroupName.TabIndex = 1;
            textBoxGroupName.Text = "new group";
            textBoxGroupName.TextChanged += textBoxGroupName_TextChanged;
            // 
            // unitsLayoutPanel
            // 
            unitsLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            unitsLayoutPanel.Dock = DockStyle.Top;
            unitsLayoutPanel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            unitsLayoutPanel.Location = new Point(0, 28);
            unitsLayoutPanel.Margin = new Padding(4, 3, 4, 3);
            unitsLayoutPanel.Name = "unitsLayoutPanel";
            unitsLayoutPanel.Size = new Size(234, 177);
            unitsLayoutPanel.TabIndex = 4;
            unitsLayoutPanel.SizeChanged += unitsLayoutPanel_SizeChanged;
            // 
            // colorDialog
            // 
            colorDialog.AnyColor = true;
            colorDialog.SolidColorOnly = true;
            // 
            // UnitPickerGroupControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(unitsLayoutPanel);
            Controls.Add(panel1);
            Name = "UnitPickerGroupControl";
            Size = new Size(234, 306);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboColor).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private TextBox textBoxGroupName;
        private FlowLayoutPanel unitsLayoutPanel;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboColor;
        private ColorDialog colorDialog;
    }
}
