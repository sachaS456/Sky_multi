namespace Sky_multi_Core.VlcWrapper
{
    internal static class VlcMediaInstanceExtensions
    {
        internal static VlcMediaInstance AddOptionToMedia(this VlcMediaInstance mediaInstance, VlcManager manager, string option)
        {
            manager.AddOptionToMedia(mediaInstance, option);
            return mediaInstance;
        }

        internal static VlcMediaInstance AddOptionToMedia(this VlcMediaInstance mediaInstance, VlcManager manager, string[] option)
        {
            manager.AddOptionToMedia(mediaInstance, option);
            return mediaInstance;
        }
    }
}
