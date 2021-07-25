using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set current audio channel.
    /// </summary>
    [LibVlcFunction("libvlc_audio_set_channel")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetAudioChannel(IntPtr mediaPlayerInstance, int channel);
}
