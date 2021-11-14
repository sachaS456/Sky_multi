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
    internal class LogoManagement : ILogoManagement
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public LogoManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public bool Enabled
        {
            get { return myManager.GetVideoLogoEnabled(myMediaPlayer); }
            set { myManager.SetVideoLogoEnabled(myMediaPlayer, value); }
        }

        public string File
        {
            set { myManager.SetVideoLogoFile(myMediaPlayer, value); }
        }

        public int X
        {
            get { return myManager.GetVideoLogoX(myMediaPlayer); }
            set { myManager.SetVideoLogoX(myMediaPlayer, value); }
        }

        public int Y
        {
            get { return myManager.GetVideoLogoY(myMediaPlayer); }
            set { myManager.SetVideoLogoY(myMediaPlayer, value); }
        }

        public int Delay
        {
            get { return myManager.GetVideoLogoDelay(myMediaPlayer); }
            set { myManager.SetVideoLogoDelay(myMediaPlayer, value); }
        }

        public int Repeat
        {
            get { return myManager.GetVideoLogoRepeat(myMediaPlayer); }
            set { myManager.SetVideoLogoRepeat(myMediaPlayer, value); }
        }

        public int Opacity
        {
            get { return myManager.GetVideoLogoOpacity(myMediaPlayer); }
            set { myManager.SetVideoLogoOpacity(myMediaPlayer, value); }
        }

        public int Position
        {
            get { return myManager.GetVideoLogoPosition(myMediaPlayer); }
            set { myManager.SetVideoLogoPosition(myMediaPlayer, value); }
        }
    }
}
