using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Navigate through DVD Menu.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_navigate")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void Navigate(IntPtr mediaPlayerInstance, NavigateModes navigate);
}
