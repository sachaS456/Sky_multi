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
    internal class LogoManagement : ILogoManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public LogoManagement(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myMediaPlayer = mediaPlayerInstance;
        }

        private void myMediaPlayerIsLoad()
        {
            if (myMediaPlayer == IntPtr.Zero)
            {
                throw new ArgumentException("Media player instance is not initialized.");
            }
        }

        public bool Enabled
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_logo_int(myMediaPlayer, VideoLogoOptions.Enable) == 1;
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_logo_int(myMediaPlayer, VideoLogoOptions.Enable, value ? 1 : 0);
            }
        }

        public string File
        {
            set 
            {
                myMediaPlayerIsLoad();
                using (Utf8StringHandle valueInterop = Utf8InteropStringConverter.ToUtf8StringHandle(in value))
                {
                    VlcNative.libvlc_video_set_logo_string(myMediaPlayer, VideoLogoOptions.File, valueInterop);
                }
            }
        }

        public int X
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_logo_int(myMediaPlayer, VideoLogoOptions.X);
            }
            set
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_logo_int(myMediaPlayer, VideoLogoOptions.X, value);
            }
        }

        public int Y
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_logo_int(myMediaPlayer, VideoLogoOptions.Y);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_logo_int(myMediaPlayer, VideoLogoOptions.Y, value);
            }
        }

        public int Delay
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_logo_int(myMediaPlayer, VideoLogoOptions.Delay);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_logo_int(myMediaPlayer, VideoLogoOptions.Delay, value);
            }
        }

        public int Repeat
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_logo_int(myMediaPlayer, VideoLogoOptions.Repeat);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_logo_int(myMediaPlayer, VideoLogoOptions.Repeat, value);
            }
        }

        public int Opacity
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_logo_int(myMediaPlayer, VideoLogoOptions.Opacity);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_logo_int(myMediaPlayer, VideoLogoOptions.Opacity, value);
            }
        }

        public int Position
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_logo_int(myMediaPlayer, VideoLogoOptions.Position);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_logo_int(myMediaPlayer, VideoLogoOptions.Position, value);
            }
        }
    }
}
