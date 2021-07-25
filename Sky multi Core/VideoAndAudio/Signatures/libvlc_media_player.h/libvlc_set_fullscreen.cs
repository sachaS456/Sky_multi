using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set fullscreen.
    /// </summary>
    [LibVlcFunction("libvlc_set_fullscreen")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetFullScreen(IntPtr mediaPlayerInstance, int value);
}
