using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void ClearLastErrorMessage()
        {
            myLibraryLoader.GetInteropDelegate<ClearLastErrorMessage>().Invoke();
        }
    }
}
