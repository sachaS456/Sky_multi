using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void ClearLastErrorMessage()
        {
            VlcNative.libvlc_clearerr();
        }
    }
}
