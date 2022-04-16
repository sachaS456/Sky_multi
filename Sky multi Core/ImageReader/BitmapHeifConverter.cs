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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Sky_multi_Core.ImageReader.Heif;

namespace Sky_multi_Core.ImageReader
{
    public static class BitmapHeifConverter
    {
        public static Bitmap OpenHeifFromPathToBitmap(string Path)
        {
            HeifDecodingOptions decodingOptions = new HeifDecodingOptions
            {
                ConvertHdrToEightBit = false
            };

            HeifContext context = new HeifContext(Path);
            HeifImageHandle primaryImage = context.GetPrimaryImageHandle();

            HeifChroma chroma;
            bool hasAlpha = primaryImage.HasAlphaChannel;
            int bitDepth = primaryImage.BitDepth;

            if (bitDepth == 8 || decodingOptions.ConvertHdrToEightBit)
            {
                chroma = hasAlpha ? HeifChroma.InterleavedRgba32 : HeifChroma.InterleavedRgb24;
            }
            else
            {
                // Use the native byte order of the operating system.
                if (BitConverter.IsLittleEndian)
                {
                    chroma = hasAlpha ? HeifChroma.InterleavedRgba64LE : HeifChroma.InterleavedRgb48LE;
                }
                else
                {
                    chroma = hasAlpha ? HeifChroma.InterleavedRgba64BE : HeifChroma.InterleavedRgb48BE;
                }
            }

            HeifImage image = primaryImage.Decode(HeifColorspace.Rgb, chroma, decodingOptions);

            Bitmap outputImage;

            switch (chroma)
            {
                case HeifChroma.InterleavedRgb24:
                    outputImage = CreateEightBitImageWithoutAlpha(image);
                    break;
                case HeifChroma.InterleavedRgba32:
                    outputImage = CreateEightBitImageWithAlpha(image, primaryImage.IsPremultipliedAlpha);
                    break;
                case HeifChroma.InterleavedRgb48BE:
                case HeifChroma.InterleavedRgb48LE:
                    outputImage = CreateSixteenBitImageWithoutAlpha(image);
                    break;
                case HeifChroma.InterleavedRgba64BE:
                case HeifChroma.InterleavedRgba64LE:
                    outputImage = CreateSixteenBitImageWithAlpha(image, primaryImage.IsPremultipliedAlpha, primaryImage.BitDepth);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported HeifChroma value.");
            }

            //byte[] exif = primaryImage.GetExifMetadata();

            image.Dispose();
            primaryImage.Dispose();
            context.Dispose();

            return outputImage;
        }

        private static unsafe byte[] CreateArrayEightBitImageWithAlpha(IntPtr Scan0, int srcStride, int Width, int Height, bool premultiplied)
        {
            byte* srcScan0 = (byte*)Scan0;
            byte* src;

            List<byte> Buffer = new List<byte>();

            for (int y = 0; y < Height; y++)
            {
                src = srcScan0 + (y * srcStride);

                for (int x = 0; x < Width; x++)
                {
                    //Color pixel = heifImage.GetPixel(x, y);

                    if (premultiplied)
                    {
                        switch (src[3]) // alpha
                        {
                            case 0:
                                //image.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                                Buffer.Add(0);// Blue
                                Buffer.Add(0);// Green
                                Buffer.Add(0);// Red
                                Buffer.Add(src[3]);// Alpha
                                break;

                            case 255:
                                //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                                Buffer.Add(src[2]);// B
                                Buffer.Add(src[1]);// G
                                Buffer.Add(src[0]);// R
                                Buffer.Add(src[3]);// Alpha
                                break;

                            default:
                                //image.SetPixel(x, y, Color.FromArgb((byte)Math.Min(MathF.Round(src[0] * 255f / alpha), 255), 
                                //(byte)Math.Min(MathF.Round(src[1] * 255f / alpha), 255), 
                                //(byte)Math.Min(MathF.Round(src[2] * 255f / alpha), 255)));
                                Buffer.Add((byte)Math.Min(MathF.Round(src[2] * 255f / src[3]), 255));// B
                                Buffer.Add((byte)Math.Min(MathF.Round(src[1] * 255f / src[3]), 255));// G
                                Buffer.Add((byte)Math.Min(MathF.Round(src[0] * 255f / src[3]), 255));// R
                                Buffer.Add(src[3]);// Alpha
                                break;
                        }
                    }
                    else
                    {
                        //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                        Buffer.Add(src[2]);// B
                        Buffer.Add(src[1]);// G
                        Buffer.Add(src[0]);// R
                        Buffer.Add(src[3]);// Alpha
                    }

                    //image.SetPixel(x, y, Color.FromArgb(src[3], pixel.R, pixel.G, pixel.B));

                    src += 4;
                }
            }

            srcScan0 = (byte*)0;
            src = (byte*)0;

            return Buffer.ToArray();
        }

