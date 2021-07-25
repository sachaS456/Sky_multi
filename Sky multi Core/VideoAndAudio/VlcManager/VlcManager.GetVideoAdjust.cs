using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal bool GetVideoAdjustEnabled(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoAdjustInteger>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Enable) == 1;
        }

        internal float GetVideoAdjustContrast(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Contrast);
        }

        internal float GetVideoAdjustBrightness(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Brightness);
        }

        internal float GetVideoAdjustHue(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Hue);
        }

        internal float GetVideoAdjustSaturation(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Saturation);
        }

        internal float GetVideoAdjustGamma(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoAdjustFloat>().Invoke(mediaPlayerInstance, VideoAdjustOptions.Gamma);
        }
    }
}
