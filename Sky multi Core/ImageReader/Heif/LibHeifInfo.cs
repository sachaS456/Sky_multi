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
using Sky_multi_Core.ImageReader.Heif.Interop;

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// Provides information about LibHeif.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public static class LibHeifInfo
    {
        private static readonly Lazy<Version> libheifVersion = new Lazy<Version>(GetLibHeifVersion);
        private static readonly Lazy<uint> libheifVersionNumber = new Lazy<uint>(GetLibHeifVersionNumber);
        private static readonly object nativeCallLock = new object();

        /// <summary>
        /// Gets a value indicating whether LibHeif can write two color profiles when both ICC and NCLX profiles are available.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if LibHeif can write two color profiles; otherwise, <see langword="false"/>.
        /// </value>
        public static bool CanWriteTwoColorProfiles => LibHeifVersion.Is1Point10OrLater;

        /// <summary>
        /// Gets the LibHeif version.
        /// </summary>
        /// <value>
        /// The LibHeif version.
        /// </value>
        public static Version Version => libheifVersion.Value;

        internal static uint VersionNumber => libheifVersionNumber.Value;

        /// <summary>
        /// Determines whether LibHeif has a decoder for the specified <see cref="HeifCompressionFormat"/>.
        /// </summary>
        /// <param name="format">The compression format.</param>
        /// <returns>
        /// <see langword="true" /> if LibHeif has a decoder for the specified <see cref="HeifCompressionFormat"/>;
        /// otherwise, <see langword="false" />.
        /// </returns>
        public static bool HaveDecoder(HeifCompressionFormat format)
        {
            lock (nativeCallLock)
            {
                return LibHeifNative.heif_have_decoder_for_format(format);
            }
        }

        /// <summary>
        /// Determines whether LibHeif has an encoder for the specified <see cref="HeifCompressionFormat"/>.
        /// </summary>
        /// <param name="format">The compression format.</param>
        /// <returns>
        /// <see langword="true" /> if LibHeif an encoder for the specified <see cref="HeifCompressionFormat"/>;
        /// otherwise, <see langword="false" />.
        /// </returns>
        public static bool HaveEncoder(HeifCompressionFormat format)
        {
            lock (nativeCallLock)
            {
                return LibHeifNative.heif_have_encoder_for_format(format);
            }
        }

        private static Version GetLibHeifVersion()
        {
            uint version = libheifVersionNumber.Value;

            int major = (int)((version >> 24) & 0xff);
            int minor = (int)((version >> 16) & 0xff);
            int maintenance = (int)((version >> 8) & 0xff);

            return new Version(major, minor, maintenance);
        }

        private static uint GetLibHeifVersionNumber()
        {
            return LibHeifNative.heif_get_version_number();
        }
    }
}
