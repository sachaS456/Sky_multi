using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoCropGeometry(VlcMediaPlayerInstance mediaPlayerInstance, string cropGeometry)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            using (var cropGeometryInterop = Utf8InteropStringConverter.ToUtf8StringHandle(cropGeometry))
            {
                VlcNative.libvlc_video_set_crop_geometry(mediaPlayerInstance, cropGeometryInterop);
            }
        }
    }
}
