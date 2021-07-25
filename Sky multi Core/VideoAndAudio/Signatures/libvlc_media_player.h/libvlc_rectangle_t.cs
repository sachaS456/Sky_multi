using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Top;
        public int Left;
        public int Bottom;
        public int Right;
    }
}
