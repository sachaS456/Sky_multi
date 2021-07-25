using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current fullscreen status. 
    /// </summary>
    [LibVlcFunction("libvlc_get_fullscreen")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetFullScreen(IntPtr mediaPlayerInstance);
}