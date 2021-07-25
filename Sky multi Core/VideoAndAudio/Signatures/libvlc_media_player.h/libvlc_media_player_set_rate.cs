using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set the requested media play rate.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_set_rate")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetRate(IntPtr mediaPlayerInstance, float rate);
}
