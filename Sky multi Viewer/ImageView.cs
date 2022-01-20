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

        private ushort Factor = 100;
        private int ImagePositionXFactor = 0;
        private int ImagePositionYFactor = 0;
        private int ImageFactorW = 0;
        private int ImageFactorH = 0;

        public ImageView()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            this.MouseWheel += new MouseEventHandler(This_MouseWheel);
            this.Resize += new EventHandler(This_Resize);
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

        private void This_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                ScaleImage((ushort)(Factor - 10), e.Location);
            }
            else
            {
                ScaleImage((ushort)(Factor + 10), e.Location);
            }
        }

        private void DrawImageScale(in Graphics g, ushort factor, in Point PointFactor)
        {
            if (CanZoom == false)
            {
                return;
            }

            g.SmoothingMode = SmoothingMode;
            g.PixelOffsetMode = PixelOffsetMode;
            g.CompositingQuality = CompositingQuality;

            g.Clear(base.BackColor);

            int DeltaImageFactorW = (int)((float)ImageWidth / 100 * factor) - ImageFactorW;
            int DeltaImageFactorH = (int)((float)ImageHeight / 100 * factor) - ImageFactorH;

            ImageFactorW += DeltaImageFactorW;
            ImageFactorH += DeltaImageFactorH;

            if (ImageFactorW <= this.Width || ImageFactorH <= this.Height)
            {
                ImagePositionXFactor = this.Width / 2 - ImageFactorW / 2;
                ImagePositionYFactor = this.Height / 2 - ImageFactorH / 2;
            }
            else
            {
                /*BLOC:
                 1; 2; 3;
                 4; 5; 6;
                 7; 8; 9;
                */
                switch (GetBlocControl(PointFactor))
                {
                    case 1: // Top Left
                        
                        break;

                    case 2: // Top Center
                        ImagePositionXFactor -= DeltaImageFactorW / 2;
                        break;

                    case 3: // Top Right
                        ImagePositionXFactor -= DeltaImageFactorW;
                        break;

                    case 4: // Middle Left
                        ImagePositionYFactor -= DeltaImageFactorH / 2;
                        break;

                    case 5: // Middle Center
                        ImagePositionXFactor -= DeltaImageFactorW / 2;
                        ImagePositionYFactor -= DeltaImageFactorH / 2;
                        break;

                    case 6: // Middle Right
                        ImagePositionXFactor -= DeltaImageFactorW;
                        ImagePositionYFactor -= DeltaImageFactorH / 2;
                        break;

                    case 7: // Bottom Left
                        ImagePositionYFactor -= DeltaImageFactorH;
                        break;

                    case 8: // Bottom Center
                        ImagePositionXFactor -= DeltaImageFactorW / 2;
                        ImagePositionYFactor -= DeltaImageFactorH;
                        break;

                    case 9: // Bottom Right
                        ImagePositionXFactor -= DeltaImageFactorW;
                        ImagePositionYFactor -= DeltaImageFactorH;
                        break;

                    default: // PointFactor is empty

                        break;
                }

                if (ImagePositionXFactor > 10)
                {
                    ImagePositionXFactor = 10;
                }

                if (ImagePositionYFactor > 10)
                {
                    ImagePositionYFactor = 10;
                }
            }

            g.DrawImage(Image, ImagePositionXFactor, ImagePositionYFactor, ImageFactorW, ImageFactorH);

            Factor = factor;
        }

        private sbyte GetBlocControl(Point point)
        {
            sbyte IdBloc = 0;
            for (sbyte y = 1; y <= 3; y++)
            {
                for (sbyte x = 1; x <= 3; x++)
                {
                    IdBloc++;
                    if (point.X < x*(this.Width / 3) && point.Y < y*(this.Height / 3))
                    {
                        return IdBloc;
                    }
                }
            }

            return -1;
        }

        public void ScaleImage(ushort scale)
        {
            ScaleImage(in scale, Point.Empty);
        }

        public void ScaleImage(in ushort scale, Point PointFactor)
        {
            DrawImageScale(this.CreateGraphics(), scale, in PointFactor);
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
            Factor = 100;
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
            if (Factor == 100)
            {
                DrawImage(e.Graphics);
            }
            else
            {
                DrawImageScale(e.Graphics, Factor, Point.Empty);
            }
            base.OnPaint(e);
        }
    }
}
