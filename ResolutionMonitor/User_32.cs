using System.Runtime.InteropServices;

namespace ResolutionMonitor
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
