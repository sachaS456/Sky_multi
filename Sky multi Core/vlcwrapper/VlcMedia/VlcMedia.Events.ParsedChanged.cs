using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public partial class VlcMedia
    {
        private EventCallback myOnMediaParsedChangedInternalEventCallback;
        public event EventHandler<VlcMediaParsedChangedEventArgs> ParsedChanged;

        private void OnMediaParsedChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaParsedChanged(args.eventArgsUnion.MediaParsedChanged.NewStatus);
        }

        public void OnMediaParsedChanged(int newStatus)
        {
            ParsedChanged?.Invoke(this, new VlcMediaParsedChangedEventArgs(newStatus));
        }
    }
}