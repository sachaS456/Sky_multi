using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public void ReleaseModuleDescriptionInstance(IntPtr moduleDescriptionInstance)
        {
            VlcNative.libvlc_module_description_list_release(moduleDescriptionInstance);
        }
    }
}
