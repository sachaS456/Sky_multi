using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerEndReachedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEndReachedEventArgs> EndReached;

        private void OnMediaPlayerEndReachedInternal(IntPtr ptr)
        {
            OnMediaPlayerEndReached();
        }

        public void OnMediaPlayerEndReached()
        {
            var del = EndReached;
            if (del != null)
                del(this, new VlcMediaPlayerEndReachedEventArgs());
        }
    }
}
