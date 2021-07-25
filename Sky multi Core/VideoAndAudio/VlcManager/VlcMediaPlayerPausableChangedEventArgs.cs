using System;

namespace Sky_multi_Core
{
    public sealed class VlcMediaPlayerPausableChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerPausableChangedEventArgs(bool paused)
        {
            IsPaused = paused;
        }

        public bool IsPaused { get; private set; }
    }
}