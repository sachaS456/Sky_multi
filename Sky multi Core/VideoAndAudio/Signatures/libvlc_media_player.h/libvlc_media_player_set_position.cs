using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get media position.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_set_position")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetMediaPosition(IntPtr mediaPlayerInstance, float position);
}
