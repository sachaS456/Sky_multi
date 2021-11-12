using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetMediaMeta(VlcMediaInstance mediaInstance, MediaMetadatas metadata, string value)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(value))
            {
                VlcNative.libvlc_media_set_meta(mediaInstance, metadata, handle);
            }
        }
    }
}
