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
    public sealed class AudioOutputDescription
    {
        private readonly VlcMediaPlayerInstance myInstance;

        public string Name { get; private set; }
        public string Description { get; private set; }

        internal AudioOutputDescription(string name, string description, VlcMediaPlayerInstance Instance)
        {
            Name = name;
            Description = description;
            myInstance = Instance;
        }

        private IEnumerable<AudioOutputDevice> GetAudioOutputDeviceList(string outputName)
        {
            using (Utf8StringHandle outputNameHandle = Utf8InteropStringConverter.ToUtf8StringHandle(in outputName))
            {
                IntPtr deviceList = VlcNative.libvlc_audio_output_device_list_get(this.myInstance, outputNameHandle);
                try
                {
                    List<AudioOutputDevice> result = new List<AudioOutputDevice>();
                    IntPtr currentPointer = deviceList;
                    while (currentPointer != IntPtr.Zero)
                    {
                        LibvlcAudioOutputDeviceT current = MarshalHelper.PtrToStructure<LibvlcAudioOutputDeviceT>(in currentPointer);
                        result.Add(new AudioOutputDevice
                        {
                            DeviceIdentifier = Utf8InteropStringConverter.Utf8InteropToString(in current.DeviceIdentifier),
                            Description = Utf8InteropStringConverter.Utf8InteropToString(in current.Description)
                        });
                        currentPointer = current.Next;
                    }

                    return result;
                }
                finally
                {
                    VlcNative.libvlc_audio_output_device_list_release(deviceList);
                }
            }
        }

        public IEnumerable<AudioOutputDevice> Devices => GetAudioOutputDeviceList(this.Name);
    }

}
