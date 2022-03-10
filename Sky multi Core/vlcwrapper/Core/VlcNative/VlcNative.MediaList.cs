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

namespace Sky_multi_Core.VlcWrapper.Core
{
    internal static partial class VlcNative
    {
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_subitems(IntPtr media);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_new")]
        internal static extern IntPtr LibVLCMediaListNew(IntPtr instance);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_release")]
        internal static extern void LibVLCMediaListRelease(IntPtr mediaList);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_discoverer_media_list")]
        internal static extern IntPtr LibVLCMediaDiscovererMediaList(IntPtr discovererMediaList);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_set_media")]
        internal static extern void LibVLCMediaListSetMedia(IntPtr mediaList, IntPtr media);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_add_media")]
        internal static extern int LibVLCMediaListAddMedia(IntPtr mediaList, IntPtr media);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_insert_media")]
        internal static extern int LibVLCMediaListInsertMedia(IntPtr mediaList, IntPtr media, int positionIndex);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_remove_index")]
        internal static extern int LibVLCMediaListRemoveIndex(IntPtr mediaList, int positionIndex);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_count")]
        internal static extern int LibVLCMediaListCount(IntPtr mediaList);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_item_at_index")]
        internal static extern IntPtr LibVLCMediaListItemAtIndex(IntPtr mediaList, int positionIndex);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_index_of_item")]
        internal static extern int LibVLCMediaListIndexOfItem(IntPtr mediaList, IntPtr media);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_is_readonly")]
        internal static extern int LibVLCMediaListIsReadonly(IntPtr mediaList);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_lock")]
        internal static extern void LibVLCMediaListLock(IntPtr mediaList);


        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_unlock")]
        internal static extern void LibVLCMediaListUnlock(IntPtr mediaList);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_event_manager")]
        internal static extern IntPtr LibVLCMediaListEventManager(IntPtr mediaList);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl,
            EntryPoint = "libvlc_media_list_retain")]
        internal static extern void LibVLCMediaListRetain(IntPtr mediaList);
    }
}
