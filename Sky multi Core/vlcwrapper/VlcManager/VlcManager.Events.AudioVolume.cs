using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerAudioVolumeInternalEventCallback;
        public event EventHandler<VlcMediaPlayerAudioVolumeEventArgs> AudioVolume;

        private void OnMediaPlayerAudioVolumeInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerAudioVolume(args.eventArgsUnion.MediaPlayerAudioVolume.volume);
        }

        public void OnMediaPlayerAudioVolume(float volume)
        {
            AudioVolume?.Invoke(this, new VlcMediaPlayerAudioVolumeEventArgs(volume));
        }
    }
}