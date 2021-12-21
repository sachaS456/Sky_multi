/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
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

        internal static List<TrackDescription> GetSubTrackDescription(in IntPtr moduleRef)
        {
            var result = new List<TrackDescription>();
            if (moduleRef != IntPtr.Zero)
            {
                var module = MarshalHelper.PtrToStructure<TrackDescriptionStructure>(in moduleRef);
                var name = Utf8InteropStringConverter.Utf8InteropToString(in module.Name);
                result.Add(new TrackDescription(module.Id, name));
                var data = GetSubTrackDescription(in module.NextTrackDescription);
                result.AddRange(data);
            }
            return result;
        }

    }
}
