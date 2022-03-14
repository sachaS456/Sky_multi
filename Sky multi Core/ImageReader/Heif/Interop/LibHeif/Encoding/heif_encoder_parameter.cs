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
    internal readonly struct heif_encoder_parameter : IEquatable<heif_encoder_parameter>
    {
        public static readonly heif_encoder_parameter Null = new heif_encoder_parameter(IntPtr.Zero);

        // This structure is a type-safe wrapper for
        // an opaque native structure.
        private readonly IntPtr value;

        private heif_encoder_parameter(IntPtr value)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is heif_encoder_parameter other && Equals(other);
        }

        public bool Equals(heif_encoder_parameter other)
        {
            return this.value == other.value;
        }

        public override int GetHashCode()
        {
            return -1584136870 + this.value.GetHashCode();
        }

        public static bool operator ==(heif_encoder_parameter left, heif_encoder_parameter right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(heif_encoder_parameter left, heif_encoder_parameter right)
        {
            return !(left == right);
        }
    }
}
