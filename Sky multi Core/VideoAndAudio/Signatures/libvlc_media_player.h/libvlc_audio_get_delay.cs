using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current audio delay.
    /// </summary>
    [LibVlcFunction("libvlc_audio_get_delay")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate long GetAudioDelay(IntPtr mediaPlayerInstance);
}
