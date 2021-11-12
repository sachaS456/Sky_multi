using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        /// <summary>
        /// Set if, and how, the video title will be shown when media is played.
        /// </summary>
        /// <param name="mediaPlayerInstance">The media player instance</param>
        /// <param name="position">position at which to display the title, or libvlc_position_disable to prevent the title from being displayed</param>
        /// <param name="timeout">title display timeout in milliseconds (ignored if libvlc_position_disable)</param>
        public void SetVideoTitleDisplay(IntPtr mediaPlayerInstance, Position position, int timeout)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_media_player_set_video_title_display(mediaPlayerInstance, position, timeout);
        }
    }
}