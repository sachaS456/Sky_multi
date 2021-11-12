using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.VlcWrapper.Core
{
    /// <summary>
    /// This class loads the required libvlc libraries.
    /// Only one instance per directory will be created.
    ///
    /// Use <see cref="GetOrCreateLoader" /> and <see cref="ReleaseLoader"/> to get a VlcLibraryLoader instance and release it properly.
    /// Do not call Dispose() by yourself, it will be called as needed by ReleaseLoader.
    /// </summary>
    internal static class VlcLibraryLoader//: IDisposable
    {
        private static IntPtr myLibVlcDllHandle;
        private static IntPtr myLibVlcCoreDllHandle;

        internal static bool LibVlcLoaded { get; private set; } = false;

        internal static void LoadLibVlc(ref DirectoryInfo dynamicLinkLibrariesPath)
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