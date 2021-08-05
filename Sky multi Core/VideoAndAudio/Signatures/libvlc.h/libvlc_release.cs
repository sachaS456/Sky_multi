﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Destroy libvlc instance.
    /// </summary>
    [LibVlcFunction("libvlc_release")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ReleaseInstance(IntPtr instance);
}
