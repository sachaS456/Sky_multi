using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal bool TakeSnapshot(VlcMediaPlayerInstance mediaPlayerInstance, uint outputNumber, string filePath, uint width, uint height)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            if(filePath == null)
                throw new ArgumentNullException(nameof(filePath));
            using (var filePathHandle = Utf8InteropStringConverter.ToUtf8StringHandle(filePath))
            {
                return myLibraryLoader.GetInteropDelegate<TakeSnapshot>().Invoke(mediaPlayerInstance, outputNumber, filePathHandle, width, height) == 0;
            }
        }
    }
}
