/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Sky_multi_Core
{
    public class WebPDecoder
    {
        private static readonly int WEBP_DECODER_ABI_VERSION = 0x0208;

        private enum decodeType
        {
            RGB,
            RGBA,
            BGR,
            BGRA,
            YUV
        };

        /// <summary>
        /// The decoder's version number
        /// </summary>
        /// <returns>The version as major.minor.revision</returns>
        public static string GetDecoderVersion()
        {
            int version = WebPGetDecoderVersion();
            return String.Format("{0}.{1}.{2}", (version >> 16) & 0xff, (version >> 8) & 0xff, version & 0xff);
        }

        /// <summary>
        /// Validate the WebP image header and retrieve the image height and width
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returnsthe height of the WebP image</param>
        /// <returns>True if the WebP image header is valid, otherwise false</returns>
        public static bool GetInfo(string path, out int imgWidth, out int imgHeight)
        {
            bool retValue = false;
            int width = 0;
            int height = 0;
            IntPtr pnt = IntPtr.Zero;

            try
            {
                byte[] data = CopyFileToManagedArray(path);
                pnt = CopyDataToUnmanagedMemory(data);
                int ret = WebPGetInfo(pnt, (uint)data.Length, ref width, ref height);
                if (ret == 1)
                {
                    retValue = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // Free the unmanaged memory.
                Marshal.FreeHGlobal(pnt);
            }

            imgWidth = width;
            imgHeight = height;
            return retValue;
        }

        public static Bitmap DecodeWebp(string Path)
        {
            if (System.IO.Path.GetExtension(Path) != ".webp")
            {
                throw new Exception("It is not a webp image.");
            }

            byte[] rawWebP = File.ReadAllBytes(Path);

            GetInfo(rawWebP, out int imgWidth, out int imgHeight, out bool hasAlpha, out bool hasAnimation, out string format);
            if (hasAlpha == true)
            {
                return decode(Path, decodeType.BGRA, PixelFormat.Format32bppArgb);
            }
            else
            {
                return decode(Path, decodeType.BGR, PixelFormat.Format24bppRgb);
            }           
        }

        private static void GetInfo(byte[] rawWebP, out int width, out int height, out bool has_alpha, out bool has_animation, out string format)
        {
            VP8StatusCode result;
            GCHandle pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);

            try
            {
                IntPtr ptrRawWebP = pinnedWebP.AddrOfPinnedObject();

                WebPBitstreamFeatures features = new WebPBitstreamFeatures();
                result = WebPGetFeaturesInternal(ptrRawWebP, (UIntPtr)rawWebP.Length, ref features, WEBP_DECODER_ABI_VERSION);

                if (result != 0)
                    throw new Exception(result.ToString());

                width = features.Width;
                height = features.Height;
                if (features.Has_alpha == 1) has_alpha = true; else has_alpha = false;
                if (features.Has_animation == 1) has_animation = true; else has_animation = false;
                switch (features.Format)
                {
                    case 1:
                        format = "lossy";
                        break;
                    case 2:
                        format = "lossless";
                        break;
                    default:
                        format = "undefined";
                        break;
                }
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message + "\r\nIn WebP.GetInfo"); 
            }
            finally
            {
                //Free memory
                if (pinnedWebP.IsAllocated)
                    pinnedWebP.Free();
            }
        }

        /// <summary>
        /// Decode the WebP image into a RGB Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image.
        /// Note that a Bitmap object use the BGR format, so if you display the Bitmap in a picturebox red and blue are mixed up</returns>
        public static Bitmap DecodeRGB(string path)
        {
            return decode(path, decodeType.RGB, PixelFormat.Format24bppRgb);
        }

        /// <summary>
        /// Decode the WebP image into a RGBA Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image.
        /// Note that a Bitmap object use the ABGR format, so if you display the Bitmap in a picturebox red and blue are mixed up</returns>
        public static Bitmap DecodeRGBA(string path)
        {
            return decode(path, decodeType.RGBA, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Decode the WebP image into a BGR Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image</returns>
        public static Bitmap DecodeBGR(string path)
        {
            return decode(path, decodeType.BGR, PixelFormat.Format24bppRgb);
        }

        /// <summary>
        /// Decode the WebP image into a BGRA Bitmap
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <returns>A Bitmap object with the decoded WebP image</returns>
        public static Bitmap DecodeBGRA(string path)
        {
            return decode(path, decodeType.BGRA, PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Decode the WebP image file into raw RGB image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public static byte[] DecodeRGB(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.RGB, PixelFormat.Format24bppRgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Decode the WebP image file into raw RGBA image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public static byte[] DecodeRGBA(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.RGBA, PixelFormat.Format32bppArgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Decode the WebP image file into raw BGR image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public static byte[] DecodeBGR(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.BGR, PixelFormat.Format24bppRgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Decode the WebP image file into raw BGRA image data
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns>A byte array containing the raw decoded image data</returns>
        public static byte[] DecodeBGRA(string path, out int imgWidth, out int imgHeight)
        {
            return decode(path, decodeType.BGRA, PixelFormat.Format32bppArgb, out imgWidth, out imgHeight);
        }

        /// <summary>
        /// Internal convert method to get a Bitmap from a WebP image file
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="type">The color type you want to convert to</param>
        /// <param name="format">The PixelFormat the Bitmap should use</param>
        /// <returns></returns>
        private static Bitmap decode(string path, decodeType type, PixelFormat format)
        {
            int width = 0;
            int height = 0;
            byte[] data = decode(path, type, format, out width, out height);
            return ConvertDataToBitmap(data, width, height, format);
        }

        /// <summary>
        /// Internal convert method to get a byte array from a WebP image file
        /// </summary>
        /// <param name="path">The path to the WebP image file</param>
        /// <param name="type">The color type you want to convert to</param>
        /// <param name="format">The PixelFormat you want to use</param>
        /// <param name="imgWidth">Returns the width of the WebP image</param>
        /// <param name="imgHeight">Returns the height of the WebP image</param>
        /// <returns></returns>
        private static byte[] decode(string path, decodeType type, PixelFormat format, out int imgWidth, out int imgHeight)
        {
            int width = 0;
            int height = 0;
            IntPtr data = IntPtr.Zero;
            IntPtr output_buffer = IntPtr.Zero;
            IntPtr result = IntPtr.Zero;

            try
            {
                // Load data
                byte[] managedData = CopyFileToManagedArray(path);

                // Copy data to unmanaged memory
                data = CopyDataToUnmanagedMemory(managedData);

                // Get image width and height
                int ret = WebPGetInfo(data, (uint)managedData.Length, ref width, ref height);

                // Get image data lenght
                UInt32 data_size = (UInt32)managedData.Length;

                // Calculate bitmap size for decoded WebP image
                int output_buffer_size = CalculateBitmapSize(width, height, format);

                // Allocate unmanaged memory to decoded WebP image
                output_buffer = Marshal.AllocHGlobal(output_buffer_size);

                // Calculate distance between scanlines
                int output_stride = (width * Image.GetPixelFormatSize(format)) / 8;

                // Convert image
                switch (type)
                {
                    case decodeType.RGB:
                        result = WebPDecodeRGBInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                    case decodeType.RGBA:
                        result = WebPDecodeRGBAInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                    case decodeType.BGR:
                        result = WebPDecodeBGRInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                    case decodeType.BGRA:
                        result = WebPDecodeBGRAInto(data, data_size, output_buffer, output_buffer_size, output_stride);
                        break;
                }

                // Set out values
                imgWidth = width;
                imgHeight = height;

                // Copy data back to managed memory and return
                return GetDataFromUnmanagedMemory(result, output_buffer_size);
            }
            catch
            {
                throw;
            }
            finally
            {
                // Free unmanaged memory
                Marshal.FreeHGlobal(data);
                Marshal.FreeHGlobal(output_buffer);
            }
        }

        /// <summary>
        /// Copy data from managed to unmanaged memory
        /// </summary>
        /// <param name="data">The data you want to copy</param>
        /// <returns>Pointer to the location of the unmanaged data</returns>
        internal static IntPtr CopyDataToUnmanagedMemory(byte[] data)
        {
            // Initialize unmanged memory to hold the array
            int size = Marshal.SizeOf(data[0]) * data.Length;
            IntPtr pnt = Marshal.AllocHGlobal(size);
            // Copy the array to unmanaged memory
            Marshal.Copy(data, 0, pnt, data.Length);
            return pnt;
        }

        /// <summary>
        /// Get data from unmanaged memory back to managed memory
        /// </summary>
        /// <param name="source">A Pointer where the data lifes</param>
        /// <param name="lenght">How many bytes you want to copy</param>
        /// <returns></returns>
        internal static byte[] GetDataFromUnmanagedMemory(IntPtr source, int lenght)
        {
            // Initialize managed memory to hold the array
            byte[] data = new byte[lenght];
            // Copy the array back to managed memory
            Marshal.Copy(source, data, 0, lenght);
            return data;
        }

        /// <summary>
        /// Get a file from the disk and copy it into a managed byte array
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>A byte array containing the file</returns>
        internal static byte[] CopyFileToManagedArray(string path)
        {
            // Load file from disk and copy it into a byte array
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            return data;
        }

        /// <summary>
        /// Convert raw image data into a Bitmap object
        /// </summary>
        /// <param name="data">The byte array containing the image data</param>
        /// <param name="imgWidth">The width of your image</param>
        /// <param name="imgHeight">The height of your image</param>
        /// <param name="format">The PixelFormat the Bitmap should use</param>
        /// <returns>The Bitmap object conating you image</returns>
        internal static Bitmap ConvertDataToBitmap(byte[] data, int imgWidth, int imgHeight, PixelFormat format)
        {
            // Create the Bitmap to the know height, width and format
            Bitmap bmp = new Bitmap(imgWidth, imgHeight, format);
            // Create a BitmapData and Lock all pixels to be written
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            //Copy the data from the byte array into BitmapData.Scan0
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            //Unlock the pixels
            bmp.UnlockBits(bmpData);
            //Return the bitmap
            return bmp;
        }

        /// <summary>
        /// Calculate the needed size for a bitmap
        /// </summary>
        /// <param name="imgWidth">The image width</param>
        /// <param name="imgHeight">The image height</param>
        /// <param name="format">The pixel format you want to use</param>
        /// <returns>The bitmap size in bytes</returns>
        internal static int CalculateBitmapSize(int imgWidth, int imgHeight, PixelFormat format)
        {
            return imgWidth * imgHeight * Image.GetPixelFormatSize(format) / 8;
        }

        /// <summary>
        /// Return the decoder's version number
        /// </summary>
        /// <returns>Hexadecimal using 8bits for each of major/minor/revision. E.g: v2.5.7 is 0x020507</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern int WebPGetDecoderVersion();

        /// <summary>
        /// This function will validate the WebP image header and retrieve the image height and width. Pointers *width and *height can be passed NULL if deemed irrelevant
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="width">The range is limited currently from 1 to 16383</param>
        /// <param name="height">The range is limited currently from 1 to 16383</param>
        /// <returns>1 if success, otherwise error code returned in the case of (a) formatting error(s).</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern int WebPGetInfo(IntPtr data, UInt32 data_size, ref int width, ref int height);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns RGB samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern IntPtr WebPDecodeRGBInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns RGBA samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern IntPtr WebPDecodeRGBAInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns BGR samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern IntPtr WebPDecodeBGRInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        /// <summary>
        ///  Decodes WEBP images pointed to by *data and returns BGRA samples into a pre-allocated buffer
        /// </summary>
        /// <param name="data">Pointer to WebP image data</param>
        /// <param name="data_size">This is the size of the memory block pointed to by data containing the image data</param>
        /// <param name="output_buffer">Pointer to decoded WebP image</param>
        /// <param name="output_buffer_size">Size of allocated buffer</param>
        /// <param name="output_stride">Specifies the distance between scanlines</param>
        /// <returns>output_buffer if function succeeds; NULL otherwise</returns>
        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern IntPtr WebPDecodeBGRAInto(IntPtr data, UInt32 data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp", CharSet = CharSet.Auto)]
        private static extern VP8StatusCode WebPGetFeaturesInternal(IntPtr rawWebP, UIntPtr data_size, ref WebPBitstreamFeatures features, int WEBP_DECODER_ABI_VERSION);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        private struct WebPBitstreamFeatures
        {
            /// <summary>Width in pixels, as read from the bitstream.</summary>
            public int Width;
            /// <summary>Height in pixels, as read from the bitstream.</summary>
            public int Height;
            /// <summary>True if the bitstream contains an alpha channel.</summary>
            public int Has_alpha;
            /// <summary>True if the bitstream is an animation.</summary>
            public int Has_animation;
            /// <summary>0 = undefined (/mixed), 1 = lossy, 2 = lossless</summary>
            public int Format;
            /// <summary>Padding for later use.</summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
            private readonly uint[] pad;
        };

        /// <summary>Enumeration of the status codes.</summary>
        private enum VP8StatusCode
        {
            /// <summary>No error.</summary>
            VP8_STATUS_OK = 0,
            /// <summary>Memory error allocating objects.</summary>
            VP8_STATUS_OUT_OF_MEMORY,
            /// <summary>Configuration is invalid.</summary>
            VP8_STATUS_INVALID_PARAM,
            VP8_STATUS_BITSTREAM_ERROR,
            /// <summary>Configuration is invalid.</summary>
            VP8_STATUS_UNSUPPORTED_FEATURE,
            VP8_STATUS_SUSPENDED,
            /// <summary>Abort request by user.</summary>
            VP8_STATUS_USER_ABORT,
            VP8_STATUS_NOT_ENOUGH_DATA,
        }
    }
}
