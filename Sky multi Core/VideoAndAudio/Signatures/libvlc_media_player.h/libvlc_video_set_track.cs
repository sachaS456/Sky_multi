using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set video track.
    /// </summary>
    [LibVlcFunction("libvlc_video_set_track")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetVideoTrack(IntPtr mediaPlayerInstance, int trackId);
}
