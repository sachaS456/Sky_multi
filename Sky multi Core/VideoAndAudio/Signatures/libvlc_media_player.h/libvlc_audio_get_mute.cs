using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current mute status.
    /// </summary>
    [LibVlcFunction("libvlc_audio_get_mute")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int IsMute(IntPtr mediaPlayerInstance);
}
