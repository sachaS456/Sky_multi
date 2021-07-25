using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerMediaChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerMediaChangedEventArgs> MediaChanged;

        private void OnMediaPlayerMediaChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerMediaChanged(new VlcMedia(this, VlcMediaInstance.New(this, args.eventArgsUnion.MediaPlayerMediaChanged.MediaInstance)));
        }

        public void OnMediaPlayerMediaChanged(VlcMedia media)
        {
            MediaChanged?.Invoke(this, new VlcMediaPlayerMediaChangedEventArgs(media));
        }
    }
}