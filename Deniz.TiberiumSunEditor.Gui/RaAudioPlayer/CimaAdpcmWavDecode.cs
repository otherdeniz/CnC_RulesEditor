using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Deniz.TiberiumSunEditor.Gui.RaAudioPlayer
{
    public class CimaAdpcmWavDecode
    {
        private short[] _data;
        private int _cbData;
        private int _cSamples;

        public void Load(byte[] inputData, int cbSamples, int cChannels, int chunkSize)
        {
            int cChunks = (cbSamples + chunkSize - 1) / chunkSize;
            _cSamples = ((cbSamples - Marshal.SizeOf<ImaAdpcmChunkHeader>() * cChannels * cChunks) << 1) + cChunks * cChannels;
            _cbData = _cSamples << 1;
            _data = new short[_cSamples];
            int dataIndex = 0;
            //short[] w = _data;
            int csRemaining = _cSamples;
            int rIndex = 0;

            while (csRemaining > 0)
            {
                if (cChannels == 1)
                {
                    ImaAdpcmChunkHeader chunkHeader = MemoryMarshal.Read<ImaAdpcmChunkHeader>(inputData.AsSpan(rIndex));
                    rIndex += Marshal.SizeOf<ImaAdpcmChunkHeader>();
                    _data[dataIndex] = chunkHeader.Sample;
                    dataIndex++;
                    csRemaining--;

                    int csChunk = Math.Min(csRemaining, (chunkSize - Marshal.SizeOf<ImaAdpcmChunkHeader>()) << 1);
                    var decoder = new AudioDecoder.AudDecode();
                    decoder.Init(chunkHeader.Index, chunkHeader.Sample);
                    var chunkData = new short[csChunk];
                    try
                    {
                        decoder.DecodeChunk(inputData.AsSpan(rIndex, csChunk >> 1).ToArray(), chunkData, csChunk);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                    rIndex += csChunk >> 1;
                    Array.Copy(chunkData, 0, _data, dataIndex, csChunk);
                    dataIndex += csChunk;
                    csRemaining -= csChunk;
                }
                else
                {
                    Debug.Assert(cChannels == 2);

                    ImaAdpcmChunkHeader leftChunkHeader = MemoryMarshal.Read<ImaAdpcmChunkHeader>(inputData.AsSpan(rIndex));
                    rIndex += Marshal.SizeOf<ImaAdpcmChunkHeader>();
                    _data[dataIndex] = leftChunkHeader.Sample;
                    dataIndex++;
                    csRemaining--;

                    ImaAdpcmChunkHeader rightChunkHeader = MemoryMarshal.Read<ImaAdpcmChunkHeader>(inputData.AsSpan(rIndex));
                    rIndex += Marshal.SizeOf<ImaAdpcmChunkHeader>();
                    _data[dataIndex] = rightChunkHeader.Sample;
                    dataIndex++;
                    csRemaining--;

                    int csChunk = Math.Min(csRemaining, (chunkSize - (Marshal.SizeOf<ImaAdpcmChunkHeader>() << 1)) << 1);
                    var leftDecoder = new AudioDecoder.AudDecode();
                    var rightDecoder = new AudioDecoder.AudDecode();
                    leftDecoder.Init(leftChunkHeader.Index, leftChunkHeader.Sample);
                    rightDecoder.Init(rightChunkHeader.Index, rightChunkHeader.Sample);

                    while (csChunk >= 16)
                    {
                        short[] leftT = new short[8];
                        short[] rightT = new short[8];

                        leftDecoder.DecodeChunk(inputData.AsSpan(rIndex, 4).ToArray(), leftT, 8);
                        rIndex += 4;
                        rightDecoder.DecodeChunk(inputData.AsSpan(rIndex, 4).ToArray(), rightT, 8);
                        rIndex += 4;

                        for (int i = 0; i < 8; i++)
                        {
                            _data[dataIndex] = leftT[i];
                            _data[dataIndex+1] = rightT[i];
                            dataIndex += 2;
                        }

                        csChunk -= 16;
                        csRemaining -= 16;
                    }

                    if (csRemaining < 16)
                    {
                        csRemaining = 0;
                    }
                }
            }

            _cSamples /= cChannels;
        }

        public byte[] GetDecodedData()
        {
            // Konvertiere die dekodierten Daten in ein Byte-Array
            byte[] byteData = new byte[_data.Length * sizeof(short)];
            Buffer.BlockCopy(_data, 0, byteData, 0, byteData.Length);
            return byteData;
        }

        public int CSamples() => _cSamples;
    }

}
