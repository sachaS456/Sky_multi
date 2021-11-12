using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal bool IsPlaying(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero) return false;
            //This seems to be called multiple time
            //Eventually throwing an uncaught exception on close
            //An unhandled exception of type 'System.ArgumentException' occurred in Vlc.DotNet.Core.Interops.dll
            //Additional information: Media player instance is not initialized.
            //if (mediaPlayerInstance == IntPtr.Zero)
            //    throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_media_player_is_playing(mediaPlayerInstance) == 1;
        }
    }
}
