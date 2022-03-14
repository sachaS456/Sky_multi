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
    internal static class LibHeifVersion
    {
        /// <summary>
        /// Gets a value indicating whether the LibHeif version is at least 1.10.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the LibHeif version is at least 1.10; otherwise, <c>false</c>.
        /// </value>
        public static bool Is1Point10OrLater => LibHeifInfo.VersionNumber >= 0x010A0000;

        /// <summary>
        /// Gets a value indicating whether the LibHeif version is at least 1.11.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the LibHeif version is at least 1.11; otherwise, <c>false</c>.
        /// </value>
        public static bool Is1Point11OrLater => LibHeifInfo.VersionNumber >= 0x010B0000;

        /// <summary>
        /// Gets a value indicating whether the LibHeif version is at least 1.12.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the LibHeif version is at least 1.12; otherwise, <c>false</c>.
        /// </value>
        public static bool Is1Point12OrLater => LibHeifInfo.VersionNumber >= 0x010C0000;

        /// <summary>
        /// Throws an exception if the LibHeif version is not supported.
        /// </summary>
        /// <exception cref="HeifException">The LibHeif version is not supported.</exception>
        public static void ThrowIfNotSupported()
        {
            const uint MinimumLibHeifVersion = 0x01090000; // Version 1.9.0

            if (LibHeifInfo.VersionNumber < MinimumLibHeifVersion)
            {
                const int Major = (int)((MinimumLibHeifVersion >> 24) & 0xff);
                const int Minor = (int)((MinimumLibHeifVersion >> 16) & 0xff);
                const int Maintenance = (int)((MinimumLibHeifVersion >> 8) & 0xff);

                throw new HeifException(string.Format(System.Globalization.CultureInfo.CurrentCulture,
                                                      Properties.Resources.LibHeifVersionNotSupportedFormat,
                                                      Major,
                                                      Minor,
                                                      Maintenance));
            }
        }
    }
}