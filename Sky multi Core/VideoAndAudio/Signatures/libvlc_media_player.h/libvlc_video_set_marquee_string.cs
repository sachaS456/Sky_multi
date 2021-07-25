using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set a string marquee option value.
    /// </summary>
    [LibVlcFunction("libvlc_video_set_marquee_string")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetVideoMarqueeString(IntPtr mediaPlayerInstance, VideoMarqueeOptions option, Utf8StringHandle value);
}
