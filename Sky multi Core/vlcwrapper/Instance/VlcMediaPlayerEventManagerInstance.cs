using System;

namespace Sky_multi_Core.VlcWrapper
{
    internal sealed class VlcMediaPlayerEventManagerInstance : VlcEventManagerInstance
    {
        internal VlcMediaPlayerEventManagerInstance(IntPtr pointer)
            : base(ref pointer)
        {
        }
    }
}