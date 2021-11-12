using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void ReleaseMediaPlayer(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                return;
            try
            {
                VlcNative.libvlc_media_player_release(mediaPlayerInstance);
            }
            finally
            {
                mediaPlayerInstance.Pointer = IntPtr.Zero;
            }
        }
    }
}
