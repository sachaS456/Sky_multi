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
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public IEnumerable<AudioOutputDevice> GetAudioOutputDeviceList(string outputName)
        {
            using (var outputNameHandle = Utf8InteropStringConverter.ToUtf8StringHandle(outputName))
            {
                var deviceList = VlcNative.libvlc_audio_output_device_list_get(this.myVlcInstance, outputNameHandle);
                try
                {
                    var result = new List<AudioOutputDevice>();
                    var currentPointer = deviceList;
                    while (currentPointer != IntPtr.Zero)
                    {
                        var current = MarshalHelper.PtrToStructure<LibvlcAudioOutputDeviceT>(ref currentPointer);
                        result.Add(new AudioOutputDevice
                        {
                            DeviceIdentifier = Utf8InteropStringConverter.Utf8InteropToString(current.DeviceIdentifier),
                            Description = Utf8InteropStringConverter.Utf8InteropToString(current.Description)
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
    }
}
