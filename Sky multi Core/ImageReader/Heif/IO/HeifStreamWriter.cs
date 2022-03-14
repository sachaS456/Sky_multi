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
using System.Runtime.InteropServices;

namespace Sky_multi_Core.ImageReader.Heif
{
    internal sealed class HeifStreamWriter : HeifWriter
    {
        // 81920 is the largest multiple of 4096 that is below the large object heap threshold.
        private const int MaxWriteBufferSize = 81920;

        private Stream stream;

        private readonly bool ownsStream;
        private readonly byte[] streamBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeifStreamWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="ownsStream"><see langword="true"/> if the writer owns the stream; otherwise, <see langword="false"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        public HeifStreamWriter(Stream stream, bool ownsStream = false)
        {
            Validate.IsNotNull(stream, nameof(stream));

            this.stream = stream;
            this.ownsStream = ownsStream;
            this.streamBuffer = new byte[MaxWriteBufferSize];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.stream != null)
                {
                    if (this.ownsStream)
                    {
                        this.stream.Dispose();
                    }
                    this.stream = null;
                }
            }

            base.Dispose(disposing);
        }

        protected override void WriteCore(IntPtr data, long count)
        {
            long offset = 0;
            long remaining = count;

            while (remaining > 0)
            {
                int copySize = (int)Math.Min(MaxWriteBufferSize, remaining);

                Marshal.Copy(new IntPtr(data.ToInt64() + offset), this.streamBuffer, 0, copySize);

                this.stream.Write(this.streamBuffer, 0, copySize);

                offset += copySize;
                remaining -= copySize;
            }
        }
    }
}
