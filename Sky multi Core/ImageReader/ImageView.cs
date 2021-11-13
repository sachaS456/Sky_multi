using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Sky_multi_Core.ImageReader
{
    public class ImageView: Control
    {
        public SmoothingMode SmoothingMode = SmoothingMode.HighQuality;
        public PixelOffsetMode PixelOffsetMode = PixelOffsetMode.HighQuality;
        public CompositingQuality CompositingQuality = CompositingQuality.HighQuality;
        public Image Image = null;

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

        public void DecodeImageFile(ref string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException();
            }

            try
            {
                Image = Bitmap.FromFile(FilePath);
                this.Refresh();
                return;
            }
            catch
            {
                try 
                {
                    Image = RawDecoder.RawToBitmap(FilePath);
                    this.Refresh();
                    return;
                }
                catch
                {
                    try
                    {
                        Image = WebPDecoder.DecodeWebp(FilePath);
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

        private void DrawImage(Graphics g)
        {
            if (this == null || this.IsDisposed || this.Disposing || g == null || Image == null)
            {
                return;
            }

            g.SmoothingMode = SmoothingMode;
            g.PixelOffsetMode = PixelOffsetMode;
            g.CompositingQuality = CompositingQuality;

            int width;
            int height = 0;
            int x;
            int y = 0;
            bool SizeSet = false;

            if (Image.Width > this.Width)
            {
                SizeSet = true;
                Size resize = ResizeImageW(Image.Width, Image.Height);
                width = resize.Width;
                height = resize.Height;
                x = this.Width / 2 - width / 2;
                y = this.Height / 2 - height / 2;
            }
            else
            {
                width = Image.Width;
                x = this.Width / 2 - Image.Width / 2;
            }

            if (SizeSet == false)
            {
                if (Image.Height > this.Height)
                {
                    Size resize = ResizeImageH(Image.Width, Image.Height);
                    width = resize.Width;
                    height = resize.Height;
                    x = this.Width / 2 - width / 2;
                    y = this.Height / 2 - height / 2;
                }
                else
                {
                    height = Image.Height;
                    y = this.Height / 2 - Image.Height / 2;
                }
            }

            g.Clear(this.BackColor);
            g.DrawImage(Image, x, y, width, height);
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
            imageheight = (this.Width / (int)rw) * (int)rh;
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
