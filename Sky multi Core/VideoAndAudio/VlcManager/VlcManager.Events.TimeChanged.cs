using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerTimeChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerTimeChangedEventArgs> TimeChanged;

        private void OnMediaPlayerTimeChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerTimeChanged(args.eventArgsUnion.MediaPlayerTimeChanged.NewTime);
        }

        public void OnMediaPlayerTimeChanged(long newTime)
        {
            TimeChanged?.Invoke(this, new VlcMediaPlayerTimeChangedEventArgs(newTime));
        }
    }
}