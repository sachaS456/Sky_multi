using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sky_multi
{
    internal sealed class RectangleResizer : Control
    {
        private Sky_framework.Rectangle ResizeButtonTopLeft = new Sky_framework.Rectangle();
        private Sky_framework.Rectangle ResizeButtonTop = new Sky_framework.Rectangle();
        private Sky_framework.Rectangle ResizeButtonTopRight = new Sky_framework.Rectangle();
        private Sky_framework.Rectangle ResizeButtonLeft = new Sky_framework.Rectangle();
        private Sky_framework.Rectangle ResizeButtonRight = new Sky_framework.Rectangle();
        private Sky_framework.Rectangle ResizeButtonBottomLeft = new Sky_framework.Rectangle();
        private Sky_framework.Rectangle ResizeButtonBottomRight = new Sky_framework.Rectangle();
        private Sky_framework.Rectangle ResizeButtonBottom = new Sky_framework.Rectangle();

        internal RectangleResizer()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            this.Size = new Size(100, 100);
            this.BackColor = Color.Transparent;

            const int SizeButton = 20;

            ResizeButtonTopLeft.BackColor = Color.White;
            ResizeButtonTopLeft.Size = new Size(SizeButton, SizeButton);
            ResizeButtonTopLeft.BorderRadius = SizeButton;
            ResizeButtonTopLeft.Border = false;
            ResizeButtonTopLeft.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            ResizeButtonTopLeft.Location = new Point(0, 0);
            ResizeButtonTopLeft.MouseMove += new MouseEventHandler(ResizeButtonTopLeft_MouseMove);
            this.Controls.Add(ResizeButtonTopLeft);

            ResizeButtonTop.BackColor = Color.White;
            ResizeButtonTop.Size = new Size(SizeButton, SizeButton);
            ResizeButtonTop.BorderRadius = SizeButton;
            ResizeButtonTop.Border = false;
            ResizeButtonTop.Anchor = AnchorStyles.Top;
            ResizeButtonTop.Location = new Point(50 - ResizeButtonTop.Width / 2, 0);
            ResizeButtonTop.MouseMove += new MouseEventHandler(ResizeButtonTop_MouseMove);
            this.Controls.Add(ResizeButtonTop);

            ResizeButtonTopRight.BackColor = Color.White;
            ResizeButtonTopRight.Size = new Size(SizeButton, SizeButton);
            ResizeButtonTopRight.BorderRadius = SizeButton;
            ResizeButtonTopRight.Border = false;
            ResizeButtonTopRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ResizeButtonTopRight.Location = new Point(100 - ResizeButtonTopRight.Width, 0);
            ResizeButtonTopRight.MouseMove += new MouseEventHandler(ResizeButtonTopRight_MouseMove);
            this.Controls.Add(ResizeButtonTopRight);

            ResizeButtonLeft.BackColor = Color.White;
            ResizeButtonLeft.Size = new Size(SizeButton, SizeButton);
            ResizeButtonLeft.BorderRadius = SizeButton;
            ResizeButtonLeft.Border = false;
            ResizeButtonLeft.Anchor = AnchorStyles.Left;
            ResizeButtonLeft.Location = new Point(0, 50 - ResizeButtonLeft.Height / 2);
            ResizeButtonLeft.MouseMove += new MouseEventHandler(ResizeButtonLeft_MouseMove);
            this.Controls.Add(ResizeButtonLeft);

            ResizeButtonRight.BackColor = Color.White;
            ResizeButtonRight.Size = new Size(SizeButton, SizeButton);
            ResizeButtonRight.BorderRadius = SizeButton;
            ResizeButtonRight.Border = false;
            ResizeButtonRight.Anchor = AnchorStyles.Right;
            ResizeButtonRight.Location = new Point(100 - ResizeButtonRight.Width, 50 - ResizeButtonRight.Height / 2);
            ResizeButtonRight.MouseMove += new MouseEventHandler(ResizeButtonRight_MouseMove);
            this.Controls.Add(ResizeButtonRight);

            ResizeButtonBottomLeft.BackColor = Color.White;
            ResizeButtonBottomLeft.Size = new Size(SizeButton, SizeButton);
            ResizeButtonBottomLeft.BorderRadius = SizeButton;
            ResizeButtonBottomLeft.Border = false;
            ResizeButtonBottomLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ResizeButtonBottomLeft.Location = new Point(0, 100 - ResizeButtonBottomLeft.Height);
            ResizeButtonBottomLeft.MouseMove += new MouseEventHandler(ResizeButtonBottomLeft_MouseMove);
            this.Controls.Add(ResizeButtonBottomLeft);

            ResizeButtonBottomRight.BackColor = Color.White;
            ResizeButtonBottomRight.Size = new Size(SizeButton, SizeButton);
            ResizeButtonBottomRight.BorderRadius = SizeButton;
            ResizeButtonBottomRight.Border = false;
            ResizeButtonBottomRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ResizeButtonBottomRight.Location = new Point(100 - ResizeButtonBottomRight.Width, 100 - ResizeButtonBottomRight.Height);
            ResizeButtonBottomRight.MouseMove += new MouseEventHandler(ResizeButtonBottomRight_MouseMove);
            this.Controls.Add(ResizeButtonBottomRight);

            ResizeButtonBottom.BackColor = Color.White;
            ResizeButtonBottom.Size = new Size(SizeButton, SizeButton);
            ResizeButtonBottom.BorderRadius = SizeButton;
            ResizeButtonBottom.Border = false;
            ResizeButtonBottom.Anchor = AnchorStyles.Bottom;
            ResizeButtonBottom.Location = new Point(50 - ResizeButtonBottom.Width / 2, 100 - ResizeButtonBottom.Height);
            ResizeButtonBottom.MouseMove += new MouseEventHandler(ResizeButtonBottom_MouseMove);
            this.Controls.Add(ResizeButtonBottom);
        }

        private void LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY)
        {
            LocationScreenX = this.Location.X;
            LocationScreenY = this.Location.Y;

            Control c = this.Parent;

            while (true)
            {
                try
                {
                    Form f = (Form)c;
                    LocationScreenX += f.Location.X;
                    LocationScreenY += f.Location.Y;
                    break;
                }
                catch
                {
                    LocationScreenX += c.Location.X;
                    LocationScreenY += c.Location.Y;
                    c = c.Parent;
                }
            }
        }

        private void ResizeButtonTopLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = this.Width;
                int h = this.Height;
                
                this.Width -= MousePosition.X - LocationScreenX;
                this.Height -= MousePosition.Y - LocationScreenY;

                this.Location = new Point(this.Location.X - (this.Width - w), this.Location.Y - (this.Height - h));
                this.Update();
            }
        }

        private void ResizeButtonTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int h = this.Height;

                this.Height -= MousePosition.Y - LocationScreenY;

                this.Location = new Point(this.Location.X, this.Location.Y - (this.Height - h));
                this.Update();
            }
        }

        private void ResizeButtonTopRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int h = this.Height;

                this.Width = MousePosition.X - LocationScreenX;
                this.Height -= MousePosition.Y - LocationScreenY;

                this.Location = new Point(this.Location.X, this.Location.Y - (this.Height - h));
                this.Update();
            }
        }

        private void ResizeButtonLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = this.Width;

                this.Width -= MousePosition.X - LocationScreenX;

                this.Location = new Point(this.Location.X - (this.Width - w), this.Location.Y);
                this.Update();
            }
        }

        private void ResizeButtonRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                this.Width = MousePosition.X - LocationScreenX;

                this.Update();
            }
        }

        private void ResizeButtonBottomLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = this.Width;

                this.Width -= MousePosition.X - LocationScreenX;
                this.Height = MousePosition.Y - LocationScreenY;

                this.Location = new Point(this.Location.X - (this.Width - w), this.Location.Y);
                this.Update();
            }
        }

        private void ResizeButtonBottomRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                this.Width = MousePosition.X - LocationScreenX;
                this.Height = MousePosition.Y - LocationScreenY;

                this.Update();
            }
        }

        private void ResizeButtonBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                this.Height = MousePosition.Y - LocationScreenY;

                this.Update();
            }
        }
    }
}
