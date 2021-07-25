using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetFullScreen(VlcMediaPlayerInstance mediaPlayerInstance, bool fullScreen)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetFullScreen>().Invoke(mediaPlayerInstance, fullScreen ? 1 : 0);
        }
    }
}