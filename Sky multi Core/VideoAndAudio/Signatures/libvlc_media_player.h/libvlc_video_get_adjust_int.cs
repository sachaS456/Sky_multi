using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get an integer adjust option value.
    /// </summary>
    [LibVlcFunction("libvlc_video_get_adjust_int")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetVideoAdjustInteger(IntPtr mediaPlayerInstance, VideoAdjustOptions option);
}