        private static unsafe void CopyEightBitImageWithAlpha(IntPtr Scan0, int srcStride, int Width, int Height, bool premultiplied, IntPtr BufferPtr)
        {
            byte* srcScan0 = (byte*)Scan0;
            byte* src;

            byte* Buffer = (byte*)BufferPtr;
            byte* src2;

            for (int y = 0; y < Height; y++)
            {
                src = srcScan0 + (y * srcStride);
                src2 = Buffer + (y * srcStride);

                for (int x = 0; x < Width; x++)
                {
                    //Color pixel = heifImage.GetPixel(x, y);

                    if (premultiplied)
                    {
                        switch (src[3]) // alpha
                        {
                            case 0:
                                //image.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                                src2[0] = (0);// Blue
                                src2[1] = (0);// Green
                                src2[2] = (0);// Red
                                src2[3] = (src[3]);// Alpha
                                break;

                            case 255:
                                //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                                src2[0] = (src[2]);// B
                                src2[1] = (src[1]);// G
                                src2[2] = (src[0]);// R
                                src2[3] = (src[3]);// Alpha
                                break;

                            default:
                                //image.SetPixel(x, y, Color.FromArgb((byte)Math.Min(MathF.Round(src[0] * 255f / alpha), 255), 
                                //(byte)Math.Min(MathF.Round(src[1] * 255f / alpha), 255), 
                                //(byte)Math.Min(MathF.Round(src[2] * 255f / alpha), 255)));
                                src2[0] = ((byte)MathF.Round((float)src[2] * src[3] / 255f));// B
                                src2[1] = ((byte)MathF.Round((float)src[1] * src[3] / 255f));// G
                                src2[2] = ((byte)MathF.Round((float)src[0] * src[3] / 255f));// R
                                src2[3] = (src[3]);// Alpha
                                break;
                        }
                    }
                    else
                    {
                        //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                        src2[0] = (src[2]);// B
                        src2[1] = (src[1]);// G
                        src2[2] = (src[0]);// R
                        src2[3] = (src[3]);// Alpha
                    }

                    //image.SetPixel(x, y, Color.FromArgb(src[3], pixel.R, pixel.G, pixel.B));

                    src += 4;
                    src2 += 4;
                }
            }

            srcScan0 = (byte*)0;
            src = (byte*)0;

            Buffer = (byte*)0;
            src2 = (byte*)0;
        }

        private static Bitmap CreateEightBitImageWithAlpha(HeifImage heifImage, bool premultiplied)
        {
            HeifPlaneData heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            byte[] Buffer = CreateArrayEightBitImageWithAlpha(heifPlaneData.Scan0, heifPlaneData.Stride, heifPlaneData.Width, heifPlaneData.Height, premultiplied);

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format32bppArgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer, 0, bmd.Scan0, heifImage.Width * heifImage.Height * 4);
            bmp.UnlockBits(bmd);

            return bmp;
        }

        private static unsafe byte[] CreateArrayEightBitImageWithoutAlpha(IntPtr Scan0, int srcStride, int Width, int Height)
        {
            byte* srcScan0 = (byte*)Scan0;
            byte* src;

            List<byte> Buffer = new List<byte>();

            for (int y = 0; y < Height; y++)
            {
                src = srcScan0 + (y * srcStride);

                for (int x = 0; x < Width; x++)
                {
                    //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                    Buffer.Add(src[2]);
                    Buffer.Add(src[1]);
                    Buffer.Add(src[0]);

                    src += 3;
                }
            }

            srcScan0 = (byte*)0;
            src = (byte*)0;

            return Buffer.ToArray();
        }

        private static unsafe void CopyEightBitImageWithoutAlpha(IntPtr Scan0, int srcStride, int Width, int Height, IntPtr BufferPtr)
        {
            byte* srcScan0 = (byte*)Scan0;
            byte* src;

            byte* Buffer = (byte*)BufferPtr;
            byte* src2;

            for (int y = 0; y < Height; y++)
            {
                src = srcScan0 + (y * srcStride);
                src2 = Buffer + (y * srcStride);

                for (int x = 0; x < Width; x++)
                {
                    //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                    src2[2] = (src[2]);
                    src2[1] = (src[1]);
                    src2[0] = (src[0]);

                    src += 3;
                    src2 += 3;
                }
            }

            srcScan0 = (byte*)0;
            src = (byte*)0;

            Buffer = (byte*)0;
            src2 = (byte*)0;
        }

