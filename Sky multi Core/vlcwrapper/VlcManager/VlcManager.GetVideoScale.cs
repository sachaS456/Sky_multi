using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal float GetVideoScale(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            return VlcNative.libvlc_video_get_scale(mediaPlayerInstance);
        }
    }
}
