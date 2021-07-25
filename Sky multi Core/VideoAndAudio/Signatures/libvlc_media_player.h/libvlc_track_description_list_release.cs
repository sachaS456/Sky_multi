using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// It will release the libvlc_track_description_t object.
    /// </summary>
    [LibVlcFunction("libvlc_track_description_list_release")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ReleaseTrackDescription(IntPtr trackDescription);
}
