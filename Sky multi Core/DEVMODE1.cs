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

using System.Runtime.InteropServices;

namespace Sky_multi_Core
{
	[StructLayout(LayoutKind.Sequential)]

	internal struct DEVMODE1
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string dmDeviceName;

		internal short dmSpecVersion;
		internal short dmDriverVersion;
		internal short dmSize;
		internal short dmDriverExtra;
		internal int dmFields;

		internal short dmOrientation;
		internal short dmPaperSize;
		internal short dmPaperLength;
		internal short dmPaperWidth;

		internal short dmScale;
		internal short dmCopies;
		internal short dmDefaultSource;
		internal short dmPrintQuality;
		internal short dmColor;
		internal short dmDuplex;
		internal short dmYResolution;
		internal short dmTTOption;
		internal short dmCollate;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string dmFormName;

		internal short dmLogPixels;
		internal short dmBitsPerPel;
		internal int dmPelsWidth;
		internal int dmPelsHeight;

		internal int dmDisplayFlags;
		internal int dmDisplayFrequency;

		internal int dmICMMethod;
		internal int dmICMIntent;
		internal int dmMediaType;
		internal int dmDitherType;
		internal int dmReserved1;
		internal int dmReserved2;

		internal int dmPanningWidth;
		internal int dmPanningHeight;
	};
}
