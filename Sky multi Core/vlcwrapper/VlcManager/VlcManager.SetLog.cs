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
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        /// <summary>
        /// Keeps a reference to the last callback that was given to the <see cref="SetLog"/> method.
        /// This is to avoid garbage collection of the delegate before the function is called.
        /// </summary>
        private LogCallback logCallbackReference;

        public void SetLog(LogCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentException("Callback for log is not initialized.");
            }

            this.logCallbackReference = callback;
            VlcNative.libvlc_log_set(this.myVlcInstance, this.logCallbackReference, IntPtr.Zero);
        }
    }
}
