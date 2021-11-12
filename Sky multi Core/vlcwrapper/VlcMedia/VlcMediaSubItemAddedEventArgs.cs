using System;

namespace Sky_multi_Core.VlcWrapper
{
    public class VlcMediaSubItemAddedEventArgs : EventArgs
    {
        public VlcMediaSubItemAddedEventArgs(VlcMedia subItemAdded)
        {
            SubItemAdded = subItemAdded;
        }

        public VlcMedia SubItemAdded { get; private set; }
    }
}