using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal VlcMediaPlayerEventManagerInstance GetMediaPlayerEventManager(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return new VlcMediaPlayerEventManagerInstance(VlcNative.libvlc_media_player_event_manager(mediaPlayerInstance));
        }
    }
}
