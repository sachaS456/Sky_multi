using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaInstance CreateNewMediaFromPath(string mrl)
        {
            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(mrl))
            {
                return VlcMediaInstance.New(this, myLibraryLoader.GetInteropDelegate<CreateNewMediaFromPath>().Invoke(myVlcInstance, handle));
            }
        }
    }
}
