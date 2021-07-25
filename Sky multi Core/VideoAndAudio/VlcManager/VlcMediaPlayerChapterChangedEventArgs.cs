using System;

namespace Sky_multi_Core
{
    public class VlcMediaPlayerChapterChangedEventArgs : EventArgs
    {
        private int NewChapter { get; }

        public VlcMediaPlayerChapterChangedEventArgs(int newChapter)
        {
            this.NewChapter = newChapter;
        }
    }
}