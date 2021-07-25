using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal MediaStates GetMediaState(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetMediaState>().Invoke(mediaInstance);
        }
    }
}
