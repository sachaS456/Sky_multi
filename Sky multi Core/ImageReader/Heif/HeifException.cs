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
using System;
using Sky_multi_Core.ImageReader.Heif.Properties;

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// The exception that is thrown for LibHeif errors.
    /// </summary>
    /// <seealso cref="Exception" />
    public sealed class HeifException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeifException"/> class.
        /// </summary>
        public HeifException() : base(Resources.UnspecifiedError)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeifException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public HeifException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeifException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public HeifException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
