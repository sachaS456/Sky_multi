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
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Sky_UI;
using Vortice.Direct2D1;
using Vortice.WIC;
using Vortice.Mathematics;
using System.Runtime.InteropServices;

namespace Sky_multi
{
    internal class ImageModifierDialog : SkyForms
    {
        private Sky_UI.Button button1;
        private Sky_multi_Viewer.ImageView imageView1;
        private Sky_UI.Button button2;
        private Sky_UI.Button button3;
        private Sky_UI.Button button4;
        private RectangleResizer CropImage;

        private readonly ID2D1Factory7 factory7;
        private readonly ID2D1HwndRenderTarget hwndRender;
        private ID2D1Bitmap ID2D1Bitmap;
        private readonly IWICImagingFactory2 imagingFactory2;
        private SizeI PixelSize;
        private float ImageWidth = 0;
        private float ImageHeight = 0;
        private readonly Color4 bgcolor;

        internal ImageModifierDialog(Image bitmap)
        {
            InitializeComponent();

            CropImage = new RectangleResizer();
            CropImage.Location = imageView1.ImagePosition;
            CropImage.Size = new System.Drawing.Size(imageView1.ImageWidth, imageView1.ImageHeight);
            CropImage.Visible = false;
            imageView1.Controls.Add(CropImage);
            imageView1.UseD2D1 = false;
            imageView1.SetImage(in bitmap);
        }

        internal ImageModifierDialog(IWICBitmap iWICBitmap)
        {
            InitializeComponent();

            CropImage = new RectangleResizer();
            CropImage.Location = imageView1.ImagePosition;
            CropImage.Size = new System.Drawing.Size(imageView1.ImageWidth, imageView1.ImageHeight);
            CropImage.Visible = false;
            imageView1.Controls.Add(CropImage);
            imageView1.UseD2D1 = true;
            imageView1.SetImage(in iWICBitmap);

            factory7 = D2D1.D2D1CreateFactory<ID2D1Factory7>();
            HwndRenderTargetProperties properties = new HwndRenderTargetProperties();
            properties.Hwnd = CropImage.Handle;
            PixelSize = new SizeI(Screen.FromHandle(this.Handle).Bounds.Width, Screen.FromHandle(this.Handle).Bounds.Height);
            properties.PixelSize = PixelSize;
            properties.PresentOptions = PresentOptions.Immediately;

            hwndRender = factory7.CreateHwndRenderTarget(new RenderTargetProperties(new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm,
                Vortice.DCommon.AlphaMode.Premultiplied)), properties);
            hwndRender.AntialiasMode = AntialiasMode.Aliased;
            bgcolor = new(0.1f, 0.1f, 0.1f, 1.0f);

            CropImage.Resize += new EventHandler(CropImage_Resize);

            imagingFactory2 = new IWICImagingFactory2();
            IWICFormatConverter converter = imagingFactory2.CreateFormatConverter();
            converter.Initialize(iWICBitmap, PixelFormat.Format32bppPBGRA, BitmapDitherType.None, null, 1f, BitmapPaletteType.FixedWebPalette);
            iWICBitmap = imagingFactory2.CreateBitmapFromSource(converter, BitmapCreateCacheOption.CacheOnLoad);

