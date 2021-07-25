using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaInstance CreateNewMediaFromLocation(ref string mrl)
        {
            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(mrl))
            {
                return VlcMediaInstance.New(this, myLibraryLoader.GetInteropDelegate<CreateNewMediaFromLocation>().Invoke(myVlcInstance, handle));
            }
        }

        internal VlcMediaInstance CreateNewMediaFromLocation(string mrl)
        {
            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(mrl))
            {
                return VlcMediaInstance.New(this, myLibraryLoader.GetInteropDelegate<CreateNewMediaFromLocation>().Invoke(myVlcInstance, handle));
            }
        }
    }
}
