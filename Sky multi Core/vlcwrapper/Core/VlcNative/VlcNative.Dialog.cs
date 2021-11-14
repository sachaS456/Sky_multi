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
using System.Runtime.InteropServices;

namespace Sky_multi_Core.VlcWrapper.Core
{
    internal static partial class VlcNative
    {
        /// <summary>
        /// Register callbacks in order to handle VLC dialogs. LibVLC 3.0.0 and later.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_dialog_set_callbacks(IntPtr instance, /*ref DialogCallbacks?*/ IntPtr callbacks, IntPtr data);

        /// <summary>
        /// Associate an opaque pointer with the dialog id
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_dialog_set_context(IntPtr dialogId, IntPtr data);

        /// <summary>
        /// Return the opaque pointer associated with the dialog id
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_dialog_get_context(IntPtr dialogId);

        /// <summary>
        /// Post a login answer
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_dialog_post_login(IntPtr dialogId, IntPtr username, IntPtr password, bool store);

        /// <summary>
        /// Post a question answer
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_dialog_post_action(IntPtr dialogId, int action);

        /// <summary>
        /// Dismiss a dialog
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_dialog_dismiss(IntPtr dialogId);        
    }

    /// <summary>
    /// The level of the dialog to be displayed.
    /// </summary>
    public enum DialogQuestionType : int
    {
        Normal = 0,
        Warning,
        Critical
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ErrorDialogCallback(IntPtr userData, IntPtr title, IntPtr text);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LoginDialogCallback(IntPtr userData, IntPtr dialogId, IntPtr title, IntPtr text, IntPtr defaultUserName, bool askStore);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void QuestionDialogCallback(IntPtr userData, IntPtr dialogId, IntPtr title, IntPtr text, DialogQuestionType questionType, IntPtr cancel, IntPtr action1, IntPtr action2);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ProgressDialogCallback(IntPtr userData, IntPtr dialogId, IntPtr title, IntPtr text, bool indeterminate, float position, IntPtr cancel);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CancelDialogCallback(IntPtr userData, IntPtr dialogId);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UpdateProgressDialogCallback(IntPtr userData, IntPtr dialogId, float position, IntPtr text);

    [StructLayout(LayoutKind.Sequential)]
    public struct DialogCallbacks
    {
        /// <summary>
        /// Called when an error message needs to be displayed
        /// </summary>
        public ErrorDialogCallback DisplayError;

        /// <summary>
        /// Called when a login dialog needs to be displayed
        /// 
        /// You can interact with this dialog by calling libvlc_dialog_post_login()
        /// to post an answer or libvlc_dialog_dismiss() to cancel this dialog.
        /// </summary>
        /// <remarks>To receive this callback, libvlc_dialog_cbs.pf_cancel should not be NULL</remarks>
        public LoginDialogCallback DisplayLogin;

        /// <summary>
        /// Called when a question dialog needs to be displayed
        /// 
        /// You can interact with this dialog by calling libvlc_dialog_post_action()
        /// to post an answer or libvlc_dialog_dismiss() to cancel this dialog.
        /// </summary>
        /// <remarks>To receive this callback, libvlc_dialog_cbs.pf_cancel should not be NULL</remarks>
        public QuestionDialogCallback DisplayQuestion;

        /// <summary>
        /// Called when a progress dialog needs to be displayed
        /// 
        /// If cancellable (psz_cancel != NULL), you can cancel this dialog by
        /// calling libvlc_dialog_dismiss()
        /// </summary>
        /// <remarks>To receive this callback, libvlc_dialog_cbs.pf_cancel should not be NULL</remarks>
        public ProgressDialogCallback DisplayProgress;

        /// <summary>
        /// Called when a displayed dialog needs to be cancelled
        /// 
        /// The implementation must call libvlc_dialog_dismiss() to really release
        /// the dialog.
        /// </summary>
        public CancelDialogCallback Cancel;

        /// <summary>
        /// Called when a progress dialog needs to be updated
        /// </summary>
        public UpdateProgressDialogCallback UpdateProgress;
    }
}
