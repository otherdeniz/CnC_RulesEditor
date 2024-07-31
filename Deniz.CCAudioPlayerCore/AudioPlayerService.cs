using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CSAudioPlayer;
using CSCore.Ffmpeg;
using CSCore.SoundOut;

namespace Deniz.CCAudioPlayerCore
{
    public static class AudioPlayerService
    {
        private static AudioPlayer audioPlayer1;
        private static cAudioPlayerWasapi? _audioPlayerWasapi;

        public static void PlaySound(Stream audStream)
        {
            if (_audioPlayerWasapi?.PlaybackState == PlaybackState.Playing)
            {
                _audioPlayerWasapi.Stop();
                _audioPlayerWasapi = null;
            }
            Task.Run(() =>
            {
                try
                {
                    var decoder = new FfmpegDecoder(audStream);
                    var audioPlayerWasapi = new cAudioPlayerWasapi();
                    audioPlayerWasapi.Play(decoder);
                    _audioPlayerWasapi = audioPlayerWasapi;
                    audioPlayerWasapi.PlayDone += (sender, args) =>
                    {
                        decoder.Dispose();
                    };
                    //while (audioPlayerWasapi.PlaybackState == PlaybackState.Playing)
                    //{
                    //    Thread.Sleep(100);
                    //}
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Playback failed: {e.Message}");
                }
            });
        }

        public static void PlaySound(string fileName)
        {
            if (audioPlayer1 == null)
            {
                audioPlayer1 = new CSAudioPlayer.AudioPlayer();
                audioPlayer1.DecodeMode = DecodeMode.FFMpeg;
                audioPlayer1.Channels = Channels.channels1;
                audioPlayer1.Bits = Bits.bits16;
                audioPlayer1.Samplerate = Samplerate.csamples22050;
                audioPlayer1.Mode = Mode.WasapiOut;
                audioPlayer1.AudioDevice = 0;
                audioPlayer1.Volume = 50;
            }

            audioPlayer1.PlayError += (sender, args) =>
            {
                Debug.WriteLine(args.String);
            };

            if (audioPlayer1.PlayingState != PlayingState.Playing)
            {
                audioPlayer1.Open(fileName);
                audioPlayer1.FileName = fileName;
                audioPlayer1.Play();
            }
        }
    }
}
