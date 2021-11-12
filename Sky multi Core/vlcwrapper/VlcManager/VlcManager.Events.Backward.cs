using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerBackwardInternalEventCallback;
        public event EventHandler<VlcMediaPlayerBackwardEventArgs> Backward;

        private void OnMediaPlayerBackwardInternal(IntPtr ptr)
        {
            OnMediaPlayerBackward();
        }

        public void OnMediaPlayerBackward()
        {
            var del = Backward;
            if (del != null)
                del(this, new VlcMediaPlayerBackwardEventArgs());
        }
    }
}