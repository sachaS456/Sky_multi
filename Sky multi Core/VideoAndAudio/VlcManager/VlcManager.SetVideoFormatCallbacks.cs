using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {

        internal void SetVideoFormatCallbacks(VlcMediaPlayerInstance mediaPlayerInstance, VideoFormatCallback videoFormatCallback, CleanupVideoCallback cleanupCallback)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            myLibraryLoader.GetInteropDelegate<SetVideoFormatCallbacks>().Invoke(mediaPlayerInstance, videoFormatCallback, cleanupCallback);
        }
    }
}
