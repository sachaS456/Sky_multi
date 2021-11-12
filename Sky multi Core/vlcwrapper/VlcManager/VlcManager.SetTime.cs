using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public void SetTime(IntPtr mediaInstance, long timeInMs)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            VlcNative.libvlc_media_player_set_time(mediaInstance, timeInMs);
        }
    }
}