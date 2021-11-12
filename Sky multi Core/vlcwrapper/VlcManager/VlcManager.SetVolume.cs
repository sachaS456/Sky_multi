using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVolume(VlcMediaPlayerInstance mediaPlayerInstance, int volume)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_audio_set_volume(mediaPlayerInstance, volume);
        }
    }
}
