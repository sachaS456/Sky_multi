using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// It will release the list of available audio outputs.
    /// </summary>
    [LibVlcFunction("libvlc_audio_output_list_release")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ReleaseAudioOutputDescription(IntPtr audioOutput);
}
