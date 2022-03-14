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
    /// The interface that all LibHeif encoder parameters implement
    /// </summary>
    public interface IHeifEncoderParameter
    {
        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        /// <value>
        /// The parameter name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        /// <value>
        /// The type of the parameter.
        /// </value>
        HeifEncoderParameterType ParameterType { get; }
    }
}