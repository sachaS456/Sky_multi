using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct LibvlcAudioOutputDeviceT
    {
        public IntPtr Next;
        public IntPtr DeviceIdentifier;
        public IntPtr Description;
    }
}