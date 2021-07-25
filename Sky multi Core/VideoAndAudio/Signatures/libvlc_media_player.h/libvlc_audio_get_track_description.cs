using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get the description of available audio tracks.
    /// </summary>
    [LibVlcFunction("libvlc_audio_get_track_description")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr GetAudioTracksDescriptions(IntPtr mediaPlayerInstance);
}
