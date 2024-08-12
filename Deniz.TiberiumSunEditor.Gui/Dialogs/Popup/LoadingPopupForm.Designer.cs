namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    partial class LoadingPopupForm
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
            pictureBoxUnitPreview = new PictureBox();
            labelLoading = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUnitPreview).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxUnitPreview
            // 
            pictureBoxUnitPreview.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxUnitPreview.Location = new Point(0, 0);
            pictureBoxUnitPreview.Name = "pictureBoxUnitPreview";
            pictureBoxUnitPreview.Size = new Size(200, 180);
            pictureBoxUnitPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxUnitPreview.TabIndex = 6;
            pictureBoxUnitPreview.TabStop = false;
            // 
            // labelLoading
            // 
            labelLoading.AutoSize = true;
            labelLoading.BackColor = Color.Transparent;
            labelLoading.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelLoading.Location = new Point(67, 156);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(70, 15);
            labelLoading.TabIndex = 7;
            labelLoading.Text = "LOADING...";
            // 
            // LoadingPopupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(200, 180);
            Controls.Add(labelLoading);
            Controls.Add(pictureBoxUnitPreview);
            Name = "LoadingPopupForm";
            Text = "LoadingPopupForm";
            ((System.ComponentModel.ISupportInitialize)pictureBoxUnitPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxUnitPreview;
        private Label labelLoading;
    }
}