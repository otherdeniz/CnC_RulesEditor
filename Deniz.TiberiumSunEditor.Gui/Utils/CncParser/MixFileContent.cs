namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public class MixFileContent
    {
        public MixFileContent(uint id, string? fileName, FileLocationInfo fileLocationInfo)
        {
            Id = id;
            FileName = fileName;
            FileLocationInfo = fileLocationInfo;
        }
        public uint Id { get; }

        public string? FileName { get; }

        public MixFile MixFile => FileLocationInfo.MixFile;

        public FileLocationInfo FileLocationInfo { get; }

        public override string ToString()
        {
            var name = FileName ?? Id.ToString();
            return MixFile.MasterMix != null
                ? $"{MixFile.MasterMix.Name}:{MixFile.Name}:{name}"
                : $"{MixFile.Name}:{name}";
        }

        public byte[] Read()
        {
            return MixFile.GetSingleFileData(FileLocationInfo.Offset, FileLocationInfo.Size);
        }
    }
}
