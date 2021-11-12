using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoScale(VlcMediaPlayerInstance mediaPlayerInstance, float factor)
        {
            VlcNative.libvlc_video_set_scale(mediaPlayerInstance, factor);
        }
    }
}
