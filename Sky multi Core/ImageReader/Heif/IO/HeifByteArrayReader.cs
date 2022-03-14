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
using Sky_multi_Core.ImageReader.Heif.Interop;

namespace Sky_multi_Core.ImageReader.Heif
{
    internal sealed class HeifByteArrayReader : HeifReader
    {
        private long position;

        private readonly byte[] buffer;
        private readonly long origin;
        private readonly long length;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeifByteArrayReader"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        public HeifByteArrayReader(byte[] buffer)
        {
            Validate.IsNotNull(buffer, nameof(buffer));

            this.buffer = buffer;
            this.origin = this.position = 0;
            this.length = buffer.Length;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeifByteArrayReader"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public HeifByteArrayReader(ArraySegment<byte> buffer)
        {
            this.buffer = buffer.Array;
            this.origin = this.position = buffer.Offset;
            this.length = buffer.Count;
        }

        protected override long GetPositionCore()
        {
            return this.position - this.origin;
        }

        protected override bool ReadCore(IntPtr data, long count)
        {
            if (((ulong)this.position + (ulong)count) > (ulong)this.length)
            {
                return false;
            }

            unsafe
            {
                fixed (byte* source = this.buffer)
                {
                    Buffer.MemoryCopy(source + this.position, data.ToPointer(), count, count);
                }
            }
            this.position += count;

            return true;
        }

        protected override bool SeekCore(long position)
        {
            if (position < this.origin || position > this.length)
            {
                return false;
            }

            this.position = position;
            return true;
        }

        protected override heif_reader_grow_status WaitForFileSizeCore(long targetSize)
        {
            return targetSize > this.length ? heif_reader_grow_status.size_beyond_eof : heif_reader_grow_status.size_reached;
        }
    }
}
