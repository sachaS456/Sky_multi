using System;
using System.Collections.Generic;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public sealed class TrackDescription
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        internal TrackDescription(int id, string name)
        {
            ID = id;
            Name = name;
        }

        internal static List<TrackDescription> GetSubTrackDescription(IntPtr moduleRef)
        {
            var result = new List<TrackDescription>();
            if (moduleRef != IntPtr.Zero)
            {
                var module = MarshalHelper.PtrToStructure<TrackDescriptionStructure>(ref moduleRef);
                var name = Utf8InteropStringConverter.Utf8InteropToString(module.Name);
                result.Add(new TrackDescription(module.Id, name));
                var data = GetSubTrackDescription(module.NextTrackDescription);
                result.AddRange(data);
            }
            return result;
        }

    }
}
