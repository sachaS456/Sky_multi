using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{

    /// <summary>
    /// CCallback function notification.
    /// </summary>
    [LibVlcFunction("libvlc_callback_t")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventCallback(IntPtr args);
}
