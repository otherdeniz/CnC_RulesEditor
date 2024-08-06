namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class LookupColorControl
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
            colorSliderRed = new ColorControls.ColorSlider();
            label1 = new Label();
            numericRed = new NumericUpDown();
            label2 = new Label();
            numericGreen = new NumericUpDown();
            label3 = new Label();
            numericBlue = new NumericUpDown();
            colorSliderGreen = new ColorControls.ColorSlider();
            colorSliderBlue = new ColorControls.ColorSlider();
            groupBoxColor = new GroupBox();
            panelColor = new Panel();
            ((System.ComponentModel.ISupportInitialize)numericRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericGreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericBlue).BeginInit();
            groupBoxColor.SuspendLayout();
            SuspendLayout();
            // 
            // colorSliderRed
            // 
            colorSliderRed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            colorSliderRed.Color2 = Color.Red;
            colorSliderRed.Location = new Point(122, 3);
            colorSliderRed.Maximum = 255F;
            colorSliderRed.Name = "colorSliderRed";
            colorSliderRed.Size = new Size(165, 28);
            colorSliderRed.TabIndex = 3;
            colorSliderRed.TabStop = false;
            colorSliderRed.ValueChanged += colorSliderRed_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 7);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 1;
            label1.Text = "Red:";
            // 
            // numericRed
            // 
            numericRed.Location = new Point(57, 5);
            numericRed.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericRed.Name = "numericRed";
            numericRed.Size = new Size(59, 23);
            numericRed.TabIndex = 0;
            numericRed.ValueChanged += numericRed_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 39);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 1;
            label2.Text = "Green:";
            // 
            // numericGreen
            // 
            numericGreen.Location = new Point(57, 37);
            numericGreen.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericGreen.Name = "numericGreen";
            numericGreen.Size = new Size(59, 23);
            numericGreen.TabIndex = 1;
            numericGreen.ValueChanged += numericGreen_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 73);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 1;
            label3.Text = "Blue";
            // 
            // numericBlue
            // 
            numericBlue.Location = new Point(57, 71);
            numericBlue.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericBlue.Name = "numericBlue";
            numericBlue.Size = new Size(59, 23);
            numericBlue.TabIndex = 2;
            numericBlue.ValueChanged += numericBlue_ValueChanged;
            // 
            // colorSliderGreen
            // 
            colorSliderGreen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            colorSliderGreen.Color2 = Color.Lime;
            colorSliderGreen.Location = new Point(122, 37);
            colorSliderGreen.Maximum = 255F;
            colorSliderGreen.Name = "colorSliderGreen";
            colorSliderGreen.Size = new Size(165, 28);
            colorSliderGreen.TabIndex = 4;
            colorSliderGreen.TabStop = false;
            colorSliderGreen.ValueChanged += colorSliderGreen_ValueChanged;
            // 
            // colorSliderBlue
            // 
            colorSliderBlue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            colorSliderBlue.Color2 = Color.Blue;
            colorSliderBlue.Location = new Point(122, 71);
            colorSliderBlue.Maximum = 255F;
            colorSliderBlue.Name = "colorSliderBlue";
            colorSliderBlue.Size = new Size(165, 28);
            colorSliderBlue.TabIndex = 5;
            colorSliderBlue.TabStop = false;
            colorSliderBlue.ValueChanged += colorSliderBlue_ValueChanged;
            // 
            // groupBoxColor
            // 
            groupBoxColor.Controls.Add(panelColor);
            groupBoxColor.Location = new Point(7, 106);
            groupBoxColor.Name = "groupBoxColor";
            groupBoxColor.Size = new Size(109, 85);
            groupBoxColor.TabIndex = 3;
            groupBoxColor.TabStop = false;
            groupBoxColor.Text = "Color";
            // 
            // panelColor
            // 
            panelColor.Dock = DockStyle.Fill;
            panelColor.Location = new Point(3, 19);
            panelColor.Name = "panelColor";
            panelColor.Size = new Size(103, 63);
            panelColor.TabIndex = 0;
            // 
            // LookupColorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBoxColor);
            Controls.Add(numericBlue);
            Controls.Add(label3);
            Controls.Add(numericGreen);
            Controls.Add(label2);
            Controls.Add(numericRed);
            Controls.Add(label1);
            Controls.Add(colorSliderBlue);
            Controls.Add(colorSliderGreen);
            Controls.Add(colorSliderRed);
            Name = "LookupColorControl";
            Size = new Size(290, 427);
            ((System.ComponentModel.ISupportInitialize)numericRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericGreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericBlue).EndInit();
            groupBoxColor.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ColorControls.ColorSlider colorSliderRed;
        private Label label1;
        private NumericUpDown numericRed;
        private Label label2;
        private NumericUpDown numericGreen;
        private Label label3;
        private NumericUpDown numericBlue;
        private ColorControls.ColorSlider colorSliderGreen;
        private ColorControls.ColorSlider colorSliderBlue;
        private GroupBox groupBoxColor;
        private Panel panelColor;
    }
}
