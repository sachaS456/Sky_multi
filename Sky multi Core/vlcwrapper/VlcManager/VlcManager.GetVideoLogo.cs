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
    public sealed partial class VlcManager
    {
        internal bool GetVideoLogoEnabled(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Enable) == 1;
        }

        internal int GetVideoLogoX(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.X);
        }
        internal int GetVideoLogoY(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Y);
        }
        internal int GetVideoLogoDelay(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Delay);
        }
        internal int GetVideoLogoRepeat(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Repeat);
        }
        internal int GetVideoLogoOpacity(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Opacity);
        }
        internal int GetVideoLogoPosition(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            return VlcNative.libvlc_video_get_logo_int(mediaPlayerInstance, VideoLogoOptions.Position);
        }
    }
}
