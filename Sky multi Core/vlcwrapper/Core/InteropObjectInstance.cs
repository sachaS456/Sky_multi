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

namespace Sky_multi_Core.VlcWrapper.Core
{
    internal abstract class InteropObjectInstance : IDisposable
    {
        internal IntPtr Pointer { get; set; }

        private bool myIsDisposing = false;

        protected InteropObjectInstance(in IntPtr pointer)
        {
            Pointer = pointer;
        }

        public void Dispose()
        {
            if (myIsDisposing)
                return;
            myIsDisposing = true;
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Pointer = IntPtr.Zero;
        }

        ~InteropObjectInstance()
        {
            Dispose(false);
        }

        public override bool Equals(object obj)
        {
            var instance = obj as InteropObjectInstance;
            if (instance != null)
                return instance.Pointer == Pointer;
            return false;
        }

        public override int GetHashCode()
        {
            return Pointer.ToInt32();
        }

        public static bool operator ==(InteropObjectInstance a, InteropObjectInstance b)
        {
            if (Equals(a, null) || Equals(b, null))
                return Equals(a, b);

            return a.Pointer.Equals(b.Pointer);
        }

        public static bool operator !=(InteropObjectInstance a, InteropObjectInstance b)
        {
            return !(a == b);
        }
    }
}
