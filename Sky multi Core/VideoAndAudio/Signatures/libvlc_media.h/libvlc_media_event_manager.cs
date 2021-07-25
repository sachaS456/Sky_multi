using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get event manager from media descriptor object.
    /// </summary>
    [LibVlcFunction("libvlc_media_event_manager")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr GetMediaEventManager(IntPtr mediaInstance);
}
