using System;

namespace Sky_multi_Core
{
    public sealed class VlcMediaPlayerTitleChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerTitleChangedEventArgs(int newTitle)
        {
            NewTitle = newTitle;
        }

        public int NewTitle { get; private set; }
    }
}