﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Pause or resume (no effect if there is no media) 
    /// </summary>
    [LibVlcFunction("libvlc_media_player_set_pause")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetPause(IntPtr mediaPlayerInstance, bool doPause);
}
