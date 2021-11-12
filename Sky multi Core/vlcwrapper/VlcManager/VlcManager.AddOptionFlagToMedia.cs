using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void AddOptionFlagToMedia(VlcMediaInstance mediaInstance, string option, uint flag)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");

            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(option))
            {
                VlcNative.libvlc_media_add_option_flag(mediaInstance, handle, flag);
            }
        }
    }
}
