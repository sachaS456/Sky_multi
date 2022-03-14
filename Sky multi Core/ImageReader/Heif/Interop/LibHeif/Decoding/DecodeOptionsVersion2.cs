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

namespace Sky_multi_Core.ImageReader.Heif.Interop
{
#pragma warning disable 0649
    internal struct DecodeOptionsVersion2
    {
        public byte version;

        // version 1 options

        // Ignore geometric transformations like cropping, rotation, mirroring.
        // Default: false (do not ignore).
        public byte ignore_transformations;

        public IntPtr start_progress;

        public IntPtr on_progress;

        public IntPtr end_progress;

        public IntPtr progress_user_data;

        // version 2 options

        public byte convert_hdr_to_8bit;
    }
#pragma warning restore 0649
}
