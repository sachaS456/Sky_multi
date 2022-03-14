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

using System.Runtime.InteropServices;
using Sky_multi_Core.ImageReader.Heif.Interop;

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// The options that can be set when decoding an image.
    /// </summary>
    /// <seealso cref="HeifImageHandle.Decode(HeifColorspace, HeifChroma, HeifDecodingOptions)"/>
    /// <threadsafety static="true" instance="false"/>
    public sealed class HeifDecodingOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeifDecodingOptions"/> class.
        /// </summary>
        public HeifDecodingOptions()
        {
            this.IgnoreTransformations = false;
            this.ConvertHdrToEightBit = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether transformations are ignored when decoding the image.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if transformations are ignored when decoding the image; otherwise, <see langword="false"/>.
        /// </value>
        public bool IgnoreTransformations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether high bit-depth images should be converted to 8-bits-per-channel.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if high bit-depth images should be converted to 8-bits-per-channel; otherwise, <see langword="false"/>.
        /// </value>
        public bool ConvertHdrToEightBit { get; set; }

        internal unsafe SafeHeifDecodingOptions CreateDecodingOptions()
        {
            var decodingOptions = LibHeifNative.heif_decoding_options_alloc();

            if (decodingOptions.IsInvalid)
            {
                ExceptionUtil.ThrowHeifException(Properties.Resources.HeifDecodingOptionsCreationFailed);
            }

            var options = (DecodeOptionsVersion1*)decodingOptions.DangerousGetHandle();
            options->ignore_transformations = (byte)(this.IgnoreTransformations ? 1 : 0);

            if (options->version >= 2)
            {
                var optionsVersion2 = (DecodeOptionsVersion2*)decodingOptions.DangerousGetHandle();

                optionsVersion2->convert_hdr_to_8bit = (byte)(this.ConvertHdrToEightBit ? 1 : 0);
            }

            return decodingOptions;
        }
    }
}
