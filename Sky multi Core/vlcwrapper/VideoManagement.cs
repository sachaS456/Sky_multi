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
    internal class VideoManagement : IVideoManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public VideoManagement(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myMediaPlayer = mediaPlayerInstance;
            Tracks = new VideoTracksManagement(mediaPlayerInstance);
            Marquee = new MarqueeManagement(mediaPlayerInstance);
            Logo = new LogoManagement(mediaPlayerInstance);
            Adjustments = new AdjustmentsManagement(mediaPlayerInstance);
        }

        private void myMediaPlayerIsLoad()
        {
            if (myMediaPlayer == IntPtr.Zero)
            {
                throw new ArgumentException("Media player instance is not initialized.");
            }
        }

        public string AspectRatio
        {
            get 
            {
                myMediaPlayerIsLoad();
                return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_video_get_aspect_ratio(myMediaPlayer)); 
            }
            set 
            {
                myMediaPlayerIsLoad();

                using (Utf8StringHandle aspectRatioInterop = Utf8InteropStringConverter.ToUtf8StringHandle(in value))
                {
                    VlcNative.libvlc_video_set_aspect_ratio(myMediaPlayer, aspectRatioInterop);
                }
            }
        }

        public string CropGeometry
        {
            get 
            {
                myMediaPlayerIsLoad();
                return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_video_get_crop_geometry(myMediaPlayer));  
            }
            set 
            {
                myMediaPlayerIsLoad();

                using (Utf8StringHandle cropGeometryInterop = Utf8InteropStringConverter.ToUtf8StringHandle(in value))
                {
                    VlcNative.libvlc_video_set_crop_geometry(myMediaPlayer, cropGeometryInterop);
                }
            }
        }

        public int Teletext
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_teletext(myMediaPlayer); 
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_teletext(myMediaPlayer, value); 
            }
        }

        public ITracksManagement Tracks { get; private set; }

        public string Deinterlace
        {
            set 
            {
                myMediaPlayerIsLoad();

                using (Utf8StringHandle deinterlaceModeInterop = Utf8InteropStringConverter.ToUtf8StringHandle(in value))
                {
                    VlcNative.libvlc_video_set_deinterlace(myMediaPlayer, deinterlaceModeInterop);
                }
            }
        }

        /// <summary>
        /// Gets or set the fullscreen mode for the player.
        /// <c>true</c> if the player is playing fullscreen
        /// </summary>
        public bool FullScreen
        {
            get
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_get_fullscreen(myMediaPlayer) != 0;
            }
            set
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_set_fullscreen(myMediaPlayer, value ? 1 : 0);
            }
        }

        public IMarqueeManagement Marquee { get; private set; }
        public ILogoManagement Logo { get; private set; }
        public IAdjustmentsManagement Adjustments { get; private set; }

        public bool IsMouseInputEnabled
        {
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_mouse_input(myMediaPlayer, value ? 1u : 0u); 
            }
        }

        public bool IsKeyInputEnabled
        {
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_key_input(myMediaPlayer, value ? 1u : 0u); 
            }
        }
    }
}
