using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal string GetMediaMrl(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            var ptr = myLibraryLoader.GetInteropDelegate<GetMediaMrl>().Invoke(mediaInstance);
            return Utf8InteropStringConverter.Utf8InteropToString(ptr);
        }
    }
}
