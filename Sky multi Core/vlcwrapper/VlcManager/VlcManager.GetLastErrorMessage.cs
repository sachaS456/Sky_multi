using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public string GetLastErrorMessage()
        {
            return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_errmsg());
        }
    }
}
