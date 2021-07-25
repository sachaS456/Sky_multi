using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Is the player able to play.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_will_play")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int CouldPlay(IntPtr mediaPlayerInstance);
}
