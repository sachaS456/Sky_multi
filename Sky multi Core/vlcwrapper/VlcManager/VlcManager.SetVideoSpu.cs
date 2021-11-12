using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoSpu(VlcMediaPlayerInstance mediaPlayerInstance, TrackDescriptionStructure trackDescription)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            SetVideoSpu(mediaPlayerInstance, trackDescription.Id);
        }

        internal void SetVideoSpu(VlcMediaPlayerInstance mediaPlayerInstance, int id)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            VlcNative.libvlc_video_set_spu(mediaPlayerInstance, id);
        }
    }
}
