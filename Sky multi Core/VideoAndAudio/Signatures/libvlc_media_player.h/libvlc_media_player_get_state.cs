﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get the media state.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_get_state")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate MediaStates GetMediaPlayerState(IntPtr mediaPlayerInstance);
}
