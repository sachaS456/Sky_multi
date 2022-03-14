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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.ImageReader.Heif.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct OutputStringArray
    {
        // A pointer to an array of const char* strings.
        private readonly IntPtr arrayPointer;

        /// <summary>
        /// Converts the string array to a read-only collection of strings.
        /// </summary>
        /// <returns>A read-only collection containing the string array values.</returns>
        public unsafe ReadOnlyCollection<string> ToReadOnlyCollection()
        {
            var items = new List<string>();

            if (this.arrayPointer != IntPtr.Zero)
            {
                var stringMemoryAdddress = (IntPtr*)this.arrayPointer;

                while (*stringMemoryAdddress != IntPtr.Zero)
                {
                    items.Add(Marshal.PtrToStringAnsi(*stringMemoryAdddress));

                    stringMemoryAdddress++;
                }
            }

            return items.AsReadOnly();
        }
    }
}
