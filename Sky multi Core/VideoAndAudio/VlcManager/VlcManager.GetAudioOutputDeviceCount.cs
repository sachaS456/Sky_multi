using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        [Obsolete("Use GetAudioOutputDeviceList instead")]
        public int GetAudioOutputDeviceCount(string outputName)
        {
            using (var outputNameHandle = Utf8InteropStringConverter.ToUtf8StringHandle(outputName))
            {
                return myLibraryLoader.GetInteropDelegate<GetAudioOutputDeviceCount>().Invoke(myVlcInstance, outputNameHandle);
            }
        }
    }
}
