using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetPause(VlcMediaPlayerInstance mediaPlayerInstance, bool isPause)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetPause>().Invoke(mediaPlayerInstance, isPause);
        }
    }
}
