using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        public void ReleaseModuleDescriptionInstance(IntPtr moduleDescriptionInstance)
        {
            myLibraryLoader.GetInteropDelegate<ReleaseModuleDescription>().Invoke(moduleDescriptionInstance);
        }
    }
}
