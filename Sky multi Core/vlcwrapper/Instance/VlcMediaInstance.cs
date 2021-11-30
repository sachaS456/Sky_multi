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
using System.Collections.Generic;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal sealed class VlcMediaInstance : InteropObjectInstance
    {
        private static readonly Dictionary<IntPtr, VlcMediaInstance> AllInstances = new Dictionary<IntPtr, VlcMediaInstance>();

        internal static VlcMediaInstance New(IntPtr pointer)
        {
            lock (AllInstances)
            {
                AllInstances.TryGetValue(pointer, out var instance);

                if (null == instance)
                {
                    instance = new VlcMediaInstance(pointer);
                    AllInstances.Add(pointer, instance);
                }

                return instance;
            }
        }

        private VlcMediaInstance(IntPtr pointer) : base(ref pointer)
        {

        }

        protected override void Dispose(bool disposing)
        {
            lock (AllInstances)
            {
                AllInstances.Remove(this);
            }
            
            if (Pointer != IntPtr.Zero)
            {
                VlcNative.libvlc_media_release(this);
            }

            base.Dispose(disposing);
        }

        public static implicit operator IntPtr(VlcMediaInstance instance)
        {
            return instance.Pointer;
        }
    }
}