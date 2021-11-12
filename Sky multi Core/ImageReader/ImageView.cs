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
            int height;
            int x;
            int y;

            if (Image.Width > this.Width)
            {
                width = this.Width;
                x = 0;
            }
            else
            {
                width = Image.Width;
                x = this.Width / 2 - Image.Width / 2;
            }

            if (Image.Height > this.Height)
            {
                height = this.Height;
                y = 0;
            }
            else
            {
                height = Image.Height;
                y = this.Height / 2 - Image.Height / 2;
            }

            g.Clear(this.BackColor);
            g.DrawImage(Image, x, y, width, height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawImage(e.Graphics);
            base.OnPaint(e);
        }
    }
}
