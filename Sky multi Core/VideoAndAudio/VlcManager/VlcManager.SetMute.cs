using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetMute(VlcMediaPlayerInstance mediaPlayerInstance, bool status)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetMute>().Invoke(mediaPlayerInstance, status ? 1 : 0);
        }
    }
}
