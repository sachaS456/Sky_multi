using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        [Obsolete("Use GetAudioOutputDeviceList instead")]
        public string GetAudioOutputDeviceName(string audioOutputDescriptionName, int deviceIndex)
        {
            using (var audioOutputInterop = Utf8InteropStringConverter.ToUtf8StringHandle(audioOutputDescriptionName))
            {
                return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_audio_output_device_id(myVlcInstance, audioOutputInterop, deviceIndex));
            }
        }
    }
}
