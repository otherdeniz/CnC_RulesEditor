using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Deniz.TiberiumSunEditor.Gui.RaAudioPlayer;

namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    public class AudFile
    {
        private readonly byte[] _audBytes;

        public AudFile(byte[] audBytes)
        {
            _audBytes = audBytes;
            Header = MemoryMarshal.Read<AudHeader>(audBytes);
        }

        public AudHeader Header { get; }

        private int GetCbSample()
        {
            return 2; // Assuming 16-bit samples (2 bytes)
        }

        private int GetCSamples()
        {
            return Convert.ToInt32(Header.SizeIn) / GetCbSample();
        }

        private  byte[] Decode()
        {
            int cb_audio = GetCbSample() * GetCSamples();
            byte[] d = new byte[cb_audio * 4];

            switch (Header.Compression)
            {
                case 1:
                    {
                        int wIndex = 0;
                        for (int chunk_i = 0; wIndex < d.Length; chunk_i++)
                        {
                            var header = GetChunkHeader(chunk_i);
                            if (header == null) break;

                            var chunkData = GetChunkData(chunk_i);
                            if (chunkData == null) break;
                            
                            // NOT NEEDED
                            //AudDecodeWsChunk(chunkData, d, ref wIndex, header.Value.size_out);
                        }
                    } 
                    break;
                case 0x63:
                    {
                        AudioDecoder.AudDecode decode = new AudioDecoder.AudDecode();
                        decode.Init(0, 0);

                        int wIndex = 0;
                        for (int chunk_i = 0; wIndex < d.Length; chunk_i++)
                        {
                            var header = GetChunkHeader(chunk_i);
                            if (header == null) break;

                            var chunkData = GetChunkData(chunk_i);
                            if (chunkData == null) break;

                            var csChunk = Convert.ToInt32(header.Value.SizeOut) / GetCbSample();
                            var decodedChunk = new short[csChunk];
                            try
                            {
                                decode.DecodeChunk(chunkData, decodedChunk, csChunk);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e.Message);
                            }
                            var byteLength = decodedChunk.Length * sizeof(short);
                            Buffer.BlockCopy(decodedChunk, 0, d, wIndex, byteLength);
                            wIndex += byteLength;
                        }
                    }
                    break;
            }

            return d;
        }

        private AudChunkHeader? GetChunkHeader(int i)
        {
            int offset = Marshal.SizeOf(typeof(AudHeader));
            for (; i > 0; i--)
            {
                if (offset + Marshal.SizeOf(typeof(AudChunkHeader)) > _audBytes.Length)
                    return null;

                var chunkHeader = MemoryMarshal.Read<AudChunkHeader>(_audBytes.AsSpan(offset));
                offset += Marshal.SizeOf(typeof(AudChunkHeader)) + chunkHeader.SizeIn;
            }

            if (offset + Marshal.SizeOf(typeof(AudChunkHeader)) > _audBytes.Length)
                return null;

            var header = MemoryMarshal.Read<AudChunkHeader>(_audBytes.AsSpan(offset));
            if (offset + Marshal.SizeOf(typeof(AudChunkHeader)) + header.SizeIn > _audBytes.Length)
                return null;

            return header;
        }

        private byte[]? GetChunkData(int i)
        {
            var header = GetChunkHeader(i);
            if (header == null) return null;

            int offset = Marshal.SizeOf(typeof(AudHeader));
            for (; i > 0; i--)
            {
                offset += Marshal.SizeOf(typeof(AudChunkHeader)) + header.Value.SizeIn;
            }

            return _audBytes.Skip(offset + Marshal.SizeOf(typeof(AudChunkHeader))).Take(header.Value.SizeIn).ToArray();
        }

        public unsafe byte[] ToWav()
        {
            var decodedData = Decode();
            var wavSize = sizeof(WavHeader) + decodedData.Length;
            var wavBytes = new byte[wavSize];
            wavBytes = WriteWavFileHeader(wavBytes, decodedData.Length, Convert.ToInt32(Header.Samplerate), 16, 1);
            Array.Copy(decodedData, 0, wavBytes, sizeof(WavHeader), decodedData.Length);
            return wavBytes;
        }

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

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AudHeader
    {
        public ushort Samplerate;
        public uint SizeIn;
        public uint SizeOut;
        public byte Flags;
        public byte Compression;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AudChunkHeader
    {
        public ushort SizeIn;
        public ushort SizeOut;
        public int Id;
    }
}
