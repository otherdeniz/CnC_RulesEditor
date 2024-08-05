namespace Deniz.Updater
{
    partial class UpdateForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            panelDownloadStatus = new Panel();
            panel2 = new Panel();
            buttonCancelDownload = new Button();
            panelExtractDetails = new Panel();
            label12 = new Label();
            labelExtractedFiles = new Label();
            labelExtractedProgress = new Label();
            label18 = new Label();
            panelDownloadDetails = new Panel();
            label5 = new Label();
            label11 = new Label();
            labelDownloadedSize = new Label();
            labelDownloadedTimeLeft = new Label();
            labelDownloadedProgress = new Label();
            label9 = new Label();
            label7 = new Label();
            labelDownloadedSpeed = new Label();
            labelDownloadTitle = new Label();
            timerStartDownload = new System.Windows.Forms.Timer(components);
            timerExtractStatus = new System.Windows.Forms.Timer(components);
            timerDownloadStatus = new System.Windows.Forms.Timer(components);
            panelDownloadStatus.SuspendLayout();
            panel2.SuspendLayout();
            panelExtractDetails.SuspendLayout();
            panelDownloadDetails.SuspendLayout();
            SuspendLayout();
            // 
            // panelDownloadStatus
            // 
            panelDownloadStatus.Controls.Add(panel2);
            panelDownloadStatus.Controls.Add(panelExtractDetails);
            panelDownloadStatus.Controls.Add(panelDownloadDetails);
            panelDownloadStatus.Controls.Add(labelDownloadTitle);
            panelDownloadStatus.Dock = DockStyle.Fill;
            panelDownloadStatus.Location = new Point(8, 8);
            panelDownloadStatus.Name = "panelDownloadStatus";
            panelDownloadStatus.Size = new Size(294, 209);
            panelDownloadStatus.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonCancelDownload);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 182);
            panel2.Name = "panel2";
            panel2.Size = new Size(294, 27);
            panel2.TabIndex = 5;
            // 
            // buttonCancelDownload
            // 
            buttonCancelDownload.Anchor = AnchorStyles.Top;
            buttonCancelDownload.Image = (Image)resources.GetObject("buttonCancelDownload.Image");
            buttonCancelDownload.Location = new Point(94, 0);
            buttonCancelDownload.Name = "buttonCancelDownload";
            buttonCancelDownload.Size = new Size(104, 23);
            buttonCancelDownload.TabIndex = 0;
            buttonCancelDownload.Text = "Cancel";
            buttonCancelDownload.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancelDownload.UseVisualStyleBackColor = true;
            buttonCancelDownload.Click += buttonCancelDownload_Click;
            // 
            // panelExtractDetails
            // 
            panelExtractDetails.Controls.Add(label12);
            panelExtractDetails.Controls.Add(labelExtractedFiles);
            panelExtractDetails.Controls.Add(labelExtractedProgress);
            panelExtractDetails.Controls.Add(label18);
            panelExtractDetails.Dock = DockStyle.Top;
            panelExtractDetails.Location = new Point(0, 117);
            panelExtractDetails.Name = "panelExtractDetails";
            panelExtractDetails.Size = new Size(294, 42);
            panelExtractDetails.TabIndex = 4;
            panelExtractDetails.Visible = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(0, 0);
            label12.Name = "label12";
            label12.Size = new Size(33, 15);
            label12.TabIndex = 1;
            label12.Text = "Files:";
            // 
            // labelExtractedFiles
            // 
            labelExtractedFiles.AutoSize = true;
            labelExtractedFiles.Location = new Point(122, 0);
            labelExtractedFiles.Name = "labelExtractedFiles";
            labelExtractedFiles.Size = new Size(12, 15);
            labelExtractedFiles.TabIndex = 2;
            labelExtractedFiles.Text = "-";
            // 
            // labelExtractedProgress
            // 
            labelExtractedProgress.AutoSize = true;
            labelExtractedProgress.Location = new Point(122, 19);
            labelExtractedProgress.Name = "labelExtractedProgress";
            labelExtractedProgress.Size = new Size(12, 15);
            labelExtractedProgress.TabIndex = 2;
            labelExtractedProgress.Text = "-";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(0, 19);
            label18.Name = "label18";
            label18.Size = new Size(55, 15);
            label18.TabIndex = 1;
            label18.Text = "Progress:";
            // 
            // panelDownloadDetails
            // 
            panelDownloadDetails.Controls.Add(label5);
            panelDownloadDetails.Controls.Add(label11);
            panelDownloadDetails.Controls.Add(labelDownloadedSize);
            panelDownloadDetails.Controls.Add(labelDownloadedTimeLeft);
            panelDownloadDetails.Controls.Add(labelDownloadedProgress);
            panelDownloadDetails.Controls.Add(label9);
            panelDownloadDetails.Controls.Add(label7);
            panelDownloadDetails.Controls.Add(labelDownloadedSpeed);
            panelDownloadDetails.Dock = DockStyle.Top;
            panelDownloadDetails.Location = new Point(0, 37);
            panelDownloadDetails.Name = "panelDownloadDetails";
            panelDownloadDetails.Size = new Size(294, 80);
            panelDownloadDetails.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(77, 15);
            label5.TabIndex = 1;
            label5.Text = "Downloaded:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(0, 55);
            label11.Name = "label11";
            label11.Size = new Size(56, 15);
            label11.TabIndex = 1;
            label11.Text = "Time left:";
            // 
            // labelDownloadedSize
            // 
            labelDownloadedSize.AutoSize = true;
            labelDownloadedSize.Location = new Point(122, 0);
            labelDownloadedSize.Name = "labelDownloadedSize";
            labelDownloadedSize.Size = new Size(12, 15);
            labelDownloadedSize.TabIndex = 2;
            labelDownloadedSize.Text = "-";
            // 
            // labelDownloadedTimeLeft
            // 
            labelDownloadedTimeLeft.AutoSize = true;
            labelDownloadedTimeLeft.Location = new Point(122, 55);
            labelDownloadedTimeLeft.Name = "labelDownloadedTimeLeft";
            labelDownloadedTimeLeft.Size = new Size(12, 15);
            labelDownloadedTimeLeft.TabIndex = 2;
            labelDownloadedTimeLeft.Text = "-";
            // 
            // labelDownloadedProgress
            // 
            labelDownloadedProgress.AutoSize = true;
            labelDownloadedProgress.Location = new Point(122, 19);
            labelDownloadedProgress.Name = "labelDownloadedProgress";
            labelDownloadedProgress.Size = new Size(12, 15);
            labelDownloadedProgress.TabIndex = 2;
            labelDownloadedProgress.Text = "-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(0, 37);
            label9.Name = "label9";
            label9.Size = new Size(42, 15);
            label9.TabIndex = 1;
            label9.Text = "Speed:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(0, 19);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 1;
            label7.Text = "Progress:";
            // 
            // labelDownloadedSpeed
            // 
            labelDownloadedSpeed.AutoSize = true;
            labelDownloadedSpeed.Location = new Point(122, 37);
            labelDownloadedSpeed.Name = "labelDownloadedSpeed";
            labelDownloadedSpeed.Size = new Size(12, 15);
            labelDownloadedSpeed.TabIndex = 2;
            labelDownloadedSpeed.Text = "-";
            // 
            // labelDownloadTitle
            // 
            labelDownloadTitle.Dock = DockStyle.Top;
            labelDownloadTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelDownloadTitle.Location = new Point(0, 0);
            labelDownloadTitle.Name = "labelDownloadTitle";
            labelDownloadTitle.Size = new Size(294, 37);
            labelDownloadTitle.TabIndex = 0;
            labelDownloadTitle.Text = "Downloading update...";
            // 
            // timerStartDownload
            // 
            timerStartDownload.Tick += timerStartDownload_Tick;
            // 
            // timerExtractStatus
            // 
            timerExtractStatus.Interval = 250;
            timerExtractStatus.Tick += timerExtractStatus_Tick;
            // 
            // timerDownloadStatus
            // 
            timerDownloadStatus.Interval = 500;
            timerDownloadStatus.Tick += timerDownloadStatus_Tick;
            // 
            // UpdateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 225);
            ControlBox = false;
            Controls.Add(panelDownloadStatus);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UpdateForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Updating...";
            Load += UpdateForm_Load;
            panelDownloadStatus.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panelExtractDetails.ResumeLayout(false);
            panelExtractDetails.PerformLayout();
            panelDownloadDetails.ResumeLayout(false);
            panelDownloadDetails.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelDownloadStatus;
        private Panel panel2;
        private Button buttonCancelDownload;
        private Panel panelExtractDetails;
        private Label label12;
        private Label labelExtractedFiles;
        private Label labelExtractedProgress;
        private Label label18;
        private Panel panelDownloadDetails;
        private Label label5;
        private Label label11;
        private Label labelDownloadedSize;
        private Label labelDownloadedTimeLeft;
        private Label labelDownloadedProgress;
        private Label label9;
        private Label label7;
        private Label labelDownloadedSpeed;
        private Label labelDownloadTitle;
        private System.Windows.Forms.Timer timerStartDownload;
        private System.Windows.Forms.Timer timerExtractStatus;
        private System.Windows.Forms.Timer timerDownloadStatus;
    }
}
