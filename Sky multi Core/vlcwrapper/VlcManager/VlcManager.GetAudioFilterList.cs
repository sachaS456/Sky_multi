using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public IntPtr GetAudioFilterList()
        {
            return VlcNative.libvlc_audio_filter_list_get(myVlcInstance);
        }
    }
}
