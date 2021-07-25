﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set the audio output. Change will be applied after stop and play.
    /// </summary>
    [LibVlcFunction("libvlc_audio_output_set")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int SetAudioOutput(IntPtr mediaPlayerInstance, Utf8StringHandle audioOutputName);
}
