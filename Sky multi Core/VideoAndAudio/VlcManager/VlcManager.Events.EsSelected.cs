using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerEsSelectedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEsChangedEventArgs> EsSelected;

        private void OnMediaPlayerEsSelectedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerEsSelected(args.eventArgsUnion.MediaPlayerEsChanged);
        }

        public void OnMediaPlayerEsSelected(MediaPlayerEsChanged eventArgs)
        {
            EsSelected?.Invoke(this, new VlcMediaPlayerEsChangedEventArgs(eventArgs.TrackType, eventArgs.Id));
        }
    }
}