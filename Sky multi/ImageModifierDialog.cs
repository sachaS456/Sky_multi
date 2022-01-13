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
using System.IO;
using Sky_UI;

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

        internal ImageModifierDialog(Bitmap bitmap)
        {
            InitializeComponent();

            CropImage = new RectangleResizer();
            CropImage.Location = imageView1.ImagePosition;
            CropImage.Size = new Size(imageView1.ImageWidth, imageView1.ImageHeight);
            CropImage.Visible = false;
            imageView1.Controls.Add(CropImage);
            imageView1.SetImage(bitmap);
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
                CropImage.RectangleMax = new System.Drawing.Rectangle(imageView1.ImagePosition.X, imageView1.ImagePosition.Y, imageView1.ImageWidth, imageView1.ImageHeight);
                CropImage.Size = new Size(imageView1.ImageWidth, imageView1.ImageHeight);
                CropImage.Location = imageView1.ImagePosition;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Images png| *.png |Images jpeg| *.jpeg; *.jpg |Images bmp| *.bmp |Images ico| *.ico |Images gif| *.gif |Images tiff| *.tiff; *.tif";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageView1.Image.Save(dialog.FileName);
                }

                dialog.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CropImage.RectangleMax = new System.Drawing.Rectangle(imageView1.ImagePosition.X, imageView1.ImagePosition.Y, imageView1.ImageWidth, imageView1.ImageHeight);
            CropImage.Size = new Size(imageView1.ImageWidth, imageView1.ImageHeight);
            CropImage.Location = imageView1.ImagePosition;

            CropImage.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button4.Visible = false;
            button2.Visible = true;

            float CoefImageW = (float)imageView1.Image.Width / imageView1.ImageWidth;
            float CoefImageH = (float)imageView1.Image.Height / imageView1.ImageHeight;
            float x, y, width, height;

            if (CoefImageW == 1.0)
            {
                x = CropImage.LocationSelectedArea.X - imageView1.ImagePosition.X;
                width = CropImage.SelectedArea.Width;
            }
            else
            {
                x = (CropImage.LocationSelectedArea.X - imageView1.ImagePosition.X) * CoefImageW;
                width = CropImage.SelectedArea.Width * CoefImageW;
            }

            if (CoefImageH == 1.0)
            {
                y = CropImage.LocationSelectedArea.Y - imageView1.ImagePosition.Y;
                height = CropImage.SelectedArea.Height;
            }
            else
            {
                y = (CropImage.LocationSelectedArea.Y - imageView1.ImagePosition.Y) * CoefImageH;
                height = CropImage.SelectedArea.Height * CoefImageH;
            }

            SetCropImage((int)x, (int)y, (int)width, (int)height);

            CropImage.Visible = false;
        }

        private void SetCropImage(int x, int y, int width, int height)
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

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button4.Visible = false;
            button2.Visible = true;
            CropImage.Visible = false;
        }
    }
}
