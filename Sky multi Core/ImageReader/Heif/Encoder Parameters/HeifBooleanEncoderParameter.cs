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

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// Represents a Boolean encoder parameter.
    /// </summary>
    /// <seealso cref="HeifEncoderParameter{T}" />
    public sealed class HeifBooleanEncoderParameter : HeifEncoderParameter<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeifBooleanEncoderParameter" /> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="hasDefaultValue"><see langword="true"/> if the parameter has a default value; otherwise, <see langword="false"/>.</param>
        /// <param name="defaultValue">The default value.</param>
        internal HeifBooleanEncoderParameter(string name, bool hasDefaultValue, bool defaultValue)
            : base(name, hasDefaultValue, defaultValue)
        {
        }

        ///<inheritdoc/>
        public override HeifEncoderParameterType ParameterType => HeifEncoderParameterType.Boolean;
    }
}
