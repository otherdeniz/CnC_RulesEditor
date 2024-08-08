using System;
using System.Collections.Generic;
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
        private static readonly List<cAudioPlayerWasapi> _audioPlayers = new();

        public static void PlaySound(Stream audStream)
        {
            foreach (var player in _audioPlayers.ToArray())
            {
                if (player.PlaybackState == PlaybackState.Playing)
                {
                    player.Stop();
                }
                try
                {
                    _audioPlayers.Remove(player);
                }
                catch (Exception)
                { /* ignore */ }
            }
            Task.Run(() =>
            {
                try
                {
                    var decoder = new FfmpegDecoder(audStream);
                    var audioPlayerWasapi = new cAudioPlayerWasapi();
                    audioPlayerWasapi.Play(decoder);
                    _audioPlayers.Add(audioPlayerWasapi);
                    audioPlayerWasapi.PlayDone += (sender, args) =>
                    {
                        decoder.Dispose();
                        try
                        {
                            _audioPlayers.Remove(audioPlayerWasapi);
                        }
                        catch (Exception)
                        { /* ignore */ }
                    };
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Playback failed: {e.Message}");
                }
            });
        }

    }
}
