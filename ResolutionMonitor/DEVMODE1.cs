using System.Runtime.InteropServices;

namespace ResolutionMonitor
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
