using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal VlcMediaInstance CloneMedia(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return VlcMediaInstance.New(this, VlcNative.libvlc_media_duplicate(mediaInstance));
        }
    }
}
