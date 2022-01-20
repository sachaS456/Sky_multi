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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Sky_multi_Core
{
	public static class ResolutionMonitor
	{
		public static bool SetResolution(int iWidth, int iHeight)
        {
			//Screen screen = Screen.PrimaryScreen;

			//int iWidth = a;
			//int iHeight = b;

			DEVMODE1 dm = new DEVMODE1();
			dm.dmDeviceName = new String(new char[32]);
			dm.dmFormName = new String(new char[32]);
			dm.dmSize = (short)Marshal.SizeOf(dm);

			if (0 != User_32.EnumDisplaySettings(null, User_32.ENUM_CURRENT_SETTINGS, ref dm))
			{

				dm.dmPelsWidth = iWidth;
				dm.dmPelsHeight = iHeight;

				int iRet = User_32.ChangeDisplaySettings(ref dm, User_32.CDS_TEST);

				if (iRet == User_32.DISP_CHANGE_FAILED)
				{
					//MessageBox.Show("On ne peut proceder au chagement de Resolution");
					//MessageBox.Show("Description: On ne peut proceder au changement de Resolution. Desolé pour cette inconvénient.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					//throw new ArgumentException("Cannot change resolution!");
					return false;
				}
				else
				{
					iRet = User_32.ChangeDisplaySettings(ref dm, User_32.CDS_UPDATEREGISTRY);

					switch (iRet)
					{
						case User_32.DISP_CHANGE_SUCCESSFUL:
						{
							return true;

							// Changé avec succes
					    }
						case User_32.DISP_CHANGE_RESTART:
						{
							//MessageBox.Show("Description: VOus devez demarer votre ordinateur pour appliquer le changement.\n Si vous avez tout probleme aplrés redemarage.\nThen Essayer d'appliquer le Changement de Resolution en 'Safe Mode'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
							return false;
							// les series windows 9x, Vous devez redemarer la machine.
						}
						default:
						{
							//MessageBox.Show("Description: Imposible de changer la Resolution.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
							return false;
							// Changement echoué...
						}
					}
				}
			}

			return false;
		}

		public static bool SetResolution(Resolution resolution)
        {
			return SetResolution(resolution.width, resolution.Height);
		}

		public static Resolution[] GetResolutionEnumeration()
        {
			DEVMODE1 DevM = new DEVMODE1();
			DevM.dmDeviceName = new String(new char[32]);
			DevM.dmFormName = new String(new char[32]);
			DevM.dmSize = (short)Marshal.SizeOf(DevM);

		    int dMode = -1;
			int index = -1;

			List<Resolution> REnum = new List<Resolution>();

			do
			{
				if (DevM.dmPelsWidth >= 800 && DevM.dmPelsHeight >= 600)
                {
					if (index == -1)
					{
						REnum.Add(new Resolution(DevM.dmPelsWidth, DevM.dmPelsHeight));
						index++;
					}
					else if (REnum[index].width != DevM.dmPelsWidth || REnum[index].Height != DevM.dmPelsHeight)
                    {
						REnum.Add(new Resolution(DevM.dmPelsWidth, DevM.dmPelsHeight));
						index++;
					}
				}

				dMode++;
			}
			while (User_32.EnumDisplaySettings(null, dMode, ref DevM) != 0);

			List<Resolution> Tri = new List<Resolution>();
		    index = -1;

			// triage des résolutions du plus grand au plus petit
			foreach (Resolution resolution in REnum)
            {
				if (index == -1)
				{
					Tri.Add(resolution);
				}
				else
				{
					for (int index2 = 0; index2 <= Tri.Count(); index2++)
                    {
						if (index2 == Tri.Count())
                        {
							Tri.Insert(0, resolution);
							break;
						}

						if (Tri[index - index2].width < resolution.width)
						{
							continue;
						}
						else if (Tri[index - index2].width == resolution.width)
						{
							if (Tri[index - index2].Height < resolution.Height)
							{
								continue;
							}
							else
							{
								Tri.Insert(index - (index2 - 1), resolution);
								break;
							}
						}
						else
						{
							Tri.Insert(index - (index2 - 1), resolution);
							break;
						}
					}
				}

				index++;
			}

			REnum.Clear();

			return Tri.ToArray();
		}

		public static bool SetResolutionMax()
        {
			// Toutes les résolutions (et ratio) pris en charge par windows 10 ils le sont pris en charge

			try
            {
				Resolution resolution = new Resolution(GetResolutionW(), GetResolutionH());

				bool resolutionChanged = SetResolution(GetResolutionEnumeration()[0]);

				if (resolution.width == GetResolutionW() && resolution.Height == GetResolutionH())
                {
					resolutionChanged = false;
				}

				return resolutionChanged;
			}
            catch
            {
				return false;
            }
		}		

		public static int GetResolutionW()
		{
			return User_32.GetSystemMetrics(User_32.SM_CXSCREEN);
		}

		public static int GetResolutionH()
		{
			return User_32.GetSystemMetrics(User_32.SM_CYSCREEN);
		}		
	}
}
