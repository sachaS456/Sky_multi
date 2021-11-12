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
