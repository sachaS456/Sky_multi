using System;

namespace Sky_multi_Core
{
    public sealed class VlcMediaPlayerPositionChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerPositionChangedEventArgs(float newPosition)
        {
            NewPosition = newPosition;
        }

        public float NewPosition { get; private set; }
    }
}