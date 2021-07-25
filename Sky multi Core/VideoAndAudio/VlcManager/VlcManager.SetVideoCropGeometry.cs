using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoCropGeometry(VlcMediaPlayerInstance mediaPlayerInstance, string cropGeometry)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");

            using (var cropGeometryInterop = Utf8InteropStringConverter.ToUtf8StringHandle(cropGeometry))
            {
                myLibraryLoader.GetInteropDelegate<SetVideoCropGeometry>()
                    .Invoke(mediaPlayerInstance, cropGeometryInterop);
            }
        }
    }
}
