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

namespace Sky_multi
{
    internal sealed class RectangleResizer : Control
    {
        private Sky_UI.Rectangle ResizeButtonTopLeft = new Sky_UI.Rectangle();
        private Sky_UI.Rectangle ResizeButtonTop = new Sky_UI.Rectangle();
        private Sky_UI.Rectangle ResizeButtonTopRight = new Sky_UI.Rectangle();
        private Sky_UI.Rectangle ResizeButtonLeft = new Sky_UI.Rectangle();
        private Sky_UI.Rectangle ResizeButtonRight = new Sky_UI.Rectangle();
        private Sky_UI.Rectangle ResizeButtonBottomLeft = new Sky_UI.Rectangle();
        private Sky_UI.Rectangle ResizeButtonBottomRight = new Sky_UI.Rectangle();
        private Sky_UI.Rectangle ResizeButtonBottom = new Sky_UI.Rectangle();

        internal Rectangle RectangleMax { get; set; } = Rectangle.Empty;

        internal RectangleResizer()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            this.Size = new Size(100, 100);
            this.BackColor = Color.Transparent;

            const int SizeButton = 15;

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

                int w = this.Width - (MousePosition.X - LocationScreenX);

                if (w <= 0)
                {
                    w = 1;
                }

                int h = this.Height - (MousePosition.Y - LocationScreenY);

                if (h <= 0)
                {
                    h = 1;
                }

                int x = this.Location.X - (w - this.Width);
                int y = this.Location.Y - (h - this.Height);

                if (RectangleMax != Rectangle.Empty)
                {
                    if (x < RectangleMax.X)
                    {
                        w -= RectangleMax.X - x;
                        x = RectangleMax.X;
                    }

                    if (y < RectangleMax.Y)
                    {
                        h -= RectangleMax.Y - y;
                        y = RectangleMax.Y;
                    }
                }

                this.Width = w;
                this.Height = h;
                this.Location = new Point(x, y);
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        private void ResizeButtonTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int h = this.Height - (MousePosition.Y - LocationScreenY);

                if (h <= 0)
                {
                    h = 1;
                }

                int y = this.Location.Y - (h - this.Height);

                if (RectangleMax != Rectangle.Empty)
                {
                    if (y < RectangleMax.Y)
                    {
                        h -= RectangleMax.Y - y;
                        y = RectangleMax.Y;
                    }
                }

                this.Height = h;
                this.Location = new Point(this.Location.X, y);
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        private void ResizeButtonTopRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = MousePosition.X - LocationScreenX;

                if (w <= 0)
                {
                    w = 1;
                }

                int h = this.Height - (MousePosition.Y - LocationScreenY);

                if (h <= 0)
                {
                    h = 1;
                }

                int y = this.Location.Y - (h - this.Height);

                if (RectangleMax != Rectangle.Empty)
                {
                    if (w + (this.Location.X - RectangleMax.X) > RectangleMax.Width)
                    {
                        w = RectangleMax.Width - (this.Location.X - RectangleMax.X);
                    }

                    if (y < RectangleMax.Y)
                    {
                        h -= RectangleMax.Y - y;
                        y = RectangleMax.Y;
                    }
                }

                this.Width = w;
                this.Height = h;
                this.Location = new Point(this.Location.X, y);
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        private void ResizeButtonLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = this.Width - (MousePosition.X - LocationScreenX);

                if (w <= 0)
                {
                    w = 1;
                }

                int x = this.Location.X - (w - this.Width);

                if (RectangleMax != Rectangle.Empty)
                {
                    if (x < RectangleMax.X)
                    {
                        w -= RectangleMax.X - x;
                        x = RectangleMax.X;
                    }
                }

                this.Width = w;
                this.Location = new Point(x, this.Location.Y);
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        private void ResizeButtonRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = MousePosition.X - LocationScreenX;

                if (w <= 0)
                {
                    w = 1;
                }

                if (RectangleMax != Rectangle.Empty)
                {
                    if (w + (this.Location.X - RectangleMax.X) > RectangleMax.Width)
                    {
                        w = RectangleMax.Width - (this.Location.X - RectangleMax.X);
                    }
                }

                this.Width = w;
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        private void ResizeButtonBottomLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = this.Width - (MousePosition.X - LocationScreenX);

                if (w <= 0)
                {
                    w = 1;
                }

                int h = MousePosition.Y - LocationScreenY;

                if (h <= 0)
                {
                    h = 1;
                }

                int x = this.Location.X - (w - this.Width);

                if (RectangleMax != Rectangle.Empty)
                {
                    if (x < RectangleMax.X)
                    {
                        w -= RectangleMax.X - x;
                        x = RectangleMax.X;
                    }

                    if (h + (this.Location.Y - RectangleMax.Y) > RectangleMax.Height)
                    {
                        h = RectangleMax.Height - (this.Location.Y - RectangleMax.Y);
                    }
                }

                this.Width = w;
                this.Height = h;
                this.Location = new Point(x, this.Location.Y);
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        private void ResizeButtonBottomRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int w = MousePosition.X - LocationScreenX;

                if (w <= 0)
                {
                    w = 1;
                }

                int h = MousePosition.Y - LocationScreenY;

                if (h <= 0)
                {
                    h = 1;
                }

                if (RectangleMax != Rectangle.Empty)
                {
                    if (w + (this.Location.X - RectangleMax.X) > RectangleMax.Width)
                    {
                        w = RectangleMax.Width - (this.Location.X - RectangleMax.X);
                    }

                    if (h + (this.Location.Y - RectangleMax.Y) > RectangleMax.Height)
                    {
                        h = RectangleMax.Height - (this.Location.Y - RectangleMax.Y);
                    }
                }

                this.Width = w;
                this.Height = h;
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        private void ResizeButtonBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LocationControlOnScreen(out int LocationScreenX, out int LocationScreenY);

                int h = MousePosition.Y - LocationScreenY;

                if (h <= 0)
                {
                    h = 1;
                }

                if (RectangleMax != Rectangle.Empty)
                {
                    if (h + (this.Location.Y - RectangleMax.Y) > RectangleMax.Height)
                    {
                        h = RectangleMax.Height - (this.Location.Y - RectangleMax.Y);
                    }
                }

                this.Height = h;
                this.Update();

                if (this.Parent != null)
                {
                    this.Parent.Update();
                }
            }
        }

        internal Rectangle SelectedArea
        {
            get
            {
                return new Rectangle(6, 6, Width - 14, Height - 14);
            }
        }

        internal Point LocationSelectedArea
        {
            get
            {
                return new Point(this.Location.X + 6, this.Location.Y + 6);
            }
        }

        public void DrawBorder()
        {
            this.ResizeButtonBottom.Refresh();
            this.ResizeButtonBottomLeft.Refresh();
            this.ResizeButtonBottomRight.Refresh();
            this.ResizeButtonTop.Refresh();
            this.ResizeButtonTopLeft.Refresh();
            this.ResizeButtonTopRight.Refresh();
            this.ResizeButtonRight.Refresh();
            this.ResizeButtonLeft.Refresh();
            Sky_UI.Border.DrawRoundRectangle(new Pen(Color.FromArgb(100, 255, 255, 255), 5), 6, 6, Width - 14, Height - 14, 0, this.CreateGraphics());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Sky_UI.Border.DrawRoundRectangle(new Pen(Color.FromArgb(100, 255, 255, 255), 5), 6, 6, Width - 14, Height - 14, 0, e.Graphics);
            base.OnPaint(e);
        }
    }
}
