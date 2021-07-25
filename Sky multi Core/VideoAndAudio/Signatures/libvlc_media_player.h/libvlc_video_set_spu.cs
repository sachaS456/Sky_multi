using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set new video subtitle.
    /// </summary>
    [LibVlcFunction("libvlc_video_set_spu")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetVideoSpu(IntPtr mediaPlayerInstance, int spu);
}
