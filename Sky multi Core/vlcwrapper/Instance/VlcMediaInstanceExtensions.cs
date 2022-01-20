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
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal static class VlcMediaInstanceExtensions
    {
        internal static VlcMediaInstance AddOptionToMedia(this VlcMediaInstance mediaInstance, in string option)
        {
            AddOptionToMediaPrivate(mediaInstance, option);
            return mediaInstance;
        }

        internal static VlcMediaInstance AddOptionToMedia(this VlcMediaInstance mediaInstance, in string[] option)
        {
            AddOptionToMediaPrivate(mediaInstance, option);
            return mediaInstance;
        }

        private static void AddOptionToMediaPrivate(in VlcMediaInstance mediaInstance, in string option)
        {
            if (mediaInstance == IntPtr.Zero)
            {
                throw new ArgumentException("Media instance is not initialized.");
            }

            if (string.IsNullOrEmpty(option))
            {
                return;
            }

            using (Utf8StringHandle handle = Utf8InteropStringConverter.ToUtf8StringHandle(option))
            {
                VlcNative.libvlc_media_add_option(mediaInstance, handle);
            }
        }

        private static void AddOptionToMediaPrivate(in VlcMediaInstance mediaInstance, string[] options)
        {
            if (mediaInstance == IntPtr.Zero)
            {
                throw new ArgumentException("Media instance is not initialized.");
            }

            options = options ?? new string[0];

            foreach (string option in options)
            {
                AddOptionToMedia(mediaInstance, option);
            }
        }
    }
}
