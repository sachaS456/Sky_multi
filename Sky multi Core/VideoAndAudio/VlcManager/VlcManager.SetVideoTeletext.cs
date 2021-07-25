using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoTeletext(VlcMediaPlayerInstance mediaPlayerInstance, int teletextPage)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoTeletext>().Invoke(mediaPlayerInstance, teletextPage);
        }
    }
}
