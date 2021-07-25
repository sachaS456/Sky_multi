using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void ReleaseMediaPlayer(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                return;
            try
            {
                myLibraryLoader.GetInteropDelegate<ReleaseMediaPlayer>().Invoke(mediaPlayerInstance);
            }
            finally
            {
                mediaPlayerInstance.Pointer = IntPtr.Zero;
            }
        }
    }
}
