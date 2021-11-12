using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal bool GetVideoMarqueeEnabled(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Enable) == 1;
        }
        internal string GetVideoMarqueeText(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_video_get_marquee_string(mediaPlayerInstance, VideoMarqueeOptions.Text));
        }
        internal int GetVideoMarqueeColor(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Color);
        }
        internal int GetVideoMarqueeOpacity(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Opacity);
        }
        internal int GetVideoMarqueePosition(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Position);
        }
        internal int GetVideoMarqueeRefresh(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Refresh);
        }
        internal int GetVideoMarqueeSize(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Size);
        }
        internal int GetVideoMarqueeTimeout(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Timeout);
        }
        internal int GetVideoMarqueeX(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.X);
        }
        internal int GetVideoMarqueeY(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Y);
        }
    }
}
