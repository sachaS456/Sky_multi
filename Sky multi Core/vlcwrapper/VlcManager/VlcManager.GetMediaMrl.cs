using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal string GetMediaMrl(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            var ptr = VlcNative.libvlc_media_get_mrl(mediaInstance);
            return Utf8InteropStringConverter.Utf8InteropToString(ptr);
        }
    }
}
