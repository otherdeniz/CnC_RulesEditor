namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class IniTextEditorControl
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
            groupTextEditor = new GroupBox();
            textEditorIni = new ICSharpCode.TextEditor.TextEditorControlEx();
            panelFilterIni = new Panel();
            labelSearchIni = new Label();
            groupTextEditor.SuspendLayout();
            panelFilterIni.SuspendLayout();
            SuspendLayout();
            // 
            // groupTextEditor
            // 
            groupTextEditor.Controls.Add(textEditorIni);
            groupTextEditor.Controls.Add(panelFilterIni);
            groupTextEditor.Dock = DockStyle.Fill;
            groupTextEditor.Location = new Point(0, 0);
            groupTextEditor.Name = "groupTextEditor";
            groupTextEditor.Size = new Size(473, 593);
            groupTextEditor.TabIndex = 1;
            groupTextEditor.TabStop = false;
            groupTextEditor.Text = "INI-File Text-Editor";
            groupTextEditor.Leave += groupTextEditor_Leave;
            // 
            // textEditorIni
            // 
            textEditorIni.Dock = DockStyle.Fill;
            textEditorIni.FoldingStrategy = "C#";
            textEditorIni.Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textEditorIni.Location = new Point(3, 19);
            textEditorIni.Name = "textEditorIni";
            textEditorIni.Size = new Size(467, 553);
            textEditorIni.SyntaxHighlighting = "C#";
            textEditorIni.TabIndex = 11;
            textEditorIni.Text = "textEditorControlEx1";
            textEditorIni.TextChanged += textEditorIni_TextChanged;
            // 
            // panelFilterIni
            // 
            panelFilterIni.Controls.Add(labelSearchIni);
            panelFilterIni.Dock = DockStyle.Bottom;
            panelFilterIni.Location = new Point(3, 572);
            panelFilterIni.Name = "panelFilterIni";
            panelFilterIni.Size = new Size(467, 18);
            panelFilterIni.TabIndex = 10;
            // 
            // labelSearchIni
            // 
            labelSearchIni.AutoSize = true;
            labelSearchIni.Location = new Point(3, 1);
            labelSearchIni.Name = "labelSearchIni";
            labelSearchIni.Size = new Size(223, 15);
            labelSearchIni.TabIndex = 1;
            labelSearchIni.Text = "Tip: press CTRL+F to open Search-Dialog";
            // 
            // IniTextEditorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupTextEditor);
            Name = "IniTextEditorControl";
            Size = new Size(473, 593);
            groupTextEditor.ResumeLayout(false);
            panelFilterIni.ResumeLayout(false);
            panelFilterIni.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupTextEditor;
        private ICSharpCode.TextEditor.TextEditorControlEx textEditorIni;
        private Panel panelFilterIni;
        private Label labelSearchIni;
    }
}
