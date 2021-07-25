using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        public string GetEventTypeName(EventTypes eventType)
        {
            return Utf8InteropStringConverter.Utf8InteropToString(myLibraryLoader.GetInteropDelegate<GetEventTypeName>().Invoke(eventType));
        }
    }
}
