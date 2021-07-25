using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current state of media descriptor object.
    /// </summary>
    [LibVlcFunction("libvlc_media_get_state")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate MediaStates GetMediaState(IntPtr mediaInstance);
}
