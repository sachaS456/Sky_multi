using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaPlayerEventManagerInstance GetMediaPlayerEventManager(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return new VlcMediaPlayerEventManagerInstance(myLibraryLoader.GetInteropDelegate<GetMediaPlayerEventManager>().Invoke(mediaPlayerInstance));
        }
    }
}
