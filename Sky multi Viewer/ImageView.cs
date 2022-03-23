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
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Sky_multi_Core.ImageReader;

namespace Sky_multi_Viewer
{
    public class ImageView: Control
    {
        public SmoothingMode SmoothingMode = SmoothingMode.HighQuality;
        public PixelOffsetMode PixelOffsetMode = PixelOffsetMode.HighQuality;
        public CompositingQuality CompositingQuality = CompositingQuality.HighQuality;
        public Image Image { get; private set; } = null;
        public Point ImagePosition { get; private set; } = Point.Empty;
        public int ImageWidth { get; private set; } = 0;
        public int ImageHeight { get; private set; } = 0;
        public bool CanZoom { get; set; } = true;
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
                    ImageViewD2D1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                    ImageViewD2D1.BringToFront();
                    this.Controls.Add(ImageViewD2D1);

                    if (Image != null)
                    {
                        ImageViewD2D1.SetBitmap((Bitmap)Image);
                    }
                }
                else
                {
                    this.Controls.Remove(ImageViewD2D1);
                    ImageViewD2D1.Dispose();
                    ImageViewD2D1 = null;
                }
            }
            get
            {
                return UseD2D1_;
            }
        }

        public void SetImage(in Image image)
        {
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
                ImageWidth = Image.Width;
                ImageHeight = Image.Height;
                CanAnimated = ImageAnimator.CanAnimate(Image);
                if (CanAnimated)
                {
                    ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                }

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
            ImageWidth = 0;
            ImageHeight = 0;
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

            try
            {
                Image = Bitmap.FromFile(FilePath);
                ImageWidth = Image.Width;
                ImageHeight = Image.Height;
                CanAnimated = ImageAnimator.CanAnimate(Image);
                if (CanAnimated)
                {
                    ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                }
                if (UseD2D1)
                {
                    ImageViewD2D1.SetBitmap((Bitmap)Image);
                }
                this.Refresh();
                return;
            }
            catch
            {
                try 
                {
                    Image = RawDecoder.RawToBitmap(FilePath);
                    ImageWidth = Image.Width;
                    ImageHeight = Image.Height;
                    CanAnimated = ImageAnimator.CanAnimate(Image);
                    if (CanAnimated)
                    {
                        ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                    }
                    if (UseD2D1)
                    {
                        ImageViewD2D1.SetBitmap((Bitmap)Image);
                    }
                    this.Refresh();
                    return;
                }
                catch
                {
                    try
                    {
                        Image = WebPDecoder.DecodeWebp(FilePath);
                        ImageWidth = Image.Width;
                        ImageHeight = Image.Height;
                        CanAnimated = ImageAnimator.CanAnimate(Image);
                        if (CanAnimated)
                        {
                            ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                        }
                        if (UseD2D1)
                        {
                            ImageViewD2D1.SetBitmap((Bitmap)Image);
                        }
                        this.Refresh();
                        return;
                    }
                    catch
                    {
                        try
                        {
                            Image = BitmapHeifCoverter.OpenHeifFromPathToBitmap(FilePath);
                            ImageWidth = Image.Width;
                            ImageHeight = Image.Height;
                            CanAnimated = ImageAnimator.CanAnimate(Image);
                            if (CanAnimated)
                            {
                                ImageAnimator.Animate(Image, new EventHandler(UpdateFrame));
                            }
                            if (UseD2D1)
                            {
                                ImageViewD2D1.SetBitmap((Bitmap)Image);
                            }
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

        private void This_MouseWheel(object sender, MouseEventArgs e)
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

        private void This_MouseDoubleClick(object sender, MouseEventArgs e)
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
            DrawImageScale(this.CreateGraphics(), scale, in xPixelWidth, in yPixelHeight);
        }

        public void ResetScale()
        {
            Factor = 1.0f;
            this.DrawImage(this.CreateGraphics());
        }

        public Bitmap GetBitmapResized()
        {
            return new Bitmap(Image, ImageWidth, ImageHeight);
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

            if (ImageWidth > this.Width || Image.Width > this.Width)
            {
                Size resize = ResizeImageW(Image.Width, Image.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
                isresize = true;
            }
            else
            {
                ImageWidth = Image.Width;
            }

            if (ImageHeight > this.Height || Image.Height > this.Height && isresize == false)
            {
                Size resize = ResizeImageH(Image.Width, Image.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
            }
            else if (isresize == false)
            {
                ImageHeight = Image.Height;
            }

            int x = this.Width / 2 - ImageWidth / 2;
            int y = this.Height / 2 - ImageHeight / 2;

            ImagePosition = new Point(x, y);

            if (CanAnimated == true)
            {
                ImageAnimator.UpdateFrames();
            }

            g.Clear(this.BackColor);
            g.DrawImage(Image, x, y, ImageWidth, ImageHeight);
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
