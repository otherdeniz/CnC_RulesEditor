using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class MixContentModel
    {
        public MixContentModel(MixFileContent mixContent)
        {
            MixContent = mixContent;
            MixFile = mixContent.MixFile.ToString();
            FileName = mixContent.FileName ?? string.Empty;
            Size = mixContent.FileLocationInfo.Size.ToString("#,##0");
        }

        [Browsable(false)]
        public MixFileContent MixContent { get; }

        [DisplayName("Mix File")]
        public string MixFile { get; }

        [DisplayName("File Name")]
        public string FileName { get; }

        public string Size { get; }
    }
}
