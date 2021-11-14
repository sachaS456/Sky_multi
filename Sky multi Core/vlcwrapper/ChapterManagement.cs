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
    internal sealed class ChapterManagement : IChapterManagement
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public ChapterManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public int Count
        {
            get { return myManager.GetMediaChapterCount(myMediaPlayer); }
        }

        public void Previous()
        {
            myManager.SetPreviousMediaChapter(myMediaPlayer);
        }

        public void Next()
        {
            myManager.SetNextMediaChapter(myMediaPlayer);
        }

        public int Current
        {
            get { return myManager.GetMediaChapter(myMediaPlayer); }
            set { myManager.SetMediaChapter(myMediaPlayer, value); }
        }
    }
}
