using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public string GetEventTypeName(EventTypes eventType)
        {
            return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_event_type_name(eventType));
        }
    }
}