        private static Bitmap CreateEightBitImageWithoutAlpha(HeifImage heifImage)
        {
            HeifPlaneData heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            byte[] Buffer = CreateArrayEightBitImageWithoutAlpha(heifPlaneData.Scan0, heifPlaneData.Stride, heifPlaneData.Width, heifPlaneData.Height);

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format24bppRgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer, 0, bmd.Scan0, heifImage.Width * heifImage.Height * 3);
            bmp.UnlockBits(bmd);

            return bmp;
        }

        private static unsafe int[] CreateArraySixteenBitImageWithAlpha(IntPtr Scan0, int srcStride, int Width, int Height, bool premultiplied, int bitDepth)
        {
            byte* srcScan0 = (byte*)Scan0;

            int maxChannelValue = (1 << bitDepth) - 1;
            float maxChannelValueFloat = maxChannelValue;
            ushort* src;

            List<int> Buffer = new List<int>();

            for (int y = 0; y < Height; y++)
            {
                src = (ushort*)(srcScan0 + (y * srcStride));

                for (int x = 0; x < Width; x++)
                {
                    //var pixel = heifImage.GetPixel(x, y);

                    if (premultiplied)
                    {
                        if (src[3] == maxChannelValue)
                        {
                            //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                            Buffer.Add(src[2]);
                            Buffer.Add(src[1]);
                            Buffer.Add(src[0]);
                            Buffer.Add(src[3]);
                        }
                        else
                        {
                            switch (src[3])
                            {
                                case 0:
                                    //image.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                                    Buffer.Add(0);
                                    Buffer.Add(0);
                                    Buffer.Add(0);
                                    Buffer.Add(src[3]);
                                    break;
                                default:
                                    //image.SetPixel(x, y, Color.FromArgb((ushort)Math.Min(MathF.Round(src[0] * maxChannelValueFloat / alpha), maxChannelValue),
                                    //(ushort)Math.Min(MathF.Round(src[1] * maxChannelValueFloat / alpha), maxChannelValue), 
                                    //(ushort)Math.Min(MathF.Round(src[2] * maxChannelValueFloat / alpha), maxChannelValue)));
                                    Buffer.Add((ushort)Math.Min(MathF.Round(src[2] * maxChannelValueFloat / src[3]), maxChannelValue));
                                    Buffer.Add((ushort)Math.Min(MathF.Round(src[1] * maxChannelValueFloat / src[3]), maxChannelValue));
                                    Buffer.Add((ushort)Math.Min(MathF.Round(src[0] * maxChannelValueFloat / src[3]), maxChannelValue));
                                    Buffer.Add(src[3]);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                        Buffer.Add(src[2]);
                        Buffer.Add(src[1]);
                        Buffer.Add(src[0]);
                        Buffer.Add(src[3]);
                    }
                    //image.SetPixel(x, y, Color.FromArgb(src[3], pixel.R, pixel.G, pixel.B));

                    src += 4;
                }
            }

            srcScan0 = (byte*)0;
            src = (ushort*)0;

            return Buffer.ToArray();
        }

        private static Bitmap CreateSixteenBitImageWithAlpha(HeifImage heifImage, bool premultiplied, int bitDepth)
        {
            HeifPlaneData heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            int[] Buffer = CreateArraySixteenBitImageWithAlpha(heifPlaneData.Scan0, heifPlaneData.Stride, heifPlaneData.Width, heifPlaneData.Height, premultiplied, bitDepth);

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format64bppArgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer, 0, bmd.Scan0, heifImage.Width * heifImage.Height * 4);
            bmp.UnlockBits(bmd);

            return bmp;
        }

        private static unsafe int[] CreateArraySixteenBitImageWithoutAlpha(IntPtr Scan0, int srcStride, int Width, int Height)
        {
            byte* srcScan0 = (byte*)Scan0;
            ushort* src;

            List<int> Buffer = new List<int>();

            for (int y = 0; y < Height; y++)
            {
                src = (ushort*)(srcScan0 + (y * srcStride));

                for (int x = 0; x < Width; x++)
                {
                    //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                    Buffer.Add(src[2]);
                    Buffer.Add(src[1]);
                    Buffer.Add(src[0]);

                    src += 3;
                }
            }

            srcScan0 = (byte*)0;
            src = (ushort*)0;

            return Buffer.ToArray();
        }

        private static Bitmap CreateSixteenBitImageWithoutAlpha(HeifImage heifImage)
        {
            HeifPlaneData heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            int[] Buffer = CreateArraySixteenBitImageWithoutAlpha(heifPlaneData.Scan0, heifPlaneData.Stride, heifPlaneData.Width, heifPlaneData.Height);

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format48bppRgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer, 0, bmd.Scan0, heifImage.Width * heifImage.Height * 3);
            bmp.UnlockBits(bmd);

            return bmp;
        }


        public static void EncodeHeif(Bitmap bitmap, string Path)
        {
            Encode(in bitmap, in Path, HeifCompressionFormat.Hevc);
        }

        public static void EncodeAvif(Bitmap bitmap, string Path)
        {
            Encode(in bitmap, in Path, HeifCompressionFormat.Av1);
        }

        public static void EncodeHeif(IntPtr Scan0, int Stride, int Width, int Height, bool alpha, string Path)
        {
            Encode(Scan0, Stride, Width, Height, alpha, in Path, HeifCompressionFormat.Hevc);
        }

        public static void EncodeAvif(IntPtr Scan0, int Stride, int Width, int Height, bool alpha, string Path)
        {
            Encode(Scan0, Stride, Width, Height, alpha, in Path, HeifCompressionFormat.Av1);
        }

        private static void Encode(IntPtr Scan0, int Stride, int Width, int Height, bool alpha, in string Path, HeifCompressionFormat format)
        {
            if (LibHeifInfo.HaveEncoder(format) == false)
            {
                throw new Exception("Encodeur Not Found!");
            }

            if (string.IsNullOrEmpty(Path) == true)
            {
                throw new Exception("Path is null!");
            }

            HeifContext heifContext = new HeifContext();
            HeifEncoder encoder = heifContext.GetEncoder(format);
            HeifPlaneData heifPlaneData;
            HeifImage image;

            if (alpha)
            {
                image = new HeifImage(Width, Height, HeifColorspace.Rgb, HeifChroma.InterleavedRgba32);
                image.AddPlane(HeifChannel.Interleaved, image.Width, image.Height, 8);
                heifPlaneData = image.GetPlane(HeifChannel.Interleaved);
                CopyEightBitImageWithAlpha(Scan0, Stride, Width, Height, false, heifPlaneData.Scan0);
                heifContext.EncodeImage(image, encoder);
                heifContext.WriteToFile(Path);
                image.Dispose();
            }
            else
            {
                image = new HeifImage(Width, Height, HeifColorspace.Rgb, HeifChroma.InterleavedRgb24);
                image.AddPlane(HeifChannel.Interleaved, image.Width, image.Height, 8);
                heifPlaneData = image.GetPlane(HeifChannel.Interleaved);
                CopyEightBitImageWithoutAlpha(Scan0, Stride, Width, Height, heifPlaneData.Scan0);
                heifContext.EncodeImage(image, encoder);
                heifContext.WriteToFile(Path);
                image.Dispose();
            }

            encoder.Dispose();
            heifContext.Dispose();
        }

        private static void Encode(in Bitmap bitmap, in string Path, HeifCompressionFormat format)
        {
            if (LibHeifInfo.HaveEncoder(format) == false)
            {
                throw new Exception("Encodeur Not Found!");
            }

            if (string.IsNullOrEmpty(Path) == true)
            {
                throw new Exception("Path is null!");
            }

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            HeifContext heifContext = new HeifContext();
            HeifEncoder encoder = heifContext.GetEncoder(format);
            HeifPlaneData heifPlaneData;
            HeifImage image;

            switch (bitmap.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:

                    image = new HeifImage(bitmap.Width, bitmap.Height, HeifColorspace.Rgb, HeifChroma.InterleavedRgb24);
                    image.AddPlane(HeifChannel.Interleaved, image.Width, image.Height, 8);
                    heifPlaneData = image.GetPlane(HeifChannel.Interleaved);
                    CopyEightBitImageWithoutAlpha(bitmapData.Scan0, bitmapData.Stride, bitmapData.Width, bitmapData.Height, heifPlaneData.Scan0);
                    heifContext.EncodeImage(image, encoder);
                    heifContext.WriteToFile(Path);
                    image.Dispose();
                    break;

                case PixelFormat.Format32bppArgb:
                    image = new HeifImage(bitmap.Width, bitmap.Height, HeifColorspace.Rgb, HeifChroma.InterleavedRgba32);
                    image.AddPlane(HeifChannel.Interleaved, image.Width, image.Height, 8);
                    heifPlaneData = image.GetPlane(HeifChannel.Interleaved);
                    CopyEightBitImageWithAlpha(bitmapData.Scan0, bitmapData.Stride, bitmapData.Width, bitmapData.Height, false, heifPlaneData.Scan0);
                    heifContext.EncodeImage(image, encoder);
                    heifContext.WriteToFile(Path);
                    image.Dispose();
                    break;

                default:
                    bitmap.UnlockBits(bitmapData);
                    encoder.Dispose();
                    heifContext.Dispose();
                    throw new Exception("format is not supported!");
            }

            bitmap.UnlockBits(bitmapData);
            encoder.Dispose();
            heifContext.Dispose();
        }
    }
}
