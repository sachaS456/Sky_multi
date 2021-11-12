using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoDeinterlace(VlcMediaPlayerInstance mediaPlayerInstance, string deinterlaceMode)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            using (var deinterlaceModeInterop = Utf8InteropStringConverter.ToUtf8StringHandle(deinterlaceMode))
            {
                VlcNative.libvlc_video_set_deinterlace(mediaPlayerInstance, deinterlaceModeInterop);
            }
        }
    }
}
