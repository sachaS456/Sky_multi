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

using System.Collections.Generic;
using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal sealed class SubTitlesManagement : ISubTitlesManagement, IEnumerableManagement<TrackDescription>
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public SubTitlesManagement(VlcMediaPlayerInstance mediaPlayerInstance)
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
                return VlcNative.libvlc_video_get_spu_count(myMediaPlayer); 
            }
        }

        public IEnumerable<TrackDescription> All
        {
            get
            {
                myMediaPlayerIsLoad();
                IntPtr module = VlcNative.libvlc_video_get_spu_description(myMediaPlayer);
                List<TrackDescription> result = TrackDescription.GetSubTrackDescription(module);
                VlcNative.libvlc_track_description_list_release(module);
                return result;
            }
        }


        public TrackDescription Current
        {
            get
            {
                myMediaPlayerIsLoad();
                int currentId = VlcNative.libvlc_video_get_spu(myMediaPlayer);
                foreach (var availableSubTitle in All)
                {
                    if (availableSubTitle.ID == currentId)
                    {
                        return availableSubTitle;
                    }
                }

                return null;
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_spu(myMediaPlayer, value.ID); 
            }
        }

        public long Delay
        {
            get 
            {
                myMediaPlayerIsLoad();
                return VlcNative.libvlc_video_get_spu_delay(myMediaPlayer); 
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_spu_delay(myMediaPlayer, value); 
            }
        }
    }
}
