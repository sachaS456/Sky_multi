using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoDeinterlace(VlcMediaPlayerInstance mediaPlayerInstance, string deinterlaceMode)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            using (var deinterlaceModeInterop = Utf8InteropStringConverter.ToUtf8StringHandle(deinterlaceMode))
            {
                myLibraryLoader.GetInteropDelegate<SetVideoDeinterlace>().Invoke(mediaPlayerInstance, deinterlaceModeInterop);
            }
        }
    }
}
