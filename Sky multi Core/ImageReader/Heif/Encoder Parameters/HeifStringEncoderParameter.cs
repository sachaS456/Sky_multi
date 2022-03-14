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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// Represents a string LibHeif encoder parameter
    /// </summary>
    /// <seealso cref="HeifEncoderParameter{T}" />
    public sealed class HeifStringEncoderParameter : HeifEncoderParameter<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeifStringEncoderParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="hasDefaultValue"><see langword="true"/> if the parameter has a default value; otherwise, <see langword="false"/>.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="validValues">The valid values.</param>
        internal HeifStringEncoderParameter(string name,
                                            bool hasDefaultValue,
                                            string defaultValue,
                                            ReadOnlyCollection<string> validValues)
            : base(name, hasDefaultValue, defaultValue)
        {
            this.ValidValues = validValues;
        }

        ///<inheritdoc/>
        public override HeifEncoderParameterType ParameterType => HeifEncoderParameterType.String;

        /// <summary>
        /// Gets the valid values.
        /// </summary>
        /// <value>
        /// The valid values.
        /// </value>
        public IReadOnlyList<string> ValidValues { get; }
    }
}
