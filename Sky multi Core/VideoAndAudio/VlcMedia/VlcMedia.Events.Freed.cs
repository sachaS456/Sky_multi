using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public partial class VlcMedia
    {
        private EventCallback myOnMediaFreedInternalEventCallback;
        public event EventHandler<VlcMediaFreedEventArgs> Freed;

        private void OnMediaFreedInternal(IntPtr ptr)
        {
            OnMediaFreed();
        }

        public void OnMediaFreed()
        {
            Freed?.Invoke(this, new VlcMediaFreedEventArgs());
        }
    }
}