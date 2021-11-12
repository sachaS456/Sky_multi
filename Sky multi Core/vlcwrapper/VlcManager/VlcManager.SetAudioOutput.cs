using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        internal int SetAudioOutput(VlcMediaPlayerInstance mediaPlayerInstance, AudioOutputDescriptionStructure output)
        {
            return SetAudioOutput(mediaPlayerInstance, output.Name);
        }

        internal int SetAudioOutput(VlcMediaPlayerInstance mediaPlayerInstance, string outputName)
        {
            using (var outputInterop = Utf8InteropStringConverter.ToUtf8StringHandle(outputName))
            {
                return VlcNative.libvlc_audio_output_set(mediaPlayerInstance, outputInterop);
            }
        }
    }
}
