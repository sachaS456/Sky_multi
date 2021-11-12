using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
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
