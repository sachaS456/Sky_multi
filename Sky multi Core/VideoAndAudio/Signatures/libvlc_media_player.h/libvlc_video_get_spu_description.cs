using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get the description of available video subtitles.
    /// </summary>
    [LibVlcFunction("libvlc_video_get_spu_description")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr GetVideoSpuDescription(IntPtr mediaPlayerInstance);
}