            ID2D1Bitmap = hwndRender.CreateBitmapFromWicBitmap(iWICBitmap, new BitmapProperties(
                new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.B8G8R8A8_UNorm, Vortice.DCommon.AlphaMode.Premultiplied)));
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageModifierDialog));
            this.button1 = new Sky_UI.Button();
            this.imageView1 = new Sky_multi_Viewer.ImageView();
            this.button2 = new Sky_UI.Button();
            this.button3 = new Sky_UI.Button();
            this.button4 = new Sky_UI.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Border = false;
            this.button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.borderRadius = 0;
            this.button1.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ID = 0;
            this.button1.Image = null;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Location = new System.Drawing.Point(2, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Enregistrer sous";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageView1
            // 
            this.imageView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.imageView1.Location = new System.Drawing.Point(2, 51);
            this.imageView1.Name = "imageView1";
            this.imageView1.Size = new System.Drawing.Size(606, 400);
            this.imageView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.imageView1.TabIndex = 4;
            this.imageView1.Text = "imageView1";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Border = false;
            this.button2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.borderRadius = 0;
            this.button2.BorderSize = 0;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ID = 0;
            this.button2.Image = null;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.Location = new System.Drawing.Point(132, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Rogner";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.Border = false;
            this.button3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button3.borderRadius = 0;
            this.button3.BorderSize = 0;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ID = 0;
            this.button3.Image = null;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button3.Location = new System.Drawing.Point(132, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(37, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "V";
            this.button3.Visible = false;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.Border = false;
            this.button4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button4.borderRadius = 0;
            this.button4.BorderSize = 0;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button4.ID = 0;
            this.button4.Image = null;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button4.Location = new System.Drawing.Point(173, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(37, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "X";
            this.button4.Visible = false;
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ImageModifierDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ButtonMaximizedVisible = true;
            this.ClientSize = new System.Drawing.Size(611, 455);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.imageView1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageModifierDialog";
            this.Text = "Sky multi - screenshot";
            this.Resize += new EventHandler(this_Resize);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.imageView1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.ResumeLayout(false);

        }

        private void this_Resize(object sender, EventArgs e)
        {
            if (CropImage.Visible == true)
            {
                this.Refresh();
                CropImage.RectangleMax = new System.Drawing.Rectangle(imageView1.ImagePosition.X - 6, imageView1.ImagePosition.Y - 6, imageView1.ImageWidth + 14, imageView1.ImageHeight + 14);
                CropImage.Size = new System.Drawing.Size(imageView1.ImageWidth + 14, imageView1.ImageHeight + 14);
                CropImage.Location = new Point(imageView1.ImagePosition.X - 6, imageView1.ImagePosition.Y - 6);
            }
        }

        private void CropImage_Resize(object sender, EventArgs e)
        {
            hwndRender.Resize(new SizeI(CropImage.Width, CropImage.Height));
            DrawImage(true);
            this.Invalidate();
        }

        private void DrawImage(bool Clear = true)
        {
            if (ID2D1Bitmap == null)
            {
                return;
            }

            bool isresize = false;

            if (ImageWidth > imageView1.Width || ID2D1Bitmap.Size.Width > imageView1.Width)
            {
                Vortice.Mathematics.Size resize = ResizeImageW(ID2D1Bitmap.Size.Width, ID2D1Bitmap.Size.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
                isresize = true;
            }
            else
            {
                ImageWidth = ID2D1Bitmap.Size.Width;
            }

            if (ImageHeight > imageView1.Height || ID2D1Bitmap.Size.Height > imageView1.Height && isresize == false)
            {
                Vortice.Mathematics.Size resize = ResizeImageH(ID2D1Bitmap.Size.Width, ID2D1Bitmap.Size.Height);
                ImageWidth = resize.Width;
                ImageHeight = resize.Height;
            }
            else if (isresize == false)
            {
                ImageHeight = (int)ID2D1Bitmap.Size.Height;
            }

            float x = imageView1.Width / 2 - ImageWidth / 2 - CropImage.Location.X;
            float y = imageView1.Height / 2 - ImageHeight / 2 - CropImage.Location.Y;

            hwndRender.BeginDraw();

            if (Clear)
            {
                hwndRender.Clear(bgcolor);
            }

            hwndRender.DrawBitmap(ID2D1Bitmap, new Rect(x, y, ImageWidth, ImageHeight), 1.0f, Vortice.Direct2D1.BitmapInterpolationMode.Linear, null);
            hwndRender.EndDraw();
        }

        private Vortice.Mathematics.Size ResizeImageW(float imagewith, float imageheight)
        {
            float rw = imagewith;
            float rh = imageheight;

            SimplifiedFractions(ref rw, ref rh);

            imagewith = imageView1.Width;
            imageheight = (int)(float)((float)(imageView1.Width / rw) * rh);
            return new Vortice.Mathematics.Size(imagewith, imageheight);
        }

        private Vortice.Mathematics.Size ResizeImageH(float imagewith, float imageheight)
        {
            float rw = imagewith;
            float rh = imageheight;

            SimplifiedFractions(ref rw, ref rh);

            imageheight = imageView1.Height;
            imagewith = (int)(float)((float)(imageView1.Height / rh) * rw);
            return new Vortice.Mathematics.Size(imagewith, imageheight);
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Images png| *.png |Images jpeg| *.jpeg; *.jpg |Images bmp| *.bmp |Images ico| *.ico |Images gif| *.gif |Images tiff| *.tiff; *.tif";
                
                if (imageView1.UseD2D1 == false)
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        imageView1.Image.Save(dialog.FileName);
                    }

                    dialog.Dispose();
                }
                else
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        switch (dialog.FilterIndex)
                        {
                            case 0:
                                imageView1.EncodeImage(dialog.FileName, ".png");
                                break;

                            case 1:
                                imageView1.EncodeImage(dialog.FileName, ".jpeg");
                                break;

                            case 2:
                                imageView1.EncodeImage(dialog.FileName, ".bmp");
                                break;

                            case 3:
                                imageView1.EncodeImage(dialog.FileName, ".ico");
                                break;

                            case 4:
                                imageView1.EncodeImage(dialog.FileName, ".gif");
                                break;

                            case 5:
                                imageView1.EncodeImage(dialog.FileName, ".tiff");
                                break;
                        }
                    }
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            CropImage.RectangleMax = new System.Drawing.Rectangle(imageView1.ImagePosition.X - 6, imageView1.ImagePosition.Y - 6, imageView1.ImageWidth + 14, imageView1.ImageHeight + 14);
            CropImage.Size = new System.Drawing.Size(imageView1.ImageWidth + 14, imageView1.ImageHeight + 14);
            CropImage.Location = new Point(imageView1.ImagePosition.X - 6, imageView1.ImagePosition.Y - 6);

            CropImage.Visible = true;
            CropImage.BringToFront();


            button3.Visible = true;
            button4.Visible = true;
            button2.Visible = false;

            imageView1.CanZoom = false;
            imageView1.ResetScale();

            await Task.Delay(10);
            DrawImage(false);
            this.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button4.Visible = false;
            button2.Visible = true;

            float CoefImageW;
            float CoefImageH;

            if (imageView1.UseD2D1)
            {
                CoefImageW = (float)imageView1.ImageDataD2D1.Width / imageView1.ImageWidth;
                CoefImageH = (float)imageView1.ImageDataD2D1.Height / imageView1.ImageHeight;
            }
            else
            {
                CoefImageW = (float)imageView1.Image.Width / imageView1.ImageWidth;
                CoefImageH = (float)imageView1.Image.Height / imageView1.ImageHeight;
            }

            float x = (CropImage.LocationSelectedArea.X - imageView1.ImagePosition.X) * CoefImageW;
            float width = CropImage.SelectedArea.Width * CoefImageW;

            float y = (CropImage.LocationSelectedArea.Y - imageView1.ImagePosition.Y) * CoefImageH;
            float height = CropImage.SelectedArea.Height * CoefImageH;

            SetCropImage((int)x, (int)y, (int)width, (int)height);

            CropImage.Visible = false;
            imageView1.CanZoom = true;
        }

        private void SetCropImage(int x, int y, int width, int height)
        {
            if (imageView1.UseD2D1 == false)
            {
                Bitmap NewBitmap = new Bitmap(width, height);
                Bitmap bitmap = imageView1.GetBitmap();

                for (int indexX = 0; indexX < width; indexX++)
                {
                    for (int indexY = 0; indexY < height; indexY++)
                    {
                        NewBitmap.SetPixel(indexX, indexY, bitmap.GetPixel(indexX + x, indexY + y));
                    }
                }

                bitmap.Dispose();
                imageView1.SetImage(NewBitmap);
            }
            else
            {
                IWICBitmap iWICBitmap = imageView1.GetImageWIC()[0];
                IWICBitmapLock iWICBitmapLock = iWICBitmap.Lock(BitmapLockFlags.Read | BitmapLockFlags.Write);

                IntPtr DataPtr = iWICBitmapLock.Data.DataPointer;

                int Size = iWICBitmap.Size.Width * iWICBitmap.Size.Height * 4;
                int stride = iWICBitmap.Size.Width * 4;
                byte[] Data = new byte[Size];

                Marshal.Copy(DataPtr, Data, 0, Size);

                Size = width * height * 4;
                int NewStride = width * 4;
                byte[] NewData = new byte[Size];

                iWICBitmapLock.Dispose();

                for (int indexY = 0; indexY < height; indexY++)
                {
                    for (int indexX = 0; indexX < width * 4; indexX++)
                    {
                        NewData[indexY * NewStride + indexX] = Data[((indexY * stride) + (y * stride)) + (indexX + x * 4)];
                    }
                }

                imageView1.SetImage(NewData, NewStride, width, height);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button4.Visible = false;
            button2.Visible = true;
            CropImage.Visible = false;
            imageView1.CanZoom = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this != null && this.Disposing == false && this.IsDisposed == false && CropImage != null && imageView1 != null)
            {
                if (CropImage.Visible = true && imageView1.UseD2D1)
                {
                    DrawImage(false);
                    CropImage.DrawBorder();
                }
            }

            base.OnPaint(e);
        }
    }
}
