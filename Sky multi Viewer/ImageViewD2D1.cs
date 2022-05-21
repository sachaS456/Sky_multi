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
using System.Windows.Forms;
using Vortice.Direct2D1;
using Vortice.Mathematics;
using Vortice.WIC;
using SharpGen.Runtime;
using Sky_multi_Core.ImageReader;
using System.IO;
using System.Runtime.InteropServices;

namespace Sky_multi_Viewer
{
    public sealed class ImageViewD2D1 : Control
    {
        private readonly ID2D1Factory7 factory7;
        private readonly ID2D1HwndRenderTarget hwndRender;
        private readonly List<ID2D1Bitmap1> bitmapD2D1;
        private readonly List<IWICBitmap> WICBitmaps;
        private readonly List<ushort> FrameDelay;
        private readonly IWICImagingFactory2 ImagingFactory;        
        private readonly Color4 bgcolor = new(0.1f, 0.1f, 0.1f, 1.0f);
        private SizeI PixelSize;
        private int XZoom = 0;
        private int YZoom = 0;

        public PointF ImagePosition { get; private set; } = PointF.Empty;
        public float ImageWidth { get; private set; } = 0;
        public float ImageHeight { get; private set; } = 0;
        public int Frame { get; private set; } = 0;
        public int FrameCount { get; private set; } = 0;
        public bool CanZoom { get; set; } = true;

        private float Factor = 1.0f;
        private int ImageFactorW = 0;
        private int ImageFactorH = 0;

        public ImageViewD2D1()
        {
            factory7 = D2D1.D2D1CreateFactory<ID2D1Factory7>();
            bitmapD2D1 = new List<ID2D1Bitmap1>();
            WICBitmaps = new List<IWICBitmap>();
            FrameDelay = new List<ushort>();
            ImagingFactory = new IWICImagingFactory2();

            HwndRenderTargetProperties properties = new HwndRenderTargetProperties();
            properties.Hwnd = this.Handle;
            PixelSize = new SizeI(Screen.FromHandle(this.Handle).Bounds.Width, Screen.FromHandle(this.Handle).Bounds.Height);
            properties.PixelSize = PixelSize;
            properties.PresentOptions = PresentOptions.Immediately;

            hwndRender = factory7.CreateHwndRenderTarget(new RenderTargetProperties(new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, 
                Vortice.DCommon.AlphaMode.Premultiplied)), properties);
            hwndRender.AntialiasMode = AntialiasMode.Aliased;
            StartShowFrames();

            /*this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();*/
        }

