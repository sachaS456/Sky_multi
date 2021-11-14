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
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed class FilterModuleDescription
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string LongName { get; private set; }
        public string Help { get; private set; }

        private FilterModuleDescription()
        {
        }

        internal static FilterModuleDescription GetFilterModuleDescription(ModuleDescriptionStructure module)
        {
            if (module.Name == IntPtr.Zero)
                return null;
            var result = new FilterModuleDescription
            {
                Name = Utf8InteropStringConverter.Utf8InteropToString(module.Name),
                ShortName = Utf8InteropStringConverter.Utf8InteropToString(module.ShortName),
                LongName = Utf8InteropStringConverter.Utf8InteropToString(module.LongName),
                Help = Utf8InteropStringConverter.Utf8InteropToString(module.Help)
            };
            return result;
        }
    }
}
