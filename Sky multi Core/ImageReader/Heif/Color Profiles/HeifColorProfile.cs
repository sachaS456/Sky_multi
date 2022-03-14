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

using Sky_multi_Core.ImageReader.Heif.Interop;

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// The base class for a HEIF image color profile.
    /// </summary>
    public abstract class HeifColorProfile
    {
        private protected HeifColorProfile(ColorProfileType profileType)
        {
            this.ProfileType = profileType;
        }

        /// <summary>
        /// Gets the type of the color profile.
        /// </summary>
        /// <value>
        /// The type of the color profile.
        /// </value>
        public ColorProfileType ProfileType { get; }

        internal abstract unsafe void SetImageColorProfile(SafeHeifImage image);
    }
}
