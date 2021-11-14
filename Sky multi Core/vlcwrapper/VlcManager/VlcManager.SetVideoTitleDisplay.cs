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
        /// <summary>
        /// Set if, and how, the video title will be shown when media is played.
        /// </summary>
        /// <param name="mediaPlayerInstance">The media player instance</param>
        /// <param name="position">position at which to display the title, or libvlc_position_disable to prevent the title from being displayed</param>
        /// <param name="timeout">title display timeout in milliseconds (ignored if libvlc_position_disable)</param>
        public void SetVideoTitleDisplay(IntPtr mediaPlayerInstance, Position position, int timeout)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_media_player_set_video_title_display(mediaPlayerInstance, position, timeout);
        }
    }
}