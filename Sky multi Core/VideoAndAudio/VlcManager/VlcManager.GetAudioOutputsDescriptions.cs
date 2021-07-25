using System;
using System.Collections.Generic;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager
    {
        public List<AudioOutputDescriptionStructure> GetAudioOutputsDescriptions()
        {
            var first = myLibraryLoader.GetInteropDelegate<GetAudioOutputsDescriptions>().Invoke(myVlcInstance);
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
                myLibraryLoader.GetInteropDelegate<ReleaseAudioOutputDescription>().Invoke(first);
            }
        }
    }
}
