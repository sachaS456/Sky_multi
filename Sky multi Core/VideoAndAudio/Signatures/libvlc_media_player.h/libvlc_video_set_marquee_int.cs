﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set an integer marquee option value.
    /// </summary>
    [LibVlcFunction("libvlc_video_set_marquee_int")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetVideoMarqueeInteger(IntPtr mediaPlayerInstance, VideoMarqueeOptions option, int value);
}
