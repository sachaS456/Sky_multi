using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerMutedInternalEventCallback;
        public event EventHandler Muted;

        private void OnMediaPlayerMutedInternal(IntPtr ptr)
        {
            OnMediaPlayerMuted();
        }

        public void OnMediaPlayerMuted()
        {
            Muted?.Invoke(this, new EventArgs());
        }
    }
}