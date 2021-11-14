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
        [Obsolete("Use GetMediaTracks instead")]
        internal MediaTrackInfosStructure[] GetMediaTracksInformations(VlcMediaInstance mediaInstance)
        {
            if (mediaInstance == IntPtr.Zero)
                throw new ArgumentException("Media instance is not initialized.");
            IntPtr fullBuffer;
            var cpt = VlcNative.libvlc_media_get_tracks_info(mediaInstance, out fullBuffer);
            if (cpt <= 0)
                return new MediaTrackInfosStructure[0];
            var result = new MediaTrackInfosStructure[cpt];
            var buffer = fullBuffer;
            for (int index = 0; index < cpt; index++)
            {
                result[index] = MarshalHelper.PtrToStructure<MediaTrackInfosStructure>(ref buffer);
                buffer = IntPtr.Size == 4
                    ? new IntPtr(buffer.ToInt32() + MarshalHelper.SizeOf<MediaTrackInfosStructure>())
                    : new IntPtr(buffer.ToInt64() + MarshalHelper.SizeOf<MediaTrackInfosStructure>());
            }
            Free(fullBuffer);
            return result;
        }
    }
}
