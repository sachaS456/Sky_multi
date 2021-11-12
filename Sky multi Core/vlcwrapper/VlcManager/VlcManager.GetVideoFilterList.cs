﻿using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public IntPtr GetVideoFilterList()
        {
            return VlcNative.libvlc_video_filter_list_get(myVlcInstance);
        }
    }
}
