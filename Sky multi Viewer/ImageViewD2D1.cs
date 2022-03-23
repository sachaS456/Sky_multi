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

namespace Sky_multi_Viewer
{
    public sealed class ImageViewD2D1 : Control
    {
        private readonly ID2D1Factory7 factory7;
        private readonly ID2D1HwndRenderTarget hwndRender;

        private Color4 bgcolor = new(0.1f, 0.1f, 0.1f, 1.0f);

        private ID2D1Bitmap1 bitmapD2D1;
        private IWICImagingFactory2 ImagingFactory = new IWICImagingFactory2();
        private SizeI PixelSize;

        public PointF ImagePosition { get; private set; } = PointF.Empty;
        public float ImageWidth { get; private set; } = 0;
        public float ImageHeight { get; private set; } = 0;

        public ImageViewD2D1()
        {
            factory7 = D2D1.D2D1CreateFactory<ID2D1Factory7>();

            HwndRenderTargetProperties properties = new HwndRenderTargetProperties();
            properties.Hwnd = this.Handle;
            PixelSize = new SizeI(Screen.FromHandle(this.Handle).Bounds.Width, Screen.FromHandle(this.Handle).Bounds.Height);
            properties.PixelSize = PixelSize;
            properties.PresentOptions = PresentOptions.Immediately;

            hwndRender = factory7.CreateHwndRenderTarget(new RenderTargetProperties(), properties);
            hwndRender.AntialiasMode = AntialiasMode.Aliased;
        }

        private ID2D1Bitmap1 BitmapD2D1FromBitmapGDI(in Bitmap bitmap)
        {
            IWICBitmap WICBitmap = ImagingFactory.CreateBitmapFromHBITMAP(bitmap.GetHbitmap(), (IntPtr)0, BitmapAlphaChannelOption.UsePremultipliedAlpha);            
            return new ID2D1Bitmap1(hwndRender.CreateBitmapFromWicBitmap(WICBitmap, new BitmapProperties()).NativePointer);
        }

        public void SetBitmap(Bitmap bitmap)
        {
            bitmapD2D1 = BitmapD2D1FromBitmapGDI(in bitmap);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            hwndRender.Resize(new SizeI(Width, Height));
            DrawImage();

            base.OnPaint(e);
        }

        private void DrawImage()
        {
            bool isresize = false;

            if (ImageWidth > this.Width || bitmapD2D1.Size.Width > this.Width)
            {
                Vortice.Mathematics.Size resize = ResizeImageW(bitmapD2D1.Size.Width, bitmapD2D1.Size.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
                isresize = true;
            }
            else
            {
                ImageWidth = bitmapD2D1.Size.Width;
            }

            if (ImageHeight > this.Height || bitmapD2D1.Size.Height > this.Height && isresize == false)
            {
                Vortice.Mathematics.Size resize = ResizeImageH(bitmapD2D1.Size.Width, bitmapD2D1.Size.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
            }
            else if (isresize == false)
            {
                ImageHeight = (int)bitmapD2D1.Size.Height;
            }

            float x = this.Width / 2 - ImageWidth / 2;
            float y = this.Height / 2 - ImageHeight / 2;

            ImagePosition = new PointF(x, y);

            hwndRender.BeginDraw();
            hwndRender.Clear(bgcolor);
            hwndRender.DrawBitmap(bitmapD2D1, new Rect(x, y, ImageWidth, ImageHeight), 1.0f, Vortice.Direct2D1.BitmapInterpolationMode.Linear, null);
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

                if (bitmapD2D1 != null)
                {
                    bitmapD2D1.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
