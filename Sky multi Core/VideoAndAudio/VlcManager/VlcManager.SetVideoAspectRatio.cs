using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoAspectRatio(VlcMediaPlayerInstance mediaPlayerInstance, string aspectRatio)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            using (var aspectRatioInterop = Utf8InteropStringConverter.ToUtf8StringHandle(aspectRatio))
            {
                myLibraryLoader.GetInteropDelegate<SetVideoAspectRatio>().Invoke(mediaPlayerInstance, aspectRatioInterop);
            }
        }
    }
}
