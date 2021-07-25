using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set current mute status.
    /// </summary>
    [LibVlcFunction("libvlc_audio_set_mute")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetMute(IntPtr mediaPlayerInstance, int status);
}
