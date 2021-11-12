using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal VlcMediaPlayerInstance CreateMediaPlayerFromMedia(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return new VlcMediaPlayerInstance(this, VlcNative.libvlc_media_player_new_from_media(mediaInstance));
        }
    }
}
