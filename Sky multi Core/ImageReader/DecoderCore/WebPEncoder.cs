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
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Sky_multi_Core.ImageReader
{
    public class WebPEncoder
    {
        /// <summary>
        /// The encoder's version number
        /// </summary>
        /// <returns>The version as major.minor.revision</returns>
        public static string GetEncoderVersion()
        {
            int version = WebPGetEncoderVersion();
            return String.Format("{0}.{1}.{2}", (version >> 16) & 0xff, (version >> 8) & 0xff, version & 0xff);
        }

        private const int WEBP_MAX_DIMENSION = 16383;

        public static void EncodeWebp(Bitmap bmp, string Path)
        {
            //test bmp
            if (bmp.Width == 0 || bmp.Height == 0)
                throw new ArgumentException("Bitmap contains no data.", "bmp");
            if (bmp.Width > WEBP_MAX_DIMENSION || bmp.Height > WEBP_MAX_DIMENSION)
                throw new NotSupportedException("Bitmap's dimension is too large. Max is " + WEBP_MAX_DIMENSION + "x" + WEBP_MAX_DIMENSION + " pixels.");
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb && bmp.PixelFormat != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Only support Format24bppRgb and Format32bppArgb pixelFormat.");

            BitmapData bmpData = null;
            IntPtr unmanagedData = IntPtr.Zero;
            try
            {
                //Get bmp data
                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

                //Compress the bmp data
                int size;
                if (bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    size = WebPEncodeLosslessBGR(bmpData.Scan0, bmp.Width, bmp.Height, bmpData.Stride, out unmanagedData);
                else
                    size = WebPEncodeLosslessBGRA(bmpData.Scan0, bmp.Width, bmp.Height, bmpData.Stride, out unmanagedData);

                //Copy image compress data to output array
                byte[] rawWebP = new byte[size];
                Marshal.Copy(unmanagedData, rawWebP, 0, size);

                File.WriteAllBytes(Path, rawWebP);
                return;
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message + "\r\nIn WebP.EncodeLossless (Simple)"); 
            }
            finally
            {
                //Unlock the pixels
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                //Free memory
                if (unmanagedData != IntPtr.Zero)
                    WebPFree(unmanagedData);
            }
        }

        /// <summary>
        /// Return the decoder's version number
        /// </summary>
        /// <returns>Hexadecimal using 8bits for each of major/minor/revision. E.g: v2.5.7 is 0x020507</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern int WebPGetEncoderVersion();

        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern int WebPEncodeLosslessBGRA([InAttribute()] IntPtr bgra, int width, int height, int stride, out IntPtr output);

        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern int WebPEncodeLosslessBGR([InAttribute()] IntPtr bgra, int width, int height, int stride, out IntPtr output);

        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern int WebPFree(IntPtr p);
    }
}