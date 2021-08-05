﻿using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get an event's type name.
    /// </summary>
    [LibVlcFunction("libvlc_event_type_name")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr GetEventTypeName(EventTypes eventType);
}
