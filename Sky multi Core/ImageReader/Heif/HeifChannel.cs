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

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// The LibHeif image channels
    /// </summary>
    public enum HeifChannel
    {
        /// <summary>
        /// The Y channel in YCbCr.
        /// </summary>
        Y = 0,

        /// <summary>
        /// The Cb channel in YCbCr.
        /// </summary>
        Cb = 1,

        /// <summary>
        /// The Cr channel in YCbCr.
        /// </summary>
        Cr = 2,

        /// <summary>
        /// The red channel in RGB and RGBA.
        /// </summary>
        R = 3,

        /// <summary>
        /// The green channel in RGB and RGBA.
        /// </summary>
        G = 4,

        /// <summary>
        /// The blue channel in RGB and RGBA.
        /// </summary>
        B = 5,

        /// <summary>
        /// The alpha channel.
        /// </summary>
        Alpha = 6,

        /// <summary>
        /// The image uses interleaved channels
        /// </summary>
        Interleaved = 10
    }
}
