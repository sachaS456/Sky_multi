using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public void ReleaseTrackDescription(IntPtr trackDescription)
        {
            VlcNative.libvlc_track_description_list_release(trackDescription);
        }
    }
}
