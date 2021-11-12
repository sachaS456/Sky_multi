using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerEsAddedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEsChangedEventArgs> EsAdded;

        private void OnMediaPlayerEsAddedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerEsAdded(args.eventArgsUnion.MediaPlayerEsChanged);
        }

        public void OnMediaPlayerEsAdded(MediaPlayerEsChanged eventArgs)
        {
            EsAdded?.Invoke(this, new VlcMediaPlayerEsChangedEventArgs(eventArgs.TrackType, eventArgs.Id));
        }
    }
}