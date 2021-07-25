using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        private EventCallback myOnMediaPlayerAudioDeviceInternalEventCallback;
        public event EventHandler<VlcMediaPlayerAudioDeviceEventArgs> AudioDevice;

        private void OnMediaPlayerAudioDeviceInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaPlayerAudioDevice(Utf8InteropStringConverter.Utf8InteropToString(args.eventArgsUnion.MediaPlayerAudioDevice.pszDevice));
        }

        public void OnMediaPlayerAudioDevice(string audioDevice)
        {
            AudioDevice?.Invoke(this, new VlcMediaPlayerAudioDeviceEventArgs(audioDevice));
        }
    }
}