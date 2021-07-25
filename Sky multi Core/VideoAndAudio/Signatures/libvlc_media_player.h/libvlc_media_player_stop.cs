using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Stop.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_stop")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void Stop(IntPtr mediaPlayerInstance);
}
