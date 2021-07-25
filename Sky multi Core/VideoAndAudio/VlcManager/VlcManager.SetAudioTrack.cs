using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetAudioTrack(VlcMediaPlayerInstance mediaPlayerInstance, TrackDescriptionStructure trackDescription)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            SetAudioTrack(mediaPlayerInstance, trackDescription.Id);
        }

        internal void SetAudioTrack(VlcMediaPlayerInstance mediaPlayerInstance, int id)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            myLibraryLoader.GetInteropDelegate<SetAudioTrack>().Invoke(mediaPlayerInstance, id);
        }
    }
}
