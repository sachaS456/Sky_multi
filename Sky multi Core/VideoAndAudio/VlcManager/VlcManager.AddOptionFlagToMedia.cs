using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void AddOptionFlagToMedia(VlcMediaInstance mediaInstance, string option, uint flag)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");

            using (var handle = Utf8InteropStringConverter.ToUtf8StringHandle(option))
            {
                myLibraryLoader.GetInteropDelegate<AddOptionFlagToMedia>().Invoke(mediaInstance, handle, flag);
            }
        }
    }
}
