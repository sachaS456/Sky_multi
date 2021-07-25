using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerPausedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPausedEventArgs> Paused;

        private void OnMediaPlayerPausedInternal(IntPtr ptr)
        {
            OnMediaPlayerPaused();
        }

        public void OnMediaPlayerPaused()
        {
            var del = Paused;
            if (del != null)
                del(this, new VlcMediaPlayerPausedEventArgs());
        }
    }
}