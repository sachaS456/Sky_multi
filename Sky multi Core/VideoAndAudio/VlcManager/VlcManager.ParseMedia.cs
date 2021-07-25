using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void ParseMedia(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<ParseMedia>().Invoke(mediaInstance);
        }
    }
}
