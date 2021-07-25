using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaPlayerInstance CreateMediaPlayerFromMedia(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            return new VlcMediaPlayerInstance(this, myLibraryLoader.GetInteropDelegate<CreateMediaPlayerFromMedia>().Invoke(mediaInstance));
        }
    }
}
