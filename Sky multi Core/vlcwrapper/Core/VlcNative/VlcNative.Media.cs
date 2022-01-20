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
        /// <summary>
        /// Add an option to the media.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_add_option(IntPtr mediaInstance, Utf8StringHandle mrl);

        /// <summary>
        /// Add an option to the media with configurable flags.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_add_option_flag(IntPtr mediaInstance, Utf8StringHandle mrl, uint flag);

        /// <summary>
        /// Duplicate a media descriptor object.
        /// </summary>
        /// <returns>Return a media descriptor object.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_duplicate(IntPtr mediaInstance);

        /// <summary>
        /// Get event manager from media descriptor object.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_event_manager(IntPtr mediaInstance);

        /// <summary>
        /// Get codec description from media elementary stream.
        ///  LibVLC 3.0.0 and later.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_get_codec_description(MediaTrackTypes type, UInt32 codec);

        /// <summary>
        /// Get duration (in ms) of media descriptor object item.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern long libvlc_media_get_duration(IntPtr mediaInstance);

        /// <summary>
        /// Read a meta of the media.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_get_meta(IntPtr mediaInstance, MediaMetadatas meta);

        /// <summary>
        /// Get the media resource locator (mrl) from a media descriptor object.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_get_mrl(IntPtr mediaInstance);

        /// <summary>
        /// Get current state of media descriptor object.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MediaStates libvlc_media_get_state(IntPtr mediaInstance);

        /// <summary>
        /// Get the current statistics about the media descriptor object.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_get_stats(IntPtr mediaInstance, out MediaStatsStructure stats);

        /// <summary>
        /// Get media descriptor's elementary streams description.
        /// </summary>
        [Obsolete("Use GetMediaTracks instead")]
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_get_tracks_info(IntPtr mediaInstance, out IntPtr tracksInformationsPointer);

        /// <summary>
        /// Get Parsed status for media descriptor object.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_is_parsed(IntPtr mediaInstance);

        /// <summary>
        /// Create a media with custom callbacks to read the data from.
        /// Note: If open_cb is <see cref="IntPtr.Zero"/>, the opaque pointer will be passed to <paramref name="read_cb"/>, <paramref name="seek_cb"/> and <paramref name="close_cb"/>, and the stream size will be treated as unknown.
        /// The callbacks may be called asynchronously (from another thread). A single stream instance need not be reentrant. However the <pararef name="open_cb needs"/> to be reentrant if the media is used by multiple player instances.
        /// Warning: The callbacks may be used until all or any player instances that were supplied the media item are stopped.
        /// </summary>
        /// <returns>Return the newly created media or <see cref="IntPtr.Zero"/> on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_new_callbacks(IntPtr mediaPlayerInstance,
        CallbackOpenMediaDelegate open_cb,
        CallbackReadMediaDelegate read_cb,
        CallbackSeekMediaDelegate seek_cb,
        CallbackCloseMediaDelegate close_cb,
        IntPtr opaque);

        /// <summary>
        /// Create a media for an already open file descriptor.
        /// The file descriptor shall be open for reading (or reading and writing).
        /// 
        /// Regular file descriptors, pipe read descriptors and character device
        /// descriptors (including TTYs) are supported on all platforms.
        /// Block device descriptors are supported where available.
        /// Directory descriptors are supported on systems that provide <c>fdopendir()</c>.
        /// Sockets are supported on all platforms where they are file descriptors,
        /// i.e. all except Windows.
        /// </summary>
        /// <remarks>
        /// This library will <b>not</b> automatically close the file descriptor
        /// under any circumstance. Nevertheless, a file descriptor can usually only be
        /// rendered once in a media player. To render it a second time, the file
        /// descriptor should probably be rewound to the beginning with lseek().
        /// </remarks>
        /// <param name="instance">
        /// The instance
        /// </param>
        /// <param name="fd">
        /// An open file descriptor
        /// </param>
        /// <returns>
        /// the newly created media or NULL on error
        /// </returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_new_fd(IntPtr instance, int fd);

        /// <summary>
        /// Create a media with a certain given media resource location, for instance a valid URL.
        /// </summary>
        /// <returns>Return the newly created media or NULL on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_new_location(IntPtr instance, Utf8StringHandle mrl);

        /// <summary>
        /// Create a media for a certain file path.
        /// </summary>
        /// <returns>Return the newly created media or NULL on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_new_path(IntPtr instance, Utf8StringHandle mrl);

        /// <summary>
        /// Parse a media meta data and tracks information. 
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_parse(IntPtr instance);

        /// <summary>
        /// Parse a media meta data and tracks information async. 
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_parse_async(IntPtr mediaInstance);

        /// <summary>
        /// It will release the media descriptor object. It will send out an libvlc_MediaFreed event to all listeners. If the media descriptor object has been released it should not be used again.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_release(IntPtr mediaInstance);

        /// <summary>
        /// Save the meta previously set.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_save_meta(IntPtr mediaInstance);

        /// <summary>
        /// Set the meta of the media (this function will not save the meta, call SaveMediaMeta in order to save the meta)
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_set_meta(IntPtr mediaInstance, MediaMetadatas meta, Utf8StringHandle value);

        /// <summary>
        /// Get media descriptor's elementary streams description.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint libvlc_media_tracks_get(IntPtr mediaInstance, out IntPtr tracksPointerArray);

        /// <summary>
        /// Release media descriptor's elementary streams description array.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_tracks_release(IntPtr tracksPointerArray, uint tracksCount);
    }
}
