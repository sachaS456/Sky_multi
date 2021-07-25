using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        internal void SetAudioOutputDevice(VlcMediaPlayerInstance mediaPlayerInstance, string audioOutputDescriptionName, string deviceName)
        {
            using (var audioOutputInterop = Utf8InteropStringConverter.ToUtf8StringHandle(audioOutputDescriptionName))
            using (var deviceNameInterop = Utf8InteropStringConverter.ToUtf8StringHandle(audioOutputDescriptionName))
            {
                myLibraryLoader.GetInteropDelegate<SetAudioOutputDevice>().Invoke(mediaPlayerInstance, audioOutputInterop, deviceNameInterop);
            }
        }
    }
}
