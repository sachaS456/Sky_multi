using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set audio track.
    /// </summary>
    [LibVlcFunction("libvlc_audio_set_track")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetAudioTrack(IntPtr mediaPlayerInstance, int trackId);
}
