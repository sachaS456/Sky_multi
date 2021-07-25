using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void Free(IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return;
            myLibraryLoader.GetInteropDelegate<Free>().Invoke(instance);
        }
    }
}
