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

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.VlcWrapper.Core
{
    internal static class VlcLibraryLoader
    {
        private static IntPtr myLibVlcDllHandle;
        private static IntPtr myLibVlcCoreDllHandle;

        internal static bool LibVlcLoaded { get; private set; } = false;

        internal static void LoadLibVlc(in DirectoryInfo dynamicLinkLibrariesPath)
        {
            if (LibVlcLoaded == true)
            {
                return;
            }

            if (File.Exists(dynamicLinkLibrariesPath + @"\libvlc.dll") && File.Exists(dynamicLinkLibrariesPath + @"\libvlccore.dll"))
            {
                myLibVlcCoreDllHandle = Win32Interops.LoadLibrary(dynamicLinkLibrariesPath + @"\libvlccore.dll");
                if (myLibVlcCoreDllHandle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                myLibVlcDllHandle = Win32Interops.LoadLibrary(dynamicLinkLibrariesPath + @"\libvlc.dll");
                if (myLibVlcDllHandle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    LibVlcLoaded = true;
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        internal static void ReleaseLoader()
        {
            if (myLibVlcDllHandle != IntPtr.Zero)
            {
                Win32Interops.FreeLibrary(myLibVlcDllHandle);
                myLibVlcDllHandle = IntPtr.Zero;
            }
            if (myLibVlcCoreDllHandle != IntPtr.Zero)
            {
                Win32Interops.FreeLibrary(myLibVlcCoreDllHandle);
                myLibVlcCoreDllHandle = IntPtr.Zero;
            }
        }
    }
}