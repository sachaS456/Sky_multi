using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get the requested media play rate.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_get_rate")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate float GetRate(IntPtr mediaPlayerInstance);
}
