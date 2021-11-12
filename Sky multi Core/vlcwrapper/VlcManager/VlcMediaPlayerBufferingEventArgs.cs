using System;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed class VlcMediaPlayerBufferingEventArgs : EventArgs
    {
        public VlcMediaPlayerBufferingEventArgs(float newCache)
        {
            NewCache = newCache;
        }

        public float NewCache { get; private set; }
    }
}