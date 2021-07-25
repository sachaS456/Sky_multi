using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get media chapter (if applicable).
    /// </summary>
    [LibVlcFunction("libvlc_media_player_get_chapter")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetMediaChapter(IntPtr mediaPlayerInstance);
}
