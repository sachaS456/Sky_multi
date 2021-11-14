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
        /// Enables/Disables libvlc's handling of keyboard input
        /// </summary>
        /// <param name="mediaPlayerInstance">The media player instance</param>
        /// <param name="on"><c>true</c> if libvlc should handle keyboard events, <c>false</c> otherwise</param>
        /// <remarks>Must be called before the stream has started playing. This is not applicable to the WPF control.</remarks>
        internal void SetKeyInput(VlcMediaPlayerInstance mediaPlayerInstance, bool on)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            VlcNative.libvlc_video_set_key_input(mediaPlayerInstance, on ? 1u : 0u);
        }
    }
}