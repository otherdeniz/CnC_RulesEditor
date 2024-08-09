namespace Deniz.TiberiumSunEditor.Gui.RaAudioPlayer
{
    public static class AudioDecoder
    {
        private static readonly int[] AudImaIndexAdjustTable = { -1, -1, -1, -1, 2, 4, 6, 8 };

        private static readonly int[] AudImaStepTable = {
        7, 8, 9, 10, 11, 12, 13, 14, 16,
        17, 19, 21, 23, 25, 28, 31, 34, 37,
        41, 45, 50, 55, 60, 66, 73, 80, 88,
        97, 107, 118, 130, 143, 157, 173, 190, 209,
        230, 253, 279, 307, 337, 371, 408, 449, 494,
        544, 598, 658, 724, 796, 876, 963, 1060, 1166,
        1282, 1411, 1552, 1707, 1878, 2066, 2272, 2499, 2749,
        3024, 3327, 3660, 4026, 4428, 4871, 5358, 5894, 6484,
        7132, 7845, 8630, 9493, 10442, 11487, 12635, 13899, 15289,
        16818, 18500, 20350, 22385, 24623, 27086, 29794, 32767
    };

        private static readonly int[] AudWsStepTable2 = { -2, -1, 0, 1 };

        private static readonly int[] AudWsStepTable4 = {
        -9, -8, -6, -5, -4, -3, -2, -1,
         0,  1,  2,  3,  4,  5,  6,  8
    };

        public static void AudDecodeImaChunk(byte[] audioIn, short[] audioOut, ref int index, ref int sample, int csChunk)
        {
            for (int sampleIndex = 0; sampleIndex < csChunk; sampleIndex++)
            {
                var inIndex = sampleIndex >> 1;
                if (audioIn.Length < inIndex + 1) break;
                int code = audioIn[inIndex];
                code = (sampleIndex & 1) != 0 ? code >> 4 : code & 0xF;
                int step = AudImaStepTable[index];
                int delta = step >> 3;
                if ((code & 1) != 0) delta += step >> 2;
                if ((code & 2) != 0) delta += step >> 1;
                if ((code & 4) != 0) delta += step;

                if ((code & 8) != 0)
                {
                    sample -= delta;
                    if (sample < -32768) sample = -32768;
                }
                else
                {
                    sample += delta;
                    if (sample > 32767) sample = 32767;
                }

                audioOut[sampleIndex] = (short)sample;
                index += AudImaIndexAdjustTable[code & 7];
                index = Math.Clamp(index, 0, 88);
            }
        }

        private static int Clip8(int value)
        {
            return Math.Clamp(value, 0, 0xFF);
        }

        public static void AudDecodeWsChunk(byte[] r, byte[] w, int cbS, int cbD)
        {
            if (cbS == cbD)
            {
                Buffer.BlockCopy(r, 0, w, 0, cbS);
                return;
            }

            int sample = 0x80;
            int rIndex = 0;
            int wIndex = 0;

            while (rIndex < cbS)
            {
                int count = r[rIndex++] & 0x3F;
                switch (r[rIndex - 1] >> 6)
                {
                    case 0:
                        for (count++; count > 0; count--)
                        {
                            int code = r[rIndex++];
                            w[wIndex++] = (byte)(sample = Clip8(sample + AudWsStepTable2[code & 3]));
                            w[wIndex++] = (byte)(sample = Clip8(sample + AudWsStepTable2[(code >> 2) & 3]));
                            w[wIndex++] = (byte)(sample = Clip8(sample + AudWsStepTable2[(code >> 4) & 3]));
                            w[wIndex++] = (byte)(sample = Clip8(sample + AudWsStepTable2[code >> 6]));
                        }
                        break;

                    case 1:
                        for (count++; count > 0; count--)
                        {
                            int code = r[rIndex++];
                            sample += AudWsStepTable4[code & 0xF];
                            w[wIndex++] = (byte)(sample = Clip8(sample));
                            sample += AudWsStepTable4[code >> 4];
                            w[wIndex++] = (byte)(sample = Clip8(sample));
                        }
                        break;

                    case 2:
                        if ((count & 0x20) != 0)
                        {
                            sample += (sbyte)count << 3 >> 3;
                            w[wIndex++] = (byte)sample;
                        }
                        else
                        {
                            Buffer.BlockCopy(r, rIndex, w, wIndex, count + 1);
                            rIndex += count + 1;
                            wIndex += count + 1;
                            sample = r[rIndex - 1];
                        }
                        break;

                    default:
                        Array.Fill(w, (byte)sample, wIndex, ++count);
                        wIndex += count;
                        break;
                }
            }
        }

        public class AudDecode
        {
            private int _index;
            private int _sample;

            public void Init(int index, int sample)
            {
                _index = index;
                _sample = sample;
            }

            public void DecodeChunk(byte[] audioIn, short[] audioOut, int csChunk)
            {
                AudDecodeImaChunk(audioIn, audioOut, ref _index, ref _sample, csChunk);
            }
        }
    }
}
