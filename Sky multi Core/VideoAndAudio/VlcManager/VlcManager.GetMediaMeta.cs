using System;
using System.Runtime.InteropServices;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal string GetMediaMeta(VlcMediaInstance mediaInstance, MediaMetadatas metadata)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            var ptr = myLibraryLoader.GetInteropDelegate<GetMediaMetadata>().Invoke(mediaInstance, metadata);
            return Utf8InteropStringConverter.Utf8InteropToString(ptr);
        }
    }
}
