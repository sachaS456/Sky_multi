using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerUncorkedInternalEventCallback;
        public event EventHandler Uncorked;

        private void OnMediaPlayerUncorkedInternal(IntPtr ptr)
        {
            OnMediaPlayerUncorked();
        }

        public void OnMediaPlayerUncorked()
        {
            Uncorked?.Invoke(this, new EventArgs());
        }
    }
}