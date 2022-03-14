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
    internal struct EncodingOptionsVersion4
    {
        public byte version;

        // version 1 options

        public byte save_alpha_channel;

        // version 2 options

        // Crops heif images with a grid wrapper instead of a 'clap' transform.
        // Results in slightly larger file size.
        // Default: on.
        public byte macOS_compatibility_workaround;

        // version 3 options

        public byte save_two_colr_boxes_when_ICC_and_nclx_available; // default: false

        // version 4 options

        // Set this to the NCLX parameters to be used in the output image or set to NULL
        // when the same parameters as in the input image should be used.
        public IntPtr output_nclx_profile;

        // Some versions of macOS and iOS cannot read files with a NCLX profile
        public byte macOS_compatibility_workaround_no_nclx_profile; // default: true
    }
#pragma warning restore 0649
}
