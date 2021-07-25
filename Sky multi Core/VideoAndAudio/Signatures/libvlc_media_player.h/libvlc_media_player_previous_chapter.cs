using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set previous media chapter (if applicable).
    /// </summary>
    [LibVlcFunction("libvlc_media_player_previous_chapter")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetPreviousMediaChapter(IntPtr mediaPlayerInstance);
}
