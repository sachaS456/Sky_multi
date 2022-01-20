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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.ImageReader
{
    public class RawDecoder
    {
        public static string LibRawVersion() => Marshal.PtrToStringAnsi(RawDecoderCore.libraw_version());

        public static Version LibRawVersionNumber
        {
            get
            {
                string versionString = LibRawVersion();
                versionString = versionString.Split('-', ' ')[0];

                return new Version(versionString);
            }
        }

        public static Bitmap RawToBitmap(string Path)
        {
            IntPtr handler = RawDecoderCore.libraw_init(RawDecoderCore.LibRaw_init_flags.LIBRAW_OPTIONS_NONE);

            RawDecoderCore.LibRaw_errors r = RawDecoderCore.libraw_open_file(handler, Path);
            Path = null;
            if (r != RawDecoderCore.LibRaw_errors.LIBRAW_SUCCESS)
            {
                //MessageBox.Show("Open file: " + System.Runtime.InteropServices.Marshal.PtrToStringAnsi(LibRAW.RAWLib.libraw_strerror(r)));
                RawDecoderCore.libraw_close(handler);
                throw new Exception("this is not a image raw");
            }

            // unpack data from raw file
            r = RawDecoderCore.libraw_unpack(handler);
            if (r != RawDecoderCore.LibRaw_errors.LIBRAW_SUCCESS)
            {
                //Console.WriteLine("Unpack: " + PtrToStringAnsi(libraw_strerror(r)));
                RawDecoderCore.libraw_close(handler);
                throw new Exception("this is not a image raw");
            }

            RawDecoderCore.libraw_set_demosaic(handler, RawDecoderCore.LibRaw_interpolation_quality.LINEAR);
            RawDecoderCore.libraw_set_fbdd_noiserd(handler, RawDecoderCore.LibRaw_FBDD_noise_reduction.NO_FBDD);

            // process data using previously defined settings
            r = RawDecoderCore.libraw_dcraw_process(handler);
            if (r != RawDecoderCore.LibRaw_errors.LIBRAW_SUCCESS)
            {
                //Console.WriteLine("Process: " + PtrToStringAnsi(libraw_strerror(r)));
                RawDecoderCore.libraw_close(handler);
                throw new Exception("this is not a image raw");
            }

            int errc = 0;
            IntPtr ptr = RawDecoderCore.libraw_dcraw_make_mem_image(handler, ref errc);

            RawDecoderCore.libraw_processed_image_t img = Marshal.PtrToStructure<RawDecoderCore.libraw_processed_image_t>(ptr);

            Array.Resize(ref img.data, (int)img.data_size);

            Marshal.Copy(ptr, img.data, 0, (int)img.data_size);

            RawDecoderCore.libraw_dcraw_clear_mem(ptr);
            ptr = IntPtr.Zero;

            // revers Colors Green and Blue
            short index2 = 0;
            for (int index = 0; index < img.data.Length; index++, index2++)
            {
                if (index2 == 2)
                {
                    index2 = -1;

                    byte G = img.data[index - 1];
                    img.data[index - 1] = img.data[index];
                    img.data[index] = G;                  
                }
            }

            Bitmap bmp = new Bitmap(img.width, img.height, PixelFormat.Format24bppRgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(img.data, 0, bmd.Scan0, (int)img.data_size);
            bmp.UnlockBits(bmd);

            bmd = null;
            RawDecoderCore.libraw_close(handler);
            handler = IntPtr.Zero;
            GC.Collect();

            return bmp;
        }

        [ObsoleteAttribute("This property is obsolete. Use LibRaw_errors.", false)]
        public static bool RawTest(string FileName)
        {
            switch (System.IO.Path.GetExtension(FileName))
            {
                case ".raw":

                    return true;

                case ".3fr":

                    return true;

                case ".arw":

                    return true;

                case ".bay":

                    return true;

                case ".crw":

                    return true;

                case ".cr2":

                    return true;

                case ".cr3":

                    return true;

                case ".cap":

                    return true;

                case ".data":

                    return true;

                case ".dcs":

                    return true;

                case ".dcr":

                    return true;

                case ".dng":

                    return true;

                case ".drf":

                    return true;

                case ".eip":

                    return true;

                case ".erf":

                    return true;

                case ".fff":

                    return true;

                case ".gpr":

                    return true;

                case ".iiq":

                    return true;

                case ".k25":

                    return true;

                case ".kdc":

                    return true;

                case ".mdc":

                    return true;

                case ".mef":

                    return true;

                case ".mos":

                    return true;

                case ".mrw":

                    return true;

                case ".nef":

                    return true;

                case ".nrw":

                    return true;

                case ".obm":

                    return true;

                case ".orf":

                    return true;

                case ".pef":

                    return true;

                case ".ptx":

                    return true;

                case ".pxn":

                    return true;

                case ".r3d":

                    return true;

                case ".raf":

                    return true;

                case ".rwl":

                    return true;

                case ".rw2":

                    return true;

                case ".rwz":

                    return true;

                case ".sr2":

                    return true;

                case ".srf":

                    return true;

                case ".srw":

                    return true;

                case ".tif":

                    return true;

                case ".x3f":

                    return true;

                default:

                    return false;
            }
        }

        /*public static void ConvertRawToTiff(string Path, string Ext = ".tiff")
        {
            IntPtr handler = RawDecoderCore.libraw_init(RawDecoderCore.LibRaw_init_flags.LIBRAW_OPTIONS_NONE);
            RawDecoderCore.libraw_set_output_tif(handler, RawDecoderCore.LibRaw_output_formats.TIFF);
            RawDecoderCore.libraw_set_no_auto_bright(handler, 0);

            RawDecoderCore.LibRaw_errors r = RawDecoderCore.libraw_open_file(handler, Path);
            if (r != RawDecoderCore.LibRaw_errors.LIBRAW_SUCCESS)
            {
                RawDecoderCore.libraw_close(handler);
                return;
            }

            r = RawDecoderCore.libraw_unpack(handler);
            if (r != RawDecoderCore.LibRaw_errors.LIBRAW_SUCCESS)
            {
                RawDecoderCore.libraw_close(handler);
                return;
            }

            r = RawDecoderCore.libraw_dcraw_process(handler);
            if (r != RawDecoderCore.LibRaw_errors.LIBRAW_SUCCESS)
            {
                RawDecoderCore.libraw_close(handler);
                return;
            }

            r = RawDecoderCore.libraw_dcraw_ppm_tiff_writer(handler, System.IO.Path.GetFileNameWithoutExtension(Path) + Ext);

            Path = null;
            RawDecoderCore.libraw_close(handler);
        }

        public static void ConvertRawToPng(string Path, string Ext = ".png")
        {
            ConvertRawToTiff(Path);
            Bitmap.FromFile(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff").Save(System.IO.Path.GetFileNameWithoutExtension(Path) + Ext, ImageFormat.Png);
            File.Delete(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff");
        }

        public static void ConvertRawToJpeg(string Path, string Ext = ".jpeg")
        {
            ConvertRawToTiff(Path);
            Bitmap.FromFile(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff").Save(System.IO.Path.GetFileNameWithoutExtension(Path) + Ext, ImageFormat.Jpeg);
            File.Delete(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff");
        }

        public static void ConvertRawToGif(string Path, string Ext = ".gif")
        {
            ConvertRawToTiff(Path);
            Bitmap.FromFile(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff").Save(System.IO.Path.GetFileNameWithoutExtension(Path) + Ext, ImageFormat.Gif);
            File.Delete(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff");
        }

        public static void ConvertRawToIco(string Path, string Ext = ".ico")
        {
            ConvertRawToTiff(Path);
            Bitmap.FromFile(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff").Save(System.IO.Path.GetFileNameWithoutExtension(Path) + Ext, ImageFormat.Icon);
            File.Delete(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff");
        }

        public static void ConvertRawToBmp(string Path, string Ext = ".bmp")
        {
            ConvertRawToTiff(Path);
            Bitmap.FromFile(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff").Save(System.IO.Path.GetFileNameWithoutExtension(Path) + Ext, ImageFormat.Bmp);
            File.Delete(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff");
        }

        public static void ConvertRawToWebp(string Path, string Ext = ".webp")
        {
            ConvertRawToTiff(Path);
            WebPEncoder.EncodeBGRA((Bitmap)Bitmap.FromFile(System.IO.Path.GetFileNameWithoutExtension(Path + ".tiff")), System.IO.Path.GetFileNameWithoutExtension(Path + ".webp"));
            File.Delete(System.IO.Path.GetFileNameWithoutExtension(Path) + ".tiff");
        }*/
    }
}
