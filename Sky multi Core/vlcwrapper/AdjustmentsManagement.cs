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
    internal class AdjustmentsManagement : IAdjustmentsManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public AdjustmentsManagement(VlcMediaPlayerInstance mediaPlayerInstance)
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
                return VlcNative.libvlc_video_get_adjust_int(myMediaPlayer, VideoAdjustOptions.Enable) == 1;
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_adjust_int(myMediaPlayer, VideoAdjustOptions.Enable, value ? 1 : 0);
            }
        }

        public float Contrast
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_adjust_float(myMediaPlayer, VideoAdjustOptions.Contrast);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_adjust_float(myMediaPlayer, VideoAdjustOptions.Contrast, value);
            }
        }

        public float Brightness
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_adjust_float(myMediaPlayer, VideoAdjustOptions.Brightness);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_adjust_float(myMediaPlayer, VideoAdjustOptions.Brightness, value);
            }
        }

        public float Hue
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_adjust_float(myMediaPlayer, VideoAdjustOptions.Hue);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_adjust_float(myMediaPlayer, VideoAdjustOptions.Hue, value);
            }
        }

        public float Saturation
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_adjust_float(myMediaPlayer, VideoAdjustOptions.Saturation);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_adjust_float(myMediaPlayer, VideoAdjustOptions.Saturation, value);
            }
        }

        public float Gamma
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_adjust_float(myMediaPlayer, VideoAdjustOptions.Gamma);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_adjust_float(myMediaPlayer, VideoAdjustOptions.Gamma, value);
            }
        }
    }
}
