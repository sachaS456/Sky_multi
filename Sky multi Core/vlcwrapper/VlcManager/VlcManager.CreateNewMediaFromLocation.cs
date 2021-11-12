using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal VlcMediaInstance CreateNewMediaFromLocation(ref string mrl)
        {
            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(mrl))
            {
                return VlcMediaInstance.New(this, VlcNative.libvlc_media_new_location(myVlcInstance, handle));
            }
        }

        internal VlcMediaInstance CreateNewMediaFromLocation(string mrl)
        {
            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(mrl))
            {
                return VlcMediaInstance.New(this, VlcNative.libvlc_media_new_location(myVlcInstance, handle));
            }
        }
    }
}
