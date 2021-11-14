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
        internal bool TakeSnapshot(VlcMediaPlayerInstance mediaPlayerInstance, uint outputNumber, string filePath, uint width, uint height)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            if(filePath == null)
                throw new ArgumentNullException(nameof(filePath));
            using (var filePathHandle = Utf8InteropStringConverter.ToUtf8StringHandle(filePath))
            {
                return VlcNative.libvlc_video_take_snapshot(mediaPlayerInstance, outputNumber, filePathHandle, width, height) == 0;
            }
        }
    }
}
