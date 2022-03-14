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
    internal sealed class SafeCoTaskMemHandle : SafeHandleZeroIsInvalid
    {
        public SafeCoTaskMemHandle(IntPtr existingHandle, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(existingHandle);
        }

        public static SafeCoTaskMemHandle Allocate(int size)
        {
            SafeCoTaskMemHandle handle;

            var memory = IntPtr.Zero;
            try
            {
                memory = Marshal.AllocCoTaskMem(size);

                handle = new SafeCoTaskMemHandle(memory, true);

                memory = IntPtr.Zero;
            }
            finally
            {
                if (memory != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(memory);
                }
            }

            return handle;
        }

        public static SafeCoTaskMemHandle FromStringAnsi(string s)
        {
            SafeCoTaskMemHandle handle;

            var memory = IntPtr.Zero;
            try
            {
                memory = Marshal.StringToCoTaskMemAnsi(s);

                handle = new SafeCoTaskMemHandle(memory, true);

                memory = IntPtr.Zero;
            }
            finally
            {
                if (memory != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(memory);
                }
            }

            return handle;
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeCoTaskMem(this.handle);
            return true;
        }
    }
}
