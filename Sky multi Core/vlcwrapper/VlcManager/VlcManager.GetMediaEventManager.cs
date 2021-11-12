using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal VlcMediaEventManagerInstance GetMediaEventManager(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return new VlcMediaEventManagerInstance(VlcNative.libvlc_media_event_manager(mediaInstance));
        }
    }
}
