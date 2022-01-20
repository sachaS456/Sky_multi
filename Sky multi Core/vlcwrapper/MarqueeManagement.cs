/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal class MarqueeManagement : IMarqueeManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public MarqueeManagement(VlcMediaPlayerInstance mediaPlayerInstance)
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
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Enable) == 1; 
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Enable, value ? 1 : 0); 
            }
        }

        public string Text
        {
            get 
            {
                myMediaPlayerIsLoad();
                return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_video_get_marquee_string(myMediaPlayer, VideoMarqueeOptions.Text)); 
            }
            set 
            {
                myMediaPlayerIsLoad();

                using (Utf8StringHandle valueInterop = Utf8InteropStringConverter.ToUtf8StringHandle(in value))
                {
                    VlcNative.libvlc_video_set_marquee_string(myMediaPlayer, VideoMarqueeOptions.Text, valueInterop);
                }
            }
        }

        public int Color
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Color); 
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Color, value);
            }
        }

        public int Opacity
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Opacity);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Opacity, value);
            }
        }

        public int Position
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Position);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Position, value);
            }
        }

        public int Refresh
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Refresh);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Refresh, value);
            }
        }

        public int Size
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Size);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Size, value);
            }
        }

        public int Timeout
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Timeout);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Timeout, value);
            }
        }

        public int X
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.X);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.X, value);
            }
        }

        public int Y
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_marquee_int(myMediaPlayer, VideoMarqueeOptions.Y);
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_marquee_int(myMediaPlayer, VideoMarqueeOptions.Y, value);
            }
        }
    }
}
