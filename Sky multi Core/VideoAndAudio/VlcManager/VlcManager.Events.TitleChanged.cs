using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerTitleChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerTitleChangedEventArgs> TitleChanged;

        private void OnMediaPlayerTitleChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerTitleChanged(args.eventArgsUnion.MediaPlayerTitleChanged.NewTitle);
        }

        public void OnMediaPlayerTitleChanged(int newTitle)
        {
            TitleChanged?.Invoke(this, new VlcMediaPlayerTitleChangedEventArgs(newTitle));
        }
    }
}