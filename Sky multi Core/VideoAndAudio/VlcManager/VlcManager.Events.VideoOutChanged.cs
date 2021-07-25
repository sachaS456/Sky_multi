using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerVideoOutChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerVideoOutChangedEventArgs> VideoOutChanged;

        private void OnMediaPlayerVideoOutChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerVideoOutChanged(args.eventArgsUnion.MediaPlayerVideoOutChanged.NewCount);
        }

        public void OnMediaPlayerVideoOutChanged(int newCount)
        {
            VideoOutChanged?.Invoke(this, new VlcMediaPlayerVideoOutChangedEventArgs(newCount));
        }
    }
}