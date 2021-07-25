using System;

namespace Sky_multi_Core
{
    internal sealed class VlcMediaEventManagerInstance : VlcEventManagerInstance
    {
        internal VlcMediaEventManagerInstance(IntPtr pointer)
            : base(ref pointer)
        {
        }
    }
}