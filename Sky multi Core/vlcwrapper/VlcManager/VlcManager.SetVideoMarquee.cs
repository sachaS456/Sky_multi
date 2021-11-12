using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetVideoMarqueeEnabled(VlcMediaPlayerInstance mediaPlayerInstance, bool value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Enable, value ? 1 : 0);
        }
        internal void SetVideoMarqueeText(VlcMediaPlayerInstance mediaPlayerInstance, string value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            using (var valueInterop = Utf8InteropStringConverter.ToUtf8StringHandle(value))
            {
                VlcNative.libvlc_video_set_marquee_string(mediaPlayerInstance, VideoMarqueeOptions.Text, valueInterop);
            }
        }
        internal void SetVideoMarqueeColor(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Color, value);
        }
        internal void SetVideoMarqueeOpacity(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Opacity, value);
        }
        internal void SetVideoMarqueePosition(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Position, value);
        }
        internal void SetVideoMarqueeRefresh(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Refresh, value);
        }
        internal void SetVideoMarqueeSize(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Size, value);
        }
        internal void SetVideoMarqueeTimeout(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Timeout, value);
        }
        internal void SetVideoMarqueeX(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.X, value);
        }
        internal void SetVideoMarqueeY(VlcMediaPlayerInstance mediaPlayerInstance, int value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_marquee_int(mediaPlayerInstance, VideoMarqueeOptions.Y, value);
        }
    }
}
