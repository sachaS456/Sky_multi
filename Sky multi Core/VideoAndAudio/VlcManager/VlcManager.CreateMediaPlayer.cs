using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaPlayerInstance CreateMediaPlayer()
        {
            return new VlcMediaPlayerInstance(this, myLibraryLoader.GetInteropDelegate<CreateMediaPlayer>().Invoke(myVlcInstance));
        }
    }
}
