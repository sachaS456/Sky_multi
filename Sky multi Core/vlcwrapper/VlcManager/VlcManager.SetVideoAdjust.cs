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
        internal void SetVideoAdjustEnabled(VlcMediaPlayerInstance mediaPlayerInstance, bool value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_int(mediaPlayerInstance, VideoAdjustOptions.Enable, value ? 1 : 0);
        }
        internal void SetVideoAdjustContrast(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Contrast, value);
        }
        internal void SetVideoAdjustBrightness(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Brightness, value);
        }
        internal void SetVideoAdjustHue(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Hue, value);
        }
        internal void SetVideoAdjustSaturation(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Saturation, value);
        }
        internal void SetVideoAdjustGamma(VlcMediaPlayerInstance mediaPlayerInstance, float value)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_adjust_float(mediaPlayerInstance, VideoAdjustOptions.Gamma, value);
        }
    }
}
