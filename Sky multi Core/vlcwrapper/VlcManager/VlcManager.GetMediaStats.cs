using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal MediaStatsStructure GetMediaStats(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            MediaStatsStructure result;
            VlcNative.libvlc_media_get_stats(mediaInstance, out result);
            return result;
        }
    }
}
