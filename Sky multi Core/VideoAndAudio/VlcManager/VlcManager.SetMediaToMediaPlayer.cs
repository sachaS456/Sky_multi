using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetMediaToMediaPlayer(VlcMediaPlayerInstance mediaPlayerInstance, VlcMediaInstance mediaInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetMediaToMediaPlayer>().Invoke(mediaPlayerInstance, mediaInstance?.Pointer ?? IntPtr.Zero);
        }
    }
}
