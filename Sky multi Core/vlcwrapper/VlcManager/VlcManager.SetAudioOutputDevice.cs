using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal void SetAudioOutputDevice(VlcMediaPlayerInstance mediaPlayerInstance, string audioOutputDescriptionName, string deviceName)
        {
            using (var audioOutputInterop = Utf8InteropStringConverter.ToUtf8StringHandle(audioOutputDescriptionName))
            using (var deviceNameInterop = Utf8InteropStringConverter.ToUtf8StringHandle(audioOutputDescriptionName))
            {
                VlcNative.libvlc_audio_output_device_set(mediaPlayerInstance, audioOutputInterop, deviceNameInterop);
            }
        }
    }
}
