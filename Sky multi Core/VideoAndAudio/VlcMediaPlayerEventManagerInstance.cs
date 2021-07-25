using System;

namespace Sky_multi_Core
{
    internal sealed class VlcMediaPlayerEventManagerInstance : VlcEventManagerInstance
    {
        internal VlcMediaPlayerEventManagerInstance(IntPtr pointer)
            : base(ref pointer)
        {
        }
    }
}