using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerBufferingInternalEventCallback;
        public event EventHandler<VlcMediaPlayerBufferingEventArgs> Buffering;

        private void OnMediaPlayerBufferingInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerBuffering(args.eventArgsUnion.MediaPlayerBuffering.NewCache);
        }

        public void OnMediaPlayerBuffering(float newCache)
        {
            Buffering?.Invoke(this, new VlcMediaPlayerBufferingEventArgs(newCache));
        }
    }
}