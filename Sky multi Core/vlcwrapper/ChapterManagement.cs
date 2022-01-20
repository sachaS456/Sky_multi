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
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal sealed class ChapterManagement : IChapterManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public ChapterManagement(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myMediaPlayer = mediaPlayerInstance;
        }

        private void myMediaPlayerIsLoad()
        {
            if (myMediaPlayer == IntPtr.Zero)
            {
                throw new ArgumentException("Media player instance is not initialized.");
            }
        }

        public int Count
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_media_player_get_chapter_count(myMediaPlayer); 
            }
        }

        public void Previous()
        {
            myMediaPlayerIsLoad();
            VlcNative.libvlc_media_player_previous_chapter(myMediaPlayer);
        }

        public void Next()
        {
            myMediaPlayerIsLoad();
            VlcNative.libvlc_media_player_next_chapter(myMediaPlayer);
        }

        public int Current
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_media_player_get_chapter(myMediaPlayer); 
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_media_player_set_chapter(myMediaPlayer, value); 
            }
        }
    }
}
