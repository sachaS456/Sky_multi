using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerEsDeletedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEsChangedEventArgs> EsDeleted;

        private void OnMediaPlayerEsDeletedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerEsDeleted(args.eventArgsUnion.MediaPlayerEsChanged);
        }

        public void OnMediaPlayerEsDeleted(MediaPlayerEsChanged eventArgs)
        {
            EsDeleted?.Invoke(this, new VlcMediaPlayerEsChangedEventArgs(eventArgs.TrackType, eventArgs.Id));
        }
    }
}