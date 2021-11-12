using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
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