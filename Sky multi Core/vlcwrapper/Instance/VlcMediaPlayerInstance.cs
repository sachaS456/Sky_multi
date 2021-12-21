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
        internal VlcMediaPlayerInstance(in IntPtr pointer) : base(in pointer)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (Pointer != IntPtr.Zero)
            {
                if (this == IntPtr.Zero)
                    return;
                try
                {
                    VlcNative.libvlc_media_player_release(this);
                }
                finally
                {
                    this.Pointer = IntPtr.Zero;
                }
            }

            base.Dispose(disposing);
        }

        public static implicit operator IntPtr(in VlcMediaPlayerInstance instance)
        {
            return instance != null
                ? instance.Pointer
                : IntPtr.Zero;
        }
    }
}