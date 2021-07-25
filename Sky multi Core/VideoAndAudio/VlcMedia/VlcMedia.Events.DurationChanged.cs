using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public partial class VlcMedia
    {
        public event EventHandler<VlcMediaDurationChangedEventArgs> DurationChanged;

        private void OnMediaDurationChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaDurationChanged(args.eventArgsUnion.MediaDurationChanged.NewDuration);
        }

        public void OnMediaDurationChanged(long newDuration)
        {
            DurationChanged?.Invoke(this, new VlcMediaDurationChangedEventArgs(newDuration));
        }
    }
}