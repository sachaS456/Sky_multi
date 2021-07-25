using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct AudioOutputDescriptionStructureInternal
    {
        public IntPtr Name;
        public IntPtr Description;
        public IntPtr NextAudioOutputDescription;
    }
    
    public struct AudioOutputDescriptionStructure
    {
        public string Name;
        public string Description;
    }
}
