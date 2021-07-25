using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current audio channel.
    /// </summary>
    [LibVlcFunction("libvlc_audio_get_channel")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetAudioChannel(IntPtr mediaPlayerInstance);
}
