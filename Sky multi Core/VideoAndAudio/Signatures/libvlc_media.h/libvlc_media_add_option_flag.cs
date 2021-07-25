using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Add an option to the media with configurable flags.
    /// </summary>
    [LibVlcFunction("libvlc_media_add_option_flag")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void AddOptionFlagToMedia(IntPtr mediaInstance, Utf8StringHandle mrl, uint flag);
}
