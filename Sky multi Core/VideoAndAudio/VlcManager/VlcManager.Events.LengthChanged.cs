using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerLengthChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerLengthChangedEventArgs> LengthChanged;

        private void OnMediaPlayerLengthChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerLengthChanged(args.eventArgsUnion.MediaPlayerLengthChanged.NewLength);
        }

        public void OnMediaPlayerLengthChanged(long newLength)
        {
            LengthChanged?.Invoke(this, new VlcMediaPlayerLengthChangedEventArgs(newLength));
        }
    }
}
