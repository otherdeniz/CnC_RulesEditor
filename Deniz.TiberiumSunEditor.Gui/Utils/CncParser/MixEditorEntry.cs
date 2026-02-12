namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public class MixEditorEntry
    {
        public MixEditorEntry(string fileName, byte[] data)
        {
            FileName = fileName;
            Data = data;
        }

        public string FileName { get; }

        public byte[] Data { get; }

        public int Size => Data.Length;
    }
}
