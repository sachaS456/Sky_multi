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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sky_multi_Core.VlcWrapper
{
    internal class AudioOutputsManagement : IAudioOutputsManagement
    {
        private readonly VlcManager myManager;
        private readonly VlcMediaPlayerInstance myMediaPlayerInstance;

        internal AudioOutputsManagement(VlcManager manager, VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myManager = manager;
            myMediaPlayerInstance = mediaPlayerInstance;
        }

        public IEnumerable<AudioOutputDescription> All
        {
            get
            {
                return myManager.GetAudioOutputsDescriptions().Select(x => new AudioOutputDescription(x.Name, x.Description, this.myManager));
            }
        }

        public int Count => myManager.GetAudioOutputsDescriptions().Count;

        public AudioOutputDescription Current
        {
            get
            {
                throw new NotSupportedException("Not implemented in LibVlc.");
            }
            set { myManager.SetAudioOutput(myMediaPlayerInstance, value.Name); }
        }
    }
}
