﻿/*--------------------------------------------------------------------------------------------------------------------
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
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Sky_multi_Core.ImageReader;
using Vortice.Direct2D1;
using Vortice.WIC;

namespace Sky_multi_Viewer
{
    public class ImageView: Control
    {
        public SmoothingMode SmoothingMode = SmoothingMode.HighQuality;
        public PixelOffsetMode PixelOffsetMode = PixelOffsetMode.HighQuality;
        public CompositingQuality CompositingQuality = CompositingQuality.HighQuality;
        public Image Image { get; private set; } = null;
        public bool CanAnimated { get; private set; } = false;

        private float Factor = 1.0f;
        private int ImageFactorW = 0;
        private int ImageFactorH = 0;
        private ImageViewD2D1 ImageViewD2D1;
        private bool UseD2D1_ = false;

        public ImageView()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            this.MouseWheel += new MouseEventHandler(This_MouseWheel);
            this.Resize += new EventHandler(This_Resize);
            this.MouseDoubleClick += new MouseEventHandler(This_MouseDoubleClick);
        }

        public Bitmap GetBitmapGDIFromBitmapWIC(IWICBitmap iWICBitmap)
        {
            IWICBitmapLock iWICBitmapLock = iWICBitmap.Lock(BitmapLockFlags.Read);
            IntPtr DataPtr = iWICBitmapLock.Data.DataPointer;

            return new Bitmap(iWICBitmapLock.Size.Width, iWICBitmapLock.Size.Height, iWICBitmapLock.Stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, DataPtr);
        }

        public bool UseD2D1
        {
            set
            {
                if (UseD2D1_ == value)
                {
                    return;
                }

                UseD2D1_ = value;

                if (UseD2D1_ == true)
                {
                    ImageViewD2D1 = new ImageViewD2D1();
                    ImageViewD2D1.Location = new Point(0, 0);
                    ImageViewD2D1.Size = this.Size;
                    ImageViewD2D1.BackColor = this.BackColor;
                    ImageViewD2D1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                    ImageViewD2D1.MouseDown += new MouseEventHandler(ImageViewD2D1_MouseDown);
                    ImageViewD2D1.MouseUp += new MouseEventHandler(ImageViewD2D1_MouseUp);
                    ImageViewD2D1.MouseClick += new MouseEventHandler(ImageViewD2D1_MouseClick);
                    ImageViewD2D1.MouseMove += new MouseEventHandler(ImageViewD2D1_MouseMove);
                    ImageViewD2D1.BringToFront();
                    this.Controls.Add(ImageViewD2D1);

                    if (Image != null)
                    {
                        ImageViewD2D1.SetBitmap((Bitmap)Image);
                        Image.Dispose();
                        Image = null;
                    }
                }
                else
                {
                    if (ImageViewD2D1 != null)
                    {
                        SetImage(GetBitmapGDIFromBitmapWIC(ImageViewD2D1.GetWICBitmap()[0]));

                        ImageViewD2D1.ResetBitmap();
                        ImageViewD2D1.Visible = false;
                        this.Controls.Remove(ImageViewD2D1);
                        ImageViewD2D1.Dispose();
                        ImageViewD2D1 = null;
                    }
                }
            }
            get
            {
                return UseD2D1_;
            }
        }

        private Point ImagePosition_ = Point.Empty;

        public Point ImagePosition
        {
            get
            {
                if (UseD2D1_)
                {
                    return new Point((int)ImageViewD2D1.ImagePosition.X, (int)ImageViewD2D1.ImagePosition.Y);
                }
                else
                {
                    return ImagePosition_;
                }
            }

            private set
            {
                ImagePosition_ = value;
            }
        }

        private int ImageWidth_ = 0;

        public int ImageWidth
        {
            get
            {
                if (UseD2D1_)
                {
                    return (int)ImageViewD2D1.ImageWidth;
                }
                else
                {
                    return ImageWidth_;
                }
            }
        }
        private int ImageHeight_ = 0;

        public int ImageHeight
        {
            get
            {
                if (UseD2D1_)
                {
                    return (int)ImageViewD2D1.ImageHeight;
                }
                else
                {
                    return ImageHeight_;
                }
            }
        }
        private bool CanZoom_ = true;

        public bool CanZoom
        {
            get
            {
                if (UseD2D1_)
                {
                    return ImageViewD2D1.CanZoom;
                }
                else
                {
                    return CanZoom_;
                }
            }

            set
            {
                if (UseD2D1_)
                {
                    ImageViewD2D1.CanZoom = value;
                }
                else
                {
                    CanZoom_ = value;
                }
            }
        }

        private void ImageViewD2D1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void ImageViewD2D1_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void ImageViewD2D1_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        private void ImageViewD2D1_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        /*public List<ID2D1Bitmap1> GetImageWithHardwareAcceleration()
        {
            if (UseD2D1_ == false)
            {
                throw new Exception("You must set 'UseD2D1_' on True!");
            }

            return ImageViewD2D1.GetBitmap();
        }*/

        public List<IWICBitmap> GetImageWIC()
        {
            if (UseD2D1_ == false)
            {
                throw new Exception("You must set 'UseD2D1_' on True!");
            }

            return ImageViewD2D1.GetWICBitmap();
        }

        public Image GetImageWithGDI()
        {
            if (UseD2D1_ == true)
            {
                throw new Exception("You must set 'UseD2D1_' on False!");
            }

            return Image;
        }

        public void SetImage(in byte[] Data, int stride, int width, int height)
        {
            if (UseD2D1_)
            {
                ImageViewD2D1.SetBitmap(in Data, stride, width, height);
            }
            else
            {
                IntPtr dataPtr;
                unsafe
                {
                    fixed (byte* dataP = Data)
                    {
                        dataPtr = (IntPtr)dataP;
                    }
                }

                Bitmap bmp = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, dataPtr);
                SetImage(bmp);
            }
        }

        public void SetImage(in IWICBitmap iWICBitmap)
        {
            if (UseD2D1_ == false)
            {
                throw new Exception("You must set 'UseD2D1_' on True!");
            }

            ImageViewD2D1.SetBitmap(iWICBitmap);
        }

        public void SetImage(in Image image)
        {
            if (UseD2D1_ == true)
            {
                throw new Exception("You must set 'UseD2D1_' on False!");
            }

            this.SuspendLayout();
            if (CanAnimated)
            {
                ImageAnimator.StopAnimate(Image, new EventHandler(UpdateFrame));
            }

            if (Image != null)
            {
                Image.Dispose();
            }

            if (image == null)
            {
                Image = null;
            }
            else
            {
                Image = new Bitmap(image);
                ImageWidth_ = Image.Width;
                ImageHeight_ = Image.Height;
                bool CanAnimated = ImageAnimator.CanAnimate(Image);

                if (CanAnimated)
                {
                    ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                }
                this.CanAnimated = CanAnimated;


                if (UseD2D1)
                {
                    ImageViewD2D1.SetBitmap((Bitmap)Image);
                }
            }

            this.ResumeLayout(false);
            this.Refresh();
        }

        public void RemoveImage()
        {
            this.SuspendLayout();

            if (CanAnimated)
            {
                ImageAnimator.StopAnimate(Image, new EventHandler(UpdateFrame));
            }

            Image.Dispose();
            Image = null;
            ImageWidth_ = 0;
            ImageHeight_ = 0;
            CanAnimated = false;
            this.ResumeLayout(false);
            this.Refresh();
        }

        public void DecodeImageFile(in string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException();
            }

            if (CanAnimated)
            {
                ImageAnimator.StopAnimate(Image, new EventHandler(UpdateFrame));
            }

            if (UseD2D1)
            {
                ImageViewD2D1.DecodeImageFromFile(in FilePath);
                return;
            }

            try
            {
                Image = Bitmap.FromFile(FilePath);
                ImageWidth_ = Image.Width;
                ImageHeight_ = Image.Height;
                bool CanAnimated = ImageAnimator.CanAnimate(Image);

                if (CanAnimated)
                {
                    ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                }
                this.CanAnimated = CanAnimated;

                this.Refresh();
                return;
            }
            catch
            {
                try 
                {
                    Image = RawDecoder.RawToBitmap(FilePath);
                    ImageWidth_ = Image.Width;
                    ImageHeight_ = Image.Height;
                    bool CanAnimated = ImageAnimator.CanAnimate(Image);

                    if (CanAnimated)
                    {
                        ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                    }
                    this.CanAnimated = CanAnimated;

                    this.Refresh();
                    return;
                }
                catch
                {
                    try
                    {
                        Image = WebPDecoder.DecodeWebp(FilePath);
                        ImageWidth_ = Image.Width;
                        ImageHeight_ = Image.Height;
                        bool CanAnimated = ImageAnimator.CanAnimate(Image);

                        if (CanAnimated)
                        {
                            ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                        }
                        this.CanAnimated = CanAnimated;

                        this.Refresh();
                        return;
                    }
                    catch
                    {
                        try
                        {
                            Image = BitmapHeifConverter.OpenHeifFromPathToBitmap(FilePath);
                            ImageWidth_ = Image.Width;
                            ImageHeight_ = Image.Height;
                            bool CanAnimated = ImageAnimator.CanAnimate(Image);

                            if (CanAnimated)
                            {
                                ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                            }
                            this.CanAnimated = CanAnimated;

                            this.Refresh();
                        }
                        catch
                        {
                            throw new Exception("this is not a image or image not supported!");
                        }
                    }
                }
            }
        }

        public ImageData ImageDataD2D1
        {
            get
            {
                return new ImageData((int)ImageViewD2D1.ImageSize.Width, (int)ImageViewD2D1.ImageSize.Height, ImageViewD2D1.PixelFormatString);
            }
        }

        public void DisposeImage()
        {
            if (UseD2D1)
            {
                ImageViewD2D1.ResetBitmap();
            }
            else
            {
                if (Image != null)
                {
                    Image.Dispose();
                    Image = null;
                }
            }
        }

        public void EncodeImage(string DestPath, string Format)
        {
            if (UseD2D1_)
            {
                ImageViewD2D1.EncodeBitmap(DestPath, Format);
            }
            else
            {
                switch (Format)
                {
                    case ".png":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Png);
                        break;

                    case ".jpg":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case ".jpeg":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case ".ico":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Icon);
                        break;

                    case ".gif":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Gif);
                        break;

                    case ".tiff":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;

                    case ".tif":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;

                    case ".bmp":
                        Image.Save(DestPath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case ".webp":
                        WebPEncoder.EncodeWebp((Bitmap)Image, DestPath);
                        break;

                    case ".heif":
                        BitmapHeifConverter.EncodeHeif((Bitmap)Image, DestPath);
                        break;

                    case ".heic":
                        BitmapHeifConverter.EncodeHeif((Bitmap)Image, DestPath);
                        break;

                    case ".avif":
                        BitmapHeifConverter.EncodeAvif((Bitmap)Image, DestPath);
                        break;

                    default:
                        throw new Exception("This format for the conversion is not supported!");
                }
            }
        }

        private void This_MouseWheel(object sender, MouseEventArgs e)
        {
            if (UseD2D1_ == false)
            {
                if (e.Delta < 0)
                {
                    ScaleImage(Factor - 0.25f, e.Location.X, e.Location.Y);
                }
                else
                {
                    ScaleImage(Factor + 0.25f, e.Location.X, e.Location.Y);
                }
            }
        }

        private void This_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (UseD2D1_ == false)
            {
                if (Factor == 1.0f)
                {
                    ScaleImage(2.5f, e.Location.X, e.Location.Y);
                }
                else
                {
                    ScaleImage(1.0f, e.Location.X, e.Location.Y);
                }
            }
        }

        private void DrawImageScale(in Graphics g, float factor, in int xPixelWidth, in int yPixelHeight)
        {
            if (CanZoom == false)
            {
                return;
            }

            g.SmoothingMode = SmoothingMode;
            g.PixelOffsetMode = PixelOffsetMode;
            g.CompositingQuality = CompositingQuality;

            g.Clear(base.BackColor);

            ImageFactorW = (int)(ImageWidth_ * factor);
            ImageFactorH = (int)(ImageHeight_ * factor);

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

            //ImagePosition = new Point(x, y);

            if (CanAnimated == true)
            {
                ImageAnimator.UpdateFrames();
            }

            g.DrawImage(Image, x, y, ImageFactorW, ImageFactorH);

            Factor = factor;
        }

        /*public void ScaleImage(float scale)
        {
            ScaleImage(in scale, -1, -1);
        }*/

        public void ScaleImage(in float scale, int xPixelWidth, int yPixelHeight)
        {
            if (UseD2D1_ == false)
            {
                DrawImageScale(this.CreateGraphics(), scale, in xPixelWidth, in yPixelHeight);
            }
            else
            {
                ImageViewD2D1.ScaleImage(in scale, xPixelWidth, yPixelHeight);
            }
        }

        public void RotateImage()
        {
            if (UseD2D1 == false)
            {
                Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Refresh();
            }
            else
            {
                ImageViewD2D1.RotateImage();
            }
        }

        public void ResetScale()
        {
            Factor = 1.0f;

            if (UseD2D1_ == false)
            {
                this.DrawImage(this.CreateGraphics());
            }
            else
            {
                ImageViewD2D1.ResetScale();
            }
        }

        public Bitmap GetBitmapResized()
        {
            return new Bitmap(Image, ImageWidth_, ImageHeight_);
        }

        public Bitmap GetBitmap()
        {
            return new Bitmap(Image);
        }

        private void This_Resize(object sender, EventArgs e)
        {
            Factor = 1.0f;
            ImageFactorW = 0;
            ImageFactorH = 0;
        }

        private void DrawImage(in Graphics g)
        {
            if (this == null || this.IsDisposed || this.Disposing || g == null || Image == null)
            {
                return;
            }

            g.SmoothingMode = SmoothingMode;
            g.PixelOffsetMode = PixelOffsetMode;
            g.CompositingQuality = CompositingQuality;

            bool isresize = false;

            if (ImageWidth_ > this.Width || Image.Width > this.Width)
            {
                Size resize = ResizeImageW(Image.Width, Image.Height);
                ImageWidth_ = resize.Width;
                ImageHeight_ = resize.Height;
                isresize = true;
            }
            else
            {
                ImageWidth_ = Image.Width;
            }

            if (ImageHeight_ > this.Height || Image.Height > this.Height && isresize == false)
            {
                Size resize = ResizeImageH(Image.Width, Image.Height);
                ImageWidth_ = resize.Width;
                ImageHeight_ = resize.Height;
            }
            else if (isresize == false)
            {
                ImageHeight_ = Image.Height;
            }

            int x = this.Width / 2 - ImageWidth_ / 2;
            int y = this.Height / 2 - ImageHeight_ / 2;

            ImagePosition = new Point(x, y);

            if (CanAnimated == true)
            {
                ImageAnimator.UpdateFrames();
            }

            g.Clear(this.BackColor);
            g.DrawImage(Image, x, y, ImageWidth_, ImageHeight_);
        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            this.Invalidate();
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

        private Size ResizeImageW(int imagewith, int imageheight)
        {
            float rw = imagewith;
            float rh = imageheight;

            SimplifiedFractions(ref rw, ref rh);

            imagewith = this.Width;
            imageheight = (int)(float)((float)(this.Width / rw) * rh);
            return new Size(imagewith, imageheight);
        }

        private Size ResizeImageH(int imagewith, int imageheight)
        {
            float rw = imagewith;
            float rh = imageheight;

            SimplifiedFractions(ref rw, ref rh);

            imageheight = this.Height;
            imagewith = (int)(float)((float)(this.Height / rh) * rw);
            return new Size(imagewith, imageheight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (UseD2D1 == false)
            {
                if (Factor == 1.0f)
                {
                    DrawImage(e.Graphics);
                }
                else
                {
                    DrawImageScale(e.Graphics, Factor, this.Width / 2 - ImageFactorW / 2, this.Height / 2 - ImageFactorH / 2);
                }
            }

            base.OnPaint(e);
        }
    }
}
