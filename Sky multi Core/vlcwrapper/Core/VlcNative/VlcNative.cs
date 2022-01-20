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
        /// Returns a list of audio filters that are available.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_audio_filter_list_get(IntPtr instance);

        /// <summary>
        /// CCallback function notification.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_callback_t(IntPtr args);

        /// <summary>
        /// Clears the LibVLC error status for the current thread. This is optional. By default, the error status is automatically overridden when a new error occurs, and destroyed when the thread exits.
        /// </summary>
        /// <returns>Return the libvlc instance or NULL in case of error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_clearerr();

        /// <summary>
        /// A human-readable error message for the last LibVLC error in the calling thread. The resulting string is valid until another error occurs (at least until the next LibVLC call).
        /// </summary>
        /// <returns>Return the libvlc instance or NULL in case of error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_errmsg();

        /// <summary>
        /// Register for an event notification.
        /// </summary>
        /// <param name="eventManagerInstance">Event manager to which you want to attach to.</param>
        /// <param name="eventType">The desired event to which we want to listen.</param>
        /// <param name="callback">The function to call when i_event_type occurs.</param>
        /// <param name="userData">User provided data to carry with the event.</param>
        /// <returns>Return 0 on success, ENOMEM on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_event_attach(IntPtr eventManagerInstance, EventTypes eventType, EventCallback callback, IntPtr userData);

        /// <summary>
        /// Unregister an event notification.
        /// </summary>
        /// <param name="eventManagerInstance">Event manager to which you want to attach to.</param>
        /// <param name="eventType">The desired event to which we want to listen.</param>
        /// <param name="callback">The function to call when i_event_type occurs.</param>
        /// <param name="userData">User provided data to carry with the event.</param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_event_detach(IntPtr eventManagerInstance, EventTypes eventType, EventCallback callback, IntPtr userData);

        /// <summary>
        /// Get an event's type name.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_event_type_name(EventTypes eventType);

        /// <summary>
        /// Frees an heap allocation returned by a LibVLC function.
        /// </summary>
        /// <returns>Return the libvlc instance or NULL in case of error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_free(IntPtr ptr);

        /// <summary>
        /// Retrieve libvlc changeset.
        /// </summary>
        /// <returns>Return a string containing the libvlc changeset.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_get_changeset();

        /// <summary>
        /// Retrieve libvlc compiler version.
        /// </summary>
        /// <returns>Return a string containing the libvlc compiler version.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_get_compiler();

        /// <summary>
        /// Retrieve libvlc version.
        /// </summary>
        /// <returns>Return a string containing the libvlc version.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_get_version();

        /// <summary>
        /// The delegate type that represent logging functions
        /// </summary>
        /// <param name="data">The data given to libvlc_log_set. In our case, this value will always be <see langword="null" /></param>
        /// <param name="level">The log level</param>
        /// <param name="logContext">The address of the structure that contains information about the log event. <see cref="GetLogContext"/></param>
        /// <param name="format">The format of the log messages</param>
        /// <param name="args">The va_list of printf arguments.</param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_log_cb(IntPtr data, VlcLogLevel level, IntPtr logContext, [MarshalAs(UnmanagedType.LPStr)] string format, IntPtr args);

        /// <summary>
        /// Gets log message debug infos.
        ///
        /// This function retrieves self-debug information about a log message:
        /// - the name of the VLC module emitting the message,
        /// - the name of the source code module (i.e.file) and
        /// - the line number within the source code module.
        ///
        /// The returned module name and file name will be NULL if unknown.
        /// The returned line number will similarly be zero if unknown.
        /// </summary>
        /// <param name="logContext">The log message context (as passed to the <see cref="LogCallback"/>)</param>
        /// <param name="module">The module name storage.</param>
        /// <param name="file">The source code file name storage.</param>
        /// <param name="line">The source code file line number storage.</param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_log_get_context(IntPtr logContext, out IntPtr module, out IntPtr file, out UIntPtr line);

        /// <summary>
        /// Registers a log callback
        /// </summary>
        /// <param name="libVlcInstance">The libvlc instance.</param>
        /// <param name="callback">The method that will be called whenever a log is available.</param>
        /// <param name="userData">User provided data to carry with the event.</param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_log_set(IntPtr libVlcInstance, LogCallback callback, IntPtr userData);

        /// <summary>
        /// Release a list of module descriptions.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_module_description_list_release(IntPtr moduleDescription);

        /// <summary>
        /// Create and initialize a libvlc instance.
        /// </summary>
        /// <returns>Return the libvlc instance or NULL in case of error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_new(int argc, IntPtr[] argv);

        /// <summary>
        /// Destroy libvlc instance.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_release(IntPtr instance);

        /// <summary>
        /// Sets some meta-information about the application. 
        /// </summary>
        /// <seealso cref="SetUserAgent" />
        /// <param name="instance">LibVLC instance</param>
        /// <param name="id">Java-style application identifier, e.g. "com.acme.foobar"</param>
        /// <param name="version">application version numbers, e.g. "1.2.3"</param>
        /// <param name="icon">application icon name, e.g. "foobar"</param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_set_app_id(IntPtr instance, Utf8StringHandle id, Utf8StringHandle version, Utf8StringHandle icon);

        /// <summary>
        /// Sets the application name.
        /// LibVLC passes this as the user agent string when a protocol requires it.
        /// </summary>
        /// <param name="instance">LibVLC instance</param>
        /// <param name="name">human-readable application name, e.g. "FooBar player 1.2.3"</param>
        /// <param name="http">HTTP User Agent, e.g. "FooBar/1.2.3 Python/2.6.0"</param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_set_user_agent(IntPtr instance, Utf8StringHandle name, Utf8StringHandle http);

        /// <summary>
        /// Returns a list of video filters that are available.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_video_filter_list_get(IntPtr instance);
    }   
}
