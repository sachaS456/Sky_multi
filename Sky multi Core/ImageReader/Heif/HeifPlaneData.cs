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

namespace Sky_multi_Core.ImageReader.Heif
{
    /// <summary>
    /// The data for an image plane.
    /// </summary>
    public sealed class HeifPlaneData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeifPlaneData"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="stride">The stride.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="scan0">The starting address of the image data.</param>
        internal HeifPlaneData(int width, int height, int stride, HeifChannel channel, IntPtr scan0)
        {
            this.Width = width;
            this.Height = height;
            this.Stride = stride;
            this.Channel = channel;
            this.Scan0 = scan0;
        }

        /// <summary>
        /// Gets the plane width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; }

        /// <summary>
        /// Gets the plane height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; }

        /// <summary>
        /// Gets the plane stride, the width of a single row of pixels.
        /// </summary>
        /// <value>
        /// The plane stride, in bytes.
        /// </value>
        public int Stride { get; }

        /// <summary>
        /// Gets the image channel.
        /// </summary>
        /// <value>
        /// The image channel.
        /// </value>
        public HeifChannel Channel { get; }

        /// <summary>
        /// Gets the starting address of the image plane data.
        /// </summary>
        /// <value>
        /// The starting address of the image plane data.
        /// </value>
        public IntPtr Scan0 { get; }
    }
}
