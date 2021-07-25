using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public partial class VlcMedia
    {
        private EventCallback myOnMediaSubItemTreeAddedInternalEventCallback;
        public event EventHandler<VlcMediaSubItemTreeAddedEventArgs> SubItemTreeAdded;

        private void OnMediaSubItemTreeAddedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaSubItemTreeAdded(new VlcMedia(myVlcMediaPlayer, VlcMediaInstance.New(myVlcMediaPlayer, args.eventArgsUnion.MediaSubItemTreeAdded.MediaInstance)));
        }

        public void OnMediaSubItemTreeAdded(VlcMedia newSubItemAdded)
        {
            SubItemTreeAdded?.Invoke(this, new VlcMediaSubItemTreeAddedEventArgs(newSubItemAdded));
        }
    }
}