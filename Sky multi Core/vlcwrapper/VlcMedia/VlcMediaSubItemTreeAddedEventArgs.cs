using System;

namespace Sky_multi_Core.VlcWrapper
{
    public class VlcMediaSubItemTreeAddedEventArgs : EventArgs
    {
        public VlcMediaSubItemTreeAddedEventArgs(VlcMedia subItemTreeAdded)
        {
            SubItemTreeAdded = subItemTreeAdded;
        }

        public VlcMedia SubItemTreeAdded { get; private set; }
    }
}