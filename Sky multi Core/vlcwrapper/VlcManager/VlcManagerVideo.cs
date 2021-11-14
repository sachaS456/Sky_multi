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

using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    using System;

    public sealed partial class VlcManager
    {
        private LockVideoCallback _lockVideoCallbackReference;
        private UnlockVideoCallback _unlockVideoCallbackReference;
        private DisplayVideoCallback _displayVideoCallbackReference;
        private VideoFormatCallback _videoFormatCallbackReference;
        private CleanupVideoCallback _cleanupCallbackReference;

        /// <summary>
        /// Sets the video callbacks to render decoded video to a custom area in memory.
        /// The media player will hold a reference on the IVideoCallbacks parameter
        /// </summary>
        /// <remarks>
        /// Rendering video into custom memory buffers is considerably less efficient than rendering in a custom window as normal.
        /// See libvlc_video_set_callbacks for detailed explanations
        /// </remarks>
        /// <param name="lockVideo">
        /// Callback to lock video memory (must not be NULL)
        /// </param>
        /// <param name="unlockVideo">
        /// Callback to unlock video memory (or NULL if not needed)
        /// </param>
        /// <param name="display">
        /// Callback to display video (or NULL if not needed)
        /// </param>
        /// <param name="userData">
        /// Private pointer for the three callbacks (as first parameter).
        /// This parameter will be overriden if <see cref="SetVideoFormatCallbacks"/> is used
        /// </param>
        public void SetVideoCallbacks(LockVideoCallback lockVideo, UnlockVideoCallback unlockVideo, DisplayVideoCallback display, IntPtr userData)
        {
            if (lockVideo == null)
            {
                throw new ArgumentNullException(nameof(lockVideo));
            }

            this._lockVideoCallbackReference = lockVideo;
            this._unlockVideoCallbackReference = unlockVideo;
            this._displayVideoCallbackReference = display;

            this.SetVideoCallbacks(this.myMediaPlayerInstance, this._lockVideoCallbackReference, this._unlockVideoCallbackReference, this._displayVideoCallbackReference, userData);
        }

        /// <summary>
        /// Set decoded video chroma and dimensions. This only works in combination with
        /// <see cref="SetVideoCallbacks" />
        /// </summary>
        /// <param name="videoFormat">Callback to select the video format (cannot be NULL)</param>
        /// <param name="cleanup">Callback to release any allocated resources (or NULL)</param>
        public void SetVideoFormatCallbacks(VideoFormatCallback videoFormat, CleanupVideoCallback cleanup)
        {
            if (videoFormat == null)
            {
                throw new ArgumentNullException(nameof(videoFormat));
            }

            this._videoFormatCallbackReference = videoFormat;
            this._cleanupCallbackReference = cleanup;

            this.SetVideoFormatCallbacks(this.myMediaPlayerInstance, this._videoFormatCallbackReference, this._cleanupCallbackReference);
        }
    }
}