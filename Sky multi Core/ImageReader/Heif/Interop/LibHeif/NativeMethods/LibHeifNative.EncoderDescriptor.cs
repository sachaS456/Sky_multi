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

namespace Sky_multi_Core.ImageReader.Heif.Interop
{
    internal static partial class LibHeifNative
    {
        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern LibHeifOwnedString heif_encoder_descriptor_get_name(heif_encoder_descriptor descriptor);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern LibHeifOwnedString heif_encoder_descriptor_get_id_name(heif_encoder_descriptor descriptor);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern HeifCompressionFormat heif_encoder_descriptor_get_compression_format(heif_encoder_descriptor descriptor);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool heif_encoder_descriptor_supports_lossy_compression(heif_encoder_descriptor descriptor);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool heif_encoder_descriptor_supports_lossless_compression(heif_encoder_descriptor descriptor);
    }
}
