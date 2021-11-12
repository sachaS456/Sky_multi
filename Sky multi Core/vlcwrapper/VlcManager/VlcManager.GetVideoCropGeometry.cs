using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal string GetVideoCropGeometry(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_video_get_crop_geometry(mediaPlayerInstance));
        }
    }
}
