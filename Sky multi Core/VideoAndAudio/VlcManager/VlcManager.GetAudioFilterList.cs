using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        public IntPtr GetAudioFilterList()
        {
            return myLibraryLoader.GetInteropDelegate<GetAudioFilterList>().Invoke(myVlcInstance);
        }
    }
}
