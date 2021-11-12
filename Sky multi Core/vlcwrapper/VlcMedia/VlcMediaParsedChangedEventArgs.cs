using System;

namespace Sky_multi_Core.VlcWrapper
{
    public class VlcMediaParsedChangedEventArgs : EventArgs
    {
        public VlcMediaParsedChangedEventArgs(int newStatus)
        {
            NewStatus = newStatus;
        }

        public int NewStatus { get; private set; }
    }
}