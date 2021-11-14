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

namespace Sky_multi_Core.VlcWrapper
{
    internal struct HardwareAccelerationTypeString
    {
        public const string d3d11 = ":avcodec-hw=d3d11va";
        public const string dxva2 = ":avcodec-hw=dxva2";
        public const string none = ":avcodec-hw=none";
    }

    public enum HardwareAccelerationType
    {
        d3d11,
        dxva2,
        none
    }
}
