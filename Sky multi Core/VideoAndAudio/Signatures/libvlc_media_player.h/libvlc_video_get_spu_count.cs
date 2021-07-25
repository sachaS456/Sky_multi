using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get the number of available video subtitles.
    /// </summary>
    [LibVlcFunction("libvlc_video_get_spu_count")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetVideoSpuCount(IntPtr mediaPlayerInstance);
}
