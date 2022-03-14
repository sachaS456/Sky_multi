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
    /// The color space of the image
    /// </summary>
    public enum HeifColorspace
    {
        /// <summary>
        /// The color space is not defined.
        /// </summary>
        Undefined = 99,

        /// <summary>
        /// The color space is YCbCr.
        /// </summary>
        YCbCr = 0,

        /// <summary>
        /// The color space is Rgb.
        /// </summary>
        Rgb = 1,

        /// <summary>
        /// The color space is monochrome.
        /// </summary>
        Monochrome = 2
    }
}
