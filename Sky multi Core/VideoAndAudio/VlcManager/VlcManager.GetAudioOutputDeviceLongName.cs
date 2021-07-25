using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        [Obsolete("Use GetAudioOutputDeviceList instead")]
        public string GetAudioOutputDeviceLongName(string audioOutputDescriptionName, int deviceIndex)
        {
            using (var audioOutputDescriptionNameInterop = Utf8InteropStringConverter.ToUtf8StringHandle(audioOutputDescriptionName))
            {
                return Utf8InteropStringConverter.Utf8InteropToString(myLibraryLoader.GetInteropDelegate<GetAudioOutputDeviceLongName>()
                    .Invoke(myVlcInstance, audioOutputDescriptionNameInterop, deviceIndex));
            }
        }
    }
}
