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

namespace Sky_multi_Core.VlcWrapper
{
    internal class AudioManagement : IAudioManagement
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        internal AudioManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayer = mediaPlayerInstance;
            Outputs = new AudioOutputsManagement(manager, mediaPlayerInstance);
            Tracks = new AudioTracksManagement(manager, mediaPlayerInstance);
        }

        public IAudioOutputsManagement Outputs { get; private set; }

        public bool IsMute
        {
            get { return myManager.IsMute(myMediaPlayer); }
            set { myManager.SetMute(myMediaPlayer, value); }
        }

        public void ToggleMute()
        {
            myManager.ToggleMute(myMediaPlayer);
        }

        public int Volume
        {
            get { return myManager.GetVolume(myMediaPlayer); }
            set { myManager.SetVolume(myMediaPlayer, value); }
        }

        public ITracksManagement Tracks { get; private set; }

        public int Channel
        {
            get { return myManager.GetAudioChannel(myMediaPlayer); }
            set { myManager.SetAudioChannel(myMediaPlayer, value); }
        }

        public long Delay
        {
            get { return myManager.GetAudioDelay(myMediaPlayer); }
            set { myManager.SetAudioDelay(myMediaPlayer, value); }
        }
    }
}
