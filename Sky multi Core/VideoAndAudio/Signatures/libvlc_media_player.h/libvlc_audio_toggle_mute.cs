using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Toggle mute status.
    /// </summary>
    [LibVlcFunction("libvlc_audio_toggle_mute")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ToggleMute(IntPtr mediaPlayerInstance);
}
