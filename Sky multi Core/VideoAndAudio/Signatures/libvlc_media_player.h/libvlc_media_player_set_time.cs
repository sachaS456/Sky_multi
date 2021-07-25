using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get time at which movie is playing.
    /// </summary>
    /// <returns>Get the requested movie play time.</returns>
    [LibVlcFunction("libvlc_media_player_set_time")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetTime(IntPtr mediaPlayerInstance, long time);
}