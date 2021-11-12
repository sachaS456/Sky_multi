using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoAdjustEnabled(VlcMediaPlayerInstance mediaPlayerInstance, bool value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_int(mediaPlayerInstance, VideoAdjustOptions.Enable, value ? 1 : 0);
        }
        internal void SetVideoAdjustContrast(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Contrast, value);
        }
        internal void SetVideoAdjustBrightness(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Brightness, value);
        }
        internal void SetVideoAdjustHue(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Hue, value);
        }
        internal void SetVideoAdjustSaturation(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Saturation, value);
        }
        internal void SetVideoAdjustGamma(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Gamma, value);
        }
    }
}
