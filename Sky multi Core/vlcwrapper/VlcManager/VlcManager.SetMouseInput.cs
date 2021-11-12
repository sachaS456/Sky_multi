using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        /// <summary>
        /// Enables/Disables libvlc's handling of mouse input
        /// </summary>
        /// <param name="mediaPlayerInstance">The media player instance</param>
        /// <param name="on"><c>true</c> if libvlc should handle mouse events, <c>false</c> otherwise</param>
        /// <remarks>Must be called before the stream has started playing. This is not applicable to the WPF control.</remarks>     
        internal void SetMouseInput(VlcMediaPlayerInstance mediaPlayerInstance, bool on)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_mouse_input(mediaPlayerInstance, on ? 1u : 0u);
        }
    }
}