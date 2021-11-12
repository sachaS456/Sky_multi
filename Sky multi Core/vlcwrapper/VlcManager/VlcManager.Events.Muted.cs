using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
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