using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal VlcMediaInstance CreateNewMediaFromFileDescriptor(int fileDescriptor)
        {
            return VlcMediaInstance.New(this, VlcNative.libvlc_media_new_fd(myVlcInstance, fileDescriptor));
        }
    }
}
