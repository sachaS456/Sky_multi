using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public partial class VlcMedia
    {
        private EventCallback myOnMediaSubItemAddedInternalEventCallback;
        public event EventHandler<VlcMediaSubItemAddedEventArgs> SubItemAdded;

        private void OnMediaSubItemAddedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaSubItemAdded(new VlcMedia(myVlcMediaPlayer, VlcMediaInstance.New(myVlcMediaPlayer, args.eventArgsUnion.MediaSubItemAdded.NewChild)));
        }

        public void OnMediaSubItemAdded(VlcMedia newSubItemAdded)
        {
            SubItemAdded?.Invoke(this, new VlcMediaSubItemAddedEventArgs(newSubItemAdded));
        }
    }
}