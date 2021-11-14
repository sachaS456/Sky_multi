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
    internal class MarqueeManagement : IMarqueeManagement
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public MarqueeManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public bool Enabled
        {
            get { return myManager.GetVideoMarqueeEnabled(myMediaPlayer); }
            set { myManager.SetVideoMarqueeEnabled(myMediaPlayer, value); }
        }

        public string Text
        {
            get { return myManager.GetVideoMarqueeText(myMediaPlayer); }
            set { myManager.SetVideoMarqueeText(myMediaPlayer, value); }
        }

        public int Color
        {
            get { return myManager.GetVideoMarqueeColor(myMediaPlayer); }
            set { myManager.SetVideoMarqueeColor(myMediaPlayer, value); }
        }

        public int Opacity
        {
            get { return myManager.GetVideoMarqueeOpacity(myMediaPlayer); }
            set { myManager.SetVideoMarqueeOpacity(myMediaPlayer, value); }
        }

        public int Position
        {
            get { return myManager.GetVideoMarqueePosition(myMediaPlayer); }
            set { myManager.SetVideoMarqueePosition(myMediaPlayer, value); }
        }

        public int Refresh
        {
            get { return myManager.GetVideoMarqueeRefresh(myMediaPlayer); }
            set { myManager.SetVideoMarqueeRefresh(myMediaPlayer, value); }
        }

        public int Size
        {
            get { return myManager.GetVideoMarqueeSize(myMediaPlayer); }
            set { myManager.SetVideoMarqueeSize(myMediaPlayer, value); }
        }

        public int Timeout
        {
            get { return myManager.GetVideoMarqueeTimeout(myMediaPlayer); }
            set { myManager.SetVideoMarqueeTimeout(myMediaPlayer, value); }
        }

        public int X
        {
            get { return myManager.GetVideoMarqueeX(myMediaPlayer); }
            set { myManager.SetVideoMarqueeX(myMediaPlayer, value); }
        }

        public int Y
        {
            get { return myManager.GetVideoMarqueeY(myMediaPlayer); }
            set { myManager.SetVideoMarqueeY(myMediaPlayer, value); }
        }
    }
}
