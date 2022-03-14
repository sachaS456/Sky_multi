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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.ImageReader.Heif.Interop
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    [StructLayout(LayoutKind.Sequential)]
    internal readonly struct heif_error
    {
#pragma warning disable IDE0032 // Use auto property
        private readonly heif_error_code errorCode;
        private readonly heif_suberror_code suberrorCode;
        private readonly IntPtr message;
#pragma warning restore IDE0032 // Use auto property

        public heif_error(heif_error_code errorCode, heif_suberror_code suberrorCode, IntPtr message)
        {
            this.errorCode = errorCode;
            this.suberrorCode = suberrorCode;
            this.message = message;
        }

        public heif_error_code ErrorCode => this.errorCode;

        public heif_suberror_code Suberror => this.suberrorCode;

        public bool IsError => this.errorCode != heif_error_code.Ok;

        public void ThrowIfError()
        {
            if (this.IsError)
            {
                ExceptionUtil.ThrowHeifException(GetErrorMessage());
            }
        }

        private string GetDebuggerDisplay()
        {
            if (this.suberrorCode != heif_suberror_code.Unspecified)
            {
                return $"{ this.errorCode }:{ this.suberrorCode }";
            }
            else
            {
                return this.errorCode.ToString();
            }
        }

        private string GetErrorMessage()
        {
            return Marshal.PtrToStringAnsi(this.message) ?? Properties.Resources.UnspecifiedError;
        }
    }
}
