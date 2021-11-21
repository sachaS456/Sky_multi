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
using Sky_framework;

namespace Sky_multi
{
    internal class ImageModifierDialog : SkyForms
    {
        private Sky_framework.Button button1;
        private Sky_multi_Viewer.ImageView imageView1;
        private Sky_framework.Button button2;
        private RectangleResizer CropImage;

        internal ImageModifierDialog(Bitmap bitmap)
        {
            InitializeComponent();

            CropImage = new RectangleResizer();
            CropImage.Location = new Point(0, 0);
            CropImage.Size = imageView1.Size;
            CropImage.Visible = false;
            imageView1.Controls.Add(CropImage);
            imageView1.SetImage(bitmap);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageModifierDialog));
            this.button1 = new Sky_framework.Button();
            this.imageView1 = new Sky_multi_Viewer.ImageView();
            this.button2 = new Sky_framework.Button();
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
            // ImageModifierDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ButtonMaximizedVisible = true;
            this.ClientSize = new System.Drawing.Size(611, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.imageView1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageModifierDialog";
            this.Text = "Sky multi - screenshot";
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.imageView1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.ResumeLayout(false);

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
            CropImage.Visible = true;
        }
    }
}
