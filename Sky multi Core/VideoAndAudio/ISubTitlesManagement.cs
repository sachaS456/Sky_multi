using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public interface ISubTitlesManagement : IEnumerableManagement<TrackDescription>
    {
        long Delay { get; set; }
    }

}
