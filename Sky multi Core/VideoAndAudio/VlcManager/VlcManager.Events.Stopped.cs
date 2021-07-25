using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
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