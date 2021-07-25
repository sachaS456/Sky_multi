using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public partial class VlcMedia
    {
        private EventCallback myOnMediaMetaChangedInternalEventCallback;
        public event EventHandler<VlcMediaMetaChangedEventArgs> MetaChanged;

        private void OnMediaMetaChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaMetaChanged(args.eventArgsUnion.MediaMetaChanged.MetaType);
        }

        public void OnMediaMetaChanged(MediaMetadatas metaType)
        {
            MetaChanged?.Invoke(this, new VlcMediaMetaChangedEventArgs(metaType));
        }
    }
}