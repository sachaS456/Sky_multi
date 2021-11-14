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
    internal class VideoManagement : IVideoManagement
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public VideoManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayer = mediaPlayerInstance;
            Tracks = new VideoTracksManagement(manager, mediaPlayerInstance);
            Marquee = new MarqueeManagement(manager, mediaPlayerInstance);
            Logo = new LogoManagement(manager, mediaPlayerInstance);
            Adjustments = new AdjustmentsManagement(manager, mediaPlayerInstance);
        }
        
        public string AspectRatio
        {
            get { return myManager.GetVideoAspectRatio(myMediaPlayer); }
            set { myManager.SetVideoAspectRatio(myMediaPlayer, value); }
        }

        public string CropGeometry
        {
            get { return myManager.GetVideoCropGeometry(myMediaPlayer);  }
            set { myManager.SetVideoCropGeometry(myMediaPlayer, value); }
        }

        public int Teletext
        {
            get { return myManager.GetVideoTeletext(myMediaPlayer); }
            set { myManager.SetVideoTeletext(myMediaPlayer, value); }
        }

        public ITracksManagement Tracks { get; private set; }

        public string Deinterlace
        {
            set { myManager.SetVideoDeinterlace(myMediaPlayer, value); }
        }

        /// <summary>
        /// Gets or set the fullscreen mode for the player.
        /// <c>true</c> if the player is playing fullscreen
        /// </summary>
        public bool FullScreen
        {
            get
            {
                return myManager.GetFullScreen(myMediaPlayer) != 0;
            }
            set
            {
                myManager.SetFullScreen(myMediaPlayer, value);
            }
        }

        public IMarqueeManagement Marquee { get; private set; }
        public ILogoManagement Logo { get; private set; }
        public IAdjustmentsManagement Adjustments { get; private set; }

        public bool IsMouseInputEnabled
        {
            set { myManager.SetMouseInput(myMediaPlayer, value); }
        }

        public bool IsKeyInputEnabled
        {
            set { myManager.SetKeyInput(myMediaPlayer, value); }
        }
    }
}
