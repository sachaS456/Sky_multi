using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetMediaChapter(VlcMediaPlayerInstance mediaPlayerInstance, int chapter)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetMediaChapter>().Invoke(mediaPlayerInstance, chapter);
        }
    }
}
