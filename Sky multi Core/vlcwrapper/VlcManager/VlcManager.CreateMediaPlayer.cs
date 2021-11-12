using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal VlcMediaPlayerInstance CreateMediaPlayer()
        {
            lock (myVlcInstance)
            {
                return new VlcMediaPlayerInstance(this, VlcNative.libvlc_media_player_new(myVlcInstance));
            }
        }
    }
}
