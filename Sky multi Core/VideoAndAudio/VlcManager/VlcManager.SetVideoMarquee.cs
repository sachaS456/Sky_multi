using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoMarqueeEnabled(VlcMediaPlayerInstance mediaPlayerInstance, bool value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Enable, value ? 1 : 0);
        }
        internal void SetVideoMarqueeText(VlcMediaPlayerInstance mediaPlayerInstance, string value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            using (var valueInterop = Utf8InteropStringConverter.ToUtf8StringHandle(value))
            {
                myLibraryLoader.GetInteropDelegate<SetVideoMarqueeString>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Text, valueInterop);
            }
        }
        internal void SetVideoMarqueeColor(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Color, value);
        }
        internal void SetVideoMarqueeOpacity(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Opacity, value);
        }
        internal void SetVideoMarqueePosition(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Position, value);
        }
        internal void SetVideoMarqueeRefresh(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Refresh, value);
        }
        internal void SetVideoMarqueeSize(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Size, value);
        }
        internal void SetVideoMarqueeTimeout(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Timeout, value);
        }
        internal void SetVideoMarqueeX(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.X, value);
        }
        internal void SetVideoMarqueeY(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoMarqueeInteger>().Invoke(mediaPlayerInstance, VideoMarqueeOptions.Y, value);
        }
    }
}
