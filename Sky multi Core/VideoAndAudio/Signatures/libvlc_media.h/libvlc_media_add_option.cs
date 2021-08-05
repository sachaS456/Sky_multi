﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Add an option to the media.
    /// </summary>
    [LibVlcFunction("libvlc_media_add_option")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void AddOptionToMedia(IntPtr mediaInstance, Utf8StringHandle mrl);
}
