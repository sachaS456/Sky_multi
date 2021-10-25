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
	internal static class User_32
	{
		[DllImport("user32.dll")]
		internal static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE1 devMode);
		[DllImport("user32.dll")]
		internal static extern int ChangeDisplaySettings(ref DEVMODE1 devMode, int flags);
		[DllImport("User32.dll")]
		internal static extern int GetSystemMetrics(int nIndex);

		internal const int SM_CXSCREEN = 0;
		internal const int SM_CYSCREEN = 1;

		internal const int ENUM_CURRENT_SETTINGS = -1;
		internal const int CDS_UPDATEREGISTRY = 0x01;
		internal const int CDS_TEST = 0x02;
		internal const int DISP_CHANGE_SUCCESSFUL = 0;
		internal const int DISP_CHANGE_RESTART = 1;
		internal const int DISP_CHANGE_FAILED = -1;

	}
}
