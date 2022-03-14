/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

namespace Sky_multi_Core.ImageReader.Heif.Interop
{
    internal enum heif_error_code
    {
        // Everything ok, no error occurred.
        Ok = 0,

        // Input file does not exist.
        Input_does_not_exist = 1,

        // Error in input file. Corrupted or invalid content.
        Invalid_input = 2,

        // Input file type is not supported.
        Unsupported_filetype = 3,

        // Image requires an unsupported decoder feature.
        Unsupported_feature = 4,

        // Library API has been used in an invalid way.
        Usage_error = 5,

        // Could not allocate enough memory.
        Memory_allocation_error = 6,

        // The decoder plugin generated an error
        Decoder_plugin_error = 7,

        // The encoder plugin generated an error
        Encoder_plugin_error = 8,

        // Error during encoding or when writing to the output
        Encoding_error = 9,

        // Application has asked for a color profile type that does not exist
        Color_profile_does_not_exist = 10
    }
}
