using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal bool GetVideoLogoEnabled(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoLogoInteger>().Invoke(mediaPlayerInstance, VideoLogoOptions.Enable) == 1;
        }

        internal int GetVideoLogoX(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoLogoInteger>().Invoke(mediaPlayerInstance, VideoLogoOptions.X);
        }
        internal int GetVideoLogoY(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoLogoInteger>().Invoke(mediaPlayerInstance, VideoLogoOptions.Y);
        }
        internal int GetVideoLogoDelay(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoLogoInteger>().Invoke(mediaPlayerInstance, VideoLogoOptions.Delay);
        }
        internal int GetVideoLogoRepeat(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoLogoInteger>().Invoke(mediaPlayerInstance, VideoLogoOptions.Repeat);
        }
        internal int GetVideoLogoOpacity(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoLogoInteger>().Invoke(mediaPlayerInstance, VideoLogoOptions.Opacity);
        }
        internal int GetVideoLogoPosition(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return myLibraryLoader.GetInteropDelegate<GetVideoLogoInteger>().Invoke(mediaPlayerInstance, VideoLogoOptions.Position);
        }
    }
}
