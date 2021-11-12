using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetFullScreen(VlcMediaPlayerInstance mediaPlayerInstance, bool fullScreen)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_set_fullscreen(mediaPlayerInstance, fullScreen ? 1 : 0);
        }
    }
}