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

using System.Runtime.InteropServices;

namespace Sky_multi_Core.ImageReader.Heif.Interop
{
    internal static class MarshalHelper
    {
        public static int SizeOf<T>()
        {
            return SizeOfHelper<T>.Size;
        }

        private static class SizeOfHelper<T>
        {
            public static readonly int Size = Marshal.SizeOf<T>();
        }
    }
}