/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal class AudioManagement : IAudioManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        internal AudioManagement(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myMediaPlayer = mediaPlayerInstance;
            Outputs = new AudioOutputsManagement(mediaPlayerInstance);
            Tracks = new AudioTracksManagement(mediaPlayerInstance);
        }

        private void myMediaPlayerIsLoad()
        {
            if (myMediaPlayer == IntPtr.Zero)
            {
                throw new ArgumentException("Media player instance is not initialized.");
            }
        }

        public IAudioOutputsManagement Outputs { get; private set; }

        public bool IsMute
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_audio_get_mute(myMediaPlayer) == 1;
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_audio_set_mute(myMediaPlayer, value ? 1 : 0);
            }
        }

        public void ToggleMute()
        {
            myMediaPlayerIsLoad();
            VlcNative.libvlc_audio_toggle_mute(myMediaPlayer);
        }

        public int Volume
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_audio_get_volume(myMediaPlayer);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_audio_set_volume(myMediaPlayer, value);
            }
        }

        public ITracksManagement Tracks { get; private set; }

        public int Channel
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_audio_get_channel(myMediaPlayer);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_audio_set_channel(myMediaPlayer, value);
            }
        }

        public long Delay
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_audio_get_delay(myMediaPlayer);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_audio_set_delay(myMediaPlayer, value);
            }
        }
    }
}
