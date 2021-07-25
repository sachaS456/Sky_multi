using System;

namespace Sky_multi_Core
{
    public sealed class VlcMediaPlayerTimeChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerTimeChangedEventArgs(long newTime)
        {
            NewTime = newTime;
        }

        public long NewTime { get; private set; }
    }
}