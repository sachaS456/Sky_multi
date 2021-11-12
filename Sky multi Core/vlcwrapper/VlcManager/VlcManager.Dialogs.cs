using System;
using System.Runtime.InteropServices;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        private DialogCallbacks? dialogCallbacks;

        private IntPtr dialogCallbacksPointer;

        /// <summary>
        /// Register callbacks in order to handle VLC dialogs. LibVLC 3.0.0 and later.
        /// </summary>
        public void SetDialogCallbacks(DialogCallbacks? callbacks, IntPtr data)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(SetDialogCallbacks)}");
            }

            if (this.dialogCallbacks.HasValue)
            {
                Marshal.FreeHGlobal(this.dialogCallbacksPointer);
                this.dialogCallbacksPointer = IntPtr.Zero;
            }

            this.dialogCallbacks = callbacks;
            if (this.dialogCallbacks.HasValue)
            {
                this.dialogCallbacksPointer = Marshal.AllocHGlobal(MarshalHelper.SizeOf<DialogCallbacks>());
                Marshal.StructureToPtr(this.dialogCallbacks.Value, this.dialogCallbacksPointer, false);
            }

            VlcNative.libvlc_dialog_set_callbacks(this.myVlcInstance, this.dialogCallbacksPointer, data);
        }

        /// <summary>
        /// Associate an opaque pointer with the dialog id
        /// </summary>
        public void SetDialogContext(IntPtr dialogId, IntPtr data)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(SetDialogContext)}");
            }

            VlcNative.libvlc_dialog_set_context(dialogId, data);
        }


        /// <summary>
        /// Return the opaque pointer associated with the dialog id
        /// </summary>
        public IntPtr GetDialogContext(IntPtr dialogId)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(GetDialogContext)}");
            }

            return VlcNative.libvlc_dialog_get_context(dialogId);
        }

        /// <summary>
        /// Post a login answer
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        public int PostLogin(IntPtr dialogId, Utf8StringHandle username, Utf8StringHandle password, bool store)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(PostLogin)}");
            }

            return VlcNative.libvlc_dialog_post_login(dialogId, username?.DangerousGetHandle() ?? IntPtr.Zero, password?.DangerousGetHandle() ?? IntPtr.Zero, store);
        }

        /// <summary>
        /// Post a question answer
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        public int PostAction(IntPtr dialogId, int action)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(PostAction)}");
            }

            return VlcNative.libvlc_dialog_post_action(dialogId, action);
        }

        /// <summary>
        /// Dismiss a dialog
        /// 
        /// After this call, p_id won't be valid anymore
        /// </summary>
        /// <returns>0 on success, or -1 on error</returns>
        public int DismissDialog(IntPtr dialogId)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(DismissDialog)}");
            }

            return VlcNative.libvlc_dialog_dismiss(dialogId);
        }
    }
}
