using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetAudioChannel(VlcMediaPlayerInstance mediaPlayerInstance, int channel)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_audio_set_channel(mediaPlayerInstance, channel);
        }
    }
}
