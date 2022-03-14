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

using System.Diagnostics;
using Sky_multi_Core.ImageReader.Heif.Interop;

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// Represents a LibHeif encoder descriptor.
    /// </summary>
    /// <seealso cref="HeifContext.GetEncoderDescriptors(HeifCompressionFormat, string)"/>
    /// <seealso cref="HeifContext.GetEncoder(HeifEncoderDescriptor)"/>
    [DebuggerDisplay("{" + nameof(Name) + ",nq}")]
    public sealed class HeifEncoderDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeifEncoderDescriptor"/> class.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        internal HeifEncoderDescriptor(heif_encoder_descriptor descriptor)
        {
            this.Name = LibHeifNative.heif_encoder_descriptor_get_name(descriptor).GetStringValue();
            this.IdName = LibHeifNative.heif_encoder_descriptor_get_id_name(descriptor).GetStringValue();
            this.CompressionFormat = LibHeifNative.heif_encoder_descriptor_get_compression_format(descriptor);
            this.SupportsLossyCompression = LibHeifNative.heif_encoder_descriptor_supports_lossy_compression(descriptor);
            this.SupportsLosslessCompression = LibHeifNative.heif_encoder_descriptor_supports_lossless_compression(descriptor);
            this.Descriptor = descriptor;
        }

        /// <summary>
        /// Gets the encoder name.
        /// </summary>
        /// <value>
        /// The encoder name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the encoder id name.
        /// </summary>
        /// <value>
        /// The encoder id name.
        /// </value>
        public string IdName { get; }

        /// <summary>
        /// Gets the encoder compression format.
        /// </summary>
        /// <value>
        /// The encoder compression format.
        /// </value>
        public HeifCompressionFormat CompressionFormat { get; }

        /// <summary>
        /// Gets a value indicating whether the encoder supports lossy compression.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if the encoder supports lossy compression; otherwise, <see langword="false"/>.
        /// </value>
        public bool SupportsLossyCompression { get; }

        /// <summary>
        /// Gets a value indicating whether the encoder supports lossless compression.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if the encoder supports lossless compression; otherwise, <see langword="false"/>.
        /// </value>
        public bool SupportsLosslessCompression { get; }

        /// <summary>
        /// Gets the encoder descriptor.
        /// </summary>
        /// <value>
        /// The encoder descriptor.
        /// </value>
        internal heif_encoder_descriptor Descriptor { get; }
    }
}
