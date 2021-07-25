using System;

namespace Sky_multi_Core
{
    public class VlcMediaPlayerAudioVolumeEventArgs : EventArgs
    {
        private float Volume { get; }

        public VlcMediaPlayerAudioVolumeEventArgs(float volume)
        {
            this.Volume = volume;
        }
    }
}