        private ID2D1Bitmap1 BitmapD2D1FromBitmapGDI(in Bitmap bitmap)
        {
            IWICBitmap WICBitmap = ImagingFactory.CreateBitmapFromHBITMAP(bitmap.GetHbitmap(), (IntPtr)0, BitmapAlphaChannelOption.UsePremultipliedAlpha);
            IWICFormatConverter converter = ImagingFactory.CreateFormatConverter();
            converter.Initialize(WICBitmap, PixelFormat.Format32bppPBGRA, BitmapDitherType.None, null, 1f, BitmapPaletteType.FixedWebPalette);
            WICBitmaps.Add(ImagingFactory.CreateBitmapFromSource(converter, BitmapCreateCacheOption.CacheOnLoad));
            return new ID2D1Bitmap1(hwndRender.CreateBitmapFromWicBitmap(converter, 
                new BitmapProperties(new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied))).NativePointer);
        }

        public void EncodeBitmap(string Path, string Format)
        {
            /*IWICBitmap iWICBitmap = ImagingFactory.CreateBitmap((int)bitmapD2D1[0].Size.Width, (int)bitmapD2D1[0].Size.Height, PixelFormat.Format32bppPBGRA, BitmapCreateCacheOption.CacheOnLoad);

            RenderTargetProperties rtProps = new RenderTargetProperties();
            rtProps.PixelFormat = new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied);
            rtProps.Type = RenderTargetType.Default;
            rtProps.Usage = RenderTargetUsage.None;

            ID2D1RenderTarget d2D1RenderTarget = factory7.CreateWicBitmapRenderTarget(iWICBitmap, rtProps);
            d2D1RenderTarget.BeginDraw();
            //d2D1RenderTarget.Clear(Colors.White);
            d2D1RenderTarget.DrawBitmap(bitmapD2D1[0]);//, new Rect(0, 0, bitmapD2D1[0].Size.Width, bitmapD2D1[0].Size.Height), 1.0f, Vortice.Direct2D1.BitmapInterpolationMode.Linear, null);
            d2D1RenderTarget.EndDraw();*/

            IWICBitmapEncoder BitmapEncoder;
            IWICBitmapLock bmpLock;

            switch (Format)
            {
                case ".png":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Png);
                    break;

                case ".jpg":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Jpeg);
                    break;

                case ".jpeg":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Jpeg);
                    break;

                case ".gif":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Gif);
                    break;

                case ".bmp":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Bmp);
                    break;

                case ".ico":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Ico);
                    break;

                case ".tiff":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Tiff);
                    break;

                case ".tif":
                    BitmapEncoder = ImagingFactory.CreateEncoder(ContainerFormatGuids.Tiff);
                    break;

                case ".webp":
                    bmpLock = WICBitmaps[0].Lock(BitmapLockFlags.Read);
                    WebPEncoder.EncodeWebp(bmpLock.Data.DataPointer, bmpLock.Stride, bmpLock.Size.Width, bmpLock.Size.Height, Path, true);
                    bmpLock.Dispose();
                    return;

                case ".heif":
                    bmpLock = WICBitmaps[0].Lock(BitmapLockFlags.Read);
                    BitmapHeifConverter.EncodeHeif(bmpLock.Data.DataPointer, bmpLock.Stride, bmpLock.Size.Width, bmpLock.Size.Height, true, true, Path);
                    bmpLock.Dispose();
                    return;

                case ".heic":
                    bmpLock = WICBitmaps[0].Lock(BitmapLockFlags.Read);
                    BitmapHeifConverter.EncodeHeif(bmpLock.Data.DataPointer, bmpLock.Stride, bmpLock.Size.Width, bmpLock.Size.Height, true, true, Path);
                    bmpLock.Dispose();
                    return;

                case ".avif":
                    bmpLock = WICBitmaps[0].Lock(BitmapLockFlags.Read);
                    BitmapHeifConverter.EncodeAvif(bmpLock.Data.DataPointer, bmpLock.Stride, bmpLock.Size.Width, bmpLock.Size.Height, true, true, Path);
                    bmpLock.Dispose();
                    return;

                default:
                    throw new Exception("This format for the conversion is not supported!");
            }

            IWICStream stream = ImagingFactory.CreateStream();
            stream.Initialize(Path, System.IO.FileAccess.Write);

            BitmapEncoder.Initialize(stream, BitmapEncoderCacheOption.NoCache);

            IWICBitmapFrameEncode bitmapFrameEncode = BitmapEncoder.CreateNewFrame(out SharpGen.Runtime.Win32.IPropertyBag2 IEncdoderOptions);
            bitmapFrameEncode.Initialize();
            bitmapFrameEncode.SetSize(WICBitmaps[0].Size);
            bitmapFrameEncode.SetPixelFormat(PixelFormat.Format32bppPBGRA);
            bitmapFrameEncode.WriteSource(WICBitmaps[0]);
            bitmapFrameEncode.Commit();

            BitmapEncoder.Commit();
            stream.Commit(SharpGen.Runtime.Win32.CommitFlags.Default);

            BitmapEncoder.Dispose();
            bitmapFrameEncode.Dispose();
            stream.Dispose();
        }

        private async void StartShowFrames()
        {
            while (true)
            {
                if (FrameDelay.Count > 0)
                {
                    await Task.Delay(FrameDelay[Frame]);
                }
                else
                {
                    await Task.Delay(100);
                    continue;
                }

                if (Frame >= FrameCount - 1)
                {
                    Frame = 0;

                    if (FrameCount - 1 >= 1)
                    {
                        DrawImage(true);
                    }
                }
                else
                {
                    Frame++;

                    DrawImage(false);
                }
            }
        }

        private void DecodeImageWICFromFile(in string Path)
        {
            //IWICBitmapDecoder bitmapDecoder = ImagingFactory.CreateDecoderFromFileName(Path, System.IO.FileAccess.Read, DecodeOptions.CacheOnLoad);//ImagingFactory.CreateDecoder(ContainerFormatGuids.Gif, null);
            using (Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                IWICBitmapDecoder bitmapDecoder = ImagingFactory.CreateDecoderFromStream(stream, DecodeOptions.CacheOnLoad);
                IWICFormatConverter converter;

                ResetBitmap();
                FrameCount = bitmapDecoder.FrameCount;

                if (bitmapDecoder.FrameCount <= 1)
                {
                    converter = ImagingFactory.CreateFormatConverter();
                    converter.Initialize(bitmapDecoder.GetFrame(0), PixelFormat.Format32bppPBGRA, BitmapDitherType.None, null, 1f, BitmapPaletteType.FixedWebPalette);
                    WICBitmaps.Add(ImagingFactory.CreateBitmapFromSource(converter, BitmapCreateCacheOption.CacheOnLoad));

                    ID2D1Bitmap image = hwndRender.CreateBitmapFromWicBitmap(converter,
                        new BitmapProperties(new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied)));
                    bitmapD2D1.Add(new ID2D1Bitmap1(image.NativePointer));
                    bitmapDecoder.GetFrame(0).Dispose();
                }
                else
                {
                    for (int i = 0; i < bitmapDecoder.FrameCount; i++)
                    {
                        converter = ImagingFactory.CreateFormatConverter();
                        converter.Initialize(bitmapDecoder.GetFrame(i), PixelFormat.Format32bppPBGRA, BitmapDitherType.None, null, 1f, BitmapPaletteType.FixedWebPalette);
                        WICBitmaps.Add(ImagingFactory.CreateBitmapFromSource(converter, BitmapCreateCacheOption.CacheOnLoad));

                        ushort value = (ushort)(bitmapDecoder.GetFrame(i).MetadataQueryReader.GetMetadataByName(@"/grctlext/Delay").Value);
                        value *= 10;

                        ID2D1Bitmap image = hwndRender.CreateBitmapFromWicBitmap(converter,
                            new BitmapProperties(new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied)));
                        bitmapD2D1.Add(new ID2D1Bitmap1(image.NativePointer));
                        FrameDelay.Add(value);
                        bitmapDecoder.GetFrame(i).Dispose();
                    }
                }

                bitmapDecoder.Release();
                bitmapDecoder.Dispose();
                stream.Close();
                stream.Dispose();
            }
        }

        public void DecodeImageFromFile(in string Path)
        {
            try
            {
                DecodeImageWICFromFile(in Path);
            }
            catch
            {
                try
                {
                    ResetBitmap();
                    bitmapD2D1.Add(BitmapD2D1FromBitmapGDI(RawDecoder.RawToBitmap(Path)));
                }
                catch
                {
                    try
                    {
                        ResetBitmap();
                        bitmapD2D1.Add(BitmapD2D1FromBitmapGDI(WebPDecoder.DecodeWebp(Path)));
                    }
                    catch
                    {
                        try
                        {
                            ResetBitmap();
                            bitmapD2D1.Add(BitmapD2D1FromBitmapGDI(BitmapHeifConverter.OpenHeifFromPathToBitmap(Path)));
                        }
                        catch
                        {
                            throw new Exception("this is not a image or image not supported!");
                        }
                    }
                }
            }

            DrawImage(true);
        }

        public void SetBitmap(Bitmap bitmap)
        {
            ResetBitmap();
            bitmapD2D1.Add(BitmapD2D1FromBitmapGDI(in bitmap));
            DrawImage(true);
        }

        public void SetBitmap(IWICBitmap iWICBitmap)
        {
            IWICFormatConverter converter = ImagingFactory.CreateFormatConverter();
            converter.Initialize(iWICBitmap, PixelFormat.Format32bppPBGRA, BitmapDitherType.None, null, 1f, BitmapPaletteType.FixedWebPalette);
            iWICBitmap = ImagingFactory.CreateBitmapFromSource(converter, BitmapCreateCacheOption.CacheOnLoad);

            ResetBitmap();
            ID2D1Bitmap iD2D1Bitmap = hwndRender.CreateBitmapFromWicBitmap(iWICBitmap, new BitmapProperties(
                new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied)));
            bitmapD2D1.Add(new ID2D1Bitmap1(iD2D1Bitmap.NativePointer));
            WICBitmaps.Add(iWICBitmap);
            DrawImage(true);
        }

        public void SetBitmap(in byte[] Data, int stride, int width, int height)
        {
            IntPtr DataPtr;
            unsafe
            {
                fixed (byte* ptr = Data)
                {
                    DataPtr = (IntPtr)ptr;
                }
            }

            SetBitmap(ImagingFactory.CreateBitmapFromMemory(width, height, PixelFormat.Format32bppPBGRA, stride, Data.Length, DataPtr));
        }

        public void SetBitmap(in IntPtr Data, int stride, int width, int height)
        {
            SetBitmap(ImagingFactory.CreateBitmapFromMemory(width, height, PixelFormat.Format32bppPBGRA, stride, stride * height, Data));
        }

        /*public List<ID2D1Bitmap1> GetBitmap()
        {
            List<ID2D1Bitmap1> RbitmapD2D1 = new List<ID2D1Bitmap1>();

            for (int i = 0; i < bitmapD2D1.Count; i++)
            {
                IWICBitmapLock WICBitmapLock = WICBitmaps[i].Lock(BitmapLockFlags.Read);

                int DataSize = WICBitmapLock.Size.Width * WICBitmapLock.Size.Height * 4;
                byte[] data = new byte[DataSize];

                Marshal.Copy(WICBitmapLock.Data.DataPointer, data, 0, DataSize);

                IntPtr DataPtr;
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        DataPtr = (IntPtr)ptr;
                    }
                }

                ID2D1Bitmap frame = hwndRender.CreateBitmap(new SizeI((int)bitmapD2D1[i].Size.Width, (int)bitmapD2D1[i].Size.Height), DataPtr, 
                    WICBitmapLock.Data.Pitch, new BitmapProperties(new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied)));

                RbitmapD2D1.Add(new ID2D1Bitmap1(frame.NativePointer));

                WICBitmapLock.Dispose();
            }

            return RbitmapD2D1;
        }*/

        public List<IWICBitmap> GetWICBitmap()
        {
            List<IWICBitmap> RWICBitmaps = new List<IWICBitmap>();

            for (int i = 0; i < WICBitmaps.Count; i++)
            {
                IWICBitmapLock WICBitmapLock = WICBitmaps[i].Lock(BitmapLockFlags.Read);

                int DataSize = WICBitmapLock.Size.Width * WICBitmapLock.Size.Height * 4;
                byte[] data = new byte[DataSize];
                int Stride = WICBitmaps[i].Size.Width * 4;

                Marshal.Copy(WICBitmapLock.Data.DataPointer, data, 0, DataSize);

                IntPtr DataPtr;
                unsafe
                {
                    fixed (byte* ptr = data)
                    {
                        DataPtr = (IntPtr)ptr;
                    }
                }

                IWICBitmap iWICBitmap = ImagingFactory.CreateBitmapFromMemory(WICBitmaps[i].Size.Width, WICBitmaps[i].Size.Height, 
                    PixelFormat.Format32bppPBGRA, Stride, DataSize, DataPtr);
                RWICBitmaps.Add(iWICBitmap);
            }

            return RWICBitmaps;
        }

        public void RotateImage()
        {
            for (int index = 0; index < WICBitmaps.Count; index++)
            {
                IWICBitmapFlipRotator iWICBitmapFlipRotator = ImagingFactory.CreateBitmapFlipRotator();
                iWICBitmapFlipRotator.Initialize(WICBitmaps[index], BitmapTransformOptions.Rotate90);
                WICBitmaps[index] = ImagingFactory.CreateBitmapFromSource(iWICBitmapFlipRotator, BitmapCreateCacheOption.CacheOnLoad);
                bitmapD2D1[index] = new ID2D1Bitmap1(hwndRender.CreateBitmapFromWicBitmap(WICBitmaps[index], new BitmapProperties(
                    new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied))).NativePointer);
            }

            //System.Numerics.Matrix3x2 Rotate = D2D1.D2D1MakeRotateMatrix(Rotation, new System.Numerics.Vector2(this.Width / 2, this.Height / 2));
            //hwndRender.Transform = Rotate;
            DrawImage(true);
        }

        public void ResetBitmap()
        {
            if (bitmapD2D1 != null)
            {
                for (int index = 0; index < bitmapD2D1.Count; index++)
                {
                    bitmapD2D1[index].Dispose();
                    WICBitmaps[index].Dispose();
                }

                bitmapD2D1.Clear();
                FrameDelay.Clear();
                WICBitmaps.Clear();
            }

            FrameCount = 0;
            Frame = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Factor == 1.0f)
            {
                DrawImage();
            }
            else
            {
                DrawImageScale(Factor, XZoom, YZoom);
            }

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            hwndRender.Resize(new SizeI(Width, Height));
            ResetScale();
            base.OnResize(e);
        }

        private void DrawImage(bool Clear = true)
        {
            if (bitmapD2D1 == null || bitmapD2D1.Count <= 0)
            {
                return;
            }

            bool isresize = false;

            if (ImageWidth > this.Width || bitmapD2D1[Frame].Size.Width > this.Width)
            {
                Vortice.Mathematics.Size resize = ResizeImageW(bitmapD2D1[Frame].Size.Width, bitmapD2D1[Frame].Size.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
                isresize = true;
            }
            else
            {
                ImageWidth = bitmapD2D1[Frame].Size.Width;
            }

            if (ImageHeight > this.Height || bitmapD2D1[Frame].Size.Height > this.Height && isresize == false)
            {
                Vortice.Mathematics.Size resize = ResizeImageH(bitmapD2D1[Frame].Size.Width, bitmapD2D1[Frame].Size.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
            }
            else if (isresize == false)
            {
                ImageHeight = (int)bitmapD2D1[Frame].Size.Height;
            }

            float x = this.Width / 2 - ImageWidth / 2;
            float y = this.Height / 2 - ImageHeight / 2;

            ImagePosition = new PointF(x, y);

            hwndRender.BeginDraw();

            if (Clear)
            {
                hwndRender.Clear(bgcolor);
            }

            hwndRender.DrawBitmap(bitmapD2D1[Frame], new Rect(x, y, ImageWidth, ImageHeight), 1.0f, Vortice.Direct2D1.BitmapInterpolationMode.Linear, null);
            hwndRender.EndDraw();
        }

        private void DrawImageScale(float factor, in int xPixelWidth, in int yPixelHeight, bool Clear = true)
        {
            if (CanZoom == false)
            {
                return;
            }

            XZoom = xPixelWidth;
            YZoom = yPixelHeight;

            ImageFactorW = (int)(ImageWidth * factor);
            ImageFactorH = (int)(ImageHeight * factor);

            int x;
            int CheckDelta;

            if (this.Width > ImageFactorW)
            {
                x = this.Width / 2 - ImageFactorW / 2;
            }
            else
            {
                x = (int)(xPixelWidth - (float)factor * (xPixelWidth - ImagePosition.X));

                if (x > 0)
                {
                    x = 0;
                }

                CheckDelta = this.Width - (x + ImageFactorW);

                if (CheckDelta > 0)
                {
                    x += CheckDelta;
                }
            }

            int y;

            if (this.Height > ImageFactorH)
            {
                y = this.Height / 2 - ImageFactorH / 2;
            }
            else
            {
                y = (int)(yPixelHeight - (float)factor * (yPixelHeight - ImagePosition.Y));

                if (y > 0)
                {
                    y = 0;
                }

                CheckDelta = this.Height - (y + ImageFactorH);

                if (CheckDelta > 0)
                {
                    y += CheckDelta;
                }
            }

            hwndRender.BeginDraw();

            if (Clear)
            {
                hwndRender.Clear(bgcolor);
            }

            hwndRender.DrawBitmap(bitmapD2D1[Frame], new Rect(x, y, ImageFactorW, ImageFactorH), 1.0f, Vortice.Direct2D1.BitmapInterpolationMode.Linear, null);
            hwndRender.EndDraw();

            Factor = factor;
        }

        public void ScaleImage(in float scale, int xPixelWidth, int yPixelHeight)
        {
            DrawImageScale(scale, in xPixelWidth, in yPixelHeight, true);
        }

        public void ResetScale()
        {
            Factor = 1.0f;
            DrawImage(true);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (Factor == 1.0f)
            {
                ScaleImage(2.5f, e.Location.X, e.Location.Y);
            }
            else
            {
                ScaleImage(1.0f, e.Location.X, e.Location.Y);
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                ScaleImage(Factor - 0.25f, e.Location.X, e.Location.Y);
            }
            else
            {
                ScaleImage(Factor + 0.25f, e.Location.X, e.Location.Y);
            }

            base.OnMouseWheel(e);
        }

        private Vortice.Mathematics.Size ResizeImageW(float imagewith, float imageheight)
        {
            float rw = imagewith;
            float rh = imageheight;

            SimplifiedFractions(ref rw, ref rh);

            imagewith = this.Width;
            imageheight = (int)(float)((float)(this.Width / rw) * rh);
            return new Vortice.Mathematics.Size(imagewith, imageheight);
        }

        private Vortice.Mathematics.Size ResizeImageH(float imagewith, float imageheight)
        {
            float rw = imagewith;
            float rh = imageheight;

            SimplifiedFractions(ref rw, ref rh);

            imageheight = this.Height;
            imagewith = (int)(float)((float)(this.Height / rh) * rw);
            return new Vortice.Mathematics.Size(imagewith, imageheight);
        }

        private void SimplifiedFractions(ref float num, ref float den)
        {
            int remNum, remDen, counter;

            if (num > den)
            {
                counter = (int)den;
            }
            else
            {
                counter = (int)num;
            }

            for (int i = 2; i <= counter; i++)
            {
                remNum = (int)num % i;
                if (remNum == 0)
                {
                    remDen = (int)den % i;
                    if (remDen == 0)
                    {
                        num = num / i;
                        den = den / i;
                        i--;
                    }
                }
            }
        }

        public Vortice.Mathematics.Size ImageSize
        {
            get
            {
                if (bitmapD2D1 == null || bitmapD2D1.Count == 0 || bitmapD2D1[0] == null)
                {
                    return Vortice.Mathematics.Size.Empty;
                }
                else
                {
                    return bitmapD2D1[0].Size;
                }
            }
        }

        public string PixelFormatString
        {
            get
            {
                if (bitmapD2D1 == null || bitmapD2D1.Count == 0 || bitmapD2D1[0] == null)
                {
                    return null;
                }
                else
                {
                    return bitmapD2D1[0].PixelFormat.Format.ToString();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {               
                factory7.Dispose();
                hwndRender.Dispose();
                ImagingFactory.Dispose();

                ResetBitmap();
            }

            base.Dispose(disposing);
        }
    }
}
