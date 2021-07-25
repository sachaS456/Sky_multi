using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Set media chapter (if applicable).
    /// </summary>
    [LibVlcFunction("libvlc_media_player_set_chapter")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetMediaChapter(IntPtr mediaPlayerInstance, int chapter);
}
