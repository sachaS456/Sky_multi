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
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Sky_multi_Core.VlcWrapper.Core;
    using System.Runtime.InteropServices;

    internal class DialogsManagement : IDialogsManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;
        private readonly VlcInstance myVlcInstance;

        private IVlcDialogManager currentDialogManager;

        private Dictionary<IntPtr, CancellationTokenSource> openDialogsCancellationTokens = new Dictionary<IntPtr, CancellationTokenSource>();

        internal DialogsManagement(VlcMediaPlayerInstance mediaPlayerInstance, VlcInstance vlcInstance)
        {
            myMediaPlayer = mediaPlayerInstance;
            myVlcInstance = vlcInstance;
        }

        private void myMediaPlayerIsLoad()
        {
            if (myMediaPlayer == IntPtr.Zero)
            {
                throw new ArgumentException("Media player instance is not initialized.");
            }
        }

        public void UseDialogManager(IVlcDialogManager dialogManager, IntPtr data = default(IntPtr))
        {
            foreach (var cts in this.openDialogsCancellationTokens.Values)
            {
                if (!cts.IsCancellationRequested)
                {
                    cts.Cancel();
                }
            }

            this.currentDialogManager = dialogManager;
            if (dialogManager == null)
            {
                this.openDialogsCancellationTokens = null;
                myMediaPlayerIsLoad();
                this.SetDialogCallbacks(null, data);
            }
            else
            {
                this.openDialogsCancellationTokens = new Dictionary<IntPtr, CancellationTokenSource>();
                myMediaPlayerIsLoad();
                this.SetDialogCallbacks(new DialogCallbacks
                {
                    DisplayError = this.OnDisplayError,
                    DisplayLogin = this.OnDisplayLogin,
                    DisplayQuestion = this.OnDisplayQuestion,
                    DisplayProgress = this.OnDisplayProgress,
                    UpdateProgress = this.OnUpdateProgress,
                    Cancel = this.OnCancel
                }, data);
            }
        }

        private void OnDisplayError(IntPtr userdata, IntPtr title, IntPtr text)
        {
            string strTitle = Utf8InteropStringConverter.Utf8InteropToString(title);
            string strText = Utf8InteropStringConverter.Utf8InteropToString(text);
            Task.Run(() => this.currentDialogManager?.DisplayErrorAsync(userdata, strTitle, strText));
        }

        private void OnDisplayLogin(IntPtr userdata, IntPtr dialogid, IntPtr title, IntPtr text, IntPtr defaultusername, bool askstore)
        {
            string strTitle = Utf8InteropStringConverter.Utf8InteropToString(title);
            string strText = Utf8InteropStringConverter.Utf8InteropToString(text);
            string strUsername = Utf8InteropStringConverter.Utf8InteropToString(defaultusername);
            var cts = new CancellationTokenSource();
            this.openDialogsCancellationTokens.Add(dialogid,cts);
            Task.Run(() => this.currentDialogManager.DisplayLoginAsync(userdata, dialogid, strTitle, strText, strUsername, askstore, cts.Token))
                .ContinueWith(task =>
                {
                    if (task.IsCompleted && task.Result != null)
                    {
                        using (var usr = Utf8InteropStringConverter.ToUtf8StringHandle(task.Result.Username))
                        using (var pass = Utf8InteropStringConverter.ToUtf8StringHandle(task.Result.Password))
                        {
                            myMediaPlayerIsLoad();
                            this.PostLogin(dialogid, usr, pass, task.Result.StoreCredentials);
                        }
                    }
                    else if(!task.IsCanceled)
                    {
                        myMediaPlayerIsLoad();
                        this.DismissDialog(dialogid);
                    }

                    this.openDialogsCancellationTokens.Remove(dialogid);
                });
        }

        private void OnDisplayQuestion(IntPtr userdata, IntPtr dialogid, IntPtr title, IntPtr text, DialogQuestionType questiontype, IntPtr cancel, IntPtr action1, IntPtr action2)
        {
            string strTitle = Utf8InteropStringConverter.Utf8InteropToString(title);
            string strText = Utf8InteropStringConverter.Utf8InteropToString(text);
            string strCancelButton = Utf8InteropStringConverter.Utf8InteropToString(cancel);
            string strAction1Button = Utf8InteropStringConverter.Utf8InteropToString(action1);
            string strAction2Button = Utf8InteropStringConverter.Utf8InteropToString(action2);
            var cts = new CancellationTokenSource();
            this.openDialogsCancellationTokens.Add(dialogid, cts);
            Task.Run(() => this.currentDialogManager.DisplayQuestionAsync(userdata, dialogid, strTitle, strText, questiontype, strCancelButton, strAction1Button, strAction2Button, cts.Token))
                .ContinueWith((Action<Task<QuestionAction?>>)(task =>
                {
                    if (task.IsCompleted && task.Result != null)
                    {
                        myMediaPlayerIsLoad();
                        this.PostAction(dialogid, (int)task.Result.Value);
                    }
                    else if (!task.IsCanceled)
                    {
                        myMediaPlayerIsLoad();
                        this.DismissDialog(dialogid);
                    }

                    this.openDialogsCancellationTokens.Remove(dialogid);
                }));
        }

        private void OnDisplayProgress(IntPtr userdata, IntPtr dialogid, IntPtr title, IntPtr text, bool indeterminate, float position, IntPtr cancel)
        {
            string strTitle = Utf8InteropStringConverter.Utf8InteropToString(title);
            string strText = Utf8InteropStringConverter.Utf8InteropToString(text);
            string strCancelButton = Utf8InteropStringConverter.Utf8InteropToString(cancel);
            var cts = new CancellationTokenSource();
            this.openDialogsCancellationTokens.Add(dialogid, cts);
            Task.Run(() => this.currentDialogManager.DisplayProgressAsync(userdata, dialogid, strTitle, strText, indeterminate, position, strCancelButton, cts.Token))
                .ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        myMediaPlayerIsLoad();
                        this.DismissDialog(dialogid);
                    }

                    this.openDialogsCancellationTokens.Remove(dialogid);
                });
        }

        private void OnUpdateProgress(IntPtr userdata, IntPtr dialogid, float position, IntPtr text)
        {
            string strText = Utf8InteropStringConverter.Utf8InteropToString(text);
            Task.Run(() => this.currentDialogManager.UpdateProgress(userdata, dialogid, position, strText));
        }

        private void OnCancel(IntPtr userdata, IntPtr dialogid)
        {
            if (this.openDialogsCancellationTokens.TryGetValue(dialogid, out var cts))
            {
                cts.Cancel();
            }
        }

        private DialogCallbacks? dialogCallbacks;

        private IntPtr dialogCallbacksPointer;

        /// <summary>
        /// Register callbacks in order to handle VLC dialogs. LibVLC 3.0.0 and later.
        /// </summary>
        private void SetDialogCallbacks(DialogCallbacks? callbacks, IntPtr data)
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
        private void SetDialogContext(IntPtr dialogId, IntPtr data)
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
        private IntPtr GetDialogContext(IntPtr dialogId)
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
        private int PostLogin(IntPtr dialogId, Utf8StringHandle username, Utf8StringHandle password, bool store)
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
        private int PostAction(IntPtr dialogId, int action)
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
        private int DismissDialog(IntPtr dialogId)
        {
            if (VlcVersionNumber.Major < 3)
            {
                throw new InvalidOperationException($"You need VLC version 3.0 or higher to be able to use {nameof(DismissDialog)}");
            }

            return VlcNative.libvlc_dialog_dismiss(dialogId);
        }

        private string VlcVersion => Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_get_version());

        private Version VlcVersionNumber
        {
            get
            {
                string versionString = this.VlcVersion;
                versionString = versionString.Split('-', ' ')[0];

                return new Version(versionString);
            }
        }
    }
}