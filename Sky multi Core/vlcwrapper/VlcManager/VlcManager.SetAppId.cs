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

using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        /// <summary>
        /// Sets some meta-information about the application. 
        /// </summary>
        /// <seealso cref="SetUserAgent" />
        /// <param name="id">Java-style application identifier, e.g. "com.acme.foobar"</param>
        /// <param name="version">application version numbers, e.g. "1.2.3"</param>
        /// <param name="icon">application icon name, e.g. "foobar"</param>
        public void SetAppId(string id, string version, string icon)
        {
            using (var idInterop = Utf8InteropStringConverter.ToUtf8StringHandle(id))
            using (var versionInterop = Utf8InteropStringConverter.ToUtf8StringHandle(version))
            using (var iconInterop = Utf8InteropStringConverter.ToUtf8StringHandle(icon))
            {
                VlcNative.libvlc_set_app_id(this.myVlcInstance, idInterop, versionInterop, iconInterop);
            }
        }
    }
}
