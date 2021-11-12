using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerPausableChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPausableChangedEventArgs> PausableChanged;

        private void OnMediaPlayerPausableChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerPausableChanged(args.eventArgsUnion.MediaPlayerPausableChanged.NewPausable == 1);
        }

        public void OnMediaPlayerPausableChanged(bool paused)
        {
            PausableChanged?.Invoke(this, new VlcMediaPlayerPausableChangedEventArgs(paused));
        }
    }
}