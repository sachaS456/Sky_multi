﻿/*--------------------------------------------------------------------------------------------------------------------
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
    // These values are from the ITU-T H.273 (2016) specification.
    // https://www.itu.int/rec/T-REC-H.273-201612-I/en

    /// <summary>
    /// The NCLX color primaries
    /// </summary>
    public enum ColorPrimaries
    {
        /// <summary>
        /// BT.709
        /// </summary>
        BT709 = 1,

        /// <summary>
        /// Unspecified
        /// </summary>
        Unspecified = 2,

        /// <summary>
        /// BT.470 System M (historical)
        /// </summary>
        BT470M = 4,

        /// <summary>
        /// BT.470 System B, G (historical)
        /// </summary>
        BT470BG = 5,

        /// <summary>
        /// BT.601
        /// </summary>
        BT601 = 6,

        /// <summary>
        /// SMPTE 240
        /// </summary>
        Smpte240 = 7,

        /// <summary>
        /// Generic film (color filters using illuminant C)
        /// </summary>
        GenericFilm = 8,

        /// <summary>
        /// BT.2020-2, BT.2100-0
        /// </summary>
        BT2020 = 9,

        /// <summary>
        /// SMPTE 428 (CIE 1921 XYZ)
        /// </summary>
        Smpte428 = 10,

        /// <summary>
        /// SMPTE RP 431-2
        /// </summary>
        Smpte431 = 11,

        /// <summary>
        /// SMPTE EG 432-1
        /// </summary>
        Smpte432 = 12,

        /// <summary>
        /// EBU Tech. 3213-E
        /// </summary>
        Ebu3213 = 22
    }
}