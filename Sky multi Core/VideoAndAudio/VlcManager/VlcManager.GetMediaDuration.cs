using System;
using System.Runtime.InteropServices;
using System.Text;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal long GetMediaDuration(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetMediaDuration>().Invoke(mediaInstance);
        }
    }
}
