using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {

        internal void SetVideoCallbacks(VlcMediaPlayerInstance mediaPlayerInstance, LockVideoCallback lockVideoCallback, UnlockVideoCallback unlockVideoCallback, DisplayVideoCallback displayVideoCallback, IntPtr userData)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            myLibraryLoader.GetInteropDelegate<SetVideoCallbacks>().Invoke(mediaPlayerInstance, lockVideoCallback, unlockVideoCallback, displayVideoCallback, userData);
        }
    }
}
