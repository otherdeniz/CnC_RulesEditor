namespace Deniz.TiberiumSunEditor.Gui.Controls;

partial class SmallUnitPictureGenerator
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
        labelKey = new Label();
        labelName = new Label();
        pictureThumbnail = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
        SuspendLayout();
        // 
        // labelKey
        // 
        labelKey.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        labelKey.Location = new Point(44, 1);
        labelKey.Name = "labelKey";
        labelKey.Size = new Size(109, 15);
        labelKey.TabIndex = 10;
        labelKey.Text = "Key";
        // 
        // labelName
        // 
        labelName.Location = new Point(44, 17);
        labelName.Name = "labelName";
        labelName.Size = new Size(109, 15);
        labelName.TabIndex = 11;
        labelName.Text = "Name";
        // 
        // pictureThumbnail
        // 
        pictureThumbnail.BackColor = Color.Transparent;
        pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
        pictureThumbnail.Location = new Point(2, 2);
        pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
        pictureThumbnail.Name = "pictureThumbnail";
        pictureThumbnail.Size = new Size(39, 29);
        pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureThumbnail.TabIndex = 8;
        pictureThumbnail.TabStop = false;
        // 
        // SmallEntityControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(labelKey);
        Controls.Add(labelName);
        Controls.Add(pictureThumbnail);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        Name = "SmallEntityControl";
        Size = new Size(153, 33);
        ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
        ResumeLayout(false);
    }

    #endregion
    private Label labelKey;
    private Label labelName;
    private PictureBox pictureThumbnail;
}