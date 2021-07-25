using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Returns a list of audio filters that are available.
    /// </summary>
    [LibVlcFunction("libvlc_audio_filter_list_get")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr GetAudioFilterList(IntPtr instance);
}
