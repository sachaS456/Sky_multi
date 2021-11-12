using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal bool GetVideoLogoEnabled(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Enable) == 1;
        }

        internal int GetVideoLogoX(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.X);
        }
        internal int GetVideoLogoY(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Y);
        }
        internal int GetVideoLogoDelay(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Delay);
        }
        internal int GetVideoLogoRepeat(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Repeat);
        }
        internal int GetVideoLogoOpacity(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Opacity);
        }
        internal int GetVideoLogoPosition(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Position);
        }
    }
}
