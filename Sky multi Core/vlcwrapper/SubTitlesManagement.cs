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

namespace Sky_multi_Core.VlcWrapper
{
    internal sealed class SubTitlesManagement : ISubTitlesManagement, IEnumerableManagement<TrackDescription>
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayer;

        public SubTitlesManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayer = mediaPlayerInstance;
        }

        public int Count
        {
            get { return myManager.GetVideoSpuCount(myMediaPlayer); }
        }

        public IEnumerable<TrackDescription> All
        {
            get
            {
                var module = myManager.GetVideoSpuDescription(myMediaPlayer);
                var result = TrackDescription.GetSubTrackDescription(module);
                myManager.ReleaseTrackDescription(module);
                return result;
            }
        }


        public TrackDescription Current
        {
            get
            {
                var currentId = myManager.GetVideoSpu(myMediaPlayer);
                foreach (var availableSubTitle in All)
                {
                    if (availableSubTitle.ID == currentId)
                        return availableSubTitle;
                }
                return null;
            }
            set { myManager.SetVideoSpu(myMediaPlayer, value.ID); }
        }

        public long Delay
        {
            get { return myManager.GetVideoSpuDelay(myMediaPlayer); }
            set { myManager.SetVideoSpuDelay(myMediaPlayer, value); }
        }
    }
}
