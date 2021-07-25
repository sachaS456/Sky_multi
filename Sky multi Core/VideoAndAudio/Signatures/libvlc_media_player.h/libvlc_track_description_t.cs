using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TrackDescriptionStructure
    {
        public int Id;
        public IntPtr Name;
        public IntPtr NextTrackDescription;
    }
}
