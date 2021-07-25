using System;
using System.IO;

namespace Sky_multi_Core
{
    internal class StreamData
    {
        public IntPtr Handle { get; set; }
        public Stream Stream { get; set; }
        public byte[] Buffer { get; set; }
    }
}
