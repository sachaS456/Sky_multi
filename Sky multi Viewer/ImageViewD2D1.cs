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
using Vortice.DirectInput;
using SharpGen.Runtime;
using Sky_multi_Core.ImageReader;

namespace Sky_multi_Viewer
{
    public sealed class ImageViewD2D1 : Control
    {
        private readonly ID2D1Factory7 factory7;
        private readonly ID2D1HwndRenderTarget hwndRender;

        private readonly Color4 bgcolor = new(0.1f, 0.1f, 0.1f, 1.0f);

        private readonly List<ID2D1Bitmap1> bitmapD2D1;
        private readonly List<ushort> FrameDelay;
        private IWICImagingFactory2 ImagingFactory = new IWICImagingFactory2();
        private SizeI PixelSize;

        public PointF ImagePosition { get; private set; } = PointF.Empty;
        public float ImageWidth { get; private set; } = 0;
        public float ImageHeight { get; private set; } = 0;
        public int Frame { get; private set; } = 0;
        public int FrameCount { get; private set; } = 0;

        public ImageViewD2D1()
        {
            factory7 = D2D1.D2D1CreateFactory<ID2D1Factory7>();
            bitmapD2D1 = new List<ID2D1Bitmap1>();
            FrameDelay = new List<ushort>();

            HwndRenderTargetProperties properties = new HwndRenderTargetProperties();
            properties.Hwnd = this.Handle;
            PixelSize = new SizeI(Screen.FromHandle(this.Handle).Bounds.Width, Screen.FromHandle(this.Handle).Bounds.Height);
            properties.PixelSize = PixelSize;
            properties.PresentOptions = PresentOptions.Immediately;

            hwndRender = factory7.CreateHwndRenderTarget(new RenderTargetProperties(), properties);
            hwndRender.AntialiasMode = AntialiasMode.Aliased;
            StartShowFrames();
        }

        private ID2D1Bitmap1 BitmapD2D1FromBitmapGDI(in Bitmap bitmap)
        {
            IWICBitmap WICBitmap = ImagingFactory.CreateBitmapFromHBITMAP(bitmap.GetHbitmap(), (IntPtr)0, BitmapAlphaChannelOption.UsePremultipliedAlpha);
            IWICFormatConverter converter = ImagingFactory.CreateFormatConverter();
            converter.Initialize(WICBitmap, PixelFormat.Format32bppPBGRA, BitmapDitherType.None, null, 1f, BitmapPaletteType.FixedWebPalette);
            return new ID2D1Bitmap1(hwndRender.CreateBitmapFromWicBitmap(converter, new BitmapProperties()).NativePointer);
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
            IWICBitmapDecoder bitmapDecoder = ImagingFactory.CreateDecoderFromFileName(Path, System.IO.FileAccess.Read, DecodeOptions.CacheOnLoad);//ImagingFactory.CreateDecoder(ContainerFormatGuids.Gif, null);
            IWICFormatConverter converter;

            ResetBitmap();
            FrameCount = bitmapDecoder.FrameCount;

            for (int i = 0; i < bitmapDecoder.FrameCount; i++)
            {
                converter = ImagingFactory.CreateFormatConverter();
                converter.Initialize(bitmapDecoder.GetFrame(i), PixelFormat.Format32bppPBGRA, BitmapDitherType.None, null, 1f, BitmapPaletteType.FixedWebPalette);

                ushort value = (ushort)(bitmapDecoder.GetFrame(i).MetadataQueryReader.GetMetadataByName(@"/grctlext/Delay").Value);
                value *= 10;

                /*if (value < 90)
                {
                    value = 90;
                }*/

                ID2D1Bitmap image = hwndRender.CreateBitmapFromWicBitmap(converter, new BitmapProperties());
                bitmapD2D1.Add(new ID2D1Bitmap1(image.NativePointer));
                FrameDelay.Add(value);
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
                    return;
                }
                catch
                {
                    try
                    {
                        ResetBitmap();
                        bitmapD2D1.Add(BitmapD2D1FromBitmapGDI(WebPDecoder.DecodeWebp(Path)));
                        return;
                    }
                    catch
                    {
                        try
                        {
                            ResetBitmap();
                            bitmapD2D1.Add(BitmapD2D1FromBitmapGDI(BitmapHeifCoverter.OpenHeifFromPathToBitmap(Path)));
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
        }

        private void ResetBitmap()
        {
            if (bitmapD2D1 != null)
            {
                for (int index = 0; index < bitmapD2D1.Count; index++)
                {
                    bitmapD2D1[index].Dispose();
                }

                bitmapD2D1.Clear();
                FrameDelay.Clear();
            }

            FrameCount = 0;
            Frame = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            hwndRender.Resize(new SizeI(Width, Height));
            DrawImage();

            base.OnPaint(e);
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
