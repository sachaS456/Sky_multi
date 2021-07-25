using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerUnmutedInternalEventCallback;
        public event EventHandler Unmuted;

        private void OnMediaPlayerUnmutedInternal(IntPtr ptr)
        {
            OnMediaPlayerUnmuted();
        }

        public void OnMediaPlayerUnmuted()
        {
            Unmuted?.Invoke(this, new EventArgs());
        }
    }
}