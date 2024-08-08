using Deniz.TiberiumSunEditor.Gui.OpenRa;
using Deniz.TiberiumSunEditor.Gui.RaAudioPlayer;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class RaAudioManager : IDisposable
    {
        private readonly Stream _audioMixStream;
        private readonly Stream _audioBagStream;

        public RaAudioManager(byte[] audioMixBytes)
        {
            _audioMixStream = new MemoryStream(audioMixBytes);
            var mixFile = new RaMixFile(_audioMixStream, "audio.mix", MixEntryNames.Instance.GlobalFilenames);
            var audioIdxStream = mixFile.GetContentFile(mixFile.Index["audio.idx"]);
            IdxFile = new AudioIdxFile(audioIdxStream);
            audioIdxStream.Dispose();
            _audioBagStream = mixFile.GetContentFile(mixFile.Index["audio.bag"]);
        }

        public AudioIdxFile IdxFile { get; }

        public void PlayEntry(AudioIdxEntry entry)
        {
            var bagAudio = new AudioBagFileEntry(_audioBagStream, entry);
            bagAudio.Play();
        }

        public void Dispose()
        {
            _audioMixStream.Dispose();
            _audioBagStream.Dispose();
        }
    }
}
