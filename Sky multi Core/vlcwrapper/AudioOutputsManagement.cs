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
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    internal class AudioOutputsManagement : IAudioOutputsManagement
    {
        private readonly VlcMediaPlayerInstance myMediaPlayerInstance;

        internal AudioOutputsManagement(VlcMediaPlayerInstance mediaPlayerInstance)
        {
            myMediaPlayerInstance = mediaPlayerInstance;
        }

        public IEnumerable<AudioOutputDescription> All
        {
            get
            {
                return GetAudioOutputsDescriptions().Select(x => new AudioOutputDescription(x.Name, x.Description, this.myMediaPlayerInstance));
            }
        }

        private void myMediaPlayerIsLoad()
        {
            if (myMediaPlayerInstance == IntPtr.Zero)
            {
                throw new ArgumentException("Media player instance is not initialized.");
            }
        }

        private List<AudioOutputDescriptionStructure> GetAudioOutputsDescriptions()
        {
            myMediaPlayerIsLoad();
            IntPtr first = VlcNative.libvlc_audio_output_list_get(myMediaPlayerInstance);
            List<AudioOutputDescriptionStructure> result = new List<AudioOutputDescriptionStructure>();

            if (first == IntPtr.Zero)
            {
                return result;
            }

            try
            {
                IntPtr currentPtr = first;
                while (currentPtr != IntPtr.Zero)
                {
                    AudioOutputDescriptionStructureInternal current = MarshalHelper.PtrToStructure<AudioOutputDescriptionStructureInternal>(ref currentPtr);
                    result.Add(new AudioOutputDescriptionStructure
                    {
                        Name = Utf8InteropStringConverter.Utf8InteropToString(current.Name),
                        Description = Utf8InteropStringConverter.Utf8InteropToString(current.Description)
                    });

                    currentPtr = current.NextAudioOutputDescription;
                }

                return result;
            }
            finally
            {
                VlcNative.libvlc_audio_output_list_release(first);
            }
        }

        public int Count => GetAudioOutputsDescriptions().Count;

        public AudioOutputDescription Current
        {
            get
            {
                throw new NotSupportedException("Not implemented in LibVlc.");
            }
            set 
            {
                myMediaPlayerIsLoad();
                using (Utf8StringHandle outputInterop = Utf8InteropStringConverter.ToUtf8StringHandle(value.Name))
                {
                    VlcNative.libvlc_audio_output_set(myMediaPlayerInstance, outputInterop);
                }
            }
        }
    }
}
