using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoAdjustEnabled(VlcMediaPlayerInstance mediaPlayerInstance, bool value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoAdjustInteger>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Enable, value ? 1 : 0);
        }
        internal void SetVideoAdjustContrast(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Contrast, value);
        }
        internal void SetVideoAdjustBrightness(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Brightness, value);
        }
        internal void SetVideoAdjustHue(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Hue, value);
        }
        internal void SetVideoAdjustSaturation(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Saturation, value);
        }
        internal void SetVideoAdjustGamma(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Gamma, value);
        }
    }
}
