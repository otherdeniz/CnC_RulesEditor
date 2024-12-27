namespace Deniz.TiberiumSunEditor.Gui.Dialogs;

partial class ShowArtChangesForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowArtChangesForm));
        panelBottom = new Panel();
        buttonClose = new Button();
        artEdit = new Controls.ArtEditMainControl();
        panelBottom.SuspendLayout();
        SuspendLayout();
        // 
        // panelBottom
        // 
        panelBottom.Controls.Add(buttonClose);
        panelBottom.Dock = DockStyle.Bottom;
        panelBottom.Location = new Point(0, 618);
        panelBottom.Name = "panelBottom";
        panelBottom.Size = new Size(984, 43);
        panelBottom.TabIndex = 2;
        // 
        // buttonClose
        // 
        buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        buttonClose.Image = (Image)resources.GetObject("buttonClose.Image");
        buttonClose.Location = new Point(882, 9);
        buttonClose.Name = "buttonClose";
        buttonClose.Size = new Size(90, 26);
        buttonClose.TabIndex = 1;
        buttonClose.Text = "Close";
        buttonClose.TextAlign = ContentAlignment.MiddleRight;
        buttonClose.TextImageRelation = TextImageRelation.ImageBeforeText;
        buttonClose.UseVisualStyleBackColor = true;
        buttonClose.Click += buttonClose_Click;
        // 
        // artEdit
        // 
        artEdit.Dock = DockStyle.Fill;
        artEdit.Location = new Point(0, 0);
        artEdit.Name = "artEdit";
        artEdit.ReadonlyMode = true;
        artEdit.Size = new Size(984, 618);
        artEdit.TabIndex = 3;
        artEdit.TitleVisible = false;
        // 
        // ShowArtChangesForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = buttonClose;
        ClientSize = new Size(984, 661);
        Controls.Add(artEdit);
        Controls.Add(panelBottom);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "ShowArtChangesForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "All Changes";
        Load += ShowArtChangesForm_Load;
        panelBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel panelBottom;
    private Button buttonClose;
    private Controls.ArtEditMainControl artEdit;
}