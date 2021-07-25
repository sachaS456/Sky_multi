using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetMediaPosition(VlcMediaPlayerInstance mediaPlayerInstance, float position)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetMediaPosition>().Invoke(mediaPlayerInstance, position);
        }
    }
}
