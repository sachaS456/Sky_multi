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

namespace Sky_multi_Core.VlcWrapper
{
    internal class AdjustmentsManagement : IAdjustmentsManagement
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public AdjustmentsManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public bool Enabled
        {
            get { return myManager.GetVideoAdjustEnabled(myMediaPlayer); }
            set { myManager.SetVideoAdjustEnabled(myMediaPlayer, value); }
        }

        public float Contrast
        {
            get { return myManager.GetVideoAdjustContrast(myMediaPlayer); }
            set { myManager.SetVideoAdjustContrast(myMediaPlayer, value); }
        }

        public float Brightness
        {
            get { return myManager.GetVideoAdjustBrightness(myMediaPlayer); }
            set { myManager.SetVideoAdjustBrightness(myMediaPlayer, value); }
        }

        public float Hue
        {
            get { return myManager.GetVideoAdjustHue(myMediaPlayer); }
            set { myManager.SetVideoAdjustHue(myMediaPlayer, value); }
        }

        public float Saturation
        {
            get { return myManager.GetVideoAdjustSaturation(myMediaPlayer); }
            set { myManager.SetVideoAdjustSaturation(myMediaPlayer, value); }
        }

        public float Gamma
        {
            get { return myManager.GetVideoAdjustGamma(myMediaPlayer); }
            set { myManager.SetVideoAdjustGamma(myMediaPlayer, value); }
        }
    }
}
