﻿/*--------------------------------------------------------------------------------------------------------------------
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

        public ImageView()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        public void DecodeImageFile(string FilePath)
        {
            DecodeImageFile(ref FilePath);
        }

        public void SetImage(Image image)
        {
            SetImage(ref image);
        }

        public void SetImage(ref Image image)
        {
            this.SuspendLayout();
            if (Image != null)
            {
                Image.Dispose();
            }
            Image = image;
            ImageWidth = Image.Width;
            ImageHeight = Image.Height;
            this.ResumeLayout(false);
            this.Refresh();
        }

        public void RemoveImage()
        {
            this.SuspendLayout();
            Image.Dispose();
            Image = null;
            ImageWidth = 0;
            ImageHeight = 0;
            this.ResumeLayout(false);
            this.Refresh();
        }

        public void DecodeImageFile(ref string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException();
            }

            try
            {
                Image = Bitmap.FromFile(FilePath);
                ImageWidth = Image.Width;
                ImageHeight = Image.Height;
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
                        this.Refresh();
                        return;
                    }
                    catch
                    {
                        throw new Exception("this is not a image");
                    }
                }
            }
        }

        public Bitmap GetBitmapResized()
        {
            return new Bitmap(Image, ImageWidth, ImageHeight);
        }

        private void DrawImage(Graphics g)
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

            g.Clear(this.BackColor);
            g.DrawImage(Image, x, y, ImageWidth, ImageHeight);
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
            DrawImage(e.Graphics);
            base.OnPaint(e);
        }
    }
}
