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
    public static class BitmapHeifCoverter
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

        private static unsafe Bitmap CreateEightBitImageWithAlpha(HeifImage heifImage, bool premultiplied)
        {
            var heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            byte* srcScan0 = (byte*)heifPlaneData.Scan0;
            int srcStride = heifPlaneData.Stride;

            List<byte> Buffer = new List<byte>();

            for (int y = 0; y < heifImage.Height; y++)
            {
                byte* src = srcScan0 + (y * srcStride);

                for (int x = 0; x < heifImage.Width; x++)
                {
                    //Color pixel = heifImage.GetPixel(x, y);

                    if (premultiplied)
                    {
                        byte alpha = src[3];

                        switch (alpha)
                        {
                            case 0:
                                //image.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                                Buffer.Add(src[3]);
                                Buffer.Add(0);
                                Buffer.Add(0);
                                Buffer.Add(0);
                                break;

                            case 255:
                                //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                                Buffer.Add(src[3]);
                                Buffer.Add(src[2]);
                                Buffer.Add(src[1]);
                                Buffer.Add(src[0]);
                                break;

                            default:
                                //image.SetPixel(x, y, Color.FromArgb((byte)Math.Min(MathF.Round(src[0] * 255f / alpha), 255), 
                                //(byte)Math.Min(MathF.Round(src[1] * 255f / alpha), 255), 
                                //(byte)Math.Min(MathF.Round(src[2] * 255f / alpha), 255)));
                                Buffer.Add(src[3]);
                                Buffer.Add((byte)Math.Min(MathF.Round(src[2] * 255f / alpha), 255));
                                Buffer.Add((byte)Math.Min(MathF.Round(src[1] * 255f / alpha), 255));
                                Buffer.Add((byte)Math.Min(MathF.Round(src[0] * 255f / alpha), 255));
                                break;
                        }
                    }
                    else
                    {
                        //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                        Buffer.Add(src[3]);
                        Buffer.Add(src[2]);
                        Buffer.Add(src[1]);
                        Buffer.Add(src[0]);
                    }

                    //image.SetPixel(x, y, Color.FromArgb(src[3], pixel.R, pixel.G, pixel.B));

                    src += 4;
                }
            }

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format32bppArgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer.ToArray(), 0, bmd.Scan0, heifImage.Width * heifImage.Height * 4);
            bmp.UnlockBits(bmd);

            return bmp;
        }

        private static unsafe Bitmap CreateEightBitImageWithoutAlpha(HeifImage heifImage)
        {
            var heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            byte* srcScan0 = (byte*)heifPlaneData.Scan0;
            int srcStride = heifPlaneData.Stride;

            List<byte> Buffer = new List<byte>();

            for (int y = 0; y < heifImage.Height; y++)
            {
                byte* src = srcScan0 + (y * srcStride);

                for (int x = 0; x < heifImage.Width; x++)
                {
                    //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                    Buffer.Add(src[2]);
                    Buffer.Add(src[1]);
                    Buffer.Add(src[0]);

                    src += 3;
                }
            }

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format24bppRgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer.ToArray(), 0, bmd.Scan0, heifImage.Width * heifImage.Height * 3);
            bmp.UnlockBits(bmd);

            return bmp;
        }

        private static unsafe Bitmap CreateSixteenBitImageWithAlpha(HeifImage heifImage, bool premultiplied, int bitDepth)
        {
            var heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            byte* srcScan0 = (byte*)heifPlaneData.Scan0;
            int srcStride = heifPlaneData.Stride;

            int maxChannelValue = (1 << bitDepth) - 1;
            float maxChannelValueFloat = maxChannelValue;

            List<short> Buffer = new List<short>();

            for (int y = 0; y < heifImage.Height; y++)
            {
                short* src = (short*)(srcScan0 + (y * srcStride));

                for (int x = 0; x < heifImage.Width; x++)
                {
                    //var pixel = heifImage.GetPixel(x, y);

                    if (premultiplied)
                    {
                        short alpha = src[3];

                        if (alpha == maxChannelValue)
                        {
                            //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                            Buffer.Add(src[3]);
                            Buffer.Add(src[2]);
                            Buffer.Add(src[1]);
                            Buffer.Add(src[0]);
                        }
                        else
                        {
                            switch (alpha)
                            {
                                case 0:
                                    //image.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                                    Buffer.Add(src[3]);
                                    Buffer.Add(0);
                                    Buffer.Add(0);
                                    Buffer.Add(0);
                                    break;
                                default:
                                    //image.SetPixel(x, y, Color.FromArgb((ushort)Math.Min(MathF.Round(src[0] * maxChannelValueFloat / alpha), maxChannelValue),
                                    //(ushort)Math.Min(MathF.Round(src[1] * maxChannelValueFloat / alpha), maxChannelValue), 
                                    //(ushort)Math.Min(MathF.Round(src[2] * maxChannelValueFloat / alpha), maxChannelValue)));
                                    Buffer.Add(src[3]);
                                    Buffer.Add((short)Math.Min(MathF.Round(src[2] * maxChannelValueFloat / alpha), maxChannelValue));
                                    Buffer.Add((short)Math.Min(MathF.Round(src[1] * maxChannelValueFloat / alpha), maxChannelValue));
                                    Buffer.Add((short)Math.Min(MathF.Round(src[0] * maxChannelValueFloat / alpha), maxChannelValue));
                                    break;
                            }
                        }
                    }
                    else
                    {
                        //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                        Buffer.Add(src[3]);
                        Buffer.Add(src[2]);
                        Buffer.Add(src[1]);
                        Buffer.Add(src[0]);
                    }
                    //image.SetPixel(x, y, Color.FromArgb(src[3], pixel.R, pixel.G, pixel.B));

                    src += 4;
                }
            }

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format64bppArgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer.ToArray(), 0, bmd.Scan0, heifImage.Width * heifImage.Height * 4);
            bmp.UnlockBits(bmd);

            return bmp;
        }

        private static unsafe Bitmap CreateSixteenBitImageWithoutAlpha(HeifImage heifImage)
        {
            var heifPlaneData = heifImage.GetPlane(HeifChannel.Interleaved);

            byte* srcScan0 = (byte*)heifPlaneData.Scan0;
            int srcStride = heifPlaneData.Stride;

            List<short> Buffer = new List<short>();

            for (int y = 0; y < heifImage.Height; y++)
            {
                short* src = (short*)(srcScan0 + (y * srcStride));

                for (int x = 0; x < heifImage.Width; x++)
                {
                    //image.SetPixel(x, y, Color.FromArgb(src[0], src[1], src[2]));
                    Buffer.Add(src[2]);
                    Buffer.Add(src[1]);
                    Buffer.Add(src[0]);

                    src += 3;
                }
            }

            Bitmap bmp = new Bitmap(heifImage.Width, heifImage.Height, PixelFormat.Format48bppRgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(Buffer.ToArray(), 0, bmd.Scan0, heifImage.Width * heifImage.Height * 3);
            bmp.UnlockBits(bmd);

            return bmp;
        }
    }
}
