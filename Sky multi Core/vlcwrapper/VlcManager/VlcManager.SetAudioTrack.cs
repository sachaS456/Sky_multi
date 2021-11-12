using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
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
            VlcNative.libvlc_audio_set_track(mediaPlayerInstance, id);
        }
    }
}
