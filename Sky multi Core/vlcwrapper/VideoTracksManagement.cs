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

using System.Collections.Generic;
using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed class VideoTracksManagement : ITracksManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        internal VideoTracksManagement(VlcMediaPlayerInstance mediaPlayerInstance)
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
                return VlcNative.libvlc_video_get_track_count(myMediaPlayer); 
            }
        }

        public TrackDescription Current
        {
            get
            {
                myMediaPlayerIsLoad();
                int currentId = VlcNative.libvlc_video_get_track(myMediaPlayer);
                foreach (var track in All)
                {
                    if (track.ID == currentId)
                    {
                        return track;
                    }
                }
                return null;
            }
            set 
            {
                myMediaPlayerIsLoad();
                VlcNative.libvlc_video_set_track(myMediaPlayer, value.ID); 
            }
        }

        public IEnumerable<TrackDescription> All
        {
            get
            {
                myMediaPlayerIsLoad();
                IntPtr module = VlcNative.libvlc_video_get_track_description(myMediaPlayer);
                List<TrackDescription> result = TrackDescription.GetSubTrackDescription(module);
                VlcNative.libvlc_track_description_list_release(module);
                return result;
            }
        }
    }
}
