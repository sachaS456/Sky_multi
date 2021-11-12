using System;
using System.Runtime.InteropServices;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal string GetMediaMeta(VlcMediaInstance mediaInstance, MediaMetadatas metadata)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            var ptr = VlcNative.libvlc_media_get_meta(mediaInstance, metadata);
            return Utf8InteropStringConverter.Utf8InteropToString(ptr);
        }
    }
}
