using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    /// Get media chapter count.
    /// </summary>
    [LibVlcFunction("libvlc_media_player_get_chapter_count")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GetMediaChapterCount(IntPtr mediaPlayerInstance);
}
