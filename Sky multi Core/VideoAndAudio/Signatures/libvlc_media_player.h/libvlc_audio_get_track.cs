﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get current audio track.
    /// </summary>
    [LibVlcFunction("libvlc_audio_get_track")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetAudioTrack(IntPtr mediaPlayerInstance);
}
