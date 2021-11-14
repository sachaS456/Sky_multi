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
