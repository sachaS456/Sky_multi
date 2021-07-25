using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaEventManagerInstance GetMediaEventManager(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return new VlcMediaEventManagerInstance(myLibraryLoader.GetInteropDelegate<GetMediaEventManager>().Invoke(mediaInstance));
        }
    }
}
