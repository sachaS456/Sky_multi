using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetAudioChannel(VlcMediaPlayerInstance mediaPlayerInstance, int channel)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetAudioChannel>().Invoke(mediaPlayerInstance, channel);
        }
    }
}
