using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public interface ISubTitlesManagement : IEnumerableManagement<TrackDescription>
    {
        long Delay { get; set; }
    }

}
