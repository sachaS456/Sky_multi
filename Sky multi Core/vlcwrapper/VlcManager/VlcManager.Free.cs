using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void Free(IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return;
            VlcNative.libvlc_free(instance);
        }
    }
}
