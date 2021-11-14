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
    internal sealed class VlcMediaPlayerInstance : InteropObjectInstance
    {
        private readonly VlcManager myManager;

        internal VlcMediaPlayerInstance(VlcManager manager, IntPtr pointer) : base(ref pointer)
        {
            myManager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            if (Pointer != IntPtr.Zero)
                myManager.ReleaseMediaPlayer(this);
            base.Dispose(disposing);
        }

        public static implicit operator IntPtr(VlcMediaPlayerInstance instance)
        {
            return instance != null
                ? instance.Pointer
                : IntPtr.Zero;
        }
    }
}