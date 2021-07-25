using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current video subtitle.
    /// </summary>
    [LibVlcFunction("libvlc_video_get_spu")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetVideoSpu(IntPtr mediaPlayerInstance);
}
