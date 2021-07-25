using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        public void ReleaseTrackDescription(IntPtr trackDescription)
        {
            myLibraryLoader.GetInteropDelegate<ReleaseTrackDescription>().Invoke(trackDescription);
        }
    }
}
