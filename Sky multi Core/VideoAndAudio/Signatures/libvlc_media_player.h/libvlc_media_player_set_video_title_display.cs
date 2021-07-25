using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    [LibVlcFunction("libvlc_media_player_set_video_title_display")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SetVideoTitleDisplay(IntPtr mp, Position position, int timeout);
}
