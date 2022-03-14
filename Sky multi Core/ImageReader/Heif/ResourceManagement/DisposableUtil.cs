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

namespace Sky_multi_Core.ImageReader.Heif.ResourceManagement
{
    internal static class DisposableUtil
    {
        public static void Free<TDisposable>(ref TDisposable disposable) where TDisposable : class, IDisposable
        {
            if (disposable != null)
            {
                disposable.Dispose();
                disposable = null;
            }
        }
    }
}
