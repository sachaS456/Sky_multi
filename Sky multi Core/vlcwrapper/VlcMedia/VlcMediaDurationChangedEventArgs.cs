using System;

namespace Sky_multi_Core.VlcWrapper
{
    public class VlcMediaDurationChangedEventArgs : EventArgs
    {
        public VlcMediaDurationChangedEventArgs(long newDuration)
        {
            NewDuration = newDuration;
        }

        public long NewDuration { get; private set; }
    }
}