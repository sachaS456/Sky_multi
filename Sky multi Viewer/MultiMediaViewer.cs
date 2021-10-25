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
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Sky_multi_Viewer
{
    public delegate void EventMediaTypedHandler(bool DifferentBackMedia);

    public sealed class MultiMediaViewer : VlcControl
    {
        public EventMediaTypedHandler ItIsPicture = null;
        public EventMediaTypedHandler ItIsAudioOrVideo = null;

        public bool ItIsAImage { get; private set; } = false;
        public bool RawImage { get; private set; } = false;

        public MultiMediaViewer()
        {
            this.BackgroundImageLayout = ImageLayout.Center;
            this.Resize += new EventHandler(this_Resize);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        }

        public void OpenFile(string FilePath)
        {
            OpenFile(FilePath, null);
        }

        public void OpenFile(string FilePath, params string[] Option)
        {
            if (File.Exists(FilePath) == false)
            {
                MessageBox.Show("File not found!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            while (Chargement == true)
            {
                System.Threading.Thread.Sleep(1);
            }

            try // test png, jpeg, gif, tiff, ico ...
            {
                this.BackgroundImage = new Bitmap(FilePath);
                RawImage = false;
            }
            catch
            {
                try // test webp
                {
                    this.BackgroundImage = Sky_multi_Core.WebPDecoder.DecodeWebp(FilePath);
                    RawImage = false;
                }
                catch
                {
                    // test raw                    

                    this.BackgroundImage = Sky_multi_Core.RawDecoder.RawToBitmap(FilePath);
                    RawImage = true;

                    if (this.BackgroundImage == null)
                    {
                        this.BackgroundImage = null;

                        if (ItIsAudioOrVideo != null)
                        {
                            ItIsAudioOrVideo(ItIsAImage);
                        }

                        ItIsAImage = false;
                        RawImage = false;

                        this.Play("File:///" + FilePath, Option);
                        return;
                    }
                }
            }

            this.Stop();
            PictureManager();

            if (ItIsPicture != null)
            {
                ItIsPicture(!ItIsAImage);
            }

            ItIsAImage = true;
        }

        private void this_Resize(object sender, EventArgs e)
        {
            PictureManager();
        }

        public void RotateImage()
        {
            this.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            PictureManager();
            this.Refresh();
        }

        private void PictureManager()
        {
            if (this.BackgroundImage != null)
            {
                if (this.BackgroundImage.Width <= this.Width && this.BackgroundImage.Height <= this.Height)
                {
                    if (this.BackgroundImageLayout != ImageLayout.Center)
                    {
                        this.BackgroundImageLayout = ImageLayout.Center;
                    }
                }
                else
                {
                    if (this.BackgroundImageLayout != ImageLayout.Zoom)
                    {
                        this.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }
            }
        }
    }
}
