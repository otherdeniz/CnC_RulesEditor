using Deniz.TiberiumSunEditor.Gui.OpenRa;

namespace Deniz.TiberiumSunEditor.Gui.RaAudioPlayer
{
    public class AudioIdxFile
    {
        public AudioIdxFile(Stream audioIdxStream)
        {
            Header = new AudioIdxHeader(audioIdxStream);
            for (int i = 0; i < Header.SoundCount; i++)
            {
                if (audioIdxStream.Position < audioIdxStream.Length)
                    Entries.Add(new AudioIdxEntry(audioIdxStream));
            }
        }

        public AudioIdxHeader Header { get; }

        public List<AudioIdxEntry> Entries { get; } = new();
    }

    public class AudioIdxHeader
    {
        public AudioIdxHeader(Stream audioIdxStream)
        {
            Id = audioIdxStream.ReadInt32();
            Two = audioIdxStream.ReadInt32();
            SoundCount = audioIdxStream.ReadInt32();
        }

        public int Id { get; }

        public int Two { get; }

        public int SoundCount { get; }
    }

    public class AudioIdxEntry
    {
        //char fname[16];
        //__int32 offset;
        //__int32 size;
        //__int32 samplerate;
        //__int32 flags;
        //__int32 chunk_size;
        public AudioIdxEntry(Stream audioIdxStream)
        {
            Name = audioIdxStream.ReadASCII(16).TrimEnd(Convert.ToChar(0));
            Offset = audioIdxStream.ReadInt32();
            Size = audioIdxStream.ReadInt32();
            Samplerate = audioIdxStream.ReadInt32();
            Flags = audioIdxStream.ReadInt32();
            ChunkSize = audioIdxStream.ReadInt32();
        }

        public string Name { get; }

        public int Offset { get; }

        public int Size { get; }

        public int Samplerate { get; }

        public int Flags { get; }

        public int ChunkSize { get; }

    }
}
