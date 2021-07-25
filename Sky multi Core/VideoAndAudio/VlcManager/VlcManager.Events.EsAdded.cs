using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
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