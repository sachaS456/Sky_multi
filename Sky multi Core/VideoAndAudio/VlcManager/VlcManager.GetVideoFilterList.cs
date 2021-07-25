using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        public IntPtr GetVideoFilterList()
        {
            return myLibraryLoader.GetInteropDelegate<GetVideoFilterList>().Invoke(myVlcInstance);
        }
    }
}
