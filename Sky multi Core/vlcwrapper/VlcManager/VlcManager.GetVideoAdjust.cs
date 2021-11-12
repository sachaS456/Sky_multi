using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal bool GetVideoAdjustEnabled(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_adjust_int(mediaPlayerInstance, VideoAdjustOptions.Enable) == 1;
        }

        internal float GetVideoAdjustContrast(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Contrast);
        }

        internal float GetVideoAdjustBrightness(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Brightness);
        }

        internal float GetVideoAdjustHue(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Hue);
        }

        internal float GetVideoAdjustSaturation(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Saturation);
        }

        internal float GetVideoAdjustGamma(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Gamma);
        }
    }
}
