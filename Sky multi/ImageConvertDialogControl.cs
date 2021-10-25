/*--------------------------------------------------------------------------------------------------------------------
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
using System.Windows.Forms;
using System.Drawing;

namespace Sky_multi
{
    internal sealed class ImageConvertDialogControl : Control
    {
        private Label label1 = new Label();
        private Label label2 = new Label();
        private Label label3 = new Label();
        private Sky_framework.Button ButtonConvert = new Sky_framework.Button();
        private Sky_framework.Button ButtonCancel = new Sky_framework.Button();
        private Label LabelFormatConvert = new Label();
        private Sky_framework.Button ButtonFormatConvertPNG = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertJPG = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertJPEG = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertICO = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertGIF = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertTIFF = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertTIF = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertBMP = new Sky_framework.Button();
        private Sky_framework.Button ButtonFormatConvertWEBP = new Sky_framework.Button();

        internal string FormatString { set; get; } = string.Empty;

        internal ImageConvertDialogControl(string Format, MouseEventHandler eConvert, MouseEventHandler eCancel, Language language)
        {
            this.Resize += new EventHandler(This_Resize);
            this.Size = new Size(300, 0);
            this.Location = new Point(20, 20);

            if (language == Language.French)
            {
                label1.Text = "Format de l'image actuelle : ";
            }
            else
            {
                label1.Text = "Current picture format : ";
            }
            label1.Location = new Point(5, 5);
            label1.ForeColor = Color.FromArgb(224, 224, 224);
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            this.Controls.Add(label1);

            if (language == Language.French)
            {
                label2.Text = "Format de l'image à convertir : ";
            }
            else 
            {
                label2.Text = "Image format to convert : ";
            }
            label2.Location = new Point(5, 20);
            label2.ForeColor = Color.FromArgb(224, 224, 224);
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            this.Controls.Add(label2);

            label3.Text = Format;
            label3.Location = new Point(label1.Width + label1.Location.X, 5);
            label3.ForeColor = Color.FromArgb(224, 224, 224);
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point);
            this.Controls.Add(label3);

            if (language == Language.French)
            {
                ButtonConvert.Text = "Convertir";
            }
            else
            {
                ButtonConvert.Text = "Convert";
            }
            ButtonConvert.Location = new Point(230, 105);
            ButtonConvert.Size = new Size(60, 30);
            ButtonConvert.MouseClick += eConvert;
            ButtonConvert.borderRadius = 5;
            this.Controls.Add(ButtonConvert);

            if (language == Language.French)
            {
                ButtonCancel.Text = "Annuler";
            }
            else
            {
                ButtonCancel.Text = "Cancel";
            }
            ButtonCancel.Location = new Point(165, 105);
            ButtonCancel.Size = new Size(60, 30);
            ButtonCancel.MouseClick += eCancel;
            ButtonCancel.borderRadius = 5;
            this.Controls.Add(ButtonCancel);

            LabelFormatConvert.Text = ".png";
            LabelFormatConvert.Location = new Point(label2.Width + label2.Location.X, 20);
            LabelFormatConvert.AutoSize = true;
            LabelFormatConvert.Font = new Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point);
            LabelFormatConvert.ForeColor = Color.FromArgb(224, 224, 224);
            this.Controls.Add(LabelFormatConvert);

            ButtonFormatConvertPNG.Text = ".png";
            ButtonFormatConvertPNG.Location = new Point(5, 50);
            ButtonFormatConvertPNG.Size = new Size(40, 25);
            ButtonFormatConvertPNG.MouseClick += new MouseEventHandler(ButtonFormatConvertPNG_MouseClick);
            ButtonFormatConvertPNG.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertPNG);

            ButtonFormatConvertJPG.Text = ".jpg";
            ButtonFormatConvertJPG.Location = new Point(50, 50);
            ButtonFormatConvertJPG.Size = new Size(40, 25);
            ButtonFormatConvertJPG.MouseClick += new MouseEventHandler(ButtonFormatConvertJPG_MouseClick);
            ButtonFormatConvertJPG.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertJPG);

            ButtonFormatConvertJPEG.Text = ".jpeg";
            ButtonFormatConvertJPEG.Location = new Point(95, 50);
            ButtonFormatConvertJPEG.Size = new Size(40, 25);
            ButtonFormatConvertJPEG.MouseClick += new MouseEventHandler(ButtonFormatConvertJPEG_MouseClick);
            ButtonFormatConvertJPEG.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertJPEG);

            ButtonFormatConvertICO.Text = ".ico";
            ButtonFormatConvertICO.Location = new Point(140, 50);
            ButtonFormatConvertICO.Size = new Size(40, 25);
            ButtonFormatConvertICO.MouseClick += new MouseEventHandler(ButtonFormatConvertICO_MouseClick);
            ButtonFormatConvertICO.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertICO);

            ButtonFormatConvertGIF.Text = ".gif";
            ButtonFormatConvertGIF.Location = new Point(185, 50);
            ButtonFormatConvertGIF.Size = new Size(40, 25);
            ButtonFormatConvertGIF.MouseClick += new MouseEventHandler(ButtonFormatConvertGIF_MouseClick);
            ButtonFormatConvertGIF.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertGIF);

            ButtonFormatConvertTIFF.Text = ".tiff";
            ButtonFormatConvertTIFF.Location = new Point(230, 50);
            ButtonFormatConvertTIFF.Size = new Size(40, 25);
            ButtonFormatConvertTIFF.MouseClick += new MouseEventHandler(ButtonFormatConvertTIFF_MouseClick);
            ButtonFormatConvertTIFF.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertTIFF);

            ButtonFormatConvertTIF.Text = ".tif";
            ButtonFormatConvertTIF.Location = new Point(5, 80);
            ButtonFormatConvertTIF.Size = new Size(40, 25);
            ButtonFormatConvertTIF.MouseClick += new MouseEventHandler(ButtonFormatConvertTIF_MouseClick);
            ButtonFormatConvertTIF.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertTIF);

            ButtonFormatConvertBMP.Text = ".bmp";
            ButtonFormatConvertBMP.Location = new Point(50, 80);
            ButtonFormatConvertBMP.Size = new Size(40, 25);
            ButtonFormatConvertBMP.MouseClick += new MouseEventHandler(ButtonFormatConvertBMP_MouseClick);
            ButtonFormatConvertBMP.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertBMP);

            ButtonFormatConvertWEBP.Text = ".webp";
            ButtonFormatConvertWEBP.Location = new Point(95, 80);
            ButtonFormatConvertWEBP.Size = new Size(40, 25);
            ButtonFormatConvertWEBP.MouseClick += new MouseEventHandler(ButtonFormatConvertWEBP_MouseClick);
            ButtonFormatConvertWEBP.borderRadius = 5;
            this.Controls.Add(ButtonFormatConvertWEBP);
        }

        private void ButtonFormatConvertPNG_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".png";
        }

        private void ButtonFormatConvertJPG_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".jpg";
        }

        private void ButtonFormatConvertJPEG_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".jpeg";
        }

        private void ButtonFormatConvertICO_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".ico";
        }

        private void ButtonFormatConvertGIF_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".gif";
        }

        private void ButtonFormatConvertTIFF_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".tiff";
        }

        private void ButtonFormatConvertTIF_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".tif";
        }

        private void ButtonFormatConvertBMP_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".bmp";
        }

        private void ButtonFormatConvertWEBP_MouseClick(object sender, MouseEventArgs e)
        {
            LabelFormatConvert.Text = ".webp";
        }

        private void This_Resize(object sender, EventArgs e)
        {
            IntPtr handle = Sky_framework.Win32.CreateRoundRectRgn(0, 0, Width, Height, 15, 15);

            if (handle != IntPtr.Zero)
            {
                Region = Region.FromHrgn(handle);
                Sky_framework.Win32.DeleteObject(handle);
            }
        }

        new internal async void Show()
        {
            this.Visible = true;
            this.Height = 0;

            for (int index = 0; index < 150; index += 10)
            {
                this.Height = index;
                await Task.Delay(5);
            }
        }

        internal async void Close()
        {
            await this.Hide();
            this.Dispose();
        }

        new internal async Task<bool> Hide()
        {
            for (int index = this.Height; index > 0; index -= 10)
            {
                this.Height = index;
                await Task.Delay(5);
            }

            this.Visible = false;
            this.Height = 0;
            return true;
        }

        internal string GetNewFormat
        {
            get
            {
                return LabelFormatConvert.Text;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.CreateGraphics().Clear(this.BackColor);
            Sky_framework.Border.DrawRoundRectangle(new Pen(Color.FromArgb(150, 150, 150), 2), 1, 1, Width - 3, Height - 3, 5, this.CreateGraphics());
        }
    }
}
