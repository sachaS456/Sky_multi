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
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct heif_depth_representation_info
    {
        public byte version;

        // version 1 fields

        public byte has_z_near;
        public byte has_z_far;
        public byte has_d_min;
        public byte has_d_max;

        public double z_near;
        public double z_far;
        public double d_min;
        public double d_max;

        public HeifDepthRepresentationType depth_representation_type;
        public uint disparity_reference_view;

        public uint depth_nonlinear_representation_model_size;
        public IntPtr depth_nonlinear_representation_model;

        // version 2 fields below
    }
}
