using System;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed class VlcMediaPlayerLengthChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerLengthChangedEventArgs(long newLength)
        {
            NewLength = newLength;
        }

        public long NewLength { get; private set; }
    }
}
