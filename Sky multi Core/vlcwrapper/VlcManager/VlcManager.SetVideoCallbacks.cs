using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {

        internal void SetVideoCallbacks(VlcMediaPlayerInstance mediaPlayerInstance, LockVideoCallback lockVideoCallback, UnlockVideoCallback unlockVideoCallback, DisplayVideoCallback displayVideoCallback, IntPtr userData)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            VlcNative.libvlc_video_set_callbacks(mediaPlayerInstance, lockVideoCallback, unlockVideoCallback, displayVideoCallback, userData);
        }
    }
}
