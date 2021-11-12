using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoAspectRatio(VlcMediaPlayerInstance mediaPlayerInstance, string aspectRatio)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            using (var aspectRatioInterop = Utf8InteropStringConverter.ToUtf8StringHandle(aspectRatio))
            {
                VlcNative.libvlc_video_set_aspect_ratio(mediaPlayerInstance, aspectRatioInterop);
            }
        }
    }
}
