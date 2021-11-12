using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoLogoEnabled(VlcMediaPlayerInstance mediaPlayerInstance, bool value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_logo_int(mediaPlayerInstance, VideoLogoOptions.Enable, value ? 1 : 0);
        }
        internal void SetVideoLogoFile(VlcMediaPlayerInstance mediaPlayerInstance, string value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            using (var valueInterop = Utf8InteropStringConverter.ToUtf8StringHandle(value))
            {
                VlcNative.libvlc_video_set_logo_string(mediaPlayerInstance, VideoLogoOptions.File, valueInterop);
            }
        }
        internal void SetVideoLogoX(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_logo_int(mediaPlayerInstance, VideoLogoOptions.X, value);
        }
        internal void SetVideoLogoY(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_logo_int(mediaPlayerInstance, VideoLogoOptions.Y, value);
        }
        internal void SetVideoLogoDelay(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_logo_int(mediaPlayerInstance, VideoLogoOptions.Delay, value);
        }
        internal void SetVideoLogoRepeat(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_logo_int(mediaPlayerInstance, VideoLogoOptions.Repeat, value);
        }
        internal void SetVideoLogoOpacity(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_logo_int(mediaPlayerInstance, VideoLogoOptions.Opacity, value);
        }
        internal void SetVideoLogoPosition(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_logo_int(mediaPlayerInstance, VideoLogoOptions.Position, value);
        }
    }
}
