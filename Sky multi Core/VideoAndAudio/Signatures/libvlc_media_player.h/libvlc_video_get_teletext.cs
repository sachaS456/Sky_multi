using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current teletext page requested.
    /// </summary>
    [LibVlcFunction("libvlc_video_get_teletext")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetVideoTeletext(IntPtr mediaPlayerInstance);
}
