using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal VlcMediaInstance CreateNewMediaFromFileDescriptor(int fileDescriptor)
        {
            return VlcMediaInstance.New(this, myLibraryLoader.GetInteropDelegate<CreateNewMediaFromFileDescriptor>().Invoke(myVlcInstance, fileDescriptor));
        }
    }
}
