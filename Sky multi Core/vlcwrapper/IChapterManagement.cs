namespace Sky_multi_Core.VlcWrapper
{
    public interface IChapterManagement
    {
        int Count { get; }
        void Previous();
        void Next();
        int Current { get; set; }
    }
}
