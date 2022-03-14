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
        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern void heif_encoder_release(IntPtr handle);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_encoder_parameter_list heif_encoder_list_parameters(SafeHeifEncoder encoder);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern unsafe LibHeifOwnedString heif_encoder_parameter_get_name(heif_encoder_parameter parameter);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern unsafe heif_encoder_parameter_type heif_encoder_parameter_get_type(heif_encoder_parameter parameter);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern unsafe heif_error heif_encoder_parameter_get_valid_integer_range(heif_encoder_parameter parameter,
                                                                                                [MarshalAs(UnmanagedType.Bool)] out bool haveMinimumMaximum,
                                                                                                ref int minimum,
                                                                                                ref int maximum);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern unsafe heif_error heif_encoder_parameter_get_valid_integer_values(heif_encoder_parameter parameter,
                                                                                                 [MarshalAs(UnmanagedType.Bool)] out bool haveMinimum,
                                                                                                 [MarshalAs(UnmanagedType.Bool)] out bool haveMaximum,
                                                                                                 ref int minimum,
                                                                                                 ref int maximum,
                                                                                                 out int numValidValues,
                                                                                                 out IntPtr validValuesArray);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern unsafe heif_error heif_encoder_parameter_get_valid_string_values(heif_encoder_parameter parameter,
                                                                                                out OutputStringArray array);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_error heif_encoder_set_lossy_quality(SafeHeifEncoder encoder, int quality);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_error heif_encoder_set_lossless(SafeHeifEncoder encoder,
                                                                    [MarshalAs(UnmanagedType.Bool)] bool lossless);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_error heif_encoder_set_parameter_boolean(SafeHeifEncoder encoder,
                                                                             [MarshalAs(UnmanagedType.LPStr)] string name,
                                                                             [MarshalAs(UnmanagedType.Bool)] bool value);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_error heif_encoder_set_parameter_integer(SafeHeifEncoder encoder,
                                                                            [MarshalAs(UnmanagedType.LPStr)] string name,
                                                                            int value);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_error heif_encoder_set_parameter_string(SafeHeifEncoder encoder,
                                                                            [MarshalAs(UnmanagedType.LPStr)] string name,
                                                                            [MarshalAs(UnmanagedType.LPStr)] string value);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_error heif_encoder_get_parameter_boolean(SafeHeifEncoder encoder,
                                                                            [MarshalAs(UnmanagedType.LPStr)] string name,
                                                                            [MarshalAs(UnmanagedType.Bool)] out bool value);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern heif_error heif_encoder_get_parameter_integer(SafeHeifEncoder encoder,
                                                                            [MarshalAs(UnmanagedType.LPStr)] string name,
                                                                            out int value);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        internal static extern unsafe heif_error heif_encoder_get_parameter_string(SafeHeifEncoder encoder,
                                                                                   [MarshalAs(UnmanagedType.LPStr)] string name,
                                                                                   byte* ptr,
                                                                                   int length);

        [DllImport(DllName, CallingConvention = DllCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool heif_encoder_has_default(SafeHeifEncoder encoder,
                                                             [MarshalAs(UnmanagedType.LPStr)] string name);
    }
}
