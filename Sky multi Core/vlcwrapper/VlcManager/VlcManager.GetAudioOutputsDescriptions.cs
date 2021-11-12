using System;
using System.Collections.Generic;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        public List<AudioOutputDescriptionStructure> GetAudioOutputsDescriptions()
        {
            var first = VlcNative.libvlc_audio_output_list_get(myVlcInstance);
            var result = new List<AudioOutputDescriptionStructure>();

            if (first == IntPtr.Zero)
            {
                return result;
            }

            try
            {
                var currentPtr = first;
                while (currentPtr != IntPtr.Zero)
                {
                    var current = MarshalHelper.PtrToStructure<AudioOutputDescriptionStructureInternal>(ref currentPtr);
                    result.Add(new AudioOutputDescriptionStructure
                    {
                        Name = Utf8InteropStringConverter.Utf8InteropToString(current.Name),
                        Description = Utf8InteropStringConverter.Utf8InteropToString(current.Description)
                    });

                    currentPtr = current.NextAudioOutputDescription;
                }

                return result;
            }
            finally
            {
                VlcNative.libvlc_audio_output_list_release(first);
            }
        }
    }
}
