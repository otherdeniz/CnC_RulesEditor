namespace Deniz.TiberiumSunEditor.Gui.Dialogs;

partial class ShowAiChangesForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowAiChangesForm));
        panelBottom = new Panel();
        buttonClose = new Button();
        aiEdit = new Controls.AiEditMainControl();
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
        // aiEdit
        // 
        aiEdit.Dock = DockStyle.Fill;
        aiEdit.Location = new Point(0, 0);
        aiEdit.Name = "aiEdit";
        aiEdit.ReadonlyMode = true;
        aiEdit.ShowUnitTaskForceTabs = false;
        aiEdit.Size = new Size(984, 618);
        aiEdit.TabIndex = 3;
        aiEdit.TitleVisible = false;
        // 
        // ShowAiChangesForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = buttonClose;
        ClientSize = new Size(984, 661);
        Controls.Add(aiEdit);
        Controls.Add(panelBottom);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "ShowAiChangesForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "All Changes";
        Load += ShowAiChangesForm_Load;
        panelBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel panelBottom;
    private Button buttonClose;
    private Controls.AiEditMainControl aiEdit;
}