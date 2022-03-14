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
using System.IO;

namespace Sky_multi_Core.ImageReader.Heif
{
    internal static class HeifReaderFactory
    {
        /// <summary>
        /// Creates a <see cref="HeifReader" /> instance from the specified file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The created <see cref="HeifReader" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="path" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="path" /> is empty, contains only whitespace or contains invalid characters.</exception>
        /// <exception cref="FileNotFoundException">The file specified by <paramref name="path" /> does not exist.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified path.</exception>
        public static HeifReader CreateFromFile(string path)
        {
            HeifReader reader;

            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                if (!fileStream.CanSeek)
                {
                    throw new IOException(Properties.Resources.FileStreamDoesNotSupportSeeking);
                }

                if (fileStream.Length <= HeifStreamReader.MaxReadBufferSize)
                {
                    byte[] bytes = CopyStreamToByteArray(fileStream);

                    reader = new HeifByteArrayReader(bytes);
                }
                else
                {
                    reader = new HeifStreamReader(fileStream, ownsStream: true);

                    fileStream = null;
                }
            }
            finally
            {
                fileStream?.Dispose();
            }

            return reader;
        }

        /// <summary>
        /// Creates a <see cref="HeifReader"/> instance from the specified byte array.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <returns>The created <see cref="HeifReader"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bytes"/> is null.</exception>
        public static HeifReader CreateFromMemory(byte[] bytes)
        {
            return new HeifByteArrayReader(bytes);
        }

        /// <summary>
        /// Creates a <see cref="HeifReader"/> instance from the specified stream, and optionally takes ownership of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="ownsStream"><see langword="true"/> if the writer owns the stream; otherwise, <see langword="false"/>.</param>
        /// <returns>The created <see cref="HeifReader"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public static HeifReader CreateFromStream(Stream stream, bool ownsStream)
        {
            Validate.IsNotNull(stream, nameof(stream));

            HeifReader reader;

            // If the stream is a MemoryStream with an accessible buffer we can avoid
            // having to copy data to a temporary buffer when reading from the stream.
            // This check excludes types that are derived from MemoryStream because they may invalidate the
            // underlying buffer when the stream is disposed.
            if (stream.GetType() == typeof(MemoryStream) && ((MemoryStream)stream).TryGetBuffer(out var buffer))
            {
                reader = new HeifByteArrayReader(buffer);

                // The documentation for GetBuffer indicates that the buffer remains valid after the MemoryStream is disposed.
                if (ownsStream)
                {
                    stream.Dispose();
                }
            }
            else
            {
                if (stream.Length <= HeifStreamReader.MaxReadBufferSize)
                {
                    byte[] bytes = CopyStreamToByteArray(stream);

                    if (ownsStream)
                    {
                        stream.Dispose();
                    }

                    reader = new HeifByteArrayReader(bytes);
                }
                else
                {
                    reader = new HeifStreamReader(stream, ownsStream);
                }
            }

            return reader;
        }

        /// <summary>
        /// Copies the stream to byte array.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A byte array containing the stream data.</returns>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        private static byte[] CopyStreamToByteArray(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];

            int offset = 0;
            int remaining = buffer.Length;

            while (remaining > 0)
            {
                int bytesRead = stream.Read(buffer, offset, remaining);

                if (bytesRead == 0)
                {
                    throw new EndOfStreamException();
                }

                offset += bytesRead;
                remaining -= bytesRead;
            }

            return buffer;
        }
    }
}
