using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get media fps rate.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_get_fps")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate float GetFramesPerSecond(IntPtr mediaPlayerInstance);
}
