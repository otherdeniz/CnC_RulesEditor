namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class FilterControl
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
            groupBoxFilter = new GroupBox();
            panel2 = new Panel();
            buttonReset = new Button();
            buttonFilter = new Button();
            panel1 = new Panel();
            textValue = new TextBox();
            comboComparison = new ComboBox();
            comboField = new ComboBox();
            label2 = new Label();
            panelFilterBuildable = new Panel();
            ultraComboSide = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            label1 = new Label();
            groupBoxFilter.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            panelFilterBuildable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboSide).BeginInit();
            SuspendLayout();
            // 
            // groupBoxFilter
            // 
            groupBoxFilter.Controls.Add(panel2);
            groupBoxFilter.Controls.Add(panel1);
            groupBoxFilter.Controls.Add(panelFilterBuildable);
            groupBoxFilter.Dock = DockStyle.Fill;
            groupBoxFilter.Location = new Point(0, 0);
            groupBoxFilter.Name = "groupBoxFilter";
            groupBoxFilter.Size = new Size(903, 50);
            groupBoxFilter.TabIndex = 0;
            groupBoxFilter.TabStop = false;
            groupBoxFilter.Text = "Filter by";
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonReset);
            panel2.Controls.Add(buttonFilter);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(736, 19);
            panel2.Name = "panel2";
            panel2.Size = new Size(161, 28);
            panel2.TabIndex = 1;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(81, 0);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 23);
            buttonReset.TabIndex = 1;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // buttonFilter
            // 
            buttonFilter.Location = new Point(0, 0);
            buttonFilter.Name = "buttonFilter";
            buttonFilter.Size = new Size(75, 23);
            buttonFilter.TabIndex = 0;
            buttonFilter.Text = "Apply";
            buttonFilter.UseVisualStyleBackColor = true;
            buttonFilter.Click += buttonFilter_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(textValue);
            panel1.Controls.Add(comboComparison);
            panel1.Controls.Add(comboField);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(275, 19);
            panel1.Name = "panel1";
            panel1.Size = new Size(461, 28);
            panel1.TabIndex = 0;
            // 
            // textValue
            // 
            textValue.Location = new Point(343, 0);
            textValue.Name = "textValue";
            textValue.Size = new Size(100, 23);
            textValue.TabIndex = 2;
            // 
            // comboComparison
            // 
            comboComparison.DropDownStyle = ComboBoxStyle.DropDownList;
            comboComparison.FormattingEnabled = true;
            comboComparison.Location = new Point(216, 0);
            comboComparison.Name = "comboComparison";
            comboComparison.Size = new Size(121, 23);
            comboComparison.TabIndex = 1;
            comboComparison.SelectedIndexChanged += comboComparison_SelectedIndexChanged;
            // 
            // comboField
            // 
            comboField.DropDownStyle = ComboBoxStyle.DropDownList;
            comboField.FormattingEnabled = true;
            comboField.Location = new Point(61, 0);
            comboField.Name = "comboField";
            comboField.Size = new Size(149, 23);
            comboField.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 4);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 0;
            label2.Text = "where";
            // 
            // panelFilterBuildable
            // 
            panelFilterBuildable.Controls.Add(ultraComboSide);
            panelFilterBuildable.Controls.Add(label1);
            panelFilterBuildable.Dock = DockStyle.Left;
            panelFilterBuildable.Location = new Point(3, 19);
            panelFilterBuildable.Name = "panelFilterBuildable";
            panelFilterBuildable.Size = new Size(272, 28);
            panelFilterBuildable.TabIndex = 0;
            // 
            // ultraComboSide
            // 
            ultraComboSide.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            ultraComboSide.Location = new Point(97, 0);
            ultraComboSide.Name = "ultraComboSide";
            ultraComboSide.Size = new Size(133, 25);
            ultraComboSide.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 4);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 0;
            label1.Text = "Buildable by:";
            // 
            // FilterControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBoxFilter);
            Name = "FilterControl";
            Size = new Size(903, 50);
            groupBoxFilter.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelFilterBuildable.ResumeLayout(false);
            panelFilterBuildable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboSide).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxFilter;
        private Panel panelFilterBuildable;
        private Panel panel1;
        private Label label2;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboSide;
        private Label label1;
        private Panel panel2;
        private TextBox textValue;
        private ComboBox comboComparison;
        private ComboBox comboField;
        private Button buttonReset;
        private Button buttonFilter;
    }
}
