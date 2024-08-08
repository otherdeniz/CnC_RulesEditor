using System.Runtime.InteropServices;

namespace Deniz.TiberiumSunEditor.Gui.RaAudioPlayer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WavFileHeader
    {
        public uint ID; // "RIFF" für WAV-Dateien
        public uint Size; // Größe des gesamten Dateiblocks
        public uint Format; // "WAVE"
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WavFormatChunk
    {
        public uint HeaderID; // "fmt " für Format-Chunk
        public uint HeaderSize; // Größe des Format-Chunks
        public ushort FormatTag; // Format-Code (1 für PCM)
        public ushort Channels; // Anzahl der Kanäle
        public uint SampleRate; // Abtastrate
        public uint ByteRate; // Byte-Rate (SampleRate * BlockAlign)
        public ushort BlockAlign; // Block-Align
        public ushort BitsPerSample; // Bits pro Sample
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WavChunkHeader
    {
        public uint ID; // Chunk-ID
        public uint Size; // Chunk-Größe
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WavHeader
    {
        public WavFileHeader FileHeader;
        public WavFormatChunk FormatChunk;
        public WavChunkHeader DataChunkHeader;
    }
}
