using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Check if media player can pause.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_can_pause")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int IsPausable(IntPtr mediaPlayerInstance);
}
