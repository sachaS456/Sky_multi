using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerCorkedInternalEventCallback;
        public event EventHandler Corked;

        private void OnMediaPlayerCorkedInternal(IntPtr ptr)
        {
            OnMediaPlayerCorked();
        }

        public void OnMediaPlayerCorked()
        {
            Corked?.Invoke(this, new EventArgs());
        }
    }
}