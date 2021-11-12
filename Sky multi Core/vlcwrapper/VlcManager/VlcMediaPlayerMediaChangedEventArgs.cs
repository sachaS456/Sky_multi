using System;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed class VlcMediaPlayerMediaChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerMediaChangedEventArgs(VlcMedia newMedia)
        {
            NewMedia = newMedia;
        }

        public VlcMedia NewMedia { get; private set; }
    }
}