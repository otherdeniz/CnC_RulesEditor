using System.Runtime.InteropServices;
using System.Text;
using Deniz.CCAudioPlayerCore;

namespace Deniz.TiberiumSunEditor.Gui.RaAudioPlayer;

public class AudioBagFileEntry
{
    public AudioBagFileEntry(Stream audioBagStream, AudioIdxEntry entry)
    {
        BagStream = audioBagStream;
        IdxHeader = entry;
        Channels = (entry.Flags & 1) == 1 ? 2 : 1;
    }

    public Stream BagStream { get; }

    public AudioIdxEntry IdxHeader { get; }

    public int Channels { get; }

    public unsafe void Play()
    {
        var entry = IdxHeader;
        int channels = (entry.Flags & 1) != 0 ? 2 : 1;

        if ((entry.Flags & 2) != 0)
        {
            // WAV file handling
            int cb_d = sizeof(WavHeader) + entry.Size;
            var d = new VirtualBinary();
            byte[] w = d.WriteStart(cb_d);

            int samples = entry.Size / channels >> 1;
            w = WriteWavFileHeader(w, samples, entry.Samplerate, 16, channels);

            BagStream.Seek(entry.Offset, SeekOrigin.Begin);
            if (BagStream.Read(w, 0, entry.Size) == entry.Size)
            {
                PlayAudio(d);
            }
        }
        else
        {
            // ADPCM handling
            if ((entry.Flags & 8) == 0)
                return;

            var s = new VirtualBinary();
            BagStream.Seek(entry.Offset, SeekOrigin.Begin);

            if (BagStream.Read(s.WriteStart(entry.Size), 0, entry.Size) == entry.Size)
            {
                var decode = new CimaAdpcmWavDecode();
                decode.Load(s.Data, s.Size, channels, entry.ChunkSize);

                int samples = decode.CSamples() / channels;
                int cb_d = sizeof(WavHeader) + (channels * samples << 1);
                var d = new VirtualBinary();
                byte[] w = d.WriteStart(cb_d);

                w = WriteWavFileHeader(w, samples, entry.Samplerate, 16, channels);
                var decodedData = decode.GetDecodedData();
                Array.Copy(decodedData, 0, w, w.Length - decodedData.Length, decodedData.Length);

                PlayAudio(d);
            }
        }
    }

    // Helper methods and classes
    private unsafe byte[] WriteWavFileHeader(byte[] buffer, int samples, int samplerate, int bitsPerSample, int channels)
    {
        var header = new WavHeader
        {
            FileHeader = new WavFileHeader
            {
                ID = BitConverter.ToUInt32(Encoding.ASCII.GetBytes("RIFF")),
                Size = Convert.ToUInt32(sizeof(WavHeader) - sizeof(int) + (channels * samples << 1)),
                Format = BitConverter.ToUInt32(Encoding.ASCII.GetBytes("WAVE"))
            },
            FormatChunk = new WavFormatChunk
            {
                HeaderID = BitConverter.ToUInt32(Encoding.ASCII.GetBytes("fmt ")),
                HeaderSize = Convert.ToUInt32(sizeof(WavFormatChunk) - sizeof(WavChunkHeader)),
                FormatTag = 1,
                Channels = Convert.ToUInt16(channels),
                SampleRate = Convert.ToUInt32(samplerate),
                ByteRate = Convert.ToUInt32(channels * samplerate * bitsPerSample / 8),
                BlockAlign = Convert.ToUInt16(channels * bitsPerSample / 8),
                BitsPerSample = Convert.ToUInt16(bitsPerSample)
            },
            DataChunkHeader = new WavChunkHeader
            {
                ID = BitConverter.ToUInt32(Encoding.ASCII.GetBytes("data")),
                Size = Convert.ToUInt32(channels * samples * bitsPerSample / 8)
            }
        };

        using (var ms = new MemoryStream(buffer))
        {
            using (var writer = new BinaryWriter(ms))
            {
                WriteStruct(writer, header);
            }
        }

        return buffer;
    }

    private void PlayAudio(VirtualBinary audio)
    {
        AudioPlayerService.PlaySound(new StupidStream(audio.Data));
    }

    private static void WriteStruct<T>(BinaryWriter writer, T structure)
    {
        int size = Marshal.SizeOf<T>();
        byte[] bytes = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(structure, ptr, true);
        Marshal.Copy(ptr, bytes, 0, size);
        Marshal.FreeHGlobal(ptr);
        writer.Write(bytes);
    }
}