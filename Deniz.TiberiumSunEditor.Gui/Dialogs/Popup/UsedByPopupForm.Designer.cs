﻿namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    partial class UsedByPopupForm
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
            groupBox1 = new GroupBox();
            usedInEntityControl1 = new Controls.UsedInEntityControl();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(usedInEntityControl1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(403, 57);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "VehicleTypes";
            // 
            // usedInEntityControl1
            // 
            usedInEntityControl1.BorderStyle = BorderStyle.FixedSingle;
            usedInEntityControl1.Dock = DockStyle.Top;
            usedInEntityControl1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            usedInEntityControl1.Location = new Point(3, 19);
            usedInEntityControl1.MinimumSize = new Size(300, 35);
            usedInEntityControl1.Name = "usedInEntityControl1";
            usedInEntityControl1.Size = new Size(397, 35);
            usedInEntityControl1.TabIndex = 0;
            // 
            // UsedByPopupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(403, 289);
            Controls.Add(groupBox1);
            MinimumSize = new Size(350, 1);
            Name = "UsedByPopupForm";
            Text = "UsedByPopupForm";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Controls.UsedInEntityControl usedInEntityControl1;
    }
}