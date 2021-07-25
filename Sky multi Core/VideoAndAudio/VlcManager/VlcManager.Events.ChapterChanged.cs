using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerChapterChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerChapterChangedEventArgs> ChapterChanged;

        private void OnMediaPlayerChapterChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerChapterChanged(args.eventArgsUnion.MediaPlayerChapterChanged.NewChapter);
        }

        public void OnMediaPlayerChapterChanged(int newChapter)
        {
            ChapterChanged?.Invoke(this, new VlcMediaPlayerChapterChangedEventArgs(newChapter));
        }
    }
}