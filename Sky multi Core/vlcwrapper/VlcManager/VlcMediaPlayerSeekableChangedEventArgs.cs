using System;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed class VlcMediaPlayerSeekableChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerSeekableChangedEventArgs(int newSeekable)
        {
            NewSeekable = newSeekable;
        }

        public int NewSeekable { get; private set; }
    }
}