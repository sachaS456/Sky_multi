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

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sky_multi
{
    internal sealed partial class ZoneCapture : Form
    {
        private Bitmap Image = null;

        internal ZoneCapture()
        {
            InitializeComponent();
            AnimationShow();
        }

        private async void AnimationShow()
        {
            while (this.Opacity < 0.4D)
            {
                this.Opacity += 0.02D;
                await Task.Delay(10);
            }

            this.Opacity = 0.4D;
        }

        private void AnimationClose()
        {
            while (this.Opacity > 0D)
            {
                this.Opacity -= 0.02D;
                System.Threading.Thread.Sleep(10);
            }

            this.Opacity = 0D;
            this.Close();
        }

        private void ZoneCapture_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel1.Visible == true)
            {
                panel1.Size = new Size(e.X - panel1.Location.X, e.Y - panel1.Location.Y);
            }
        }

        private void ZoneCapture_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Location = new Point(e.X, e.Y);
            panel1.Visible = true;
        }

        internal Bitmap BitmapCapture
        {
            get
            {
                return Image;
            }
        }

        private void ZoneCapture_MouseUp(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            if (panel1.Width <= 0 || panel1.Height <= 0)
            {
                return;
            }
            AnimationClose();
            Image = CaptureScreen(ref panel1);
        }

        private Bitmap CaptureScreen(ref Panel rect)
        {
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            }

            return bitmap;
        }
    }
}
