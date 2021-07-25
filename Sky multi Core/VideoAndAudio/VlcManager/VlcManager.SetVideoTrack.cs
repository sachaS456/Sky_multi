using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetVideoTrack(VlcMediaPlayerInstance mediaPlayerInstance, TrackDescriptionStructure trackDescription)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            SetVideoTrack(mediaPlayerInstance, trackDescription.Id);
        }

        internal void SetVideoTrack(VlcMediaPlayerInstance mediaPlayerInstance, int id)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetVideoTrack>().Invoke(mediaPlayerInstance, id);
        }
    }
}
