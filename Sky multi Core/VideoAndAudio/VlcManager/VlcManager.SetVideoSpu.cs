using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoSpu(VlcMediaPlayerInstance mediaPlayerInstance, TrackDescriptionStructure trackDescription)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            SetVideoSpu(mediaPlayerInstance, trackDescription.Id);
        }

        internal void SetVideoSpu(VlcMediaPlayerInstance mediaPlayerInstance, int id)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoSpu>().Invoke(mediaPlayerInstance, id);
        }
    }
}
