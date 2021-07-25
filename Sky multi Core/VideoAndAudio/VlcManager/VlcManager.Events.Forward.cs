using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerForwardInternalEventCallback;
        public event EventHandler<VlcMediaPlayerForwardEventArgs> Forward;

        private void OnMediaPlayerForwardInternal(IntPtr ptr)
        {
            OnMediaPlayerForward();
        }

        public void OnMediaPlayerForward()
        {
            var del = Forward;
            if (del != null)
                del(this, new VlcMediaPlayerForwardEventArgs());
        }
    }
}