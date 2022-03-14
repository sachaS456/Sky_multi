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
using System.Runtime.InteropServices;

namespace Sky_multi_Core.ImageReader.Heif.Interop
{
    internal static partial class LibHeifNative
    {
        private const string DllName = "libheif";
        private const CallingConvention DllCallingConvention = CallingConvention.Cdecl;

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern uint heif_get_version_number();

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool heif_have_decoder_for_format(HeifCompressionFormat format);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool heif_have_encoder_for_format(HeifCompressionFormat format);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern SafeHeifDecodingOptions heif_decoding_options_alloc();

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern void heif_decoding_options_free(IntPtr handle);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern SafeHeifEncodingOptions heif_encoding_options_alloc();

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern void heif_encoding_options_free(IntPtr handle);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern SafeHeifNclxColorProfile heif_nclx_color_profile_alloc();

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern void heif_nclx_color_profile_free(IntPtr handle);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern void heif_depth_representation_info_free(IntPtr handle);
    }
}
