using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public string GetCompiler()
        {
            return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_get_compiler());
        }
    }
}
