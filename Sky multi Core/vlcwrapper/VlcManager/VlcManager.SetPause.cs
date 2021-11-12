using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetPause(VlcMediaPlayerInstance mediaPlayerInstance, bool isPause)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_media_player_set_pause(mediaPlayerInstance, isPause);
        }
    }
}
