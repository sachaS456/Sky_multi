using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {

        internal void SetVideoFormatCallbacks(VlcMediaPlayerInstance mediaPlayerInstance, VideoFormatCallback videoFormatCallback, CleanupVideoCallback cleanupCallback)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            VlcNative.libvlc_video_set_format_callbacks(mediaPlayerInstance, videoFormatCallback, cleanupCallback);
        }
    }
}
