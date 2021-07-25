using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
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
                return myLibraryLoader.GetInteropDelegate<SetAudioOutput>().Invoke(mediaPlayerInstance, outputInterop);
            }
        }
    }
}
