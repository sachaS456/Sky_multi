using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal MediaStatsStructure GetMediaStats(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            MediaStatsStructure result;
            myLibraryLoader.GetInteropDelegate<GetMediaStats>().Invoke(mediaInstance, out result);
            return result;
        }
    }
}
