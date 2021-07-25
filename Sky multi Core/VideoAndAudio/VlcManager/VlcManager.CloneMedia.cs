using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaInstance CloneMedia(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return VlcMediaInstance.New(this, myLibraryLoader.GetInteropDelegate<CloneMedia>().Invoke(mediaInstance));
        }
    }
}
