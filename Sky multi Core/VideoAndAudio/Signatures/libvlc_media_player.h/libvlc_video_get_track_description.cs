using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get the description of available video tracks.
    /// </summary>
    [LibVlcFunction("libvlc_video_get_track_description")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr GetVideoTracksDescriptions(IntPtr mediaPlayerInstance);
}
