using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set logo option as integer.
    /// </summary>
    [LibVlcFunction("libvlc_video_set_logo_int")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetVideoLogoInteger(IntPtr mediaPlayerInstance, VideoLogoOptions option, int value);
}
