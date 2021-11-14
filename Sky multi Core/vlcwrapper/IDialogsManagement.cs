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

    public interface IDialogsManagement
    {
        /// <summary>
        /// Use this dialog manager to answer libvlc's questions
        /// </summary>
        /// <param name="dialogManager">The dialog manager to be used, or <c>null</c> to remove any dialog manager</param>
        /// <param name="data">Custom parameters to be passed to the dialogs</param>
        void UseDialogManager(IVlcDialogManager dialogManager, IntPtr data = default(IntPtr));
    }
}