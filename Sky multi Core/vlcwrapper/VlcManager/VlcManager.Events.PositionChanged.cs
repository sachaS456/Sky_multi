using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerPositionChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPositionChangedEventArgs> PositionChanged;

        private void OnMediaPlayerPositionChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerPositionChanged(args.eventArgsUnion.MediaPlayerPositionChanged.NewPosition);
        }

        public void OnMediaPlayerPositionChanged(float newPosition)
        {
            PositionChanged?.Invoke(this, new VlcMediaPlayerPositionChangedEventArgs(newPosition));
        }
    }
}