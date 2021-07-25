using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetMediaMeta(VlcMediaInstance mediaInstance, MediaMetadatas metadata, string value)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(value))
            {
                myLibraryLoader.GetInteropDelegate<SetMediaMetadata>().Invoke(mediaInstance, metadata, handle);
            }
        }
    }
}
