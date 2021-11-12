using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public class VlcMediaStateChangedEventArgs : EventArgs
    {
        public VlcMediaStateChangedEventArgs(MediaStates state)
        {
            State = state;
        }

        public MediaStates State { get; private set; }
    }
}