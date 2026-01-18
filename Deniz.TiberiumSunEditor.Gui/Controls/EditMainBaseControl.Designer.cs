namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class EditMainBaseControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditMainBaseControl));
            groupTextEditor = new GroupBox();
            textEditorIni = new ICSharpCode.TextEditor.TextEditorControlEx();
            panelFilterIni = new Panel();
            buttonResetSearchIni = new Button();
            labelSearchIni = new Label();
            textBoxSearchIni = new TextBox();
            groupTextEditor.SuspendLayout();
            panelFilterIni.SuspendLayout();
            SuspendLayout();
            // 
            // groupTextEditor
            // 
            groupTextEditor.Controls.Add(textEditorIni);
            groupTextEditor.Controls.Add(panelFilterIni);
            groupTextEditor.Dock = DockStyle.Right;
            groupTextEditor.Location = new Point(577, 0);
            groupTextEditor.Name = "groupTextEditor";
            groupTextEditor.Size = new Size(341, 709);
            groupTextEditor.TabIndex = 0;
            groupTextEditor.TabStop = false;
            groupTextEditor.Text = "INI Text Editor";
            // 
            // textEditorIni
            // 
            textEditorIni.Dock = DockStyle.Fill;
            textEditorIni.FoldingStrategy = "C#";
            textEditorIni.Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textEditorIni.Location = new Point(3, 43);
            textEditorIni.Name = "textEditorIni";
            textEditorIni.Size = new Size(335, 663);
            textEditorIni.SyntaxHighlighting = "C#";
            textEditorIni.TabIndex = 11;
            textEditorIni.Text = "textEditorControlEx1";
            // 
            // panelFilterIni
            // 
            panelFilterIni.Controls.Add(buttonResetSearchIni);
            panelFilterIni.Controls.Add(labelSearchIni);
            panelFilterIni.Controls.Add(textBoxSearchIni);
            panelFilterIni.Dock = DockStyle.Top;
            panelFilterIni.Location = new Point(3, 19);
            panelFilterIni.Name = "panelFilterIni";
            panelFilterIni.Size = new Size(335, 24);
            panelFilterIni.TabIndex = 10;
            // 
            // buttonResetSearchIni
            // 
            buttonResetSearchIni.Enabled = false;
            buttonResetSearchIni.Image = (Image)resources.GetObject("buttonResetSearchIni.Image");
            buttonResetSearchIni.Location = new Point(165, 0);
            buttonResetSearchIni.Name = "buttonResetSearchIni";
            buttonResetSearchIni.Size = new Size(23, 23);
            buttonResetSearchIni.TabIndex = 2;
            buttonResetSearchIni.UseVisualStyleBackColor = true;
            // 
            // labelSearchIni
            // 
            labelSearchIni.AutoSize = true;
            labelSearchIni.Location = new Point(3, 3);
            labelSearchIni.Name = "labelSearchIni";
            labelSearchIni.Size = new Size(45, 15);
            labelSearchIni.TabIndex = 1;
            labelSearchIni.Text = "Search:";
            // 
            // textBoxSearchIni
            // 
            textBoxSearchIni.Location = new Point(66, 0);
            textBoxSearchIni.Name = "textBoxSearchIni";
            textBoxSearchIni.Size = new Size(100, 23);
            textBoxSearchIni.TabIndex = 0;
            // 
            // EditMainBaseControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupTextEditor);
            Name = "EditMainBaseControl";
            Size = new Size(918, 709);
            groupTextEditor.ResumeLayout(false);
            panelFilterIni.ResumeLayout(false);
            panelFilterIni.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupTextEditor;
        private ICSharpCode.TextEditor.TextEditorControlEx textEditorIni;
        private Panel panelFilterIni;
        private Button buttonResetSearchIni;
        private Label labelSearchIni;
        private TextBox textBoxSearchIni;
    }
}
