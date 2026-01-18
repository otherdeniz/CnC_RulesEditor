using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Deniz.TiberiumSunEditor.Gui.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class EditMainBaseControl : UserControl
    {
        private bool _syntaxModeApplyed;

        public EditMainBaseControl()
        {
            InitializeComponent();
        }

        public void LoadIniFile(IniFile file)
        {
            textEditorIni.Text = file.SaveAsString();
            textEditorIni.ActiveTextAreaControl.TextArea.TextView.FirstVisibleLine = 20;
            ApplyIniSyntaxHighligthing();
        }

        public void ApplyIniSyntaxHighligthing()
        {
            if (_syntaxModeApplyed) return;

            // Load and apply the syntax modes
            FileSyntaxModeProvider provider = new FileSyntaxModeProvider(Path.Combine(ResourcesRepository.Instance.ResourcesPath, "SyntaxModes"));
            HighlightingManager.Manager.AddSyntaxModeFileProvider(provider);

            // Apply to the editor  
            textEditorIni.SetHighlighting("INI-Light");
            _syntaxModeApplyed = true;
        }
    }
}
