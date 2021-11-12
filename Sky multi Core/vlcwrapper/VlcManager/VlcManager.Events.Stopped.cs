using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerStoppedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerStoppedEventArgs> Stopped;

        private void OnMediaPlayerStoppedInternal(IntPtr ptr)
        {
            OnMediaPlayerStopped();
        }

        public void OnMediaPlayerStopped()
        {
            var del = Stopped;
            if (del != null)
                del(this, new VlcMediaPlayerStoppedEventArgs());
        }
    }
}