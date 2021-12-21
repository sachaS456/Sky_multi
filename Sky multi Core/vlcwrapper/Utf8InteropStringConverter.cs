/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

namespace Sky_multi_Core.VlcWrapper
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The class that allows easy conversion from and to UTF-8 character array
    /// </summary>
    public class Utf8InteropStringConverter
    {
        /// <summary>
        /// Converts a c-style byte array (const char*) in UTF-8 to a strign
        /// </summary>
        /// <param name="ptr">The c-style string pointer</param>
        /// <returns>The string object</returns>
        public static string Utf8InteropToString(in IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return null;
            }

            int length = 0;

            while (Marshal.ReadByte(ptr, length) != 0)
            {
                length++;
            }

            byte[] buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// Allocates an area in memory that stores the Utf-8 bytes representing the input string.
        /// </summary>
        /// <param name="source">The source string to be converted to UTF-8 so that it can be passed to libvlc.</param>
        /// <returns>The safe handle</returns>
        public static Utf8StringHandle ToUtf8StringHandle(in string source)
        {
            if (source == null)
            {
                return null;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(source);
            IntPtr buffer = Marshal.AllocHGlobal(bytes.Length + 1);
            try
            {
                Marshal.Copy(bytes, 0, buffer, bytes.Length);
                Marshal.WriteByte(buffer, bytes.Length, 0);
            }
            catch (Exception)
            {
                Marshal.FreeHGlobal(buffer);
                throw;
            }

            return new Utf8StringHandle(buffer);
        }
    }
}